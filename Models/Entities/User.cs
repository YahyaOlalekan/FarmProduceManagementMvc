using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FarmProduceManagement.Models.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
         public string ProfilePicture { get; set; }
        public string RoleId { get; set; }
        public Role Role { get; set; }

        internal static object FindFirst(string role)
        {
            throw new NotImplementedException();
        }
    }
}