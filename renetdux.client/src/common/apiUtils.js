import { loadState } from './localStorage';
import { loginSuccess, loginError } from '../slices/authSlice';
import store from '../index';

let baseUrl = 'http://localhost:5000/';

export default class API {
  static getHeaders() {
    const headers = new Headers();

    headers.append('Content-Type', 'application/json');
    headers.append('Accept', 'application/json');

    const state = loadState();
    if (state && state.auth.token) {
       headers.append('Authorization', 'Bearer ' + state.auth.token);
    }

    return headers;
  }
  
  static handleErrors(response) {
    if (!response.ok && response.status !== 400) {
      throw Error(response.statusText);
    }
    return response;
  }

  static checkToken() {
    return new Promise((resolve, reject) => {
      const state = loadState();
      if (state && state.auth.expiration && state.auth.refresh_token) {
        const currentEpoch = new Date().getTime() / 1000;
        if (currentEpoch < state.auth.expiration) {
          resolve();
        } else {
          fetch(baseUrl + 'token', {
            method: 'post',
            body: JSON.stringify({
              grant_type: 'refresh_token',
              refresh_token: state.auth.refresh_token
            }),
            headers: this.getHeaders()
          })
            .then(res => this.handleErrors(res))
            .then(res => res.json())
            .then((response) => {
              store.dispatch(loginSuccess(response));
              resolve();
            })
            .catch((error) => {
              store.dispatch(loginError(error.message));
              reject(error);
            });
        }
      } else {
        resolve();
      }
    });
  }

  static request(type, url, data = null) {
    return new Promise((resolve, reject) => {
      this.checkToken()
        .then(() => {
          fetch(baseUrl + url, {
            method: type,
            body: data ? JSON.stringify(data) : null,
            headers: this.getHeaders()
          })
            .then(res => this.handleErrors(res))
            .then(res => res.json())
            .then(
              (result) => {
                if (result.status === 401)
                  reject(result);
                if (result.errorCode)
                  reject(result);
                resolve(result);
              },
              (error) => {
                reject(error);
              }
            );
        })
        .catch((error) => {
          reject(error);
        });
    });
  }
}