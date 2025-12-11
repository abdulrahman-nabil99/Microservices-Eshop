using Ordering.Domain.Enums;

namespace Ordering.Infrastructure.Data.Configuration
{
    public class OrderConfigration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id)
                .HasConversion(oId => oId.Value, dbId => OrderId.Of(dbId));

            builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(o => o.CustomerId)
                .IsRequired();

            builder.HasMany(o => o.OrderItems)
                .WithOne()
                .HasForeignKey(o => o.OrderId);

            builder.ComplexProperty(o => o.OrderName, namBuilder =>
            {
                namBuilder.Property(n => n.Value)
                .HasColumnName(nameof(Order.OrderName))
                .HasMaxLength(100)
                .IsRequired();
            });
            builder.ComplexProperty(o => o.BillingAddress, bAddressBuilder =>
            {
                bAddressBuilder.Property(a => a.FirstName)
                .HasMaxLength(50)
                .IsRequired();

                bAddressBuilder.Property(a => a.LastName)
                .HasMaxLength(50)
                .IsRequired();

                bAddressBuilder.Property(a => a.EmailAddress)
                .HasMaxLength(50);

                bAddressBuilder.Property(a => a.AddressLine)
                .HasMaxLength(200)
                .IsRequired();

                bAddressBuilder.Property(a => a.Country)
                .HasMaxLength(50);

                bAddressBuilder.Property(a => a.State)
                .HasMaxLength(50);

                bAddressBuilder.Property(a => a.ZipCode)
                .HasMaxLength(6)
                .IsRequired();
            });

            builder.ComplexProperty(o => o.ShippingAddress, sAddressBuilder =>
            {
                sAddressBuilder.Property(a => a.FirstName)
                .HasMaxLength(50)
                .IsRequired();

                sAddressBuilder.Property(a => a.LastName)
                .HasMaxLength(50)
                .IsRequired();

                sAddressBuilder.Property(a => a.EmailAddress)
                .HasMaxLength(50);

                sAddressBuilder.Property(a => a.AddressLine)
                .HasMaxLength(200)
                .IsRequired();

                sAddressBuilder.Property(a => a.Country)
                .HasMaxLength(50);

                sAddressBuilder.Property(a => a.State)
                .HasMaxLength(50);

                sAddressBuilder.Property(a => a.ZipCode)
                .HasMaxLength(6)
                .IsRequired();
            });

            builder.ComplexProperty(o => o.Payment, paymentBuilder =>
            {
                paymentBuilder.Property(p => p.CardName)
                .HasMaxLength(50);

                paymentBuilder.Property(p => p.CardNumber)
                .HasMaxLength(24)
                .IsRequired();

                paymentBuilder.Property(p => p.Expiration)
                .HasMaxLength(10);

                paymentBuilder.Property(p => p.CVV)
                .HasMaxLength(4)
                .IsRequired();

                paymentBuilder.Property(p => p.PaymentMethod);
            });

            builder.Property(o => o.OrderStatus)
                .HasDefaultValue(OrderStatus.Draft)
                .HasConversion(os => os.ToString(), dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));

            builder.Property(o => o.TotalPrice);
        }
    }
}
