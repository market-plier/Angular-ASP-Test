import {Customer} from "./Customer";
import {ProductOrders} from "./ProductOrders";

export interface Order {
  Id?: number;
  Customer?: Customer;
  ProductOrders?: ProductOrders[];
}
