import {Inject, Injectable} from '@angular/core';
import {HttpClient, HttpResponse} from "@angular/common/http";
import {ProductQuantityDto} from "../dtos/productQuantity-dto";

@Injectable({
  providedIn: 'root'
})
// Сервис для работы с корзиной
export class CartService {

  constructor(private http: HttpClient, @Inject('BASE_API_URL') private baseUrl: string) { }

  // Получить кол-во каждого товара в корзине.
  getCart(){
    return this.http.get<ProductQuantityDto[]>(this.baseUrl + "/Cart/GetQuantityOfProducts")
  }

  // Добавить товар в корзину по Id товара.
  addToCart(productId: number){
    return this.http.post<HttpResponse<any>>(this.baseUrl + "/Cart/AddProductById", productId);
  }

  // Удалить товар из корзины по Id товара.
  removeFromCart(productId: number){
    return this.http.delete(this.baseUrl + "/Cart/RemoveFromCartById",
      {params: {productId: productId}, observe: 'response'});
  }
}
