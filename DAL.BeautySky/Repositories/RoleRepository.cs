using DAL.BeautySky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BeautySky.Repositories
{
    public class RoleRepository
    {
        private ProjectSwpContext _context; 
        public List<Role> GetAll()
        {
            _context = new(); 
            return _context.Roles.ToList();
        }
    }
}
