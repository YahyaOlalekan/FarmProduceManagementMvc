using System.ComponentModel.DataAnnotations;

namespace FarmProduceManagement.Models.Enums
{
    public enum TransactionStatus
    {
        Delivered = 1,

        Approved,

        [Display(Name="Awaiting Approval")]
        Pending,

        Rejected,
        NotDelivered
    }
}