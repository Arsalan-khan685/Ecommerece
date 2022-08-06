using Shop.Model.CustomModels;
using Shop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Logic.Services
{
    public class AdminService : IAdminService
    {
        private readonly EcommerceDBContext _dbContext = null;
        public AdminService(EcommerceDBContext appDbContext)
        {
            this._dbContext = appDbContext;
        }
        public ResponceModel AdminLogin(LogInModel logInModel)
        {
         //   var data = _dbContext.AdminInfos.Where(x=>x.Email==logInModel.EmailId && x.Password==logInModel.Password).FirstOrDefault();
            ResponceModel _responceModel = new ResponceModel();

            try
            {
                var userdata = _dbContext.AdminInfos.Where(x => x.Email == logInModel.EmailId ).FirstOrDefault();
                if (userdata != null)
                {
                    if (userdata.Password == logInModel.Password)
                    {
                        _responceModel.Status = true;
                        _responceModel.Message = Convert.ToString(userdata.AdminId) + "|" + userdata.AdminName + "|" + userdata.Email;
                    }
                    else
                    {
                        _responceModel.Status = false;
                        _responceModel.Message = "Your Password is incorrect";
                    }
                }
                else
                {
                    _responceModel.Status = false;
                    _responceModel.Message = "Email Not registered. Please Check your Email ID";     
                }
                return _responceModel;
            }
            catch (Exception ex)
            {
                _responceModel.Status = false;
                _responceModel.Message = "An Error has occured, Please try again.";
                return _responceModel;
            }
        }
        public CategoryModel SaveCategory(CategoryModel categoryModel)
        {
            try
            {
                Category _category = new Category();
                _category.CategoryName = categoryModel.CategoryName;
                _dbContext.Add(_category);
                _dbContext.SaveChanges();
                return categoryModel;
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool UpdateCategories(CategoryModel categoryToUpdate)
        {
            bool flag = false;
            var _cat = _dbContext.Categories.Where(x => x.CategoryId == categoryToUpdate.CategoryID).FirstOrDefault();
            if (_cat != null)
            {
                _cat.CategoryName = categoryToUpdate.CategoryName;
                _dbContext.Categories.Update(_cat);
                _dbContext.SaveChanges();
                flag = true;
            }
            return flag;
        }
        public bool DeleteCategories(CategoryModel categoryToDelete)
        {
            bool flag = false;
            var _cat = _dbContext.Categories.Where(x => x.CategoryId == categoryToDelete.CategoryID).FirstOrDefault();
            if (_cat != null)
            {
                _cat.CategoryName = categoryToDelete.CategoryName;
                _dbContext.Categories.Remove(_cat);
                _dbContext.SaveChanges();
                flag = true;
            }
            return flag;
        }
        public List<CategoryModel> GetCategories()
        {
            var data = _dbContext.Categories.ToList();
            List<CategoryModel> _categoryList = new List<CategoryModel>();
            foreach(var  c in data)
            {
                CategoryModel _ca = new CategoryModel();
                _ca.CategoryID = c.CategoryId;
                _ca.CategoryName = c.CategoryName;
                _categoryList.Add(_ca);
            }
            return _categoryList;
        }
        public List<ProductModel> GetProducts()
        {
            List<Category> categories = _dbContext.Categories.ToList();
            List<Product> products = _dbContext.Products.ToList();
            List<ProductModel> _productList = new List<ProductModel>();
           foreach(var p in products)
            {
                ProductModel _productModel = new ProductModel();
                _productModel.ProductID = p.ProductId;
                _productModel.ProductName = p.ProductName;
                _productModel.Price = (int)p.Price;
                _productModel.Stock = (int)p.Stock;
                _productModel.ImageUrl = p.ImageUrl;
                _productModel.Category_ID = (int)p.CategoryId;
                _productModel.Category_Name = categories.Where(x => x.CategoryId == p.CategoryId).Select(x => x.CategoryName).FirstOrDefault();
                _productList.Add(_productModel);
            }
            return _productList;
        }
        public ProductModel SaveProduct(ProductModel _product)
        {
            try
            {
                Product _prod = new Product();
                _prod.ProductName = _product.ProductName;
                _prod.Price = _product.Price;
                _prod.CategoryId = _product.Category_ID;
                _prod.ImageUrl = _product.ImageUrl;
                _prod.Stock = _product.Stock;
                _dbContext.Add(_prod);
                _dbContext.SaveChanges();
                return _product;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int GetNewProductId()
        {
            try
            {
                int nextProductId = _dbContext.Products.ToList().OrderByDescending(x => x.ProductId).Select(x => x.ProductId).FirstOrDefault();
                return nextProductId;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool DeleteProduct(ProductModel productToDelete)
        {
            bool flag = false;
            var _product = _dbContext.Products.Where(x => x.ProductId == productToDelete.ProductID).FirstOrDefault();
            if (_product != null)
            {
                _dbContext.Products.Remove(_product);
                _dbContext.SaveChanges();
                flag = true;
            }
            return flag;
        }
        public List<StockModel> GetStock()
        {
            List<StockModel> stockList = new List<StockModel>();
            List<Category> categorys = _dbContext.Categories.ToList();
            List<Product> products = _dbContext.Products.ToList();
            foreach(var p in products)
            {
                StockModel _stock = new StockModel();
                _stock.StockID = p.ProductId;
                _stock.ProductName = p.ProductName;
                _stock.Stock = (int)p.Stock;
                _stock.Category_Name = categorys.Where(x => x.CategoryId == p.CategoryId).Select(x => x.CategoryName).FirstOrDefault();
                stockList.Add(_stock);
            }
            return stockList;
        }
        public bool UpdateStock(StockModel _stk)
        {
            bool flag = false;
            var _prod = _dbContext.Products.Where(x => x.ProductId == _stk.StockID).First();
            if(_prod != null)
            {
                _prod.Stock = _stk.Stock + _stk.NewStock;
                _dbContext.Products.Update(_prod);
                _dbContext.SaveChanges();
                flag = true;
            }
            return flag;
        }
        public List<CustomerOrder> GetAllOrders()
        {
            List<CustomerOrder> _listOfCustomerOrders = new List<CustomerOrder>();

            _listOfCustomerOrders = _dbContext.CustomerOrders.ToList();

            return _listOfCustomerOrders;
        }
        public List<CartModel> GetOrderDetailForCustomer(string orderNo)
        {
            List<CartModel> cartItem = new List<CartModel>();

            var customerorder = _dbContext.CustomerOrders.Where(x => x.OrderId == orderNo).ToList();
            //var cut = _dbContext.CustomerOrders.Join(_dbContext.OrderDetails,
            //                                        c => c.OrderId,
            //                                        o => o.OrderId,
            //                                        (c, o) => new
            //                                        {
            //                                            c.OrderId,
            //                                            c.CustomerId,
            //                                            c.PaymentMode,
            //                                            c.ShippingAddress,
            //                                            c.ShippingCharges,
            //                                            c.ShippingStatus,
            //                                            c.Subtotal,
            //                                            c.Total,
            //                                            c.CreatedOn,
            //                                            o.Quantity,
            //                                            o.ProductId
            //                                        }).ToList();

             using(EcommerceDBContext db = new EcommerceDBContext())
            {
                var abc = (from co in _dbContext.CustomerOrders
                           join o in _dbContext.OrderDetails on co.OrderId equals o.OrderId 
                           select new
                           {
                               OrderId = co.OrderId,
                               customerId = co.CustomerId,
                               paymentMode = co.PaymentMode,
                               shippingAddress = co.ShippingAddress,
                               shippingCharges = co.ShippingCharges,
                               shippingStatus = co.ShippingStatus,
                               subTotal = co.Subtotal,
                               Total = co.Total,
                               Quantity = o.Quantity,
                               ProductId = o.ProductId
                           }).Take(5).ToList();
                foreach (var temp in abc)
                {
                    var prod = _dbContext.Products.ToList().Where(x => x.ProductId == temp.ProductId).FirstOrDefault();
                    CartModel cart = new CartModel();
                    cart.ProductName = prod.ProductName;
                    cart.price = (int)prod.Price;
                    cart.ProductImage = prod.ImageUrl;
                    cart.Quantity = (int)temp.Quantity;
                    cart.ShippingCharges = (int)temp.shippingCharges;
                    cart.ShippingAddress = temp.shippingAddress;
                    cart.SubTotal = (int)temp.subTotal;
                    cart.Total = (int)temp.Total;
                    cart.PaymentMode = temp.paymentMode;
                    cartItem.Add(cart);
                } 
            }
            return cartItem;

        }
    }
}
