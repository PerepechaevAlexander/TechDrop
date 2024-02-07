import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {AuthDto} from "../dtos/auth-dto";
import {UserInfoDto} from "../dtos/userInfo-dto";
import {Router} from "@angular/router";
import {UserInfo} from "../dtos/localStorage/userInfo";

@Injectable({
  providedIn: 'root'
})
// Сервис авторизации/регистрации
// TODO для строк, используемых только здесь, сделать локальные константы
export class AuthService {

  /* Для авторизации и регистрации используется один и тот же объект - AuthDto.
     Ответ на оба запроса также представляет собой один объект - UserInfoDto.
     Фактически логика одинаковая, только при регистрации - создаётся новый юзер. */

  constructor(private http: HttpClient,
              @Inject('BASE_API_URL') private baseUrl: string,
              private router: Router) { }

  // Запрос на аутентификацию
  login(email: string, password: string){
    let authDto: AuthDto = {
      email: email,
      password: password
    };

    return this.http.post<UserInfoDto>(this.baseUrl + '/Auth/Login', authDto, { observe: 'response' });
  }

  // Запрос на регистрацию
  register(email: string, password: string){
    let authDto: AuthDto = {
      email: email,
      password: password
    };

    return this.http.post<UserInfoDto>(this.baseUrl + '/Auth/Register', authDto, { observe: 'response' });
  }

  // Действия после успешной авторизации/регистрации
  afterAuth(userInfoDto: UserInfoDto){
    // Маппим информацию о пользователе для localStorage
    let userInfo: UserInfo = {
      email: userInfoDto.email
    }
    // Заносим в localStorage метку об авторизации, информацию о пользователе и токен доступа
    localStorage.setItem('isAuthenticated', 'true');
    localStorage.setItem('userInfo', JSON.stringify(userInfo))
    localStorage.setItem('accessToken', userInfoDto.accessToken);
    // Перенаправляем юзера на страницу с категориями
    this.router.navigate(['']).then();
  }

  // Выход из системы
  logOut(){
    localStorage.clear()
  }

  // Авторизован ли пользователь
  isAuthenticated(): boolean {
    return localStorage.getItem('isAuthenticated') == 'true';
  }

  // Получить инофрмацию о пользователе из localStorage
  getUserInfo() : UserInfo {
    return JSON.parse(localStorage.getItem('userInfo')!) as UserInfo;
  }

  // Получить токен доступа (for AuthInterceptor only)
  getAccessToken(): string {
    return localStorage.getItem('accessToken')!;
  }
}
