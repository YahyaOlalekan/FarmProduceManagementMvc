using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Dtos;

namespace FarmProduceManagement.Services.Interfaces
{
    public interface IRoleService
    {
         BaseResponse<RoleDto> Create(CreateRoleRequestModel model);
        BaseResponse<RoleDto> Update(string id, UpdateRoleRequestModel model);
        BaseResponse<RoleDto> Get(string id);
        BaseResponse<IEnumerable<RoleDto>> GetAll();
        BaseResponse<RoleDto> Delete(string id);
    }
}