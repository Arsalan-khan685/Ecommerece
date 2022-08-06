using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Logic.Services;
using Shop.Model.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;   
        }
        [HttpGet]
        [Route("GetCategories")]
        public IActionResult GetCategories()
        {
            var data = _userService.GetCategories();
            return Ok(data);
        }
        [HttpGet]
        [Route("GetProductByCategoryId")]
        public IActionResult GetProductByCategoryId(int categoryId)
        {
            var data = _userService.GetCategoriesByProductId(categoryId);
            return Ok(data);
        }
        [HttpPost]
        [Route("RegisterUser")]
        public IActionResult RegisterUser(RegisterModel registerModel)
        {
            var data = _userService.RegisterUser(registerModel);
            return Ok(data);
        }
        [HttpPost]
        [Route("LoginUser")]
        public IActionResult LoginUser(LogInModel loginModel)
        {
            var data = _userService.LoginUser(loginModel);
            return Ok(data);
        }
        [HttpPost]
        [Route("CheckOut")]
        public IActionResult CheckOut(List<CartModel> cartItems)
        {
            var data = _userService.CheckOut(cartItems);
            return Ok(data);
        }
        [HttpGet]
        [Route("GetOrderByCustomerId")]
        public IActionResult GetOrderByCustomerId(int customerId)
        {
            var data = _userService.GetOrderByCustomerId(customerId);
            return Ok(data);
        }
        [HttpGet]
        [Route("GetOrderDetailForCustomer")]
        public IActionResult GetOrderDetailForCustomer(int userkey,string orderno)
        {
            var data = _userService.GetOrderDetailForCustomer(userkey, orderno);
            return Ok(data);
        }
        [HttpGet]
        [Route("GetShippingStatusForOrder")]
        public IActionResult GetShippingStatusForOrder(string orderno)
        {
            var data = _userService.GetShippingStatusForOrder(orderno);
            return Ok(data);
        }
        [HttpPost]
        [Route("ChangePassword")]
        public IActionResult ChangePassword(PasswordModel password)
        {
            var data = _userService.ChangePassword(password);
            return Ok(data);
        }
    }
}
