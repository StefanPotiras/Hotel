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
            //_context.Database.EnsureDeleted();
            //_context.Database.EnsureCreated();

        }

        public void Dispose()
        {
            _context.Dispose();
        }

        //tested
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

        //tested
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

        //tested
        public ObservableCollection<RoomTypeModel> GetAllRooms()
        {
            ObservableCollection<RoomTypeModel> roomTypeModels = new ObservableCollection<RoomTypeModel>();

            var roomTypesDb = _context.RoomTypes
                .Include(roomType => roomType.Features)
                .Include(roomType => roomType.Images)
                .Include(roomType => roomType.Prices)
                .Include(roomType => roomType.Rooms);

            foreach (RoomType roomTypeDb in roomTypesDb)
            {
                ObservableCollection<FeatureModel> featureModels = new ObservableCollection<FeatureModel>();
                ObservableCollection<ImageModel> imageModels = new ObservableCollection<ImageModel>();

                foreach (Feature featureDb in roomTypeDb.Features)
                {
                    featureModels.Add(new FeatureModel { Id = featureDb.Id, Name = featureDb.Name });
                }
                foreach (Image imageDb in roomTypeDb.Images)
                {
                    imageModels.Add(new ImageModel { Id = imageDb.Id, Data = imageDb.Data });
                }

                DateTime now = DateTime.Now;
                RoomPrice currentPrice = roomTypeDb.Prices
                    .Where(rp => rp.StartDate.CompareTo(now) < 0 && rp.EndTime.CompareTo(now) > 0)
                    .FirstOrDefault();

                RoomTypeModel roomTypeModel = new RoomTypeModel()
                {
                    Capacity = roomTypeDb.Capacity,
                    Description = roomTypeDb.Description,
                    Id = roomTypeDb.Id,
                    RoomTitle = roomTypeDb.RoomTitle,
                    NumberOfRooms = roomTypeDb.Rooms.Count,
                    Features = featureModels,
                    Images = imageModels,
                    Price = currentPrice != null ? currentPrice.Price : roomTypeDb.BasePrice
                };
                roomTypeModels.Add(roomTypeModel);
            }

            return roomTypeModels;
        }


        private int NrOfAvailableRooms(int RoomTypeId)
        {
            return _context.Rooms.Where(room => room.RoomType.Id == RoomTypeId
            && room.Reservation.EndDate < DateTime.Now && room.Reservation.BeginDate < DateTime.Now).Count();
        }


        public ObservableCollection<RoomTypeModel> GetRoomsByDate(DateTime startDate, DateTime endDate)
        {
            var rooms = GetAllRooms();
            foreach (RoomTypeModel roomTypeModel in rooms)
            {
                roomTypeModel.NumberOfRooms = NrOfAvailableRooms(roomTypeModel.Id);
            }
            return rooms.Where(room => room.NumberOfRooms > 0) as ObservableCollection<RoomTypeModel>;
        }


        public void UpdateReservationStatus(int ReservationId, ReservationState state)
        {
            Reservation reservation = _context.Find<Reservation>(ReservationId);
            reservation.State = state;
            _context.SaveChanges();
        }

        public void DeleteRoom(int roomNo)
        {
            Room room = _context.Find<Room>(roomNo);
            room.Deleted = true;
            _context.SaveChanges();
        }

        public void AddRoomType(RoomTypeModel roomTypeModel)
        {
            _context.Add(new RoomType
            {
                Description = roomTypeModel.Description,
                BasePrice = roomTypeModel.Price,
                Capacity = roomTypeModel.Capacity,
                RoomTitle = roomTypeModel.RoomTitle,
                Images = roomTypeModel.Images.Select(modelImage => new Image { Data = modelImage.Data }) as ICollection<Image>,
                Features = roomTypeModel.Features.Select(featureModel => new Feature { Name = featureModel.Name }) as ICollection<Feature>
            });
            _context.SaveChanges();
        }

        public void AddService(ServicesModel servicesModel)
        {
            _context.Add(new ExtraService { Name = servicesModel.Name, Price = servicesModel.Price });
            _context.SaveChanges();
        }
    }
}
