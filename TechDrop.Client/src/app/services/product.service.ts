import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {CatalogProcessor} from "../dtos/catalogProcessor-dto";
import {ProductProcessor} from "../dtos/productProcessor-dto";

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http: HttpClient, @Inject('BASE_API_URL') private baseUrl: string) { }

  getProcessors(){
    return this.http.get<CatalogProcessor[]>(this.baseUrl + "/Processor/ForCatalog");
  }

  getProcessor(id: number){
    return this.http.get<ProductProcessor>(this.baseUrl + "/Processor/ById?" + 'id=' + id);
  }

}
