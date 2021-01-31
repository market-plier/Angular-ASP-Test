import {ProductOrders} from "./ProductOrders";

export interface Product {
  Id?: number;
  Name?: string;
  Category?: string;
  Size?: string;
  Quantity?: number;
  Price?: number;
  ProductOrders?: ProductOrders[];
}
