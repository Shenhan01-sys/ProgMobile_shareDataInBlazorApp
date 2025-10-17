using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PizzaList.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PizzaStoreContext>
    {
        public PizzaStoreContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PizzaStoreContext>();
            
            // Menggunakan connection string dummy. EF Tools hanya butuh ini untuk
            // menganalisis model dan membuat migration, bukan untuk mengakses database.
            optionsBuilder.UseSqlite("Data Source=design_time.db");

            return new PizzaStoreContext(optionsBuilder.Options);
        }
    }
}
