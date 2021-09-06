import React, { useState, useEffect } from "react";
import { connect } from 'react-redux';
import * as productActions from '../../store/actions/product';
import * as globalLoaderActions from '../../store/actions/globalLoader';
import { ButtonGroup } from "@material-ui/core";
import EditIcon from "@material-ui/icons/Edit";
import DeleteIcon from "@material-ui/icons/Delete";
import { Button } from "@material-ui/core";
import { useToasts } from "react-toast-notifications";

import PropTypes from 'prop-types';
import IconButton from '@material-ui/core/IconButton';
import TextField from '@material-ui/core/TextField';
import {
    DataGrid,
    GridToolbarDensitySelector,
    GridToolbarFilterButton,
} from '@material-ui/data-grid';
import ClearIcon from '@material-ui/icons/Clear';
import SearchIcon from '@material-ui/icons/Search';
import { createTheme } from '@material-ui/core/styles';
import { makeStyles } from '@material-ui/styles';
import Fab from '@material-ui/core/Fab';
import AddIcon from '@material-ui/icons/Add';
import { useHistory } from "react-router-dom";
import { apiActions } from "../../store/actions/apiActions";
import * as api from "../../utils/baseUrl";
import axios from "axios";

function escapeRegExp(value) {
    return value.replace(/[-[\]{}()*+?.,\\^$|#\s]/g, '\\$&');
}

const defaultTheme = createTheme();
const useStyles = makeStyles(
    (theme) => ({
        root: {
            padding: theme.spacing(0.5, 0.5, 0),
            justifyContent: 'space-between',
            display: 'flex',
            alignItems: 'flex-start',
            flexWrap: 'wrap',
        },
        textField: {
            [theme.breakpoints.down('xs')]: {
                width: '100%',
            },
            margin: theme.spacing(1, 0.5, 1.5),
            '& .MuiSvgIcon-root': {
                marginRight: theme.spacing(0.5),
            },
            '& .MuiInput-underline:before': {
                borderBottom: `1px solid ${theme.palette.divider}`,
            },
        },
    }),
    { defaultTheme },
);

function QuickSearchToolbar(props) {
    const classes = useStyles();

    return (
        <div className={classes.root}>
            <div>
                <GridToolbarFilterButton />
                <GridToolbarDensitySelector />
            </div>
            <TextField
                variant="standard"
                value={props.value}
                onChange={props.onChange}
                placeholder="Search…"
                className={classes.textField}
                InputProps={{
                    startAdornment: <SearchIcon fontSize="small" />,
                    endAdornment: (
                        <IconButton
                            title="Clear"
                            aria-label="Clear"
                            size="small"
                            style={{ visibility: props.value ? 'visible' : 'hidden' }}
                            onClick={props.clearSearch}
                        >
                            <ClearIcon fontSize="small" />
                        </IconButton>
                    ),
                }}
            />
        </div>
    );
}

QuickSearchToolbar.propTypes = {
    clearSearch: PropTypes.func.isRequired,
    onChange: PropTypes.func.isRequired,
    value: PropTypes.string.isRequired,
};

const Products = ({ classes, ...props }) => {
    const history = useHistory();

    const { addToast } = useToasts();

    const columns = [
        { field: "index", headerName: "#", width: 100 },
        { field: "code", headerName: "პროდუქციის კოდი", width: 300 },
        { field: "name", headerName: "პროდუქციის დასახელება", width: 300 },
        { field: "price", headerName: "პროდუქციის გასაყიდი ფასი", width: 300 },

        {
            field: "actions",
            headerName: "Actions",
            sortable: false,
            width: 140,
            disableClickEventBubbling: true,
            renderCell: (params) => {
                return (
                    <ButtonGroup variant="text">
                        <Button>
                            <EditIcon
                                onClick={() => goToProductDetails(params.row.id)}
                                color="primary" />
                        </Button>
                        <Button>
                            <DeleteIcon
                                onClick={() => onDelete(params.row.id)}
                                color="secondary" />
                        </Button>
                    </ButtonGroup>
                );
            }
        }
    ];

    const [searchText, setSearchText] = useState('');
    const [rows, setRows] = useState([]);

    const requestSearch = (searchValue) => {
        setSearchText(searchValue);
        const searchRegex = new RegExp(escapeRegExp(searchValue), 'i');
        const filteredRows = rows.filter((row) => {
            return Object.keys(row).some((field) => {
                return searchRegex.test(row[field].toString());
            });
        });
        setRows(filteredRows);
    };

    useEffect(async () => {
        await loadData();
    }, [])

    const loadData = async () => {
        props.useGlobalLoader(true);
        await axios.get(api.baseUrl + apiActions.getAllProducts)
            .then(res => {
                setRows(res.data);
                props.useGlobalLoader(false);
            })
            .catch(err => {
                console.log(err)
            });
    }


    const redirectToForm = () => {
        history.push('/productForm');
    }

    const goToProductDetails = (currentId) => {
        history.push(`/productForm/${currentId}`);
    }

    const onDelete = async id => {
        // if (window.confirm('Are you sure to delete this record?')) {
        //     props.deleteProduct(id, apiActions.deleteProduct, () => addToast("Deleted successfully", { appearance: 'info' }))
        // }
        if (window.confirm('Are you sure to delete this record?')) {
           await axios.delete(api.baseUrl + apiActions.deleteProduct + id)
                .then(res => {
                    if (res) {
                        loadData();
                        if(!props.loading){
                            addToast("Deleted successfully", { appearance: 'info' })
                        }
                    }
                })
                .catch(err => {
                    console.log(err)
                })
        }
    }

    return (
        <div style={{ height: 700, width: '100%' }}>
            <Fab onClick={(redirectToForm)} style={{ marginBottom: 10 }} color="primary" aria-label="add">
                <AddIcon />
            </Fab>
            <DataGrid
                loading={props.loading}
                components={{ Toolbar: QuickSearchToolbar }}
                rows={rows}
                columns={columns}
                componentsProps={{
                    toolbar: {
                        value: searchText,
                        onChange: (event) => requestSearch(event.target.value),
                        clearSearch: () => requestSearch(''),
                    },
                }}
            />
        </div>
    );
}

const mapStateToProps = state => ({
    productsList: state.product.productsList,
    loading: state.globalLoader.loading
});

const mapActionToProps = {
    fetchAllProducts: productActions.fetchAllProducts,
    deleteProduct: productActions.DeleteProduct,
    useGlobalLoader: globalLoaderActions.useGlobalLoader,
}

export default connect(mapStateToProps, mapActionToProps)(Products);