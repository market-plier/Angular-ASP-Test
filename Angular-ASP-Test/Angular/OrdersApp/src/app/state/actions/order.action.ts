import { createAction, props } from '@ngrx/store';

export const addProductOrders = createAction(
  '[ProductOrders List] Add ProductOrders',
  props<{ productOrders }>()
);
