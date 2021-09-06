import { makeStyles } from "@material-ui/core";
import React from "react";
import Drawer from '@material-ui/core/Drawer';
import List from "@material-ui/core/List";
import ListItem from "@material-ui/core/ListItem";
import ListItemIcon from "@material-ui/core/ListItemIcon";
import ListItemText from "@material-ui/core/ListItemText";
import PeopleIcon from '@material-ui/icons/People';
import SalesIcon from '@material-ui/icons/MonetizationOn';
import { SubjectOutlined } from "@material-ui/icons";
import { useHistory, useLocation } from "react-router-dom";

const drawerWidth = 300;


const useStyles = makeStyles({
    page: {
        background: '#f9f9f9',
        width: '100%',
        padding: 100
    },
    drawer: {
        width: drawerWidth
    },
    drawerPaper: {
        width: drawerWidth
    },
    root: {
        display: 'flex'
    },
    active: {
        background: '#f4f4f4'
    }
});

export default function Layout({ children }) {
    const classes = useStyles();
    const history = useHistory();
    const location = useLocation();

    const menuItems = [
        {
            text: 'კონსულტანტები',
            icon: <PeopleIcon color="primary" />,
            path: '/'
        },
        {
            text: 'გაყიდვები',
            icon: <SalesIcon color="primary" />,
            path: '/sales'
        },
        {
            text: 'პროდუქტები',
            icon: <SubjectOutlined color="primary" />,
            path: '/products'
        },
        {
            text: 'გაყიდვები კონსულტანტების მიხედვით',
            icon: <SubjectOutlined color="primary" />,
            path: '/salesByConsultants'
        },
        {
            text: 'გაყიდვები პროდუქციის ფასების მიხედვით',
            icon: <SubjectOutlined color="primary" />,
            path: '/salesByProductPrices'
        },
        {
            text: 'კონსულტანტები ხშირად გაყიდვადი პროდუქტების მიხედვით',
            icon: <SubjectOutlined color="primary" />,
            path: '/consultantsByFrequentlySoldProducts'
        },
        {
            text: 'კონსულტანტები ჯამური გაყიდვების მიხედვით',
            icon: <SubjectOutlined color="primary" />,
            path: '/consultantsBySumSales'
        },
        {
            text: 'კონსულტანტები ყველაზე გაყიდვადი პროდუქტების მიხედვით',
            icon: <SubjectOutlined color="primary" />,
            path: '/consultantsByMostSoldProducts'
        }
    ]

    return (
        <div className={classes.root}>
            <Drawer
                className={classes.drawer}
                variant="permanent"
                anchor="left"
                classes={{ paper: classes.drawerPaper }}>
                <List>
                    {
                        menuItems.map(item => (
                            <ListItem
                                button
                                key={item.text}
                                onClick={() => history.push(item.path)}
                                className={location.pathname === item.path ? classes.active : null}
                            >
                                <ListItemIcon>{item.icon}</ListItemIcon>
                                <ListItemText primary={item.text} />
                            </ListItem>
                        ))
                    }
                </List>
            </Drawer>
            <div className={classes.page}>
                {children}
            </div>
        </div>
    )
}