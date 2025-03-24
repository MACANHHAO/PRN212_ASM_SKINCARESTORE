using DAL.BeautySky.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BeautySky.Repositories
{
    public class UserRepository
    {
        private ProjectSwpContext _context;

        public List<User> GetAll()
        {
            _context = new ProjectSwpContext();
            //return _context.PerfumeInformations.ToList(); //Select * form
            return _context.Users.Include("Role").ToList();
        }
        public User? GetOne(string email, string pass)
        {
            _context = new();  //new Context() trước khi dùng
            //return _context.StaffMembers.ToList(); //join thêm .Include()
            //return _context.StaffMembers.FirstOrDefault(where trên email và pass);
            return _context.Users.FirstOrDefault(x => x.Email.ToLower() == email.Trim().ToLower() && x.Password == pass);

        }
        public void Create(User user)
        {
            _context = new();  //mỗi lần xài 1 lần new _context
            _context.Users.Add(user); //trong ram
            _context.SaveChanges(); //chính thức tạo mới trong db
        }
    }
}
