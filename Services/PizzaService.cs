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
            Debug.WriteLine("🔍 Mencoba mengambil data dari database...");
            
            // Ambil data TANPA sorting untuk menghindari error SQLite decimal
            var pizzas = await _context.Pizzas.ToListAsync(); // Menggunakan .Pizzas
            
            // Sort di client side (LINQ to Objects)
            var sortedPizzas = pizzas.OrderByDescending(p => p.BasePrice).ToList();
            
            Debug.WriteLine($"✅ Berhasil mengambil {sortedPizzas.Count} pizza dari database");
            foreach (var pizza in sortedPizzas)
            {
                Debug.WriteLine($"   - {pizza.Name} (${pizza.BasePrice})");
            }
            
            return sortedPizzas;
        }

        // Method sync untuk backward compatibility
        public List<Pizza> GetPizzas()
        {
            Debug.WriteLine("🔍 Mencoba mengambil data dari database (sync)...");
            
            // Ambil data TANPA sorting dulu
            var pizzas = _context.Pizzas.ToList(); // Menggunakan .Pizzas
            
            // Sort di client side
            var sortedPizzas = pizzas.OrderByDescending(p => p.BasePrice).ToList();
            
            Debug.WriteLine($"✅ Berhasil mengambil {sortedPizzas.Count} pizza dari database");
            return sortedPizzas;
        }

        private List<Pizza> GetStaticPizzas()
        {
            Debug.WriteLine("⚠️  Menggunakan GetStaticPizzas() - DATA FALLBACK");
            // Mengembalikan list kosong untuk memastikan data hanya dari database
            return new List<Pizza>();
        }
    }
}
