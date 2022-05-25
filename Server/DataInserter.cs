using ModelsClasses;
using Server.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class DataInserter
    {
        private HotelContext _context;

        public DataInserter(HotelContext context)
        {
            _context = context;
        }

        public void InsertFeatures()
        {
            var features = new List<Feature>
            {
                new Feature {Name = "Gratar"},
                new Feature {Name = "Foisor"},
                new Feature {Name = "Balcon"},
                new Feature {Name = "Masina de spalat"},
                new Feature {Name = "Bucatarie"},
            };
            _context.Features.AddRange(features);
            _context.SaveChanges();
        }

        public void InsertExtraServices()
        {
            var services = new List<ExtraService>
            {
                new ExtraService { Name = "Breakfast" , Price = 30},
                new ExtraService { Name = "Airport Transport" , Price = 50},
                new ExtraService { Name = "Spa Access" , Price = 80},
            };
            _context.ExtraServices.AddRange(services);
            _context.SaveChanges();
        }

        public void InsertRoomTypes()
        {
            var rooms = new List<RoomType>
            {
                new RoomType
                {
                    BasePrice = 100,
                    RoomTitle = "King room",
                    Capacity = 2,
                    Description = "Most comfortable bed",
                    Features = new List<Feature>
                    {
                        _context.Features.SingleOrDefault(f => f.Id ==1),
                        _context.Features.SingleOrDefault(f => f.Id ==2)
                    },
                    Rooms = new List<Room>
                    {
                        new Room(),
                        new Room(),
                        new Room(),
                    },
                    Prices= new List<RoomPrice>
                    {
                        new RoomPrice
                        {
                            StartDate = new DateTime(2022,05,24),
                            EndTime = new DateTime(2022,05,26),
                            Price = 80,
                        },
                        new RoomPrice
                        {
                            StartDate = new DateTime(2022,05,28),
                            EndTime = new DateTime(2022,05,29),
                            Price = 119.89m,
                        }
                    }
                },
                new RoomType
                {
                    BasePrice = 150,
                    RoomTitle = "Deluxe room",
                    Capacity = 2,
                    Description = "Pure lux",
                    Features = new List<Feature>
                    {
                        _context.Features.SingleOrDefault(f => f.Id ==3),
                        _context.Features.SingleOrDefault(f => f.Id ==4)
                    },
                    Rooms = new List<Room>
                    {
                        new Room(),
                        new Room(),
                    },
                    Prices= new List<RoomPrice>
                    {
                        new RoomPrice
                        {
                            StartDate = new DateTime(2022,05,24),
                            EndTime = new DateTime(2022,05,26),
                            Price = 110,
                        },
                        new RoomPrice
                        {
                            StartDate = new DateTime(2022,05,28),
                            EndTime = new DateTime(2022,06,01),
                            Price = 440,
                        }
                    }
                }
            };

            //rooms[0].Rooms = new List<Room> { new Room(), new Room() };
            _context.RoomTypes.AddRange(rooms);
            _context.SaveChanges();
        }

        //no!
        public void InsertUsers()
        {
            var users = new List<User>
            {
                new Admin
                {
                    FirstName = "Madalin",
                    LastName = "Vladoiu",
                    Username = "mvladoiu",
                    Password="123",
                },
                new Admin
                {
                    FirstName = "Stefan",
                    LastName = "Potiras",
                    Username = "spotiras",
                    Password="123",
                },
                new Employee
                {
                    FirstName = "Andrei",
                    LastName = "Mateescu",
                    Username = "amateescu",
                    Password="123",
                },
                new Employee
                {
                    FirstName = "Leonard",
                    LastName = "Ene",
                    Username = "lene",
                    Password="123",
                },
                new Customer
                {
                    FirstName = "Andrei",
                    LastName = "Radu",
                    Username = "aradu",
                    Password="123",
                    Reservations = new List<Reservation>
                    {
                        new Reservation
                        {
                            StartDate= new DateTime(2022,05,25),
                            EndDate = new DateTime(2022,05,27),
                            ExtraServices= new List<ExtraService>
                            {
                                _context.ExtraServices.SingleOrDefault(es => es.Id == 2),
                                _context.ExtraServices.SingleOrDefault(es => es.Id == 3),
                            },
                            Rooms = new List<Room>
                            {
                                _context.Rooms.SingleOrDefault ( r => r.RoomNo == 2),
                                _context.Rooms.SingleOrDefault ( r => r.RoomNo == 5),
                            },
                            State = ReservationState.Active

                        },
                        new Reservation
                        {
                            StartDate= new DateTime(2022,06,14),
                            EndDate = new DateTime(2022,06,17),
                            ExtraServices= new List<ExtraService>
                            {
                                _context.ExtraServices.SingleOrDefault(es => es.Id == 1),
                            },
                            Rooms = new List<Room>
                            {
                                _context.Rooms.SingleOrDefault ( r => r.RoomNo == 1),
                                //_context.Rooms.SingleOrDefault ( r => r.RoomNo == 2),
                            },
                            State = ReservationState.Active
                        }
                    },
                },
                new Customer
                {
                    FirstName = "Raul",
                    LastName = "Radu",
                    Username = "rradu",
                    Password="123",
                    Reservations = new List<Reservation>
                    {
                        new Reservation
                        {
                            StartDate= new DateTime(2022,05,25),
                            EndDate = new DateTime(2022,05,27),
                            ExtraServices= new List<ExtraService>
                            {
                                _context.ExtraServices.SingleOrDefault(es => es.Id == 1),
                            },
                            Rooms = new List<Room>
                            {
                                _context.Rooms.SingleOrDefault ( r => r.RoomNo == 1),
                                _context.Rooms.SingleOrDefault ( r => r.RoomNo == 4),
                            },
                            State = ReservationState.Active
                        },
                    },


                }
            };
            _context.Users.AddRange(users);
            _context.SaveChanges();
        }

    }
}
