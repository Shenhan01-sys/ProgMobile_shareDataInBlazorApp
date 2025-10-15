using Microsoft.Extensions.Logging;
using PizzaList.Services;
using PizzaList.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace PizzaList
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
            
            // Add Entity Framework
            builder.Services.AddDbContext<PizzaStoreContext>(options =>
                options.UseSqlite("Data Source=pizza.db"));
            
            // Add Services
            builder.Services.AddScoped<PizzaService>();
            builder.Services.AddScoped<PizzaSalesState>();
            builder.Services.AddScoped<OrderState>(); // ← Add OrderState service

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();

            // Initialize database
            try
            {
                using (var scope = app.Services.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<PizzaStoreContext>();
                    
                    Debug.WriteLine("📋 Mengecek koneksi database...");
                    
                    bool canConnect = db.Database.CanConnect();
                    Debug.WriteLine($"🔗 Database connection: {(canConnect ? "✅ OK" : "❌ GAGAL")}");
                    
                    bool created = db.Database.EnsureCreated();
                    Debug.WriteLine($"🗄️  Database created: {created}");
                    
                    int existingCount = db.SetPizza.Count();
                    Debug.WriteLine($"📊 Existing pizza count: {existingCount}");
                    
                    if (created || existingCount == 0)
                    {
                        Debug.WriteLine("🌱 Seeding database dengan SeedPizza...");
                        SeedPizza.Initialize(db);
                        
                        int newCount = db.SetPizza.Count();
                        Debug.WriteLine($"✅ Seeding selesai. Total pizza: {newCount}");
                    }
                    else
                    {
                        Debug.WriteLine("ℹ️  Database sudah berisi data, skip seeding");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ Database initialization error: {ex.Message}");
            }

            return app;
        }
    }
}
