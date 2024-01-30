import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Category} from "../dtos/category-dto";

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http: HttpClient, @Inject('BASE_API_URL') private baseUrl: string) { }

  getCategories(){
    return this.http.get<Category[]>(this.baseUrl + "/Category/GetAll");
  }

}
