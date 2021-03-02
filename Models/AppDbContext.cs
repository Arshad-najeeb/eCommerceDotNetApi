using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceNet.Models
{
    public class AppDbContext : DbContext

    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) { }
        protected override void OnModelCreating(ModelBuilder mb)
        {
            foreach (var rel in mb.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))

                rel.DeleteBehavior = DeleteBehavior.Restrict;
            foreach (var prop in mb.Model.GetEntityTypes()
                                        .SelectMany(t => t.GetProperties())
                                        .Where(q => q.ClrType == typeof(decimal)))
            {
                prop.Relational().ColumnType = "decimal(18,2)";
                mb.Entity<Comments>().HasKey(c => new { c.UserId, c.ProductId });
                mb.Entity<PurchaseHistory>().HasKey(ph => new { ph.UserId, ph.ProductId });

            }
            base.OnModelCreating(mb);
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Product>Product { get; set; }
        public  virtual DbSet<Comments> Comments { get; set; }
       public DbSet<PurchaseHistory>PurchaseHistory { get; set; }
        public virtual DbSet<Cart> Cart { get; set; }
    }
}
