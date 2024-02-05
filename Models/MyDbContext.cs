using Microsoft.EntityFrameworkCore; // DbContext eklenmeli

namespace MyWebApi.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
            
        }

        public DbSet<NemSicaklikVerisi> NemSicaklikVerileri { get; set; }
    }
}