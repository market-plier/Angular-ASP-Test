import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable, throwError} from "rxjs";
import {Customer} from "../models/Customer";
import {catchError} from "rxjs/operators";
import {Order} from "../models/Order";
import {Product} from "../models/Product";
import {log} from "util";

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  httpOptions = {
    headers: new HttpHeaders({'Content-Type': 'application/json'})
  };

  private orderUrl = 'http://localhost:5000/api/product';

  constructor(private http: HttpClient) {
  }

  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(this.orderUrl)
      .pipe(
        catchError((err) => {
          console.log('error caught in service')
          console.error(err);
          return throwError(err);    //Rethrow it back to component
        }));
  }
  addProduct(product: Product) : Observable<Product> {
    console.log(product);
    return this.http.post<Product>(this.orderUrl, product, this.httpOptions).pipe(
      catchError((err) => {
        console.log('error caught in service')
        console.error(err);
        return throwError(err);
      })
    );
  }
}
