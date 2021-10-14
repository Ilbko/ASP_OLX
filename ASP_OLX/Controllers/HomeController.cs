using ASP_OLX.Models;
using DapperDll;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_OLX.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public HomeController(IConfiguration configuration, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            ProductSingleton.Init(_configuration.GetConnectionString("DefaultConnection"));
            ImageSingleton.Init(_configuration.GetConnectionString("DefaultConnection"));
            CategorySingleton.Init(_configuration.GetConnectionString("DefaultConnection"));
        }
        //public HomeController(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //    ProductSingleton.Init(_configuration.GetConnectionString("DefaultConnection"));
        //    ImageSingleton.Init(_configuration.GetConnectionString("DefaultConnection"));
        //    CategorySingleton.Init(_configuration.GetConnectionString("DefaultConnection"));
        //    //Aa_123123
        //}

        public IActionResult Store()
        {
            //return View(new StoreModel());
            return View(new StoreModel(_userManager, _signInManager));
        }

        public IActionResult AddProduct()
        {
            if (_signInManager.IsSignedIn(User))
                return View(new AddProductModel());
            else
                return Redirect("/Identity/Account/Login/");
        }

        [HttpPost]
        public IActionResult Add(IFormCollection collection)
        {
            Product product = new Product()
            {
                Product_Name = collection["Name"].ToString(),
                Product_Description = collection["Description"].ToString(),
                Product_Price = decimal.Parse(collection["Price"].ToString()),
                Product_CategoryId = int.Parse(collection["Category"].ToString()),
                Product_UserId = _userManager.GetUserId(User).ToString()
            };
            int productId = ProductSingleton.Repository.Insert(product);

            List<string> URLs = collection["URL"].ToString().Split(" ").ToList();

            foreach(string item in URLs)
            {
                ImageSingleton.Repository.Insert(new Image() { Image_URL = item, Image_ProductId = productId });
            }

            return Redirect("/Home/Store/");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
