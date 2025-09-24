using PizzaList.Models;
using PizzaList.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace PizzaList.Services
{
    public class PizzaService
    {
        private readonly PizzaStoreContext _context;

        public PizzaService(PizzaStoreContext context)
        {
            _context = context;
        }

        // Method async untuk Entity Framework
        public async Task<List<Pizza>> GetPizzasAsync()
        {
            try
            {
                Debug.WriteLine("🔍 Mencoba mengambil data dari database...");
                
                // Ambil data tanpa sorting dulu (untuk menghindari masalah decimal di SQLite)
                var pizzas = await _context.SetPizza.ToListAsync();
                
                // Sort di client side (LINQ to Objects) setelah data diambil
                var sortedPizzas = pizzas.OrderByDescending(p => p.BasePrice).ToList();
                
                Debug.WriteLine($"✅ Berhasil mengambil {sortedPizzas.Count} pizza dari database");
                foreach (var pizza in sortedPizzas)
                {
                    Debug.WriteLine($"   - {pizza.Name} (${pizza.BasePrice})");
                }
                
                return pizzas;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ Error mengambil data dari database: {ex.Message}");
                Debug.WriteLine("🔄 Menggunakan fallback data...");
                
                // Fallback ke data statis jika database error
                return GetStaticPizzas();
            }
        }

        // Method sync untuk backward compatibility
        public List<Pizza> GetPizzas()
        {
            try
            {
                Debug.WriteLine("🔍 Mencoba mengambil data dari database (sync)...");
                var pizzas = _context.SetPizza
                    .OrderByDescending(p => p.BasePrice)
                    .ToList();
                
                Debug.WriteLine($"✅ Berhasil mengambil {pizzas.Count} pizza dari database");
                return pizzas;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ Error mengambil data dari database: {ex.Message}");
                Debug.WriteLine("🔄 Menggunakan fallback data...");
                return GetStaticPizzas();
            }
        }

        private List<Pizza> GetStaticPizzas()
        {
            Debug.WriteLine("⚠️  Menggunakan GetStaticPizzas() - DATA FALLBACK");
            return new List<Pizza>
            {
                new Pizza { PizzaId = 1, Name = "The Baconatorizor", BasePrice = 11.99M, Description = "It has EVERY kind of bacon", ImageUrl = "https://via.placeholder.com/300x200?text=Bacon+Pizza" },
                new Pizza { PizzaId = 2, Name = "Buffalo chicken", BasePrice = 12.75M, Description = "Spicy chicken, hot sauce, and blue cheese, guaranteed to warm you up", ImageUrl = "https://via.placeholder.com/300x200?text=Buffalo+Chicken" },
                new Pizza { PizzaId = 3, Name = "Veggie Delight", BasePrice = 11.5M, Description = "It's like salad, but on a pizza", ImageUrl = "https://via.placeholder.com/300x200?text=Veggie+Pizza" },
                new Pizza { PizzaId = 4, Name = "Margherita", BasePrice = 9.99M, Description = "Traditional Italian pizza with tomatoes and basil", ImageUrl = "https://via.placeholder.com/300x200?text=Margherita" },
                new Pizza { PizzaId = 5, Name = "Basic Cheese Pizza", BasePrice = 11.99M, Description = "It's cheesy and delicious. Why wouldn't you want one?", ImageUrl = "img/pizzas/cheese.jpg" },
                new Pizza { PizzaId = 6, Name = "Classic pepperoni", BasePrice = 10.5M, Description = "It's the pizza you grew up with, but Blazing hot!", ImageUrl = "https://via.placeholder.com/300x200?text=Pepperoni" }
            };
        }
    }
}
