import sms from "./api";
import { ACTION_TYPES } from "./actionsTypes";

export const fetchAllConsultants = (action) => dispatch => {
    sms.api().fetchAll(action)
        .then(response => {
            dispatch({
                type: ACTION_TYPES.FETCH_ALL_CONSULTANTS,
                payload: response.data
            });
        })
        .catch(
            err => console.log("fetchAll error", err)
        );
}

export const create = (data, action, onSuccess, onFailure) => dispatch => {
    sms.api().create(action, data)
        .then(response => {
            dispatch({
                type: ACTION_TYPES.CREATE_CONSULTANT,
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

export const update = (id, action, data, onSuccess, onFailure) => dispatch => {
    sms.api().update(id, action, data)
        .then(response => {
            dispatch({
                type: ACTION_TYPES.UPDATE_CONSULTANT,
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

export const Delete = (id, action, onSuccess) => dispatch => {
    sms.api().delete(id, action)
        .then(response => {
            dispatch({
                type: ACTION_TYPES.DELETE_CONSULTANT,
                payload: id
            })
            onSuccess()
        })
        .catch(err => console.log(err));
}