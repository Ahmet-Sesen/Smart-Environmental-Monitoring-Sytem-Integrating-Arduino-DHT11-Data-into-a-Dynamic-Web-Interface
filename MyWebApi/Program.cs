using MyWebApi.Models;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Configuration
        builder.Configuration.AddJsonFile("appsettings.json");

        // Veritabanı bağlantısı burada eklenmeli
        builder.Services.AddDbContext<MyDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        var app = builder.Build();

        // Veritabanı işlemlerini ekleyin
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var dbContext = services.GetRequiredService<MyDbContext>();
                dbContext.Database.EnsureCreated(); // Veritabanını oluşturur (varsa hiçbir şey yapmaz)
            }
            catch (Exception ex)
            {
                Console.WriteLine("Veritabanı hatası: " + ex.Message);
            }
        }

        var env = app.Environment; // env değişkenini tanımla
        // Routing ayarları
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyWebApi v1"));
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }


        app.UseStaticFiles();

        // ...

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}