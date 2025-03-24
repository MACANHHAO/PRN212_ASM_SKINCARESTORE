using DAL.BeautySky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BeautySky.Repositories
{
    public class CategoryRepository
    {
        private ProjectSwpContext _context; //ko new, lúc nào dùng thì new

        //lấy tất cả hàng và cột của table SupplierCompany
        //4 cột, 5 dòng
        public List<Category> GetAll()
        {
            _context = new();  //viết ngắn
            return _context.Categories.ToList();
        }
    }
}
