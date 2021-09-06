import React, { useState, useEffect } from "react";
import { connect } from 'react-redux';
import * as actions from '../../store/actions/report';
import * as globalLoaderActions from '../../store/actions/globalLoader';
import { DataGrid } from '@material-ui/data-grid';
import { apiActions } from "../../store/actions/apiActions";
import {
    TextField,
    withStyles
} from "@material-ui/core";

import Grid from '@material-ui/core/Grid';
import DateFnsUtils from '@date-io/date-fns';
import {
    MuiPickersUtilsProvider,
    KeyboardDatePicker,
} from '@material-ui/pickers';
import moment from "moment";
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

const SalesByProductPrices = ({ classes, ...props }) => {
    const [rows, setRows] = useState([]);

    const columns = [
        { field: "index", headerName: "#", width: 100 },
        { field: "saleUniqueNumber", headerName: "გაყიდვის უნიკალური ნომერი", width: 200 },
        { field: "saleDate", headerName: "გაყიდვის თარიღი", width: 200 },
        { field: "consultantUniqueNumber", headerName: "კონსულტანტის უნიკალური ნომერი", width: 200 },
        { field: "consultantFullName", headerName: "კონსულტანტის სახელი და გვარი", width: 200 },
        { field: "consultantPersonalId", headerName: "კონსულტანტის პირადი ნომერი", width: 200 },
        { field: "productCount", headerName: "პროდუქციის რაოდენობა", width: 300 },
    ];

    const [filter, setFilter] = useState({
        startDate: new moment().subtract(14, "days").format("YYYY-MM-DD"),
        endDate: new moment().format("YYYY-MM-DD")
    });

    const handleStartDateChange = (startDate) => {
        let formatedStartDate = moment(startDate).format("YYYY-MM-DD")
        setFilter({ ...filter, startDate: formatedStartDate });
    };

    const handleEndDateChange = (endDate) => {
        let formatedEndDate = moment(endDate).format("YYYY-MM-DD")
        setFilter({ ...filter, endDate: formatedEndDate });
    };

    const [filterValues, setFilterValues] = useState({
        minPrice: null,
        maxPrice: null
    });

    const handleInputChange = (event) => {
        const target = event.target;
        const value = target.value;
        const name = target.name;

        setFilterValues({
            ...filterValues,
            [name]: value
        });
    }

    useEffect(async () => {
        await loadData();
    }, [
        filter.startDate,
        filter.endDate,
        filterValues.minPrice,
        filterValues.maxPrice
    ]);

    const loadData = async () => {
        props.useGlobalLoader(true);
        await axios.get(api.baseUrl + apiActions.getSalesByProductPrices, {
            params: {
                startDate: filter.startDate,
                endDate: filter.endDate,
                minPrice: filterValues.minPrice,
                maxPrice: filterValues.maxPrice
            }
        })
            .then(res => {
                setRows(res.data);
                props.useGlobalLoader(false);
            })
            .catch(err => {
                console.log(err)
            })
    }

    return (
        <div className={classes.root} style={{ height: 700, width: '100%' }}>
            <Grid container>
                <Grid item xs={6}>
                    <MuiPickersUtilsProvider utils={DateFnsUtils}>
                        <KeyboardDatePicker
                            disableToolbar
                            variant="inline"
                            inputVariant="outlined"
                            format="dd.MM.yyyy"
                            margin="normal"
                            label="საწყისი თარიღი"
                            value={filter.startDate}
                            onChange={handleStartDateChange}
                            KeyboardButtonProps={{
                                'aria-label': 'change date',
                            }}
                        />
                        <KeyboardDatePicker
                            disableToolbar
                            variant="inline"
                            inputVariant="outlined"
                            format="dd.MM.yyyy"
                            margin="normal"
                            label="საბოლოო თარიღი"
                            value={filter.endDate}
                            onChange={handleEndDateChange}
                            KeyboardButtonProps={{
                                'aria-label': 'change date',
                            }}
                        />
                    </MuiPickersUtilsProvider>
                </Grid>
                <Grid item xs={6}>
                    <TextField
                        name="minPrice"
                        variant="outlined"
                        label="პროდუქციის მინ. ფასი"
                        value={filterValues.minPrice}
                        onChange={handleInputChange}
                    />
                    <TextField
                        name="maxPrice"
                        variant="outlined"
                        label="პროდუქციის მაქს. ფასი"
                        value={filterValues.maxPrice}
                        onChange={handleInputChange}
                    />
                </Grid>
            </Grid>
            <DataGrid
                loading={props.loading}
                rows={rows}
                columns={columns}
            />
        </div>
    );
}

const mapStateToProps = state => ({
    // salesByProductPrices: state.salesByProductPricesReport.salesByProductPrices
    loading: state.globalLoader.loading
});

const mapActionToProps = {
    // fetchAllSalesByProductPrices: actions.fetchAllSalesByProductPrices,
    useGlobalLoader: globalLoaderActions.useGlobalLoader,
}

export default connect(mapStateToProps, mapActionToProps)(withStyles(styles)(SalesByProductPrices));

// export default connect(mapStateToProps, mapActionToProps)(withStyles(styles)(SalesByProductPrices));