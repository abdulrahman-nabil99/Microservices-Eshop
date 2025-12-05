using Ordering.Domain.ValueObjects;
using System.Xml.Linq;

namespace Ordering.Domain.Models
{
    public class Order : Aggregate<OrderId>
    {
        private readonly List<OrderItem> _orderItems = new(); 
        public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
        public CustomerId CustomerId { get; private set; } 
        public OrderName OrderName { get; private set; }
        public Address ShippingAddress { get; private set; }
        public Address BillingAddress { get; private set; }
        public Payment Payment { get; private set; }
        public OrderStatus OrderStatus { get; private set; } = OrderStatus.Pending;
        public decimal TotalPrice
        {
            get => OrderItems.Sum(i => i.Price * i.Quantity);
            private set { }
        }

        public static Order Create(OrderId orderId,
                                   CustomerId customerId,
                                   OrderName orderName,
                                   Address shippingAddress,
                                   Address billingAddress,
                                   Payment payment)
        {
            Order order = new()
            {
                Id = orderId,
                CustomerId = customerId,
                OrderName = orderName,
                ShippingAddress = shippingAddress,
                BillingAddress = billingAddress,
                Payment = payment,
                OrderStatus = OrderStatus.Pending,
            };
            order.AddDomainEvent(new OrderCreatedEvent(order));
            return order;
        }

        public void Update(OrderName orderName,
                           Address shippingAddress,
                           Address billingAddress,
                           Payment payment,
                           OrderStatus orderStatus)
        {
            OrderName = orderName;
            ShippingAddress = shippingAddress;
            BillingAddress = billingAddress;
            Payment = payment;
            OrderStatus = orderStatus;
            AddDomainEvent(new OrderUpdatedEvent(this));
        }

        public void Add(ProductId productId, int quantity, decimal price)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
            OrderItem orderItem = new(Id, productId, quantity, price);
            _orderItems.Add(orderItem);
        }
        public void Remove(ProductId productId)
        {
            OrderItem orderItem = _orderItems.FirstOrDefault(o => o.ProductId.Value == productId.Value);
            if (orderItem != null) 
                _orderItems.Remove(orderItem);
        }
    }
}
