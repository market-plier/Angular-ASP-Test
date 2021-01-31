import {Order} from "./Order";

export interface Customer {
  Id?: number;
  Name?: string;
  Address?: string;
  OrderedCost?: number;
  OrderQuantity?: number;
  Orders?: Order[];
}
