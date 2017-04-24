using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using OnlineShopping.Domain.Entities;


namespace OnlineShopping.Domain.Concrete
{
    public class EFDBContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

    }
}
