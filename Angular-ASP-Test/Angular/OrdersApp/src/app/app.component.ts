import {Component, EventEmitter, Output} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {select, Store} from "@ngrx/store";
import {CreateOrderComponent} from "./Components/create-order/create-order.component";
import {ProductOrders} from "./models/ProductOrders";
import {addProductOrders} from "./state/actions/order.action";
import {selectProductOrders} from "./state/selectors/order.selectors";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(
    private store: Store,
    public route: ActivatedRoute) {

  }
  title = 'OrdersApp';
  links = [
    { title: 'Orders', fragment: 'orders' },
    { title: 'Products', fragment: 'products' },
    { title: 'Customers', fragment: 'customers' }
    ];
  onAdd(productOrders) {
    console.log(productOrders);
    this.store.dispatch(addProductOrders({ productOrders }));
    this.store.pipe(select(selectProductOrders)).subscribe(value => console.log(value));
  }
}
