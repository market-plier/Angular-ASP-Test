import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';

import {Observable, of, throwError} from "rxjs";
import {catchError} from "rxjs/operators";
import {MatSnackBar} from "@angular/material/snack-bar";
import {Order} from "../models/Order";

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  httpOptions = {
    headers: new HttpHeaders({'Content-Type': 'application/json'})
  };

  private orderUrl = 'http://localhost:5000/api/order';

  constructor(private _snackBar: MatSnackBar,
              private http: HttpClient) {
  }


  getOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(this.orderUrl)
      .pipe(
        catchError((err) => {
          console.log('error caught in service')
          console.error(err);
          this.openSnackBar(err.error.errors, 'OK')
          return throwError(err);    //Rethrow it back to component
        }));
  }

  getOrder(id: number): Observable<Order> {
    const url = `${this.orderUrl}/${id}`;
    return this.http.get<Order>(url).pipe(
      catchError((err) => {
        console.log('error caught in service')
        console.error(err);
        this.openSnackBar(err.error.errors, 'OK')
        return throwError(err);    //Rethrow it back to component
      }));
  }

  addOrder(order: Order) : Observable<Order> {
    return this.http.post<Order>(this.orderUrl, order, this.httpOptions).pipe(
      catchError((err) => {
        console.log('error caught in service')
        console.error(err);
        this.openSnackBar(err.error.errors, 'OK')
        return throwError(err);    //Rethrow it back to component
      })
    );
  }

  openSnackBar(message: string, action: string) {
    this._snackBar.open(message, action, {
      duration: 10000,
    });
  }
}
