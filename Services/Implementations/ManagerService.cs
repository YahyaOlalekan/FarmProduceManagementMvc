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
    public class ManagerService : IManagerService
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IAdminRepository _adminRepository;

        public ManagerService(IManagerRepository managerRepository, IRoleRepository roleRepository, IUserRepository userRepository, IWebHostEnvironment webHostEnvironment, IAdminRepository adminRepository)
        {
            _managerRepository = managerRepository;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _webHostEnvironment = webHostEnvironment;
            _adminRepository = adminRepository;
        }

        public BaseResponse<ManagerDto> Create(string loginId, CreateManagerRequestModel model)
        {
            var managerExist = _managerRepository.Get(m => m.User.Email == model.Email);
            // BaseEntity managerExist = _managerRepository.Get(m => m.User.Email == model.Email);
            if (managerExist != null)
            {
                return new BaseResponse<ManagerDto>
                {
                    Message = "Email already exists",
                    Status = false,
                };
            }
            var phoneNumer = _managerRepository.Get(m => m.User.PhoneNumber == model.PhoneNumber);
            if (phoneNumer != null)
            {
                return new BaseResponse<ManagerDto>
                {
                    Message = "Phone number already exists",
                    Status = false,
                };
            }

            var profilePicture = UploadFile(model.ProfilePicture);


            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Password = model.Password,
                Address = model.Address,
                Email = model.Email,
                ProfilePicture = profilePicture,
                RoleId = _roleRepository.Get(a => a.RoleName == "Manager").Id,
                CreatedBy = loginId,
            };

            var manager = new Manager
            {
                CreatedBy = loginId,
                RegistrationNumber = GenerateManagerRegNum(),
                UserId = user.Id,
                User = user,
            };
            _userRepository.Create(user);
            _managerRepository.Create(manager);
            _managerRepository.Save();

            return new BaseResponse<ManagerDto>
            {
                Message = "Manager successfully added",
                Status = true,
                Data = new ManagerDto
                {
                    Id = manager.Id,
                    RegistrationNumber = manager.RegistrationNumber,
                    FirstName = manager.User.FirstName,
                    LastName = manager.User.LastName,
                    Email = manager.User.Email,
                    PhoneNumber = manager.User.PhoneNumber,
                    ProfilePicture = manager.User.ProfilePicture,
                }
            };
        }


        private string UploadFile(IFormFile file)
        {
            var appUploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "Upload/images");
            if (!Directory.Exists(appUploadPath))
            {
                Directory.CreateDirectory(appUploadPath);
            }
            var fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var fullPath = Path.Combine(appUploadPath, fileName);
            file.CopyTo(new FileStream(fullPath, FileMode.Create));
            return fileName;
        }



        public BaseResponse<ManagerDto> Delete(string id)
        {
            var manager = _managerRepository.Get(d => d.Id == id);
            if (manager != null)
            {
                manager.IsDeleted = true;
                _managerRepository.Update(manager);
                _managerRepository.Save();

                return new BaseResponse<ManagerDto>
                {
                    Message = "successful",
                    Status = true
                };
            }
            return new BaseResponse<ManagerDto>
            {
                Message = "Manager does not exist",
                Status = false
            };
        }

        public BaseResponse<ManagerDto> Get(string id)
        {
            var manager = _managerRepository.Get(g => g.Id == id || g.UserId == id);
            if (manager != null)
            {

                return new BaseResponse<ManagerDto>
                {
                    Message = "successful",
                    Status = true,
                    Data = new ManagerDto
                    {
                        Id = manager.Id,
                        RegistrationNumber = manager.RegistrationNumber,
                        FirstName = manager.User.FirstName,
                        LastName = manager.User.LastName,
                        Email = manager.User.Email,
                        PhoneNumber = manager.User.PhoneNumber,
                        ProfilePicture = manager.User.ProfilePicture,
                        Address = manager.User.Address
                    },
                };
            }
            return new BaseResponse<ManagerDto>
            {
                Message = "Manager is not fund",
                Status = false
            };
        }

        public BaseResponse<IEnumerable<ManagerDto>> GetAll()
        {
            var managers = _managerRepository.GetAll();
            if (managers == null)
            {
                return new BaseResponse<IEnumerable<ManagerDto>>
                {
                    Message = "No Manager found",
                    Status = false
                };
            }


            return new BaseResponse<IEnumerable<ManagerDto>>
            {
                Message = "Successful",
                Status = true,
                Data = managers.Select(m => new ManagerDto
                {
                    Id = m.Id,
                    RegistrationNumber = m.RegistrationNumber,
                    FirstName = m.User.FirstName,
                    LastName = m.User.LastName,
                    Email = m.User.Email,
                    PhoneNumber = m.User.PhoneNumber,
                    Address = m.User.Address,
                    ProfilePicture = m.User.ProfilePicture,
                })
            };
        }



        public BaseResponse<ManagerDto> Update(string id, UpdateManagerRequestModel model)
        {
            var manager = _managerRepository.Get(a => a.Id == id);
            if (manager is not null)
            {
                var profilePicture = UploadFile(model.ProfilePicture);

                manager.User.FirstName = model.FirstName;
                manager.User.Address = model.Address;
                manager.User.LastName = model.LastName;
                manager.User.ProfilePicture = profilePicture;
                manager.User.Email = model.Email;
                // manager.User.Password = model.Password;
                manager.User.PhoneNumber = model.PhoneNumber;

                _managerRepository.Update(manager);
                _managerRepository.Save();

                return new BaseResponse<ManagerDto>
                {
                    Message = "Manager Updated Successfully",
                    Status = true,
                    Data = new ManagerDto
                    {
                        ProfilePicture = manager.User.ProfilePicture,
                        FirstName = manager.User.FirstName,
                        LastName = manager.User.LastName,
                        PhoneNumber = manager.User.PhoneNumber,
                        Email = manager.User.Email,
                        Address = manager.User.Address,
                    }
                };
            }
            return new BaseResponse<ManagerDto>
            {
                Message = "Unable to Update",
                Status = false,
            };
        }

        public decimal GetCompanyBalance()
        {
            return _adminRepository.GetCompanyWallet();
        }

        private string GenerateManagerRegNum()
        {
            return "FPM/MAG/00" + $"{_managerRepository.GetAll().Count() + 1}";
        }


    }
}