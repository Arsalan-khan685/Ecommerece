using Shop.Model.CustomModels;
using Shop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Logic.Services
{
    public interface IUserService
    {
        List<CategoryModel> GetCategories();
        List<ProductModel> GetCategoriesByProductId(int categoryId);
        ResponceModel RegisterUser(RegisterModel registerModel);
        ResponceModel LoginUser(LogInModel loginModel);
        ResponceModel CheckOut(List<CartModel> cartItems);
        List<CustomerOrder> GetOrderByCustomerId(int customerId);
        List<CartModel> GetOrderDetailForCustomer(int userkey, string orderno);
        ResponceModel ChangePassword(PasswordModel password);
        List<string> GetShippingStatusForOrder(string orderno);
    }
}
