using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace FarmProduceManagement.Models.Dtos
{
    public class ManagerDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string ProfilePicture { get; set; }
        public string RegistrationNumber { get; set; }
        public string UserId { get; set; }
         public string TransactionId { get; set; }
        public List<TransactionDto> Transactions { get; set; }
    }
    public class CreateManagerRequestModel
    {
        [Required, MaxLength(20), MinLength(3)]
        public string FirstName { get; set; }
        [Required, MaxLength(20), MinLength(3)]
        public string LastName { get; set; }
        [Required, MaxLength(14), MinLength(11)]
        public string PhoneNumber { get; set; }
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$",
       ErrorMessage = "Enter a valid email address!")]
        public string Email { get; set; }
        [Required, MaxLength(12), MinLength(3)]
        public string Password { get; set; }
        [Required, MaxLength(20), MinLength(3)]
        public string Address { get; set; }
        public IFormFile ProfilePicture { get; set; }

    }
    public class UpdateManagerRequestModel
    {
        [Required, MaxLength(20), MinLength(3)]
        public string FirstName { get; set; }
        [Required, MaxLength(20), MinLength(3)]
        public string LastName { get; set; }
        [Required, MaxLength(14), MinLength(11)]
        public string PhoneNumber { get; set; }
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$",
       ErrorMessage = "Enter a valid email address!")]
        public string Email { get; set; }
        // [Required, MaxLength(12), MinLength(3)]
        // public string Password { get; set; }
        [Required, MaxLength(20), MinLength(3)]
        public string Address { get; set; }
        public IFormFile ProfilePicture { get; set; }
    }
}