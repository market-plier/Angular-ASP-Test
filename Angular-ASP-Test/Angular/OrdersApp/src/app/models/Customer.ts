import {Order} from "./Order";

export interface Customer {
  id?: number;
  name?: string;
  address?: string;
  orderedCost?: number;
  orderQuantity?: number;
  orders?: Order[];
}
