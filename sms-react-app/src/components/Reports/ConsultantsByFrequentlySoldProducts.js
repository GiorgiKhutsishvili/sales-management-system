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

const ConsultantsByFrequentlySoldProducts = ({ classes, ...props }) => {
    const columns = [
        { field: "index", headerName: "#", width: 100 },
        { field: "consultantUniqueNumber", headerName: "კონსულტანტის უნიკალური ნომერი", width: 200 },
        { field: "consultantFullName", headerName: " სახელი და გვარი", width: 200 },
        { field: "consultantPersonalId", headerName: "პირადი ნომერი", width: 200 },
        { field: "consultantBirthDate", headerName: "დაბადების თარიღი", width: 200 },
        { field: "soldProductCode", headerName: "პროდუქტის კოდი", width: 200 },
        { field: "soldProductMinQuantity", headerName: "პროდუქტის რაოდენობა", width: 200 },
    ];

    const [rows, setRows] = useState([]);

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
        soldProductCode: null,
        soldProductMinQuantity: null
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

    const filterObj = {
        startDate: filter.startDate,
        endDate: filter.endDate,
        soldProductCode: filterValues.soldProductCode,
        soldProductMinQuantity: filterValues.soldProductMinQuantity
    }

    useEffect(async () => {
        await loadData();
    }, [
        filter.startDate,
        filter.endDate,
        filterObj.soldProductCode,
        filterObj.soldProductMinQuantity
    ])

    const loadData = async () => {
        props.useGlobalLoader(true);
        await axios.get(api.baseUrl + apiActions.getConsultantsByFrequentlySoldProducts, {
            params: {
                startDate: filter.startDate,
                endDate: filter.endDate,
                soldProductCode: filterObj.soldProductCode,
                soldProductMinQuantity: filterObj.soldProductMinQuantity
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
                        name="soldProductCode"
                        variant="outlined"
                        label="პროდუქციის კოდი"
                        value={filterValues.soldProductCode}
                        onChange={handleInputChange}
                    />
                    <TextField
                        name="soldProductMinQuantity"
                        variant="outlined"
                        label="გაყიდული პროდუქციის მინიმალური რაოდენობა"
                        value={filterValues.soldProductMinQuantity}
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
    // consultantsByFrequentlySoldProducts: state.consultantsByFrequentlySoldProductsReport.consultantsByFrequentlySoldProducts
    loading: state.globalLoader.loading
});

const mapActionToProps = {
    // fetchAllConsultantsByFrequentlySoldProducts: actions.fetchAllConsultantsByFrequentlySoldProducts,
    useGlobalLoader: globalLoaderActions.useGlobalLoader,
}

export default connect(mapStateToProps, mapActionToProps)(withStyles(styles)(ConsultantsByFrequentlySoldProducts));