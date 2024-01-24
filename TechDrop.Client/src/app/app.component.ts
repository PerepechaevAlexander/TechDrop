import {Component, OnInit} from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title = 'TechDrop.Client';

  ngOnInit() {
    // Добавляем обработчик события прокрутки страницы
    window.addEventListener("scroll", function() {
      let scrollButton = document.getElementById("scroll-button");
      // Если страница прокручена на определенную высоту, показываем кнопку
      if (window.scrollY > 300) {
        scrollButton!.style.display = "block";
      } else {
        scrollButton!.style.display = "none";
      }
    });
  }

  // Метод прокрутки страницы
  public onTop(){
    // Получаем ссылку на кнопку прокрутки вверх и добавляем обработчик события на клик
    let scrollButton = document.getElementById("scroll-button");
    scrollButton!.addEventListener("click", function() {
      // Прокручиваем страницу вверх при клике на кнопку
      window.scrollTo({
        top: 0,
        behavior: "smooth" // Для плавной прокрутки
      });
    });
  }

}
