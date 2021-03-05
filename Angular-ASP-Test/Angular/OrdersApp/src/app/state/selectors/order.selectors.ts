import { createSelector, createFeatureSelector } from "@ngrx/store";
import {AppState} from "../app.state";
import {ProductOrders} from "../../models/ProductOrders";

export const selectProductOrders = createSelector(
  (state: AppState) => state.productOrders,
  (productOrders: Array<ProductOrders>) => productOrders
);
