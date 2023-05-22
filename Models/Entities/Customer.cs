using FarmProduceManagement.Models.Enums;

namespace FarmProduceManagement.Models.Entities
{
    public class Customer: BaseEntity
    {
        public string RegistrationNumber { get; set; }
        public decimal Wallet { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }

        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
       // public ICollection<CustomerManager> CustomerManagers { get; set; } = new HashSet<CustomerManager>();

    }
}
