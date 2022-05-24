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
                .UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=Hotel;Trusted_Connection=True;")
                .Options;

            using (var db = new HotelContext(options))
            {
                //db.Database.EnsureDeleted();
                //db.Database.EnsureCreated();


                //var param = new SqlParameter("@Id", 1);
                //var admins = db.Admins.FromSqlRaw()

                //db.SaveChanges();
            }

        }
    }
}
