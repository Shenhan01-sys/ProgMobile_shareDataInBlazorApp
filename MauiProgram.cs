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

            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "pizza.db");
            Debug.WriteLine($"Database path: {dbPath}"); // <-- Tambahkan ini untuk debugging
            builder.Services.AddDbContext<PizzaStoreContext>(options =>
                options.UseSqlite($"Data Source={dbPath}"));

            // Add Services
            builder.Services.AddScoped<PizzaService>();
            builder.Services.AddScoped<PizzaSalesState>();
            builder.Services.AddScoped<OrderState>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();

            // --- START PERBAIKAN MIGRATIONS ---
            try
            {
                using (var scope = app.Services.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<PizzaStoreContext>();
                    
                    db.Database.Migrate();
                    Debug.WriteLine("✅ Database migrations applied.");

                    // Selalu re-seed data untuk memastikan konsistensi selama development
                    Debug.WriteLine("🌱 Seeding database dengan SeedPizza...");
                    SeedPizza.Initialize(db);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ Database initialization/migration error: {ex.Message}");
            }
            // --- END PERBAIKAN MIGRATIONS ---

            return app;
        }
    }
}
