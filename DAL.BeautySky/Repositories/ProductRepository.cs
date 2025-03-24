using DAL.BeautySky.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BeautySky.Repositories
{
    public class ProductRepository
    {
        private ProjectSwpContext _context; //ko new, lúc nào dùng thì new

        //lấy tất cả hàng và cột của table SupplierCompany
        //4 cột, 5 dòng
        public List<Product> GetAll()
        {
            _context = new ProjectSwpContext();
            //return _context.PerfumeInformations.ToList(); //Select * form
            return _context.Products.Include("Category").ToList();
        }
        public void Create(Product obj)
        {
            _context = new();  //mỗi lần xài 1 lần new _context
            _context.Products.Add(obj); //trong ram
            _context.SaveChanges(); //chính thức tạo mới trong db
        }
        public void Delete(Product obj)
        {
            _context = new();  //mỗi lần xài 1 lần new _context
            _context.Products.Remove(obj); //trong ram
            _context.SaveChanges(); //chính thức xoá trong db
        }
    }
}
