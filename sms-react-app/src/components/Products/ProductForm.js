import React, { useState, useEffect } from 'react'
import { connect } from 'react-redux';
import * as prdouctAction from '../../store/actions/product';
import {
    Button,
    Grid,
    TextField,
    withStyles
} from "@material-ui/core";
import { useToasts } from "react-toast-notifications";
import useForm from "../../utils/useForm";
import { useParams } from "react-router-dom";
import { useHistory } from "react-router-dom";
import { apiActions } from "../../store/actions/apiActions";
import * as api from "../../utils/baseUrl";
import axios from "axios";

const styles = theme => ({
    root: {
        '& .MuiFormControl-root': {
            width: '90%',
            margin: theme.spacing(1)
        }
    },
    smMargin: {
        margin: theme.spacing(1),
    },
    label: {
        textTransform: 'none'
    }
});

const initialFieldValues = {
    id: '',
    name: '',
    price: ''
}

const ProductForm = ({ classes, ...props }) => {
    const history = useHistory();

    const params = useParams();
    const productId = params.id;

    const { addToast } = useToasts();

    const validate = (fieldValues = values) => {
        let temp = { ...errors }
        let requiredText = "ველი სავალდებულოა";

        if ('name' in fieldValues)
            temp.name = fieldValues.name ? "" : requiredText;

        if ('price' in fieldValues)
            temp.price = fieldValues.price ? "" : requiredText; // TODO: allow only numbers

        setErrors({
            ...temp
        });

        if (fieldValues === values)
            return Object.values(temp).every(x => x === "");
    }

    const {
        values,
        setValues,
        errors,
        setErrors,
        handleInputChange,
        resetForm
    } = useForm(initialFieldValues, validate);

    const handleSubmit = e => {
        e.preventDefault();

        if (validate()) {
            const onSuccess = () => {
                history.push("/products");
                resetForm();
                addToast("ოპერაცია წარმატებით განხორციელდა", { appearance: 'success' });
            }
            const onFailure = (message) => {
                addToast(message, { appearance: 'error' });
                if (!productId) {
                    resetForm();
                }
            }

            if (!productId) {
                props.createProduct(
                    values,
                    apiActions.createProduct,
                    onSuccess,
                    onFailure
                );
            }
            else {
                props.updateProduct(
                    productId,
                    apiActions.updateProduct,
                    values,
                    onSuccess,
                    onFailure
                );
            }
        }
    }

    useEffect(async () => {
        if (productId) {
            await axios.get(api.baseUrl + apiActions.getProduct + productId)
                .then(res => {
                    setValues({
                        ...res.data
                    });
                    setErrors({});
                })
                .catch(err => {
                    console.log(err)
                })
        }
    }, [productId])

    const goBack = () => {
        history.goBack();
    }

    return (
        <div>
            <form autoComplete="off" noValidate className={classes.root} onSubmit={handleSubmit}>
                <Grid container>
                    <Grid item xs={6}>
                        <TextField
                            name="name"
                            variant="outlined"
                            label="პროდუქციის დასახელება"
                            value={values.name}
                            onChange={handleInputChange}
                            {...(errors.name && { error: true, helperText: errors.name })}
                        />
                    </Grid>
                    <Grid item xs={6}>
                        <TextField
                            name="price"
                            variant="outlined"
                            label="პროდუქციის გასაყიდი ფასი"
                            type="number"
                            InputLabelProps={{
                                shrink: true,
                            }}
                            value={values.price}
                            onChange={handleInputChange}
                            {...(errors.price && { error: true, helperText: errors.price })}
                        />
                        <div>
                            <Button
                                variant={"contained"}
                                size={"large"}
                                color={"primary"}
                                classes={{ root: classes.smMargin }}
                                type="submit"
                            >
                                შენახვა
                            </Button>
                            {
                                productId ?
                                    <Button
                                        variant={"contained"}
                                        size={"large"}
                                        classes={{ root: classes.smMargin }}
                                        onClick={goBack}
                                    >
                                        უკან დაბრუნება
                                    </Button>
                                    :
                                    <Button
                                        variant={"contained"}
                                        size={"large"}
                                        classes={{ root: classes.smMargin }}
                                        onClick={resetForm}
                                    >
                                        გასუფთავება
                                    </Button>
                            }
                        </div>
                    </Grid>
                </Grid>
            </form>
        </div>
    )
}

const mapStateToProps = state => ({
});

const mapActionToProps = {
    createProduct: prdouctAction.createProduct,
    updateProduct: prdouctAction.updateProduct
}

export default connect(mapStateToProps, mapActionToProps)(withStyles(styles)(ProductForm));
