using DAL.BeautySky.Models;
using DAL.BeautySky.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BeautySky.Services
{
    public class UserService
    {
        private UserRepository _repo = new(); //new luôn okie do nó xa DbContext

        //email và pass từ GUI Login
        public List<User> GetAllUsers() => _repo.GetAll();
        public void CreateUser(User user) => _repo.Create(user);

        public User? Authenticate(string email, string pass)
        {
            return _repo.GetOne(email, pass);
        }
    }
}
