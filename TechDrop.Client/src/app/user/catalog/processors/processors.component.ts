import {Component, OnInit} from '@angular/core';
import {ProductService} from "../../../services/product.service";
import {CatalogProcessor} from "../../../dtos/catalogProcessor-dto";

@Component({
  selector: 'app-processors',
  templateUrl: './processors.component.html',
  styleUrls: ['./processors.component.css']
})
export class ProcessorsComponent implements OnInit {

  categoryId: number = 0;
  catalogProcessors: CatalogProcessor[] = [];

  constructor(
    private productService: ProductService) {
  }

  ngOnInit(): void {
    this.productService.getProcessors().subscribe((res)=> {
      this.catalogProcessors = res;
    });
  }

}
