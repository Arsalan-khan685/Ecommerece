using Shop.Model.CustomModels;
using Shop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Admin.Services
{
    public interface IAdminPanelService
    {
        Task<ResponceModel> AdminLogin(LogInModel loginModel);
        Task<CategoryModel> SaveCategory(CategoryModel categoryModel);
        Task<List<CategoryModel>> GetCategories();
        Task<bool> UpdateCategories(CategoryModel categoryToUpdate);
        Task<bool> DeleteCategories(CategoryModel categoryToDelete);
        Task<List<ProductModel>> GetProducts();
        Task<ProductModel> SaveProduct(ProductModel productModel);
        Task<bool> DeleteProduct(ProductModel productToDelete);
        Task<List<StockModel>> GetStock();
        Task<bool> UpdateStock(StockModel stockToUpdate);
        Task<List<CustomerOrder>> GetAllOrders();
        Task<List<CartModel>> GetOrderDetailForCustomer(string orderNo);
    }
}
