import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {Customer} from "../../models/Customer";
import {Product} from "../../models/Product";
import {ActivatedRoute} from "@angular/router";
import {Location} from '@angular/common';
import {OrderService} from "../../services/OrderService";
import {CustomerServiceService} from "../../services/customer-service.service";
import {MatTableDataSource} from "@angular/material/table";
import {ProductOrders} from "../../models/ProductOrders";
import {ProductService} from "../../services/product.service";
import {Order} from "../../models/Order";

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
    'New', 'Paid','Shipped','Delivered','Closed'
  ];
  selectedProducts: ProductOrders[] = [];
  myColor="white";
  datasource: MatTableDataSource<ProductOrders>
  displayedColumns: string[] = ['name', 'productCategory', 'productSize', 'price', 'quantity'];

  constructor(private route: ActivatedRoute,
              private orderService: OrderService,
              private productService: ProductService,
              private customerService: CustomerServiceService,
              private location: Location) {
    this.initForm()
  }

  ngOnInit(): void {
    this.getCustomers();
this.getProducts();
  }
  initForm(): void {
    this.form = new FormGroup({
      customer: new FormControl('', Validators.required),
      status: new FormControl('', Validators.required),
      product: new FormControl('', Validators.required),
      quantity: new FormControl('', Validators.required),
    })
  }
  getCustomers(){
    this.customerService.getCustomers()
      .subscribe((customers) => {
          this.customers = customers;
          console.log(this.customers);
        },
        (error => {
          console.error('error caught in component')
          throw error;
        }));
  }
  getProducts(){
    this.productService.getProducts()
      .subscribe((products) => {
          this.products = products;
          console.log(this.products);
        },
        (error => {
          console.error('error caught in component')
          throw error;
        }));
  }
  cancel() {
    this.location.back();
  }
    addToSelectedProducts(){
    const product = this.products.filter(product => product.id == this.form.get('product').value)[0];
      console.log(product);
      const orderProduct: ProductOrders ={
        product: product,
        quantity: this.form.get('quantity').value
      }
      console.log(orderProduct);
        this.selectedProducts.push(orderProduct);
      this.datasource = new MatTableDataSource<ProductOrders>(this.selectedProducts)
    }
  add() {
    const order: Order ={
      status: this.form.get('status').value
    };
    order.customer = this.customers.filter(customer => customer.id == this.form.get('customer').value)[0];
    order.productOrders = this.selectedProducts;
    console.log(order);
    this.orderService.addOrder(order)
      .subscribe(() => this.cancel());
  }
}
