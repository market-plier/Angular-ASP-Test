import { createReducer, on, Action } from '@ngrx/store';
import {addProductOrders} from "../actions/order.action";
import {ProductOrders} from "../../models/ProductOrders";

export const initialState: ReadonlyArray<ProductOrders> = [];

export const productOrdersReducer = createReducer(
  initialState,
  on(addProductOrders, (state, { productOrders }) => {
    if (state.indexOf(productOrders) > -1) return state;

    return [...state, productOrders];
  })
);
