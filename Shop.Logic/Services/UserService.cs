using Shop.Model.CustomModels;
using Shop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Logic.Services
{
    public class UserService:IUserService
    {
        private readonly EcommerceDBContext _dbContext = null;
        public UserService(EcommerceDBContext appDbContext)
        {
            this._dbContext = appDbContext;
        }
        public List<CategoryModel> GetCategories()
        {
            var data = _dbContext.Categories.ToList();
            List<CategoryModel> _categoryList = new List<CategoryModel>();
            foreach(var c in data)
            {
                CategoryModel _ctg = new CategoryModel();
                _ctg.CategoryID = c.CategoryId;
                _ctg.CategoryName = c.CategoryName;
                _categoryList.Add(_ctg);
            }
            return _categoryList;
        }
        public List<ProductModel> GetCategoriesByProductId(int categoryId)
        {
            var data = _dbContext.Products.Where(x => x.CategoryId == categoryId).ToList();
            List<ProductModel> _products = new List<ProductModel>();
            foreach(var p in data)
            {
                ProductModel _prod = new ProductModel();
                _prod.ProductID = p.ProductId;
                _prod.ProductName = p.ProductName;
                _prod.Price = (int)p.Price;
                _prod.Stock = (int)p.Stock;
                _prod.ImageUrl = p.ImageUrl;
                _products.Add(_prod);
            }
            return _products;
        }
        public ResponceModel RegisterUser(RegisterModel registerModel)
        {
            ResponceModel responce = new ResponceModel();
            try
            {
                var isexist = _dbContext.Customers.Where(x => x.Email == registerModel.EmailID).Any();
                if (!isexist)
                {
                    Customer _customer = new Customer();
                    _customer.CustomerName = registerModel.Name;
                    _customer.Email = registerModel.EmailID;
                    _customer.MobileNo = registerModel.MobileNo;
                    _customer.Password = registerModel.Password;
                    _dbContext.Add(_customer);
                    _dbContext.SaveChanges();

                    LogInModel loginModel = new LogInModel()
                    {
                        EmailId = registerModel.EmailID,
                        Password = registerModel.Password
                    };
                    responce = LoginUser(loginModel);
                }
                else
                {
                    responce.Status = false;
                    responce.Message = "Email already registered";
                }
                return responce;
            }
            catch (Exception ex)
            {
                responce.Status = false;
                responce.Message = "An Error has occured. Please try again !";
                return responce;
            }
        }
        public ResponceModel LoginUser(LogInModel loginModel)
        {
            ResponceModel responce = new ResponceModel();
            try
            {
                var userData = _dbContext.Customers.Where(x => x.Email == loginModel.EmailId).FirstOrDefault();
                if (userData!=null)
                {
                    if(userData.Password == loginModel.Password)
                    {
                        responce.Status = true;
                        responce.Message = Convert.ToString(userData.CustomerId) + "|" + userData.CustomerName + "|" + userData.Email;
                    }
                    else
                    {
                        responce.Status = false;
                        responce.Message = "InCorrect Password";
                    }
                }
                else
                {
                    responce.Status = false;
                    responce.Message = "Email Not registered. Please check your email ID";
                } 
                return responce;
            }
            catch (Exception ex)
            {
                responce.Status = false;
                responce.Message = "An Error occured. Please trt again later";
                return responce; 
            }
        }
        public ResponceModel CheckOut(List<CartModel> cartItems)
        {
            string OrderID = GenerateOrderID();
            var products = _dbContext.Products.ToList();
            try
            {
                var details = cartItems.FirstOrDefault();
                CustomerOrder _customers = new CustomerOrder();
                _customers.CustomerId = details.UserID;
                _customers.OrderId = OrderID;
                _customers.PaymentMode = details.PaymentMode;
                _customers.ShippingAddress = details.ShippingAddress;
                _customers.ShippingCharges = details.ShippingCharges;
                _customers.Subtotal = details.SubTotal;
                _customers.Total = details.ShippingCharges + details.SubTotal;
                _customers.CreatedOn = DateTime.Now;
                _customers.UpdatedOn = DateTime.Now;

                _dbContext.CustomerOrders.Add(_customers);

                foreach(var item in cartItems)
                {
                    OrderDetail _orderDetail = new OrderDetail();
                    _orderDetail.OrderId = OrderID;
                    _orderDetail.ProductId = item.ProductID;
                    _orderDetail.Price = item.price;
                    _orderDetail.Quantity = item.Quantity;
                    _orderDetail.SubTotal = item.price * item.Quantity;
                    _orderDetail.CreatedOn = DateTime.Now;
                    _orderDetail.UpdatedOn = DateTime.Now;
                    _dbContext.OrderDetails.Add(_orderDetail);

                    var selectd_product = products.Where(x => x.ProductId == item.ProductID).FirstOrDefault();
                    selectd_product.Stock = selectd_product.Stock - item.Quantity;
                    _dbContext.Products.Update(selectd_product);
                }
                _dbContext.SaveChanges();
                ResponceModel responce = new ResponceModel();
                responce.Message = OrderID;
                return responce;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private string GenerateOrderID()
        {
            string OrderID = string.Empty;
            Random generator = null;
            for (int i = 0; i < 1000; i++)
            {
                generator = new Random();
                OrderID = "P" + generator.Next(1, 10000000).ToString("D8");
                if (!_dbContext.CustomerOrders.Where(x => x.OrderId == OrderID).Any())
                {
                    break;
                }
            }
            return OrderID;
        }
        public List<CustomerOrder> GetOrderByCustomerId(int customerId)
        {
            var _customerOrders = _dbContext.CustomerOrders.Where(x => x.CustomerId == customerId).OrderByDescending(x=>x.CustomerOrderId).ToList();
            return _customerOrders;
        }
        public List<CartModel> GetOrderDetailForCustomer(int userkey,string orderno)
        {
            List<CartModel> cartitems = new List<CartModel>();
            var customerOrders = _dbContext.CustomerOrders.Where(x => x.CustomerId == userkey && x.OrderId == orderno).FirstOrDefault();
            if (customerOrders != null)
            {
                var orderDetails = _dbContext.OrderDetails.Where(x => x.OrderId == orderno).ToList();
                var products = _dbContext.Products.ToList();
                foreach(var order in orderDetails)
                {
                    var prod = products.Where(x => x.ProductId == order.ProductId).FirstOrDefault();
                    CartModel cartModel = new CartModel();
                    cartModel.ProductName = prod.ProductName;
                    cartModel.price = (int)prod.Price;
                    cartModel.ProductImage = prod.ImageUrl;
                    cartModel.Quantity = Convert.ToInt32(order.Quantity);
                    cartitems.Add(cartModel);
                }
                cartitems.FirstOrDefault().ShippingAddress = customerOrders.ShippingAddress;
                cartitems.FirstOrDefault().ShippingCharges = (int)customerOrders.ShippingCharges;
                cartitems.FirstOrDefault().SubTotal = (int)customerOrders.Subtotal;
                cartitems.FirstOrDefault().Total = (int)customerOrders.Total;
                cartitems.FirstOrDefault().PaymentMode = customerOrders.PaymentMode;
            }
            return cartitems;
        }
        public ResponceModel ChangePassword(PasswordModel password)
        {
            ResponceModel responce = new ResponceModel();
            responce.Status = true;
            var customers = _dbContext.Customers.Where(x => x.CustomerId == password.userKey).FirstOrDefault();
            if (customers != null)
            {
                customers.Password = password.NewPassword;
                _dbContext.Customers.Update(customers);
                _dbContext.SaveChanges();

                responce.Message = "Password Updated Succesfully";
            }
            else
            {
                responce.Message = "User Does not exist. Try Again!";
            }
            return responce;
        }
        public List<string> GetShippingStatusForOrder(string orderno)
        {
            List<string> shippingStatus = new List<string>();
            var order = _dbContext.CustomerOrders.Where(x => x.OrderId == orderno).FirstOrDefault();
            if(order!=null && order.ShippingStatus != null)
            {
                shippingStatus = order.ShippingStatus.Split("|").ToList();
            }
            return shippingStatus;
        }
    }
}
