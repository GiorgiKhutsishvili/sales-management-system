import React, { useState, useEffect } from 'react'
import { connect } from 'react-redux';
import * as actions from '../../store/actions/consultant';
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
import {
    MuiPickersUtilsProvider,
    KeyboardDatePicker
} from "@material-ui/pickers";
import DateFnsUtils from "@date-io/date-fns";
import { useToasts } from "react-toast-notifications";
import useForm from "../../utils/useForm";
import { useParams } from "react-router-dom";
import { useHistory } from "react-router-dom";
import { apiActions } from "../../store/actions/apiActions";
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

const genderItems = [
    { id: 'Male', title: 'Male' },
    { id: 'Female', title: 'Female' }
]

const initialFieldValues = {
    id: '',
    personalId: '',
    firstName: '',
    lastName: '',
    gender: '',
    birthDate: new moment().format("YYYY-MM-DD"),
    recommendatorId: null
}

const ConsultatntForm = ({ classes, ...props }) => {
    const history = useHistory();

    const params = useParams();
    const consultantId = params.id;

    const { addToast } = useToasts();

    const validate = (fieldValues = values) => {
        let temp = { ...errors }
        let requiredText = "ველი სავალდებულოა";

        if ('personalId' in fieldValues)
            temp.personalId = fieldValues.personalId ? "" : requiredText;

        if ('firstName' in fieldValues)
            temp.firstName = fieldValues.firstName ? "" : requiredText;

        if ('lastName' in fieldValues)
            temp.lastName = fieldValues.lastName ? "" : requiredText;

        if ('gender' in fieldValues)
            temp.gender = fieldValues.gender ? "" : requiredText;

        if ('birthDate' in fieldValues)
            temp.birthDate = fieldValues.birthDate ? "" : requiredText;

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

    //material-ui select
    const inputLabel = React.useRef(null);
    const [labelWidth, setLabelWidth] = React.useState(0);
    React.useEffect(() => {
        setLabelWidth(inputLabel.current.offsetWidth);
    }, []);

    const handleSubmit = e => {
        e.preventDefault();

        if (validate()) {
            const onSuccess = () => {
                history.push("/");
                resetForm();
                addToast("ოპერაცია წარმატებით განხორციელდა", { appearance: 'success' });
            }
            const onFailure = (message) => {
                addToast(message, { appearance: 'error' });
                if (!consultantId) {
                    resetForm();
                }
            }

            if (!consultantId) {
                props.createConsultant(
                    values,
                    apiActions.createConsultant,
                    onSuccess,
                    onFailure
                );
            }
            else {
                props.updateConsultant(
                    consultantId,
                    apiActions.updateConsultant,
                    values,
                    onSuccess,
                    onFailure
                );
            }
        }
    }

    const handleBirthDateChange = (birthDate) => {
        let formatedBirthDate = moment(birthDate).format("YYYY-MM-DD");
        setValues({ ...values, birthDate: formatedBirthDate });
    };

    const [consultants, setConsultants] = useState([]);

    useEffect(async () => {
        await axios.get(api.baseUrl + apiActions.getAllConsultants)
            .then(res => {
                setConsultants(res.data);
            })
            .catch(err => {
                console.log(err)
            });

        if (consultantId) {
            await axios.get(api.baseUrl + apiActions.getConsultatn + consultantId)
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
    }, [consultantId])

    const goBack = () => {
        history.goBack();
    }

    return (
        <div>
            <form autoComplete="off" noValidate className={classes.root} onSubmit={handleSubmit}>
                <Grid container>
                    <Grid item xs={6}>
                        <TextField
                            name="firstName"
                            variant="outlined"
                            label="სახელი"
                            value={values.firstName}
                            onChange={handleInputChange}
                            {...(errors.firstName && { error: true, helperText: errors.firstName })}
                        />
                        <TextField
                            name="personalId"
                            variant="outlined"
                            label="პირადი ნომერი"
                            value={values.personalId}
                            onChange={handleInputChange}
                            {...(errors.personalId && { error: true, helperText: errors.personalId })}
                        />
                        <FormControl variant="outlined"
                        >
                            <InputLabel ref={inputLabel}>რეკომენდატორი</InputLabel>
                            <MuiSelect
                                name="recommendatorId"
                                value={values.recommendatorId}
                                onChange={handleInputChange}
                                labelWidth={labelWidth}
                            >
                                {
                                    consultants.length === 0 &&
                                    <MenuItem value="">None</MenuItem>
                                }
                                {
                                    consultants.filter(x => x.id !== consultantId).map(
                                        item => (<MenuItem key={item.id} value={item.id}>{item.firstName} {item.lastName}</MenuItem>)
                                    )
                                }
                            </MuiSelect>
                            {errors.gender && <FormHelperText>{errors.gender}</FormHelperText>}
                        </FormControl>
                    </Grid>
                    <Grid item xs={6}>
                        <TextField
                            name="lastName"
                            variant="outlined"
                            label="გვარი"
                            value={values.lastName}
                            onChange={handleInputChange}
                            {...(errors.lastName && { error: true, helperText: errors.lastName })}
                        />
                        <FormControl variant="outlined"
                        >
                            <InputLabel>სქესი</InputLabel>
                            <MuiSelect
                                label="სქესი"
                                name="gender"
                                value={values.gender}
                                onChange={handleInputChange}
                            >
                                <MenuItem value="">None</MenuItem>
                                {
                                    genderItems.map(
                                        item => (<MenuItem key={item.id} value={item.id}>{item.title}</MenuItem>)
                                    )
                                }
                            </MuiSelect>
                            {errors.gender && <FormHelperText>{errors.gender}</FormHelperText>}
                        </FormControl>
                        <MuiPickersUtilsProvider utils={DateFnsUtils}>
                            <KeyboardDatePicker
                                disableToolbar
                                variant="inline"
                                inputVariant="outlined"
                                format="dd.MM.yyyy"
                                margin="normal"
                                label="დაბადების თარიღი"
                                value={values.birthDate}
                                onChange={handleBirthDateChange}
                                KeyboardButtonProps={{
                                    'aria-label': 'change date',
                                }}
                            />
                        </MuiPickersUtilsProvider>

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
                                consultantId ?
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
    createConsultant: actions.create,
    updateConsultant: actions.update
}

export default connect(mapStateToProps, mapActionToProps)(withStyles(styles)(ConsultatntForm));
