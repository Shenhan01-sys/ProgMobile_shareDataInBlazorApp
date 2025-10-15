using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PizzaList.Models;
using PizzaList.Data;
using System.Diagnostics;

namespace PizzaList.Data
{
    public class SeedPizza
    {
        public static void Initialize(PizzaStoreContext db)
        {
            try
            {
                Debug.WriteLine("🌱 Memulai seeding data pizza...");
                
                // Clear existing data jika ada
                if (db.SetPizza.Any())
                {
                    Debug.WriteLine("🗑️  Menghapus data pizza yang ada...");
                    db.SetPizza.RemoveRange(db.SetPizza);
                    db.SaveChanges();
                }
                
                var specials = new Pizza[]
                {
                    new Pizza
                    {
                        PizzaId = 1,
                        Name = "Basic Cheese Pizza",
                        Description = "It's cheesy and delicious. Why wouldn't you want one?",
                        BasePrice = 9.99m,
                        ImageUrl = "https://via.placeholder.com/300x200?text=Cheese+Pizza",
                    },
                    new Pizza
                    {
                        PizzaId = 2,
                        Name = "The Baconatorizor",
                        Description = "It has EVERY kind of bacon",
                        BasePrice = 11.99m,
                        ImageUrl = "https://via.placeholder.com/300x200?text=Bacon+Pizza",
                    },
                    new Pizza
                    {
                        PizzaId = 3,
                        Name = "Classic pepperoni",
                        Description = "It's the pizza you grew up with, but Blazing hot!",
                        BasePrice = 10.50m,
                        ImageUrl = "https://via.placeholder.com/300x200?text=Pepperoni+Pizza",
                    },
                    new Pizza
                    {
                        PizzaId = 4,
                        Name = "Buffalo chicken",
                        Description = "Spicy chicken, hot sauce and bleu cheese, guaranteed to warm you up",
                        BasePrice = 12.75m,
                        ImageUrl = "https://via.placeholder.com/300x200?text=Buffalo+Chicken",
                    },
                    new Pizza
                    {
                        PizzaId = 5,
                        Name = "Mushroom Lovers",
                        Description = "It has mushrooms. Isn't that obvious?",
                        BasePrice = 11.00m,
                        ImageUrl = "https://via.placeholder.com/300x200?text=Mushroom+Pizza",
                    },
                    new Pizza
                    {
                        PizzaId = 7,
                        Name = "Veggie Delight",
                        Description = "It's like salad, but on a pizza",
                        BasePrice = 11.50m,
                        ImageUrl = "https://via.placeholder.com/300x200?text=Veggie+Pizza",
                    },
                    new Pizza
                    {
                        PizzaId = 8,
                        Name = "Margherita",
                        Description = "Traditional Italian pizza with tomatoes and basil",
                        BasePrice = 9.99m,
                        ImageUrl = "https://via.placeholder.com/300x200?text=Margherita",
                    },
                };
                
                Debug.WriteLine($"➕ Menambahkan {specials.Length} pizza ke database...");
                db.SetPizza.AddRange(specials);
                
                int changes = db.SaveChanges();
                Debug.WriteLine($"✅ Seeding berhasil! {changes} record ditambahkan.");
                
                // Verify data
                int totalCount = db.SetPizza.Count();
                Debug.WriteLine($"📊 Total pizza di database: {totalCount}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ ERROR saat seeding: {ex.Message}");
                throw;
            }
        }
    }
}
