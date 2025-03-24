using DAL.BeautySky.Models;
using DAL.BeautySky.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BeautySky.Services
{
    public class CategoryService
    {
        private CategoryRepository _repo = new();

        public List<Category> GetAllCategory()
        {
            return _repo.GetAll();
        }
    }
}
