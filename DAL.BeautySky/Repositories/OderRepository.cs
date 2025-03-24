using DAL.BeautySky.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.BeautySky.Repositories
{
    public class OrderRepository
    {
        private readonly ProjectSwpContext _context;

        public OrderRepository(ProjectSwpContext context)
        {
            _context = context;
        }

        public void AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
        public List<Order> GetAll()
        {
            return _context.Orders.ToList();
        }

        public void AddOrderProduct(OrderProduct orderProduct)
        {
            _context.OrderProducts.Add(orderProduct);
            _context.SaveChanges();
        }

        public List<Order> GetOrdersByUser(int userId)
        {
            return _context.Orders.Where(o => o.UserId == userId).ToList();
        }
        public Order? GetById(int orderId)
        {
            return _context.Orders.FirstOrDefault(o => o.OrderId == orderId);
        }
        public void Update(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }

    }
}
