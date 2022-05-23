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

                switch (userModel.Type)
                {
                    case UserModel.UserType.Customer:
                        users.Add(user as Customer);
                        break;
                    case UserModel.UserType.Employee:
                        users.Add(user as Employee);
                        break;
                    case UserModel.UserType.Admin:
                        users.Add(user as Admin);
                        break;
                    default:
                        break;
                }
                
                _context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
