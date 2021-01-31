import { Order } from './Order';
import {Product} from "./Product";

export interface ProductOrders {
  id?: number;
  product?: Product;
  order?: Order;
  quantity?: number;
}
