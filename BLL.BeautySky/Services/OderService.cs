using DAL.BeautySky;
using DAL.BeautySky.Models;
using DAL.BeautySky.Repositories;
using System;

namespace BLL.BeautySky.Services
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;

        public OrderService(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public List<Order> GetAllOrder()
        {
            return _orderRepository.GetAll();
        }

        public void CreateOrder(int userId, int productId, decimal price)
        {
            var order = new Order
            {
                OrderDate = DateTime.Now,
                UserId = userId,
                TotalAmount = price,
                DiscountAmount = 0,
                FinalAmount = price,
                Status = "Pending"
            };

            _orderRepository.AddOrder(order);

            var orderProduct = new OrderProduct
            {
                OrderId = order.OrderId,
                ProductId = productId,
                Quantity = 1,
                UnitPrice = price,
                TotalPrice = price
            };

            _orderRepository.AddOrderProduct(orderProduct);
        }
        public void UpdateOrderStatus(int orderId, string status)
        {
            using (var context = new ProjectSwpContext()) // Tạo DbContext mới
            {
                var order = context.Orders.FirstOrDefault(o => o.OrderId == orderId);
                if (order != null)
                {
                    order.Status = status;
                    context.SaveChanges(); // Lưu thay đổi xuống database
                }
            }
        }


    }
}
