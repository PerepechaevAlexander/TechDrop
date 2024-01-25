import { Component } from '@angular/core';
import {AuthService} from "../../services/auth.service";

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
// Компонент навигационной панели вверху страницы
export class NavbarComponent {

  // Локальное хранилище
  protected readonly localStorage = localStorage;

  constructor(private authService: AuthService) { }

  // Обработчик кнопки "Выйти"
  logOut(){
    this.authService.logOut();
  }
}
