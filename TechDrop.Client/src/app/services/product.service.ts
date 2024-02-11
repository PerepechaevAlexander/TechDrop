import {Inject, Injectable} from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {CatalogProcessor} from "../dtos/catalogProcessor-dto";
import {ProductProcessor} from "../dtos/productProcessor-dto";
import {ProcessorFilterDto} from "../dtos/processorFilter-dto";

@Injectable({
  providedIn: 'root'
})
// Сервис для работы с товарами
export class ProductService {

  constructor(private http: HttpClient, @Inject('BASE_API_URL') private baseUrl: string) { }

  // Получить главную ифнормацию о процессорах
  getProcessors(){
    return this.http.get<CatalogProcessor[]>(this.baseUrl + "/Processor/ForCatalog");
  }

  // Получить главную ифнормацию о процессорах, с фильтрами
  getFilteredProcessors(filter: ProcessorFilterDto){
    let params = new HttpParams();

    if (filter.manufacturers.length > 0){
      params = params.append('manufacturers', filter.manufacturers.join('-'));
    }
    if (filter.available){
      params = params.append('available', filter.available);
    }
    if (filter.minCost != null && filter.minCost > 0){
      params = params.append('minCost', filter.minCost);
    }
    if (filter.maxCost != null && filter.maxCost > 0){
      params = params.append('maxCost', filter.maxCost);
    }

    return this.http.get<CatalogProcessor[]>(this.baseUrl + "/Processor/ForCatalog",
      {params, observe: 'response'});
  }

  // Получить всю информацию о процессоре
  getProcessor(id: number){
    return this.http.get<ProductProcessor>(this.baseUrl + "/Processor/ById",
      {params: {id: id}, observe: 'response'});
  }

}
