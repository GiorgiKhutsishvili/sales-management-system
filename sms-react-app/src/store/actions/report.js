import sms from "./api";
import { ACTION_TYPES } from "./actionsTypes";

export const fetchAllSalesByConsultants = (action, filter) => dispatch => {
    sms.api().fetchAllSalesByConsultants(action, filter)
        .then(response => {
            dispatch({
                type: ACTION_TYPES.FETCH_ALL_SALESBYCONSULTANTS,
                payload: response.data
            });
        })
        .catch(
            err => console.log(err)
        );
}

export const fetchAllSalesByProductPrices = (action, filter) => dispatch => {
    sms.api().fetchAllSalesByProductPrices(action, filter)
        .then(response => {
            dispatch({
                type: ACTION_TYPES.FETCH_ALL_SALESBYPRODUCTPRICES,
                payload: response.data
            });
        })
        .catch(
            err => console.log(err)
        );
}

export const fetchAllConsultantsByFrequentlySoldProducts = (action, filter) => dispatch => {
    sms.api().fetchAllConsultantsByFrequentlySoldProducts(action, filter)
        .then(response => {
            dispatch({
                type: ACTION_TYPES.FETCH_ALL_CONSULTANTSBYFREQUENTLYSOLDPRODUCTS,
                payload: response.data
            });
        })
        .catch(
            err => console.log(err)
        );
}


export const fetchAllConsultantsBySumSales = (action, filter) => dispatch => {
    sms.api().fetchAllConsultantsBySumSales(action, filter)
        .then(response => {
            dispatch({
                type: ACTION_TYPES.FETCH_ALL_CONSULTANTSBYSUMSALES,
                payload: response.data
            });
        })
        .catch(
            err => console.log(err)
        );
}