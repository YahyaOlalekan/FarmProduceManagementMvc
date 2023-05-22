using FarmProduceManagement.Models.Entities;
using FarmProduceManagement.Models.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FarmProduceManagement.Models.Dtos
{
    public class OrderCartDto
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }
        public Produce Produce { get; set; }
        public double Quantity { get; set; }
        public decimal Price { get; set; }
        public string NameOfCategory { get; set; }
        public string UnitOfMeasurement { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        
    }

    public class CreateOrderCartRequestModel
    {
        [Required, MinLength(3), MaxLength(50)]
        [Display(Name = "Name")]
        public string ProductId { get; set; }
        [Required]
        public double Quantity { get; set; }
        [Required]
        [Display(Name = "Price")]
        public decimal Price { get; set; }
        [Required]
        [Display(Name = "Unit Of Measurement")]
        public string UnitOfMeasurement { get; set; }
        [Required]
        [Display(Name = "Category")]
        public string CategoryId { get; set; }
        
    }
}
