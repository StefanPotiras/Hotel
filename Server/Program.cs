using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using Server.Entities;

namespace Server
{
    class Program
    { 
        static IEnumerable<User> CreateData()
        {
            var users = new List<User>
            {
                new Admin
                {
                    FirstName = "Madalin",
                    LastName = "Vladoiu",
                    Username = "madal",
                    Password = "meteoritzii"
                },
                new Admin
                {
                    FirstName = "Stefan",
                    LastName = "Potiras",
                    Username = "Cioti",
                    Password = "party1047"
                },
                new Employee
                {
                    FirstName = "Mihai",
                    LastName = "Oprea",
                    Username = "Mike",
                    Password = "Biceps"
                },
                new Employee
                {
                    FirstName = "Adelin",
                    LastName = "Balan",
                    Username = "adel",
                    Password = "1q2w3e"
                },
                new Customer
                {
                    FirstName = "Andrei",
                    LastName = "Mateescu",
                    Username = "Matesex",
                    Password = "delta"
                },
                new Customer
                {
                    FirstName = "Leonard",
                    LastName = "Ene",
                    Username ="Lene",
                    Password = "upsilon"
                }
                
                

            };
            return users;
        }

        static void Main(string[] args)
        { 
            var options = new DbContextOptionsBuilder<HotelContext>()
                .UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=Hotel;Trusted_Connection=True;")
                .Options;

            using (var db = new HotelContext(options))
            {
               db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                //db.SaveChanges();
            }

        }
    }
}
