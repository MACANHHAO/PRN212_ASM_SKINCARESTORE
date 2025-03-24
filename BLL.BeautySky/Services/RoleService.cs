
using DAL.BeautySky.Models;
using DAL.BeautySky.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BeautySky.Services
{
    public class RoleService
    {
        private readonly RoleRepository _roleRepo = new();

        public List<Role> GetAllRoles() => _roleRepo.GetAll();
    }
}
