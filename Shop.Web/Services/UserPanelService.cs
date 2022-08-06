using Microsoft.AspNetCore.Components;
using Shop.Model.CustomModels;
using Shop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
namespace Shop.Web.Services
{
    public class UserPanelService:IUserPanelService
    {
        private readonly HttpClient httpClient;
        private readonly ProtectedSessionStorage sessionStorage;
        public UserPanelService(HttpClient _httpClient, ProtectedSessionStorage _sessionStorage)
        {
            this.httpClient = _httpClient;
            this.sessionStorage = _sessionStorage;
            
        }
        public async Task<bool> IsUserLoggedIn()
        {
            var flag = false;
            var result = await sessionStorage.GetAsync<string>("userKey");
            if (result.Success)
            {
                flag = true;
            }
            return flag;
        }
        public async Task<List<CategoryModel>> GetCategories()
        {
            return await httpClient.GetJsonAsync<List<CategoryModel>>("api/user/GetCategories");
        }
        public async Task<List<ProductModel>> GetProductByCategoryId(int categoryId)
        {
            return await httpClient.GetJsonAsync<List<ProductModel>>("api/user/GetProductByCategoryId/?categoryId=" + categoryId);
        }
        public async Task<ResponceModel> RegisterUser(RegisterModel registerModel)
        {
            return await httpClient.PostJsonAsync<ResponceModel>("api/user/RegisterUser", registerModel);
        }
        public async Task<ResponceModel> LoginUser(LogInModel loginModel)
        {
            return await httpClient.PostJsonAsync<ResponceModel>("api/user/LoginUser", loginModel);
        }
        public async Task<ResponceModel> CheckOut(List<CartModel>  myCart)
        {
            return await httpClient.PostJsonAsync<ResponceModel>("api/user/CheckOut", myCart);
        }
        public async Task<List<CustomerOrder>> GetOrderByCustomerId(int customerId)
        {
            return await httpClient.GetJsonAsync<List<CustomerOrder>>("api/user/GetOrderByCustomerId/?customerId=" + customerId);
        }
        public async Task<List<CartModel>> GetOrderDetailForCustomer(int userkey,string orderno)
        {
            return await httpClient.GetJsonAsync<List<CartModel>>("api/user/GetOrderDetailForCustomer/?userkey=" + userkey + "&orderno=" + orderno);
        }
        public async Task<List<string>> GetShippingStatusForOrder(string orderno)
        {
            return await httpClient.GetJsonAsync<List<string>>("api/user/GetShippingStatusForOrder/?orderno=" + orderno);
        }
        public async Task<ResponceModel> ChangePassword(PasswordModel password)
        {
            return await httpClient.PostJsonAsync<ResponceModel>("api/user/ChangePassword/", password);
        }

    }
}
