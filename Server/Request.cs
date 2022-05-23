using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModelsClasses;
using Server.Entities;

namespace Server
{
    public class Request : IDisposable
    {
        private HotelContext _context;

        public Request(string connectionString)
        {
            var options = new DbContextOptionsBuilder<HotelContext>()
               .UseSqlServer(connectionString)
               .Options;
            //@"Server=localhost\SQLEXPRESS;Database=Hotel;Trusted_Connection=True;"
            
            _context = new HotelContext(options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public bool Register(UserModel userModel)
        {
            var users = _context.Users;

            bool exists = false;
            foreach (User user in users)
            {
                if(user.Username == userModel.Username)
                {
                    exists = true;
                    break;
                }
            }
            if(!exists)
            {
                User user = new User
                {
                    Username = userModel.Username,
                    FirstName = userModel.FirstName,
                    LastName = userModel.LastName,
                    Password = userModel.Password
                };
                users.Add(user);
                _context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
