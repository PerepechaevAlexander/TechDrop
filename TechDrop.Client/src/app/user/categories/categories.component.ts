import {Component, OnInit} from '@angular/core';
import {CategoryService} from "../../services/category.service";
import {Category} from "../../dtos/category-dto";
import {Router} from "@angular/router";

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent implements OnInit {
  categories: Category[] = [];

  //Для выделения при наведении
  select: boolean = false;

  constructor(
    private categoryService: CategoryService,
    private router: Router) { }

  ngOnInit(): void {
    this.categoryService.getCategories().subscribe((res) =>{
      this.categories = res;
    })
  }

  public route(id: number){
    if (id == 1) {
      this.router.navigate(['catalog/processors']).then();
    }
  }

}
