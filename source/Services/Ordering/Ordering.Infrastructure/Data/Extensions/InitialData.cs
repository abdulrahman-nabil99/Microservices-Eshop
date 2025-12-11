namespace Ordering.Infrastructure.Data.Extensions
{
    internal class InitialData
    {
        public static IEnumerable<Customer> Customers =>
            [
                Customer.Create(CustomerId.Of(new Guid("77FFC9E5-8245-4804-807F-76B275F52884")), "Omar", "omar@email.com"),
                Customer.Create(CustomerId.Of(new Guid("4A10C51E-EB9F-45D5-B162-A5AA5FC099FF")), "Ahmed", "ahmed@email.com")
            ];

        public static IEnumerable<Product> Products =>
            [
                Product.Create(ProductId.Of(new Guid("a3dfbc88-59c1-4bfc-a1aa-8742ef816bd6")), "IPhone 17", 1999.9m),
                Product.Create(ProductId.Of(new Guid("c498398b-88b4-4bc3-a418-e0c9d98c6ed5")), "IPhone 13", 999.9m),
                Product.Create(ProductId.Of(new Guid("43beea6c-6f0b-4c31-98ae-3344acc2e8a2")), "Mac Air", 1500.9m),
                Product.Create(ProductId.Of(new Guid("8abf3549-4a7f-4b7e-9de1-7ddbb93e3753")), "Asus Rog X991", 1900m),
            ];

        public static IEnumerable<Order> Orders { 
            get
            {
                var address1 = Address.Of("Omar", "Aly", "omar@email.com", "Tanta", "Egypt", "Gharbia", "31726");
                var address2 = Address.Of("Ahmed", "Gomaa", "ahmed@email.com", "Cairo", "Egypt", "Cairo", "22331");
                var payment1 = Payment.Of("NBK", "4676278498761234", "11/28", "332", 1);
                var payment2 = Payment.Of("NBE", "5432876876526752", "12/31", "761", 2);

                var order1 = Order.Create(
                    OrderId.Of(Guid.NewGuid()),
                    CustomerId.Of(new Guid("77FFC9E5-8245-4804-807F-76B275F52884")),
                    OrderName.Of("Order 1"),
                    address1, address1,
                    payment1
                    );

                order1.Add(ProductId.Of(new Guid("a3dfbc88-59c1-4bfc-a1aa-8742ef816bd6")), 1, 1999.9m);
                order1.Add(ProductId.Of(new Guid("43beea6c-6f0b-4c31-98ae-3344acc2e8a2")), 1, 1500.9m);

                var order2 = Order.Create(
                    OrderId.Of(Guid.NewGuid()),
                    CustomerId.Of(new Guid("4A10C51E-EB9F-45D5-B162-A5AA5FC099FF")),
                    OrderName.Of("Order 2"),
                    address2, address2,
                    payment2
                    );
                order2.Add(ProductId.Of(new Guid("c498398b-88b4-4bc3-a418-e0c9d98c6ed5")), 1, 999.9m);
                order2.Add(ProductId.Of(new Guid("8abf3549-4a7f-4b7e-9de1-7ddbb93e3753")), 1, 1900m);

                return [order1, order2];
            } 
        }

    }
}
