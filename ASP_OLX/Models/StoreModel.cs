using DapperDll;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_OLX.Models
{
    public class StoreModel
    {
        public UserManager<IdentityUser> userManager { get; set; }
        public SignInManager<IdentityUser> signInManager { get; set; }

        public List<Product> Products { get; set; }
        public List<Image> Images { get; set; }
        public List<Category> Categories { get; set; }

        public StoreModel()
        {
            Products = ProductSingleton.Repository.Select();
            Images = ImageSingleton.Repository.Select();
            Categories = CategorySingleton.Repository.Select();
        }

        public StoreModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;

            Products = ProductSingleton.Repository.Select();
            Images = ImageSingleton.Repository.Select();
            Categories = CategorySingleton.Repository.Select();
        }
    }
}
