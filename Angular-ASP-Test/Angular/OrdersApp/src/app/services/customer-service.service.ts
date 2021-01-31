import { Injectable } from '@angular/core';
import {Observable, throwError} from "rxjs";
import {Order} from "../models/Order";
import {catchError} from "rxjs/operators";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {MatSnackBar} from "@angular/material/snack-bar";
import {Customer} from "../models/Customer";

@Injectable({
  providedIn: 'root'
})
export class CustomerServiceService {


  httpOptions = {
    headers: new HttpHeaders({'Content-Type': 'application/json'})
  };

  private orderUrl = 'http://localhost:5000/api/customer';

  constructor(private _snackBar: MatSnackBar,
              private http: HttpClient) {
  }

  getCustomers(): Observable<Customer[]> {
    return this.http.get<Customer[]>(this.orderUrl)
      .pipe(
        catchError((err) => {
          console.log('error caught in service')
          console.error(err);
          this.openSnackBar(err.error.errors, 'OK')
          return throwError(err);    //Rethrow it back to component
        }));
  }
  openSnackBar(message: string, action: string) {
    this._snackBar.open(message, action, {
      duration: 10000,
    });
  }
}
