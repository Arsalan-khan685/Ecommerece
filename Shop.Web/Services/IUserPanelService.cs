using Shop.Model.CustomModels;
using Shop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Services
{
    public interface IUserPanelService
    {
        Task<bool> IsUserLoggedIn();
        Task<List<CategoryModel>> GetCategories();
        Task<List<ProductModel>> GetProductByCategoryId(int categoryId);
        Task<ResponceModel> RegisterUser(RegisterModel registerModel);
        Task<ResponceModel> LoginUser(LogInModel loginModel);
        Task<ResponceModel> CheckOut(List<CartModel> myCart);
        Task<List<CustomerOrder>> GetOrderByCustomerId(int customerId);
        Task<List<string>> GetShippingStatusForOrder(string orderno);
        Task<List<CartModel>> GetOrderDetailForCustomer(int userkey, string orderno);

        Task<ResponceModel> ChangePassword(PasswordModel password);
    }
}
