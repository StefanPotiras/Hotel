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

                foreach (Feature featureDb in roomTypeDb.Features.Where(f => !f.Deleted))
                {
                    featureModels.Add(new FeatureModel { Id = featureDb.Id, Name = featureDb.Name });
                }
                foreach (Image imageDb in roomTypeDb.Images.Where(img => !img.Deleted))
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


        private int NrOfAvailableRooms(int RoomTypeId, DateTime StartDate, DateTime EndDate)
        {
            if (_context.RoomTypes.Find(RoomTypeId).Deleted) return 0;

            int UnavailableRooms = 0;
            int TotalRooms = _context.Rooms.Where(r => r.RoomType.Id == RoomTypeId).Count();

            foreach (Reservation reservation in _context.Reservations)
            {
                foreach (Room room in reservation.Rooms)
                {
                    if (room.RoomType.Id == RoomTypeId)
                    {
                        if (reservation.StartDate < EndDate || reservation.EndDate > StartDate)
                        {
                            UnavailableRooms++;
                        }
                    }
                }
            }

            return TotalRooms - UnavailableRooms;
        }


        public ObservableCollection<RoomTypeModel> GetRoomsByDate(DateTime startDate, DateTime endDate)
        {
            var rooms = GetAllRooms();
            foreach (RoomTypeModel roomTypeModel in rooms)
            {
                roomTypeModel.NumberOfRooms = NrOfAvailableRooms(roomTypeModel.Id, startDate, endDate);
            }
            return Convertor.EnumToObsCol(rooms.Where(room => room.NumberOfRooms > 0));
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

        public void DeleteRoomType(int roomTypeId)
        {
            RoomType roomType = _context.RoomTypes.Include(rt => rt.Rooms).Where(rt => rt.Id == roomTypeId).First();

            foreach (Room room in roomType.Rooms)
            {
                room.Deleted = true;
            }
            roomType.Deleted = true;

            _context.SaveChanges();
        }

        public void AddRoomType(RoomTypeModel roomTypeModel)
        {
            var images = roomTypeModel.Images.Select(modelImage => new Image { Data = modelImage.Data });

            ICollection<Image> imgCol = new List<Image>();
            foreach (Image image in images)
            {
                imgCol.Add(image);
            }

            RoomType roomType = new RoomType
            {
                Description = roomTypeModel.Description,
                BasePrice = roomTypeModel.Price,
                Capacity = roomTypeModel.Capacity,
                RoomTitle = roomTypeModel.RoomTitle,
                Images = Convertor.EnumToCol(roomTypeModel.Images.Select(modelImage => new Image { Data = modelImage.Data })),
                Features = Convertor.EnumToCol(roomTypeModel.Features.Select(featureModel => new Feature { Name = featureModel.Name }))
            };
            ICollection<Room> rooms = new List<Room>();

            for (int i = 0; i < roomTypeModel.NumberOfRooms; i++)
            {
                rooms.Add(new Room());
            }
            roomType.Rooms = rooms;

            _context.Add(roomType);
            _context.SaveChanges();
        }

        public void AddService(ServicesModel servicesModel)
        {
            _context.Add(new ExtraService { Name = servicesModel.Name, Price = servicesModel.Price });
            _context.SaveChanges();
        }

        public ICollection<ReservationModel> GetAllReservations()
        {
            ICollection<ReservationModel> reservationModels = new List<ReservationModel>();

            foreach (Reservation reservation in _context.Reservations
                .Include(r => r.Rooms)
                .ThenInclude(room => room.RoomType)
                .Include(r => r.ExtraServices))
            {
                decimal price = 0;

                foreach (Room room in reservation.Rooms)
                {
                    price += room.RoomType.BasePrice * (decimal)(reservation.EndDate - reservation.StartDate).TotalDays;
                }
                foreach (ExtraService service in reservation.ExtraServices)
                {
                    price += service.Price * (decimal)(reservation.EndDate - reservation.StartDate).TotalDays;
                }

                // ObservableCollection<RoomTypeNumberModel> roomTypes = new ObservableCollection<RoomTypeNumberModel>();

                //foreach(RoomType roomType)

                reservationModels.Add(new ReservationModel
                {
                    StartDate = reservation.StartDate,
                    EndDate = reservation.EndDate,
                    State = reservation.State,
                    UserId = reservation.Id,
                    Username = _context.Customers.Find(reservation.Id).Username,
                    NumberOfRooms = reservation.Rooms.Count(),
                    Services = Convertor.EnumToObsCol(
                        reservation.ExtraServices.Select(serv => new ServicesModel
                        {
                            Id = serv.Id,
                            Name = serv.Name,
                            Price = serv.Price
                        })),
                    Price = price,
                    //AllRoomsWithType = \
                });
            }
            return reservationModels;
        }

        public ICollection<ReservationModel> GetReservationsForCustomer(int customerId)
        {
            return Convertor.EnumToCol(GetAllReservations().Where(r => r.UserId == customerId));
        }

        public void UpdateRoomType(RoomTypeModel roomTypeModel)
        {
            var rooms = _context.Rooms.Where(r => r.RoomType.Id == roomTypeModel.Id);

            RoomType updatedRoomType = new RoomType
            {
                BasePrice = roomTypeModel.Price,
                Capacity = roomTypeModel.Capacity,
                Description = roomTypeModel.Description,
                Images = Convertor.EnumToCol(roomTypeModel.Images.Select(img => new Image { Data = img.Data })),
                RoomTitle = roomTypeModel.RoomTitle,
                Features = Convertor.EnumToCol(roomTypeModel.Features.Select(f => new Feature { Name = f.Name })),
            };
            _context.Update(updatedRoomType);

            if (roomTypeModel.NumberOfRooms < rooms.Count())
            {
                var itr = rooms.GetEnumerator();

                for (int i = 0; i < rooms.Count() - roomTypeModel.NumberOfRooms; i++)
                {
                    itr.MoveNext();
                    itr.Current.Deleted = true;
                }

            }
            else if (roomTypeModel.NumberOfRooms > rooms.Count())
            {
                for (int i = 0; i < -rooms.Count() + roomTypeModel.NumberOfRooms; i++)
                {
                    _context.Rooms.Add(new Room { RoomType = updatedRoomType });
                }
            }

            _context.SaveChanges();
        }

        public void AddReservation(ReservationModel reservationModel)
        {
            ICollection<Room> rooms = new List<Room>();

            foreach (var roomTypeNumberModel in reservationModel.AllRoomsWithType)
            {
                foreach (int roomNo in roomTypeNumberModel.RoomNumbers)
                {
                    rooms.Add(new Room { RoomNo = roomNo });
                }
            }

            _context.Add(new Reservation
            {
                StartDate = reservationModel.StartDate,
                EndDate = reservationModel.EndDate,
                Customer = _context.Find<Customer>(reservationModel.UserId),
                Rooms = rooms,
                ExtraServices = Convertor.EnumToCol(
                    reservationModel.Services.Select(serviceModel => new ExtraService { Id = serviceModel.Id })),
                State = ReservationState.Active,
            });
            _context.SaveChanges();
        }
    }
}
