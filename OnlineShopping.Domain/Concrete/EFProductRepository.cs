using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShopping.Domain.Abstract;
using OnlineShopping.Domain.Entities;

namespace OnlineShopping.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private readonly EFDBContext context = new EFDBContext();

        public IEnumerable<Product> Products
        {
            get { return context.Products; }
        }
    }
}
