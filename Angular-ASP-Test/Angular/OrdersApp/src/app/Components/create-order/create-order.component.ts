import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {Customer} from "../../models/Customer";
import {Product} from "../../models/Product";
import {ActivatedRoute} from "@angular/router";
import {Location} from '@angular/common';
import {OrderService} from "../../services/OrderService";

@Component({
  selector: 'app-create-order',
  templateUrl: './create-order.component.html',
  styleUrls: ['./create-order.component.css']
})
export class CreateOrderComponent implements OnInit {

  form: FormGroup;
  customers: Customer[];
  products: Product[];
  status: string[] = [
    'Enabled', 'Paid','Shipped','Delivered','Closed'
  ];
  selectedProducts: Product[];
  constructor(private route: ActivatedRoute,
              private service: OrderService,
              private location: Location) { }

  ngOnInit(): void {
  }
  initForm(): void {
    this.form = new FormGroup({
      customer: new FormControl('', Validators.required),
      status: new FormControl('', Validators.required)
    })
  }

  cancel() {
    this.location.back();
  }
}
