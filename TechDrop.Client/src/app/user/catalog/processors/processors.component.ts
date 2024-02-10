import {Component, OnInit} from '@angular/core';
import {ProductService} from "../../../services/product.service";
import {CatalogProcessor} from "../../../dtos/catalogProcessor-dto";
import {CartService} from "../../../services/cart.service";
import {ProductQuantityDto} from "../../../dtos/productQuantity-dto";
import {HttpErrorResponse} from "@angular/common/http";
import {AuthService} from "../../../services/auth.service";

@Component({
  selector: 'app-processors',
  templateUrl: './processors.component.html',
  styleUrls: ['./processors.component.css']
})
// Компонент каталога процессоров
export class ProcessorsComponent implements OnInit {

  // Каталог всех товаров
  catalogProcessors: CatalogProcessor[] = [];
  // Товары в корзине пользователя
  productsInCart: ProductQuantityDto[] =[];

  constructor(
    private productService: ProductService, private cartService: CartService,
    public authService: AuthService) {
  }

  ngOnInit(): void {
    // Получаем каталог товаров
    this.productService.getProcessors().subscribe((res)=> {
      this.catalogProcessors = res;
    });
    // Получаем товары из корзины пользователя
    this.cartService.getCart().subscribe((res) => {
      this.productsInCart = res;
    });
  }

  // Добавить товар в корзину
  AddToCart(productId: number){
    // Запрос на добавление в корзину
    this.cartService.addToCart(productId).subscribe({
      next: () => {
        // Если пришёл 200 -> Увеличиваем число товара в корзине
        let product = this.productsInCart.find(p=> p.productId === productId);

        // Если такого товара в корзине нет -> создаём запись
        if (product === undefined){
          let newProductInCart: ProductQuantityDto = { productId: productId, quantity: 1 };
          this.productsInCart.push(newProductInCart);
        }
        // Иначе просто прибавляем к найденному
        else {
          product.quantity += 1;
        }

      },
      error: (e: HttpErrorResponse) => {
        // TODO сделать нормальную обработку ошибок
        console.log(e.error.ErrorMessage);
      }
    })
  }

  // Удалить товар из корзины
  RemoveFromCart(productId: number){
    // Запрос на удаление из корзины
    this.cartService.removeFromCart(productId).subscribe({
      next: () => {

        // Если пришёл 200 -> уменьшаем число товара в корзине
        let productIndex = this.productsInCart.findIndex(p=> p.productId === productId)!;
        let productQuantity = --this.productsInCart[productIndex].quantity;

        // Если теперь число товара === 0 -> удаляяем запись
        if (productQuantity === 0){
          this.productsInCart.splice(productIndex, 1);
        }

      },
      error: (e: HttpErrorResponse) => {
        // TODO сделать нормальную обработку ошибок
        console.log(e.error.ErrorMessage);
      }
    });
  }

  // Проверить товар в корзине пользователя.
  // True - если товар не найден.
  CheckProductInCart(productId: number){
    return this.productsInCart.find(p => p.productId === productId) === undefined;
  }

  // Получить кол-во товара в корзине пользователя.
  GetQuantityInCart(productId: number){
    return this.productsInCart.find(p => p.productId === productId)?.quantity!;
  }
}
