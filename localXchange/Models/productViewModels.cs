using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace localXchange.Models
{
    public class productViewModels
    {
    }
    public class viewProductViewModel
    {
        public ProductModel thisProduct { get; set; }
    }
    public class pagedProductsViewModel
    {
        public List<ProductModel> productPack { get; set; }
    }
}