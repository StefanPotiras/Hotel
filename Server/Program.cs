using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Server.Entities;

namespace Server
{
    class Program
    {



        static void Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<HotelContext>()
                .UseSqlServer(@"Server=DESKTOP-6GD6S01\SQLEXPRESS;Database=Hotel;Trusted_Connection=True;")
                .Options;

            using (var db = new HotelContext(options))
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                DataInserter dataInserter = new DataInserter(db);
                dataInserter.InsertFeatures();
                dataInserter.InsertExtraServices();
                dataInserter.InsertRoomTypes();
                dataInserter.InsertUsers();
            }

            Request request = new Request(@"Server=DESKTOP-6GD6S01\SQLEXPRESS;Database=Hotel;Trusted_Connection=True;");
            Console.WriteLine(request.NrOfAvailableRooms(1, new DateTime(2023, 01, 01), new DateTime(2023, 01, 02)));
            Console.WriteLine(request.NrOfAvailableRooms(2, new DateTime(2023, 01, 01), new DateTime(2023, 01, 02)));
            Console.WriteLine(request.NrOfAvailableRooms(2, new DateTime(2022, 05, 24), new DateTime(2022, 06, 25)));


          
        }
    }
}
