import React, { useState, useEffect } from 'react'
import { connect } from 'react-redux';
import * as consultantActions from '../../store/actions/consultant';
import * as saleActions from '../../store/actions/sale';
import * as productActions from '../../store/actions/product';
import * as api from "../../utils/baseUrl";
import axios from "axios";

import { ACTION_TYPES } from "../../store/actions/actionsTypes";
import Fab from '@material-ui/core/Fab';
import AddIcon from '@material-ui/icons/Add';
import RemoveIcon from '@material-ui/icons/Remove';
import {
    Button,
    Select as MuiSelect,
    FormControl,
    FormHelperText,
    Grid,
    InputLabel,
    MenuItem,
    TextField,
    withStyles
} from "@material-ui/core";
import { useToasts } from "react-toast-notifications";
import useForm from "../../utils/useForm";
import { useParams, useHistory } from "react-router-dom";
import { apiActions } from "../../store/actions/apiActions";
import { set } from 'date-fns';

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
    consultantId: '',
    productId: '',
    productCount: 0
}


const SaleForm = ({ classes, ...props }) => {

    const [consultantId, setConsultantId] = useState({ consultantId: "" });
    const [inputList, setInputList] = useState([{ productId: '', productCount: 0 }])
    const [consultants, setConsultants] = useState([]);
    const [products, setProducts] = useState([]);

    const history = useHistory();
    const params = useParams();
    const saleId = params.id;

    const { addToast } = useToasts();

    const validate = (fieldValues = values) => {
        let temp = { ...errors }
        let requiredText = "ველი სავალდებულოა";

        setErrors({
            ...temp
        });

        if (fieldValues === values)
            return Object.values(temp).every(x => x === "");
    }

    const handleInputChange = (e, index) => {
        const { name, value } = e.target;
        if (e.target.name === "consultantId") {
            setConsultantId(value)
        } else {
            const list = [...inputList];
            list[index][name] = value;

            setInputList(list);
            setValues({
                ...list,
            });
            validate(value);
        }
    };

    const handleRemoveClick = index => {
        const list = [...inputList];
        list.splice(index, 1);
        setInputList(list);
    };

    const handleAddClick = () => {
        setInputList([...inputList, { productId: '', productCount: 0 }]);
    };

    const {
        values,
        setValues,
        errors,
        setErrors,
        resetForm
    } = useForm(initialFieldValues, validate);

    const handleSubmit = e => {
        e.preventDefault();
        if (validate()) {
            const onSuccess = () => {
                history.push("/sales");
                resetForm();
                addToast("ოპერაცია წარმატებით განხორციელდა", { appearance: 'success' });
            }
            const onFailure = (message) => {
                addToast(message, { appearance: 'error' });
                if (!saleId) {
                    resetForm();
                }
            }

            const postData = {
                consultantId: consultantId,
                products: inputList
            }

            if (!saleId) {
                props.createSale(
                    postData,
                    apiActions.createSale,
                    onSuccess,
                    onFailure
                );
            }
            else {
                props.updateSale(
                    saleId,
                    apiActions.updateSale,
                    postData,
                    onSuccess,
                    onFailure
                );
            }
        }
    }

    //material-ui select
    const inputLabel = React.useRef(null);
    const [labelWidth, setLabelWidth] = React.useState(0);
    React.useEffect(() => {
        setLabelWidth(inputLabel.current.offsetWidth);
    }, []);

    useEffect(async () => {
        await axios.get(api.baseUrl + apiActions.getAllConsultants)
            .then(res => {
                setConsultants(res.data);
            })
            .catch(err => {
                console.log(err)
            });

        await axios.get(api.baseUrl + apiActions.getAllProducts)
            .then(res => {
                setProducts(res.data);
            })
            .catch(err => {
                console.log(err)
            });

        if (saleId) {
            await axios.get(api.baseUrl + apiActions.getSale + saleId)
                .then(res => {
                    const data = {
                        productId: res.data.productId,
                        productCount: res.data.productCount
                    }
                    setConsultantId(res.data.consultantId);
                    setInputList([data]);
                    setErrors({})
                })
                .catch(err => {
                    console.log(err)
                })
        }
    }, [saleId])

    const goBack = () => {
        history.goBack();
    }

    return (
        <div>
            <form autoComplete="off" noValidate className={classes.root} onSubmit={handleSubmit}>
                <Grid container>
                    <Grid item xs={6}>
                        <FormControl variant="outlined"
                        >
                            <InputLabel ref={inputLabel}>კონსულტანტი</InputLabel>
                            <MuiSelect
                                name="consultantId"
                                value={consultantId}
                                onChange={e => handleInputChange(e)}
                                labelWidth={labelWidth}
                            >
                                {
                                    consultants.length === 0 &&
                                    <MenuItem value="">None</MenuItem>
                                }
                                {
                                    consultants.map(
                                        item => (<MenuItem key={item.id} value={item.id}>{item.firstName} {item.lastName}</MenuItem>)
                                    )
                                }
                            </MuiSelect>
                            {errors.consultantId && <FormHelperText>{errors.consultantId}</FormHelperText>}
                        </FormControl>
                    </Grid>
                    {
                        inputList.map((values, i) => {
                            return (
                                <Grid container>
                                    <Grid item xs={3}>
                                        <FormControl variant="outlined"
                                        >
                                            <InputLabel ref={inputLabel}>პროდუქტი</InputLabel>
                                            <MuiSelect
                                                name="productId"
                                                value={values.productId}
                                                onChange={e => handleInputChange(e, i)}
                                                labelWidth={labelWidth}
                                            >
                                                {
                                                    products.length === 0 &&
                                                    <MenuItem value="">None</MenuItem>
                                                }
                                                {
                                                    products.map(
                                                        item => (<MenuItem key={item.id} value={item.id}>
                                                            {item.name + " -- " + item.price + "₾"}
                                                        </MenuItem>)
                                                    )
                                                }
                                            </MuiSelect>
                                            {errors.productId && <FormHelperText>{errors.productId}</FormHelperText>}
                                        </FormControl>
                                    </Grid>
                                    <Grid item xs={3}>
                                        <TextField
                                            name="productCount"
                                            variant="outlined"
                                            label="რაოდენობა"
                                            value={values.productCount}
                                            type="number"
                                            onChange={e => handleInputChange(e, i)}
                                            InputLabelProps={{
                                                shrink: true,
                                            }}
                                            {...(errors.productCount && { error: true, helperText: errors.productCount })}
                                        />
                                    </Grid>
                                    {
                                        !saleId && <Grid item xs={0}>
                                            {inputList.length - 1 === i &&
                                                <Fab style={{ marginLeft: '.3rem' }}
                                                    color="primary"
                                                    aria-label="add"
                                                    onClick={() => handleAddClick(values.consultantId, values.productId)}
                                                >
                                                    <AddIcon />
                                                </Fab>}
                                        </Grid>
                                    }
                                    {
                                        !saleId && <Grid item xs={0}>
                                            {inputList.length !== 1 &&
                                                <Fab style={{ marginLeft: '.3rem' }}
                                                    color="secondary"
                                                    aria-label="add"
                                                    onClick={() => handleRemoveClick(i)}
                                                >
                                                    <RemoveIcon />
                                                </Fab>}
                                        </Grid>
                                    }
                                </Grid>
                            )
                        })
                    }
                </Grid>

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
                        saleId ?
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
            </form>
        </div>
    )
}

const mapStateToProps = state => ({
    consultantsList: state.consultant.consultantsList,
    salesList: state.sale.salesList,
    productsList: state.product.productsList
});

const mapActionToProps = {
    fetchAllConsultants: consultantActions.fetchAllConsultants,
    fetchAllProducts: productActions.fetchAllProducts,
    createSale: saleActions.createSale,
    updateSale: saleActions.updateSale
}

export default connect(mapStateToProps, mapActionToProps)(withStyles(styles)(SaleForm));
