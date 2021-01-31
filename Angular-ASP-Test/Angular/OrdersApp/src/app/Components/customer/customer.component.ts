import { Component, OnInit } from '@angular/core';
import {MatTableDataSource} from "@angular/material/table";
import {Order} from "../../models/Order";
import {Product} from "../../models/Product";
import {ProductService} from "../../services/product.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.css']
})
export class CustomerComponent implements OnInit {

  myColor="white";
  datasource: MatTableDataSource<Order>
  products: Product[];
  displayedColumns: string[] = ['name', 'productCategory', 'productSize', 'price', 'quantity'];

  constructor(private productService: ProductService,
              private router: Router) { }

  ngOnInit(): void {
    this.getProducts();
  }

  route(id: number) {
    this.router.navigate([`/order/${id}`]);
  }
  getProducts() {
    this.productService.getProducts()
      .subscribe((products) => {
          this.products = products;
          console.log(this.products);
          this.datasource = new MatTableDataSource(products);
        },
        (error => {
          console.error('error caught in component')
          throw error;
        }));
  }
}
