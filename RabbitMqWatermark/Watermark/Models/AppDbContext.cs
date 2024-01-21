using Microsoft.EntityFrameworkCore;

namespace Watermark.Models
{
    public class AppDbContext:DbContext
    {
        //: base(options) base yapısı ile startup tarafında optins kısmı olacaktır.
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
    }
}
