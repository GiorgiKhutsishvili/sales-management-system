import './App.css';
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import Consultants from "./components/Consultants/Consultants";
import ConsultatntForm from "./components/Consultants/ConsultatntForm";
import Sales from "./components/Sales/Sales";
import SaleForm from "./components/Sales/SaleForm";
import SalesByConsultants from "./components/Reports/SalesByConsultants";
import SalesByProductPrices from "./components/Reports/SalesByProductPrices";
import ConsultantsByFrequentlySoldProducts from "./components/Reports/ConsultantsByFrequentlySoldProducts";
import ConsultantsBySumSales from "./components/Reports/ConsultantsBySumSales";
import ConsultantsByMostSoldProducts from "./components/Reports/ConsultantsByMostSoldProducts";
import Products from "./components/Products/Products";
import ProductForm from "./components/Products/ProductForm";
import Layout from "./components/Layout/Layout";
import { store } from "./store/actions/store";
import { Provider } from "react-redux";
import { ToastProvider } from "react-toast-notifications";

function App() {
  return (
    <Provider store={store}>
      <ToastProvider autoDismiss={true}>
        <Router>
          <Layout>
            <Switch>
              <Route exact path="/">
                <Consultants />
              </Route>
              <Route path="/consultatntForm/:id">
                <ConsultatntForm />
              </Route>
              <Route path="/consultatntForm">
                <ConsultatntForm />
              </Route>
              <Route path="/products">
                <Products />
              </Route>
              <Route path="/productForm/:id">
                <ProductForm />
              </Route>
              <Route path="/productForm">
                <ProductForm />
              </Route>
              <Route path="/saleForm/:id">
                <SaleForm />
              </Route>
              <Route path="/saleForm">
                <SaleForm />
              </Route>
              <Route path="/sales">
                <Sales />
              </Route>
              <Route path="/salesByConsultants">
                <SalesByConsultants />
              </Route>
              <Route path="/salesByProductPrices">
                <SalesByProductPrices />
              </Route>
              <Route path="/consultantsByFrequentlySoldProducts">
                <ConsultantsByFrequentlySoldProducts />
              </Route>
              <Route path="/consultantsBySumSales">
                <ConsultantsBySumSales />
              </Route>
              <Route path="/consultantsByMostSoldProducts">
                <ConsultantsByMostSoldProducts />
              </Route>
            </Switch>
          </Layout>
        </Router>
      </ToastProvider>
    </Provider>
  );
}

export default App;
