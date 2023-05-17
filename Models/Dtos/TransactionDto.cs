using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Enums;

namespace FarmProduceManagement.Models.Dtos
{
    public class TransactionDto
    {
        public string Id { get; set; }
        public string TransactionNum { get; set; }
        public string FarmerId { get; set; }
        public FarmerDto Farmer { get; set; }
        public List<TransactionProduceDto> TransactionProduces { get; set; }
        public string ProduceName { get; set; }
        public TransactionStatus Status  { get; set; }
        public double TotalQuantity { get; set; }
        public decimal TotalAmount { get; set; }
        public string UnitOfMeasurement { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    public class CreateTransactionRequestModel
    {
        public List<TransactionDetailsRequestModel> Produce { get; set; }
        public string TransactionNum { get; set; }
    }

    public class TransactionDetailsRequestModel
    {

        //[Required]
        //[Display(Name = "Name")]
        //public string ProduceName { get; set; }
        [Required]
        [Display(Name = "Quantity")]
        public double Quantity { get; set; }
        [Required]
        [Display(Name = "Produce")]
        public string ProduceId { get; set; }
        [Required]
        [Display(Name = "Category")]
        public string CategoryId { get; set; }
        //[Required]
        //[Display(Name = "Unit Of Measurement")]
        //public string UnitOfMeasurement { get; set; }
    }
    public class UpdateTransactionRequestModel
    {
        [Required]
        [Display(Name = "Name")]
        public string ProduceName { get; set; }
        [Required]
        [Display(Name = "Quantity Available")]
        public double QuantityAvailable { get; set; }
        [Required]
        [Display(Name = "Unit Of Measurement")]
        public string UnitOfMeasurement { get; set; }
    }

}
