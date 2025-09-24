using Microsoft.EntityFrameworkCore;
using PizzaList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaList.Data
{
    public class PizzaStoreContext : DbContext
    {
        public PizzaStoreContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Pizza> SetPizza { get; set; }
    }
}
