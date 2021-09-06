import sms from "./api";
import { ACTION_TYPES } from "./actionsTypes";

export const fetchAllProducts = (action) => dispatch => {
    sms.api().fetchAll(action)
        .then(response => {
            dispatch({
                type: ACTION_TYPES.FETCH_ALL_PRODUCTS,
                payload: response.data
            });
        })
        .catch(
            err => console.log("fetchAll error", err)
        );
}

export const createProduct = (data, action, onSuccess, onFailure) => dispatch => {
    sms.api().create(action, data)
        .then(response => {
            dispatch({
                type: ACTION_TYPES.CREATE_PRODUCT,
                payload: response.data
            })
            onSuccess()
        })
        .catch(error => {
            let errors = error.response && (
                error.response.data.message
                || error.response.data
                || error.response.statusText);
            if (errors) {
                errors.split(/\r?\n/).forEach(message => {
                    onFailure(message);
                });
            }
        });
}

export const updateProduct = (id, action, data, onSuccess, onFailure) => dispatch => {
    sms.api().update(id, action, data)
        .then(response => {
            dispatch({
                type: ACTION_TYPES.UPDATE_PRODUCT,
                payload: { id, ...data }
            })
            onSuccess()
        })
        .catch(error => {
            let errors = error.response && (
                error.response.data.message
                || error.response.data
                || error.response.statusText);
            if (errors) {
                errors.split(/\r?\n/).forEach(message => {
                    onFailure(message);
                });
            }
        });
}

export const DeleteProduct = (id, action, onSuccess) => dispatch => {
    sms.api().delete(id, action)
        .then(response => {
            dispatch({
                type: ACTION_TYPES.DELETE_PRODUCT,
                payload: id
            })
            onSuccess()
        })
        .catch(err => console.log(err));
}