import { Order } from './Order';
import {Product} from "./Product";

export interface ProductOrders {
  Id?: number;
  Product?: Product;
  Order?: Order;
  Quantity?: number;
}
