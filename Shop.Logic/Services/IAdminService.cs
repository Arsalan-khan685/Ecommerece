using Shop.Model.CustomModels;
using Shop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace Shop.Logic.Services
{
    public interface IAdminService
    {
        ResponceModel AdminLogin(LogInModel logInModel);
        CategoryModel SaveCategory(CategoryModel categoryModel);
        List<CategoryModel> GetCategories();
        bool UpdateCategories(CategoryModel categoryToUpdate);
        bool DeleteCategories(CategoryModel categoryToDelete);
        List<ProductModel> GetProducts();
        ProductModel SaveProduct(ProductModel _product);
        int GetNewProductId();
        bool DeleteProduct(ProductModel productToDelete);
        List<StockModel> GetStock();
        bool UpdateStock(StockModel _stk);
        List<CustomerOrder> GetAllOrders();
        List<CartModel> GetOrderDetailForCustomer(string orderNo);
    }
}
