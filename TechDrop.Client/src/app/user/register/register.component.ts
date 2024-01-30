import { Component } from '@angular/core';
import {AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators} from "@angular/forms";
import {AuthService} from "../../services/auth.service";
import {HttpErrorResponse} from "@angular/common/http";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})

// Компонент регистрации
export class RegisterComponent {
  // Форма регистрации
  registerForm = new FormGroup(
    {
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
      ]),
      repeatPassword: new FormControl('', [
        Validators.maxLength(20),
        Validators.minLength(6),
        Validators.required
      ])
    },
    {
      validators: MatchValidator
    }
  );

  // Текст "общей" ошибки, возвращаемой сервером
  ErrorMessage = '';

  // Геттер для email-а
  get email() {
    return this.registerForm.controls['email'];
  }
  // Геттер для пароля
  get password() {
    return this.registerForm.controls['password'];
  }
  // Геттер для повторного пароля
  get repeatPassword() {
    return this.registerForm.controls['repeatPassword'];
  }

  constructor(private authService: AuthService) { }

  // Обработчик кнопки "Зарегистрироваться"
  onSubmit() {
    const email = this.registerForm.get('email')?.value!;
    const password = this.registerForm.get('password')?.value!;

    this.authService.register(email, password).subscribe({
      next: (res)=> {
        this.authService.afterAuth(res.body!)
      },
      error: (e: HttpErrorResponse)=> {
        this.ErrorMessage = e.error.ErrorMessage;
        this.registerForm.setErrors({ badRegister: true })
      }
    })
  }

  // Снятие "общей" ошибки, возвращаемой сервером
  onInputClick(){
    this.ErrorMessage = '';
    this.registerForm.setErrors({  });
  }
}

// Валидатор идентичности паролей
export const MatchValidator: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {
  const password: string = control.get('password')?.value!;
  const repeatPassword: string = control.get('repeatPassword')?.value!;

  if (password != repeatPassword) return { passwordsMismatch: true };
  return null;
}
