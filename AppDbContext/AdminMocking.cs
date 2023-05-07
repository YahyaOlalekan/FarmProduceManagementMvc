using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Entities;

namespace FarmProduceManagement.AppDbContext
{
    public class AdminMocking
    {
        public static void Mock(IApplicationBuilder _applicationBuilder)
        {
            using (var service = _applicationBuilder.ApplicationServices.CreateScope())
            {
                var _context = service.ServiceProvider.GetService<Context>();
                if (!_context.Users.Any())
                {
                    var role = new Role
                    {
                        RoleName = "Admin",
                        RoleDescription = "AppOwner"
                    };
                   
                    var user = new User
                    {
                        FirstName = "Ola",
                        LastName = "Bisi",
                        PhoneNumber = "08132759937",
                        Email ="ola@gmail.com",
                        Password ="123",
                        Address="Abk",
                        ProfilePicture="admin.jpg",
                        RoleId = role.Id
                    };
                   
                    _context.Roles.Add(role);
                    _context.Users.Add(user);
                    _context.SaveChanges();
                }
            }
        }
    }
}
 