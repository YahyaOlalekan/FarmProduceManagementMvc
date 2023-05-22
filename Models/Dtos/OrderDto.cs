using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Enums;

namespace FarmProduceManagement.Models.Dtos
{
    public class OrderDto
    {
        public string Id { get; set; }
        public string OrderNumber { get; set; }
        public string CustomerId { get; set; }
        public CustomerDto Customer { get; set; }
        public List<OrderProductDto> OrderProducts { get; set; }
        public string ProduceName { get; set; }
        public double TotalQuantity { get; set; }
        public decimal TotalAmount { get; set; }
        public string UnitOfMeasurement { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    public class CreateOrderRequestModel
    {
        public List<OrderDetailsRequestModel> Product { get; set; }
        public string OrderNumber { get; set; }
    }

    public class OrderDetailsRequestModel
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
    public class UpdateOrderRequestModel
    {
        [Required]
        [Display(Name = "Name")]
        public string ProduceName { get; set; }
        // [Required]
        // [Display(Name = "Quantity Available")]
        // public double QuantityAvailabl { get; set; }
        [Required]
        [Display(Name = "Unit Of Measurement")]
        public string UnitOfMeasurement { get; set; }
    }

}
