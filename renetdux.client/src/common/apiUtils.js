let baseUrl = 'http://localhost:52446/';

export function apiGet(url) {
  return new Promise((resolve, reject) => {
    fetch(baseUrl + url)
      .then(handleErrors)
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

function handleErrors(response) {
  if (!response.ok) {
    throw Error(response.statusText);
  }
  return response;
}