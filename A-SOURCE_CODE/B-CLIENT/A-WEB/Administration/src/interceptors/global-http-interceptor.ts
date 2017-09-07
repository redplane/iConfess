/**
 * Created by Linh Nguyen on 6/17/2017.
 */
import {XHRBackend, RequestOptions, Response, Http, RequestOptionsArgs, Headers, Request} from '@angular/http';
import {Observable} from "rxjs/Observable";
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import {Inject, Injectable} from "@angular/core";
import {IAuthenticationService} from "../interfaces/services/authentication-service.interface";
import {ToastrService} from "ngx-toastr";

@Injectable()
export class GlobalHttpInterceptor extends Http {

  //#region Constructor

  /*
   * Initiate handler service with injectors.
   * */
  public constructor(private backend: XHRBackend,
                     private defaultOptions: RequestOptions,
                     private toastr: ToastrService,
                     @Inject('IAuthenticationService') private authenticationService: IAuthenticationService) {
    super(backend, defaultOptions);
  }

  //#endregion

  //#region Methods

  /*
  * Catch request function and analyze its responses.
  * */
  public request(url: string | Request, options?: RequestOptionsArgs): Observable<Response> {
    return super.request(url, options)
      .catch((x: Response) => {

      // Unauthorize response.
      if (x.status === 0 || x.status === 500)
        this.toastr.error('Có lỗi xảy ra trên máy chủ hệ thống. Xin hãy thử lại sau ít phút.', 'Thông tin hệ thống', null);
      else if (x.status == 401){ // Unauthenticated.

        // Clear authorization information.
        this.authenticationService.clearIdentity();

        // Redirect to login page.
        this.authenticationService.redirectToLogin();
      }
      else{

        let result = x.json();
        if (result != null && result['message'] != null)
          this.toastr.error(result['message'], 'Thông tin hệ thống', null);
      }

      return Observable.throw(x);
    });
  }

  //#endregion
}
