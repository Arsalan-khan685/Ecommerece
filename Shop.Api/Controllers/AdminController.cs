using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Logic.Services;
using Shop.Model.CustomModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IWebHostEnvironment env;
        private readonly IAdminService _adminService;
        private readonly IUserService _userService;

        public AdminController(IAdminService adminService, IUserService userService, IWebHostEnvironment _env)
        {
            this._adminService = adminService;
            this._userService = userService;
            this.env = _env;
        }

        [HttpPost]
        [Route("AdminLogin")]
        public IActionResult AdminLogin(LogInModel loginModel)
        {
            var data = _adminService.AdminLogin(loginModel);
            return Ok(data);
        }
        [HttpPost]
        [Route("SaveCategory")]
        public IActionResult SaveCategory(CategoryModel categoryModel)
        {
            var data = _adminService.SaveCategory(categoryModel);
            return Ok(data);
        }
        [HttpGet]
        [Route("GetCategories")]
        public IActionResult GetCategories()
        {
            var data = _adminService.GetCategories();
            return Ok(data);
        }
        [HttpPost]
        [Route("UpdateCategories")]
        public IActionResult UpdateCategories(CategoryModel categoryToUpdate)
        {
            var data = _adminService.UpdateCategories(categoryToUpdate);
            return Ok(data);
        }
        [HttpPost]
        [Route("DeleteCategories")]
        public IActionResult DeleteCategories(CategoryModel categoryToDelete)
        {
            var data = _adminService.DeleteCategories(categoryToDelete);
            return Ok(data);
        }
        [HttpGet]
        [Route("GetProducts")]
        public IActionResult GetProducts()
        {
            var data = _adminService.GetProducts();
            return Ok(data);
        }
        [HttpPost]
        [Route("SaveProduct")]
        public IActionResult SaveProduct(ProductModel productModel)
        {
            int nextProductId = _adminService.GetNewProductId();
            productModel.ImageUrl = @"Images/" + nextProductId + ".png";
            var path = $"{env.WebRootPath}\\Images\\{nextProductId + ".png"}";
            var fs = System.IO.File.Create(path);
            fs.Write(productModel.FileContent, 0, productModel.FileContent.Length);
            fs.Close();
            string uploadsFolder = Path.Combine(env.WebRootPath,"Images");
            var data = _adminService.SaveProduct(productModel);
            return Ok(data);
        }
        [HttpPost]
        [Route("DeleteProduct")]
        public IActionResult DeleteProduct(ProductModel productToDelete)
        {
            var data = _adminService.DeleteProduct(productToDelete);
            return Ok(data);
        }
        [HttpGet]
        [Route("GetStock")]
        public IActionResult GetStock()
        {
            var data = _adminService.GetStock();
            return Ok(data);
        }
        [HttpPost]
        [Route("UpdateStock")]
        public IActionResult UpdateStock(StockModel _stk)
        {
            var data = _adminService.UpdateStock(_stk);
            return Ok(data);
        }
        [HttpGet]
        [Route("GetAllOrders")]
        public IActionResult GetAllOrders()
        {
            var data = _adminService.GetAllOrders();
            return Ok(data);
        }

        [HttpGet]
        [Route("GetOrderDetailForCustomer")]
        public IActionResult GetOrderDetailForCustomer(string orderNo)
        {
            var data = _adminService.GetOrderDetailForCustomer(orderNo);
            return Ok(data);
        }
    }
} 
