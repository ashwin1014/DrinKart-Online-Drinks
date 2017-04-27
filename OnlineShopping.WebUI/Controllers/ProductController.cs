using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShopping.Domain.Abstract;
using OnlineShopping.Domain.Entities;
using OnlineShopping.WebUI.Models;
using System.Web.Mvc.Html;


namespace OnlineShopping.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository repository;
        public int PageSize = 4;

        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult List ( string category, int Page = 1 )
        {

            ProductsListViewModel model = new ProductsListViewModel
            {

                Products = repository.Products
                                     .Where(p => category == null || p.Category == category)
                                     .OrderBy(p => p.ProductId)
                                     .Skip((Page - 1) * PageSize)
                                     .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = Page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null?
                                             repository.Products.Count():
                                             repository.Products.Where(p=> p.Category==category).Count()
                },

                CurrentCategory = category


            };

        return View(model);

        }

        public FileContentResult GetImage ( int productId )
        {
            Product prod = repository.Products
            .FirstOrDefault(p => p.ProductId == productId);
            if (prod != null)
            {
                return File(prod.ImageData, prod.ImageMimeType);
            }
            else
            {
                return null;
            }
        }


    }
    }
