using Shop.Model.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;
using Shop.Model.Models;


namespace Shop.Admin.Services
{
    public class AdminPanelService : IAdminPanelService
    {
        private readonly HttpClient httpClient;
        public AdminPanelService(HttpClient _httpClient)
        {
            this.httpClient = _httpClient;
        }
        public async Task<ResponceModel> AdminLogin(LogInModel loginModel)
        {
            return await httpClient.PostJsonAsync<ResponceModel>("api/admin/AdminLogin", loginModel);
        }
        public async Task<CategoryModel> SaveCategory(CategoryModel categoryModel)
        {
            return await httpClient.PostJsonAsync<CategoryModel>("api/admin/SaveCategory", categoryModel);
        }
        public async Task<List<CategoryModel>> GetCategories()
        {
            return await httpClient.GetJsonAsync<List<CategoryModel>>("api/admin/GetCategories");
        }
        public async Task<bool> UpdateCategories(CategoryModel categoryToUpdate)
        {
            return await httpClient.PostJsonAsync<bool>("api/admin/UpdateCategories", categoryToUpdate);
        }
        public async Task<bool> DeleteCategories(CategoryModel categoryToDelete)
        {
            return await httpClient.PostJsonAsync<bool>("api/admin/DeleteCategories", categoryToDelete);
        }
        public async Task<List<ProductModel>> GetProducts()
        {
            return await httpClient.GetJsonAsync<List<ProductModel>>("api/admin/GetProducts");
        }
        public async Task<ProductModel> SaveProduct(ProductModel productModel)
        {
            return await httpClient.PostJsonAsync<ProductModel>("api/admin/SaveProduct", productModel);
        }
        public async Task<bool> DeleteProduct(ProductModel productToDelete)
        {
            return await httpClient.PostJsonAsync<bool>("api/admin/DeleteProduct", productToDelete);
        }
        public async Task<List<StockModel>> GetStock()
        {
            return await httpClient.GetJsonAsync<List<StockModel>>("api/admin/GetStock");
        }
        public async Task<bool> UpdateStock(StockModel stockToUpdate)
        {
            return await httpClient.PostJsonAsync<bool>("api/admin/UpdateStock", stockToUpdate);
        }
        public async Task<List<CustomerOrder>> GetAllOrders()
        {
            return await httpClient.GetJsonAsync<List<CustomerOrder>>("api/admin/GetAllOrders");
        }
        public async Task<List<CartModel>> GetOrderDetailForCustomer(string orderNo)
        {
            return await httpClient.GetJsonAsync<List<CartModel>>("api/admin/GetOrderDetailForCustomer/?orderNo=" + orderNo);
        }
    }
}
