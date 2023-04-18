using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Dtos;
using FarmProduceManagement.Models.Entities;
using FarmProduceManagement.Repositories.Interfaces;
using FarmProduceManagement.Services.Interfaces;

namespace FarmProduceManagement.Services.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
         private readonly IHttpContextAccessor _httpAccessor;

        public RoleService(IRoleRepository roleRepository, IHttpContextAccessor httpAccessor)
        {
            _roleRepository = roleRepository;
            _httpAccessor = httpAccessor;
        }

        public BaseResponse<RoleDto> Create(CreateRoleRequestModel model)
        {
            var loginId = _httpAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var roleExist = _roleRepository.Get(a => a.RoleName == model.RoleName);
            if (roleExist == null)
            {
                var role = new Role();
                role.RoleName = model.RoleName;
                role.RoleDescription = model.RoleDescription;
                role.CreatedBy = loginId;

                _roleRepository.Create(role);
                _roleRepository.Save();

                return new BaseResponse<RoleDto>
                {
                    Message = "Successful",
                    Status = true,
                    Data = new RoleDto
                    {
                        Id = role.Id,
                        RoleName = role.RoleName,
                        RoleDescription = role.RoleDescription
                    }
                };
            }
            return new BaseResponse<RoleDto>
            {
                Message = "Already exists",
                Status = false
            };

        }

        public BaseResponse<RoleDto> Delete(string id)
        {
            var role = _roleRepository.Get(id);
            if (role is null)
            {
                return new BaseResponse<RoleDto>
                {
                    Message = "The role does not exist",
                    Status = false
                };
            }
            role.IsDeleted = true;
          
            _roleRepository.Update(role);
            _roleRepository.Save();
            return new BaseResponse<RoleDto>
            {
                Message = "Role Deleted Successfully ",
                Status = true
            };

        }

        public BaseResponse<RoleDto> Get(string id)
        {
            var role = _roleRepository.Get(id);
            if (role == null)
            {
                return new BaseResponse<RoleDto>
                {
                    Message = "Not found",
                    Status = false,
                };
            }
            return new BaseResponse<RoleDto>
            {
                Message = "Found",
                Status = true,
                Data = new RoleDto
                {
                    Id = role.Id,
                    RoleName = role.RoleName,
                    RoleDescription = role.RoleDescription
                }
            };

        }

        public BaseResponse<IEnumerable<RoleDto>> GetAll()
        {
            var role = _roleRepository.GetAll();
            if (role == null)
            {
                return new BaseResponse<IEnumerable<RoleDto>>
                {
                    Message = "Not found",
                    Status = false,
                };
            }
            return new BaseResponse<IEnumerable<RoleDto>>
            {
                Message = "Found",
                Status = true,
                Data = role.Select(c => new RoleDto
                {
                    Id = c.Id,
                    RoleName = c.RoleName,
                    RoleDescription = c.RoleDescription
                })
            };
        }



        public BaseResponse<RoleDto> Update(string id, UpdateRoleRequestModel model)
        {
            var role = _roleRepository.Get(a => a.Id == id);
            if (role is not null)
            {

                role.RoleName = model.RoleName;
                role.RoleDescription = model.RoleDescription;

                _roleRepository.Update(role);
                _roleRepository.Save();

                return new BaseResponse<RoleDto>
                {
                    Message = "Role Updated Successfully",
                    Status = true,
                    Data = new RoleDto
                    {
                        RoleName = role.RoleName,
                        RoleDescription = role.RoleDescription,
                        Id = role.Id,
                    }
                };
            }
            return new BaseResponse<RoleDto>
            {
                Message = "Unable to Update",
                Status = false,
            };
        }


    }
}