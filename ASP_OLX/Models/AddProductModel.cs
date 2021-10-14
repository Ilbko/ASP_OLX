using DapperDll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_OLX.Models
{
    public class AddProductModel
    {
        public List<Category> Categories { get; set; }

        public AddProductModel()
        {
            Categories = CategorySingleton.Repository.Select();
        }
    }
}
