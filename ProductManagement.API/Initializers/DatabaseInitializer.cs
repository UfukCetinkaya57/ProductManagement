using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Persistence.Contexts;

namespace ProductManagement.API.Initializers
{
    public static class DatabaseInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ProductManagementDbContext>();
                    context.Database.EnsureCreated(); // veya context.Database.Migrate();
                    Console.WriteLine("Database creation or migration was successful.");

                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    string[] roles = { "USER", "ADMIN" }; // Gerekli rolleri tanımlayın

                    foreach (var role in roles)
                    {
                        if (!await roleManager.RoleExistsAsync(role))
                        {
                            await roleManager.CreateAsync(new IdentityRole(role));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Database creation or role initialization failed: {ex.Message}");
                    // Loglama veya hata işleme
                }
            }
        }
    }
}
