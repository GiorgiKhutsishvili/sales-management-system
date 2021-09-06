import { combineReducers } from "redux";
import { consultant } from './consultant';
import { sale } from './sale';
import { product } from './product';
import { genericReducer } from './genericReducer';
import { globalLoader } from './globalLoader';
import * as actions from './report';

const salesByConsultantsReport = actions.salesByConsultantsReport;
const salesByProductPricesReport = actions.salesByProductPricesReport;
const consultantsByFrequentlySoldProductsReport = actions.consultantsByFrequentlySoldProductsReport;

export const reducers = combineReducers({
    genericReducer,
    consultant,
    sale,
    product,
    salesByConsultantsReport,
    salesByProductPricesReport,
    consultantsByFrequentlySoldProductsReport,
    globalLoader
});