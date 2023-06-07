using FarmProduceManagement.Models.Entities;
using FarmProduceManagement.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FarmProduceManagement.Models.Dtos
{
    public class CustomerDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string ProfilePicture { get; set; }
        public string RegistrationNumber { get; set; }
        public decimal Wallet { get; set; }
        public string UserId { get; set; }
         public User User { get; set; }
        public string RoleId { get; set; }
        public string OrderId { get; set; }

        public List<OrderDto> Orders { get; set; }

    }

    public class CreateCustomerRequestModel
    {
        [Required, MaxLength(20), MinLength(3)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required, MaxLength(20), MinLength(3)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required, MaxLength(14), MinLength(11)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$",
        ErrorMessage = "Enter a valid email address!")]
        public string Email { get; set; }
        [Required, MaxLength(12), MinLength(3)]
        public string Password { get; set; }
        [Required, MaxLength(20), MinLength(3)]
        public string Address { get; set; }

        [Display(Name = "Profile Picture"), Required(ErrorMessage = "Please select file.")]
       // [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif|.jpeg)$", ErrorMessage = "Only Image file allowed.")]
        public IFormFile ProfilePicture { get; set; }
        // [Required]
        // public decimal Wallet { get; set; }
    }
    public class UpdateCustomerRequestModel
    {
        [Required, MaxLength(20), MinLength(3)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required, MaxLength(20), MinLength(3)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required, MaxLength(14), MinLength(11)]
        [Display(Name = "Phone Number")]
        // [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNumber { get; set; }
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$",
        ErrorMessage = "Enter a valid email address!")]
        public string Email { get; set; }
        // [Required, MaxLength(12), MinLength(3)]
        // public string Password { get; set; }
        [Required, MaxLength(20), MinLength(3)]
        public string Address { get; set; }
    
         [Display(Name = "Profile Picture")]
       // [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif|.jpeg)$", ErrorMessage = "Only Image file allowed.")]
        public IFormFile ProfilePicture { get; set; }
        // [Required]
        // public decimal Wallet { get; set; }
    }

    public class FundWalletRequestModel
    {
        public decimal Amount {get;set;}
    }

}
