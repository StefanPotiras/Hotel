using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsClasses
{  
   public  class UserModel
    {
        public enum UserType { Customer, Employee, Admin }
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public UserType Type { get; set; }
        
    }
}
