export const apiActions = {

    //Consultants
    getAllConsultants: "consultants/GetAll",
    getConsultatn: "consultants/GetConsultant/",
    createConsultant: "consultants/CreateConsultant",
    updateConsultant: "consultants/UpdateConsultant/",
    deleteConsultant: "consultants/DeleteConsultant/",

    //Products
    getAllProducts: "products/GetAll",
    getProduct: "products/GetProduct/",
    createProduct: "products/CreateProduct",
    updateProduct: "products/UpdateProduct/",
    deleteProduct: "products/DeleteProduct/",

    //Sales
    getAllSales: "sales/GetAll",
    getSale: "sales/GetSaleProduct/",
    createSale: "sales/CreateSale",
    updateSale: "sales/UpdateSale/",
    deleteSale: "sales/DeleteSale/",

    //Reports
    getSalesByConsultants: "reports/GetSalesByConsultants",
    getSalesByProductPrices: "reports/GetSalesByProductPrices",
    getConsultantsByFrequentlySoldProducts: "reports/GetConsultantsByFrequentlySoldProducts",
    getConsultantsBySumSales: "reports/GetConsultantsBySumSales",
    getConsultantsByMostSoldProducts: "reports/GetConsultantsByMostSoldProducts"
}