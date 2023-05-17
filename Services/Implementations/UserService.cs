using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Dtos;
using FarmProduceManagement.Repositories.Interfaces;
using FarmProduceManagement.Services.Interfaces;

namespace FarmProduceManagement.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IFarmerRepository _farmerRepository;

        public UserService(IUserRepository userRepository, IFarmerRepository farmerRepository)
        {
            _userRepository = userRepository;
            _farmerRepository = farmerRepository;
        }

        public BaseResponse<UserDto> Get(string id)
        {
            var user = _userRepository.Get(id);
            if (user != null)
            {
                return new BaseResponse<UserDto>
                {
                    Message = "user found successfully",
                    Status = true,
                    Data = new UserDto
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        Id = user.Id
                    }
                };
            }
            return new BaseResponse<UserDto>
            {
                Message = "User not found",
                Status = false,
            };
        }

        public BaseResponse<List<UserDto>> GetAll()
        {
            var getUsers = _userRepository.GetAll();
            if (getUsers != null)
            {
                return new BaseResponse<List<UserDto>>
                {
                    Message = "Successful",
                    Status = true,
                    Data = getUsers.Select(g => new UserDto
                    {
                        FirstName = g.FirstName,
                        LastName = g.LastName,
                        Email = g.Email,
                        PhoneNumber = g.PhoneNumber,
                        Id = g.Id

                    }).ToList()
                };
            }
            return new BaseResponse<List<UserDto>>
            {
                Message = "Not Successful",
                Status = false,
            };
        }

        public BaseResponse<UserDto> Login(LoginUserRequestModel model)
        {
            var user = _userRepository.Get(a => a.Email == model.Email && a.Password == model.Password);
            if (user != null)
            {
                var userLogin = new BaseResponse<UserDto>
                {
                    Message = "Login Successful",
                    Status = true,
                    Data = new UserDto
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        RoleId = user.Role.Id,
                        RoleName = user.Role.RoleName,
                        RoleDescription = user.Role.RoleDescription
                    }
                };

                // var farmer = _farmerRepository.Get(f => f.Id == user.Id);
                // if(farmer.FarmerRegStatus != Models.Enums.FarmerRegStatus.Approved)
                // {
                //     if(farmer.FarmerRegStatus == Models.Enums.FarmerRegStatus.Pending)
                //     {
                //          new BaseResponse<FarmerDto>
                //          {
                //             Message = "The approval of your application is still pending!",
                //             Status = false,
                //          };
                //     }
                //     else if(farmer.FarmerRegStatus == Models.Enums.FarmerRegStatus.Declined)
                //     {
                //           new BaseResponse<FarmerDto>
                //          {
                //             Message = "Sorry, your application is declined!",
                //             Status = false,
                //          }; 
                //     }
                // }
                return userLogin;

            }
            return new BaseResponse<UserDto>
            {
                Message = "Incorrect email or password",
                Status = false
            };
        }


    }
}