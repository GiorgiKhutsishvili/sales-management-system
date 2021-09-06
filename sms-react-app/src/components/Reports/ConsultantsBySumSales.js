import React, { useState, useEffect } from "react";
import { connect } from 'react-redux';
import * as globalLoaderActions from '../../store/actions/globalLoader';
import { DataGrid } from '@material-ui/data-grid';
import { apiActions } from "../../store/actions/apiActions";
import { withStyles } from "@material-ui/core";
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


const ConsultantsBySumSales = ({ classes, ...props }) => {
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

    const columns = [
        { field: "index", headerName: "#", width: 100 },
        { field: "consultantUniqueNumber", headerName: "კონსულტანტის უნიკალური ნომერი", width: 200 },
        { field: "consultantFullName", headerName: "კონსულტანტის სახელი და გვარი", width: 200 },
        { field: "consultantPersonalId", headerName: "კონსულტანტის პირადი ნომერი", width: 200 },
        { field: "consultantBirthDate", headerName: "დაბადების თარიღი", width: 300 },
        { field: "consultantSaleSum", headerName: "საკუთარი გაყიდვების ჯამი", width: 300 },
        { field: "consultantHierarchySaleSum", headerName: "კონსულტანტის გაყიდვების ჯამი", width: 300 },
    ];

    useEffect(async () => {
        await loadData();
    }, [filter.startDate, filter.endDate])

    const loadData = async () => {
        props.useGlobalLoader(true);
        await axios.get(api.baseUrl + apiActions.getConsultantsBySumSales, {
            params: {
                startDate: filter.startDate,
                endDate: filter.endDate
            }
        })
            .then(res => {
                setRows(res.data);
                props.useGlobalLoader(false);
            })
            .catch(err => {
                console.log(err)
            });
    }

    return (
        <div className={classes.root} style={{ height: 700, width: '100%' }}>
            <MuiPickersUtilsProvider utils={DateFnsUtils}>
                <Grid container>
                    <Grid item xs={3}>
                        <KeyboardDatePicker
                            disableToolbar
                            variant="inline"
                            format="dd.MM.yyyy"
                            margin="normal"
                            id="date-picker-inline"
                            label="საწყისი თარიღი"
                            value={filter.startDate}
                            onChange={handleStartDateChange}
                            KeyboardButtonProps={{
                                'aria-label': 'change date',
                            }}
                        />
                    </Grid>
                    <Grid item xs={3}>
                        <KeyboardDatePicker
                            disableToolbar
                            variant="inline"
                            format="dd.MM.yyyy"
                            margin="normal"
                            id="date-picker-inline"
                            label="საბოლოო თარიღი"
                            value={filter.endDate}
                            onChange={handleEndDateChange}
                            KeyboardButtonProps={{
                                'aria-label': 'change date',
                            }}
                        />
                    </Grid>
                </Grid>
            </MuiPickersUtilsProvider>
            <DataGrid
                loading={props.loading}
                rows={rows}
                columns={columns}
            />
        </div>
    );
}

const mapStateToProps = state => ({
    loading: state.globalLoader.loading
});

const mapActionToProps = {
    useGlobalLoader: globalLoaderActions.useGlobalLoader,
}

export default connect(mapStateToProps, mapActionToProps)(withStyles(styles)(ConsultantsBySumSales));