import {Injectable} from '@angular/core';
import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Observable} from 'rxjs';
import {catchError} from 'rxjs/operators';
import {ToastrService} from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private toastr: ToastrService) {
  }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {

    return next.handle(request).pipe(
      // @ts-ignore
      catchError(err => {
        if (err.status === 503) {
          this.toastr.info("Token expired refreshing the page")
          window.location.reload();
          return
        } else if (err.error.title) {
          this.toastr.error(err.error.title);

        } else if (err.error) {
          this.toastr.error(err.error);
        }
      })
    );
  }
}
