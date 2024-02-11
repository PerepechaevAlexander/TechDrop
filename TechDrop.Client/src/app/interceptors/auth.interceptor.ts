import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import {AuthService} from "../services/auth.service";

// Интерцептор авторизации. Навешивает заголовок Authorization в запросы.
@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private authService: AuthService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    // Если юзер авторизован -> вешаем заголовок Authorization в запрос
    if (this.authService.isAuthenticated()) {
      request = request.clone({
        setHeaders: { Authorization: `Bearer ${this.authService.getAccessToken()}` }
      })
    }
    return next.handle(request);
  }
}
