import { loadState } from './localStorage';

let baseUrl = 'http://localhost:52446/';

export default class ApiUtils {
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
  
  handleErrors(response) {
    if (!response.ok) {
      throw Error(response.statusText);
    }
    return response;
  }

  static request(type, url, data = null) {
    return new Promise((resolve, reject) => {
      fetch(baseUrl + url, {
        method: type,
        body: data ? JSON.stringify(data) : null,
        headers: this.getHeaders()
      })
        .then(this.handleErrors)
        .then(res => res.json())
        .then(
          (result) => {
            resolve(result);
          },
          (error) => {
            reject(error);
          }
        );
    });
  }
}