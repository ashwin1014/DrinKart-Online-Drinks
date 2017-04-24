using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineShopping.Domain.Entities;
using System.Web.Mvc.Html;


namespace OnlineShopping.WebUI.Models
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}