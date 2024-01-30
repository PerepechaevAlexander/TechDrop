import { Component } from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {AuthService} from "../../services/auth.service";
import {HttpErrorResponse} from "@angular/common/http";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
// Компонет авторизации
export class LoginComponent {
  // Форма авторизации
  loginForm = new FormGroup({
    email: new FormControl('', [
      Validators.email,
      Validators.maxLength(31),
      Validators.required,
    ]),
    password: new FormControl('', [
      Validators.maxLength(20),
      Validators.minLength(6),
      Validators.required,
      Validators.pattern('[A-Za-z0-9_]+'),
    ])
  });

  // Текст "общей" ошибки, возвращаемой сервером
  ErrorMessage = '';

  // Геттер для email-а
  get email() {
    return this.loginForm.controls['email'];
  }
  // Геттер для пароля
  get password() {
    return this.loginForm.controls['password'];
  }

  constructor(private authService: AuthService) { }

  // Обработчик кнопки "Войти"
  onSubmit() {
    const email = this.loginForm.get('email')?.value!;
    const password = this.loginForm.get('password')?.value!;

    this.authService.login(email, password).subscribe({
        next: (res) => {
          this.authService.afterAuth(res.body!);
        },
        error: (e: HttpErrorResponse) => {
          this.ErrorMessage = e.error.ErrorMessage;
          this.loginForm.setErrors({ badLogin: true });
        }
    })
  }

  // Снятие "общей" ошибки, возвращаемой сервером
  onInputClick(){
    this.ErrorMessage = '';
    this.loginForm.setErrors({  });
  }
}
