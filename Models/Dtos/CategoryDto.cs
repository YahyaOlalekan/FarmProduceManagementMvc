using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Entities;

namespace FarmProduceManagement.Models.Dtos
{
    public class CategoryDto
    {
        public string Id { get; set; }
        public string NameOfCategory { get; set; }
        public string DescriptionOfCategory { get; set; }
        public List<ProduceDto> Produces { get; set; }
    }
    public class CreateCategoryRequestModel
    {
        [Required, MaxLength(30), MinLength(3)]
        public string NameOfCategory { get; set; }
        [Required, MaxLength(60), MinLength(3)]
        public string DescriptionOfCategory { get; set; }
    }
    public class UpdateCategoryRequestModel
    {
        [Required, MaxLength(30), MinLength(3)]
        public string NameOfCategory { get; set; }
        [Required, MaxLength(60), MinLength(3)]
        public string DescriptionOfCategory { get; set; }
    }

}



