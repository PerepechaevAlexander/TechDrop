import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {CategoriesComponent} from "./user/categories/categories.component";
import {LoginComponent} from "./user/login/login.component";
import {RegisterComponent} from "./user/register/register.component";
import {ProcessorsComponent} from "./user/catalog/processors/processors.component";
import {ProcessorComponent} from "./user/products/processor/processor.component";

const routes: Routes = [
  {path:'', component: CategoriesComponent},
  {path:'login', component: LoginComponent},
  {path:'register', component: RegisterComponent},
  {path:'catalog/processors', component: ProcessorsComponent},
  {path:'product/processor', component: ProcessorComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
