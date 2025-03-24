using DAL.BeautySky.Models;
using DAL.BeautySky.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BeautySky.Services
{
    public class ProductService
    {
        private ProductRepository _repo = new();

        public List<Product> GetAllProduct()
        {
            return _repo.GetAll();
        }
        public void CreateProduct(Product obj) => _repo.Create(obj);
        public List<Product> SearchProducts(string keyword)
        {
            return _repo.GetAll()
                        .Where(p => p.ProductName.Contains(keyword, System.StringComparison.OrdinalIgnoreCase))
                        .ToList();
        }
        public void DeleteProduct(Product obj)
        {
            _repo.Delete(obj);
        }
    }
}
