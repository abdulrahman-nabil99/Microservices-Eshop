namespace Discount.gRPC.Data
{
    public class DiscountContext : DbContext
    {
        public DiscountContext(DbContextOptions<DiscountContext> options):base(options) { }
        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(
                new Coupon { Id = 1, ProductName = "IPhone", Description = "Apple Discount", Amount = 150 },
                new Coupon { Id = 2, ProductName = "Samsung", Description = "Samsung Discount", Amount = 70 }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
