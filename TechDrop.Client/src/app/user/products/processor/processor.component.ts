import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {ProductService} from "../../../services/product.service";
import {ProductProcessor} from "../../../dtos/productProcessor-dto";

@Component({
  selector: 'app-processor',
  templateUrl: './processor.component.html',
  styleUrls: ['./processor.component.css']
})
export class ProcessorComponent implements OnInit {

  processorId: number = 0;
  processor: ProductProcessor = {
    productId: 0,
    description: '',
    cost: 0,
    quantity: 0,
    discount: 0,
    pictures: [],
    manufacturer: 'string',
    processorId: 0,
    model: '',
    socket: '',
    year: 0,
    coolingSystem: false,
    cores: 0,
    threads: 0,
    performanceCores: 0,
    energyCores: 0,
    l2: 0,
    l3: 0,
    techProcess: 0,
    baseFrequency: 0,
    maxFrequency: 0,
    baseFrequencyEnergyCores: 0,
    maxFrequencyEnergyCores: 0,
    freeMultiplier: false,
    ramTypes: [],
    ramCapacity: 0,
    ramChannels: 0,
    ramMaxFrequency: 0,
    tdp: 0,
    maxTemp: 0,
    graphCoreAvailable: false,
    graphCore: {
      model: '',
      maxFrequency: 0,
      executiveBlocks: 0,
      shadingUnits: 0
    },
    pciExpress: '',
    pciExpressLines: 0
  };

  // Для меню "Характеристики/Описание"
  visibilityDescription: boolean = false;
  visibilitySpecifications: boolean = true;
  activeButtonDescription: boolean = true; // Изначально активна кнопка "Описание"
  inactiveButtonDescription: boolean = false;
  activeButtonSpecifications: boolean = false;
  inactiveButtonSpecifications: boolean = true;

  constructor(
    private route: ActivatedRoute,
    private productService: ProductService) {
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe((queryParam)=> {
      this.processorId = queryParam["id"];
    });

    this.productService.getProcessor(this.processorId).subscribe((res)=> {
      this.processor = res;
    });
  }

  // Логика работы меню "Характеристики/Описание"
  public display(menu: string){
    if (menu == "desc") { // Если нажата кнопка "Описание"
      if (!this.visibilityDescription){ // если описание сейчас видно
        this.visibilityDescription = true; // скрываем описание, ДЕактивируем кнопку "описание"
        this.activeButtonDescription = false;
        this.inactiveButtonDescription = true;
      }
      else { // если описание сейчас НЕ видно
        this.visibilityDescription = false; // показываем описание, активируем кнопку "описание"
        this.activeButtonDescription = true;
        this.inactiveButtonDescription = false;
        this.visibilitySpecifications = true; // и скрываем характиристики, ДЕактивируем кнопку "характиристики"
        this.activeButtonSpecifications = false;
        this.inactiveButtonSpecifications = true;
      }
    }
    if (menu === "spec") { // Если нажата кнопка "Характеристики"
      if (!this.visibilitySpecifications){ // если характиристики сейчас видно
        this.visibilitySpecifications = true; // скрываем характиристики, ДЕактивируем кнопку "характиристики"
        this.activeButtonSpecifications = false;
        this.inactiveButtonSpecifications = true;
      }
      else { // если характиристики сейчас НЕ видно
        this.visibilitySpecifications = false; // показываем характиристики, активируем кнопку "характиристики"
        this.activeButtonSpecifications = true;
        this.inactiveButtonSpecifications = false;
        this.visibilityDescription = true; // и скрываем описание, ДЕактивируем кнопку "описание"
        this.activeButtonDescription = false;
        this.inactiveButtonDescription = true;
      }
    }
  }

}
