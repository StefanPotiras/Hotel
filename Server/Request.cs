using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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


        public UserModel.UserType Login(string username, string password)
        {
            var users = _context.Users;

            foreach (User user in users)
            {
                if (user.Username == username && password == user.Password)
                {
                    if (user is Admin) return UserModel.UserType.Admin;
                    if (user is Employee) return UserModel.UserType.Employee;
                    if (user is Customer) return UserModel.UserType.Customer;
                }
            }
            return UserModel.UserType.None;
        }

        public bool Register(UserModel userModel)
        {
            var users = _context.Users;

            bool exists = false;
            foreach (User user in users)
            {
                if (user.Username == userModel.Username)
                {
                    exists = true;
                    break;
                }
            }
            if (!exists)
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
                        users.Add(new Customer
                        {
                            Username = userModel.Username,
                            FirstName = userModel.FirstName,
                            LastName = userModel.LastName,
                            Password = userModel.Password
                        });
                        break;
                    case UserModel.UserType.Employee:
                        users.Add(new Employee
                        {
                            Username = userModel.Username,
                            FirstName = userModel.FirstName,
                            LastName = userModel.LastName,
                            Password = userModel.Password
                        });
                        break;
                    case UserModel.UserType.Admin:
                        users.Add(new Admin
                        {
                            Username = userModel.Username,
                            FirstName = userModel.FirstName,
                            LastName = userModel.LastName,
                            Password = userModel.Password
                        });
                        break;
                    default:
                        break;
                }

                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public ObservableCollection<RoomTypeModel> GetAllRooms()
        {
            ObservableCollection<RoomTypeModel> roomTypes = new ObservableCollection<RoomTypeModel>();

            var roomTypesDb = _context.RoomTypes.Include(roomType => roomType.Prices).
        }
    }
}
