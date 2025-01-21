using LanguageInstall.Data.Data;
using LanguageInstall.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageInstall.Service.Service
{
    public interface IUserService
    {
        bool Register(User user);
        User Login(string email, string password);
        void Logout();
    }

    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public bool Register(User user)
        {
            if (_context.Users.Any(u => u.Email == user.Email))
                return false; // Email already exists

            _context.Users.Add(user);
            _context.SaveChanges();
            return true;
        }

        public User Login(string email, string password)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        public void Logout()
        {
            // Logic for logout (if needed)
        }
    }
}
