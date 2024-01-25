import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {AuthDto} from "../dtos/auth-dto";
import {UserDto} from "../dtos/user-dto";
import {UserInfo} from "../dtos/localStorage/userInfo";
import {Router} from "@angular/router";
import {AuthData} from "../dtos/localStorage/authData";

@Injectable({
  providedIn: 'root'
})
// Сервис авторизация/регистрации
export class AuthService {

  /* Для авторизации и регистрации используется один и тот же объект - AuthDto.
     Ответ на оба запроса также представляет собой один объект - UserDto.
     Фактически, логика одинаковая, только при регистрации - создаётся новый юзер. */

  constructor(private http: HttpClient,
              @Inject('BASE_API_URL') private baseUrl: string,
              private router: Router) { }

  // Запрос на авторизацию
  login(email: string, password: string){
    let authDto: AuthDto = {
      email: email,
      password: password
    };

    return this.http.post<UserDto>(this.baseUrl + '/Auth/Login', authDto, { observe: 'response' });
  }

  // Запрос на регистрацию
  register(email: string, password: string){
    let authDto: AuthDto = {
      email: email,
      password: password
    };

    return this.http.post<UserDto>(this.baseUrl + '/Auth/Register', authDto, { observe: 'response' });
  }

  // Действия после успешной авторизации/регистрации
  afterAuth(userId: number, email: string, password: string){
    let userInfo: UserInfo = {
      userId: userId,
      email: email
    };
    let authData: AuthData = {
      data: window.btoa(userId.toString()) + window.btoa(email) + window.btoa(password)
    }

    // Заносим в localStorage инфу о юзере, метку об авторизации, а также данные, необходимые для работы с сервером
    localStorage.setItem('isAuthenticated', 'true');
    localStorage.setItem('userInfo', JSON.stringify(userInfo));
    localStorage.setItem('authData', JSON.stringify(authData));
    // Перенаправляем юзера на страницу с категориями
    this.router.navigate(['']).then();
  }

  // Выход из системы
  logOut(){
    localStorage.clear()
  }
}
