using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmProduceManagement.Models.Dtos
{
    public class UserDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string RoleId { get; set; }
         public string RoleName { get; set; }
        public string RoleDescription { get; set; }
    }
    public class LoginUserRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }


    public class LoginUserModel
    {
        public decimal Balance { get; set; }
        public string UserId { get; set; }
    }


    

     public class AdminDashboardModel
    {
        public int NoOfUsers { get; set; }
        public int NoOfManagers { get; set; }
        public int NoOfPendingFarmers { get; set; }
        public int NoOfVerifiedFarmers { get; set; }
        public int NoOfCustomers { get; set; }
        public decimal CompanyBalance { get; set; }
    }
}