import { ACTION_TYPES } from "../actions/actionsTypes";
const initialState = {
    salesByConsultants: [],
    salesByProductPrices: [],
    consultantsByFrequentlySoldProducts: []
}

export const salesByConsultantsReport = (state = initialState, action) => {
    switch (action.type) {
        case ACTION_TYPES.FETCH_ALL_SALESBYCONSULTANTS:
            return {
                ...state,
                salesByConsultants: [...action.payload]
            }
        default:
            return state;
    }
}

export const salesByProductPricesReport = (state = initialState, action) => {
    switch (action.type) {
        case ACTION_TYPES.FETCH_ALL_SALESBYPRODUCTPRICES:
            return {
                ...state,
                salesByProductPrices: [...action.payload]
            }
        default:
            return state;
    }
}

export const consultantsByFrequentlySoldProductsReport = (state = initialState, action) => {
    switch (action.type) {
        case ACTION_TYPES.FETCH_ALL_CONSULTANTSBYFREQUENTLYSOLDPRODUCTS:
            return {
                ...state,
                consultantsByFrequentlySoldProducts: [...action.payload]
            }
        default:
            return state;
    }
}

export const consultantsBySumSales = (state = initialState, action) => {
    switch (action.type) {
        case ACTION_TYPES.FETCH_ALL_CONSULTANTSBYSUMSALES:
            return {
                ...state,
                consultantsByFrequentlySoldProducts: [...action.payload]
            }
        default:
            return state;
    }
}