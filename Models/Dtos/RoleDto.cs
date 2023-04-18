using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FarmProduceManagement.Models.Dtos
{
    public class RoleDto
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public List<UserDto> Users { get; set; }
    }
    public class CreateRoleRequestModel
    {
        [Required, MaxLength(30), MinLength(3)]
        public string RoleName { get; set; }
        [Required, MaxLength(60), MinLength(3)]
        public string RoleDescription { get; set; }
    }
    public class UpdateRoleRequestModel
    {
        [Required, MaxLength(30), MinLength(3)]
        public string RoleName { get; set; }
        [Required, MaxLength(60), MinLength(3)]
        public string RoleDescription { get; set; }
    }
}