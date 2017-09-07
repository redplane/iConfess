import {Http, RequestOptions} from "@angular/http";

// Interface provides function to access service api.
export interface IApiService {

  //#region Methods

  /*
  * Send 'GET' to service.
  * */
  get(baseUrl: string, url: string, parameters: any): any;

  /*
  * Send 'POST' to service.
  * */
  post(baseUrl: string, url: string, parameters: any, body: any): any;

  /*
  * Send 'PUT' to service.
  * */
  put(baseUrl: string, url: string, parameters: any, body: any): any;

  /*
  * Send 'PUT' to service.
  * */
  delete(baseUrl: string, url: string, parameters: any, body: any): any;

  /*
  * Encrypt url parameters to prevent dangerous parameters are passed to service.
  * */
  encryptUrlParameters(parameters: any): string;

  /*
  * Redirect user to root url.
  * */
  redirectToRoot(): void;

  /*
   * Get http service.
   * */
  getHttp(): Http;

  /*
  * Initiate http request header.
  * */
  initOptions(value: RequestOptions): void;

  //#endregion
}
