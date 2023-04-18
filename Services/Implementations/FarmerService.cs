using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Dtos;
using FarmProduceManagement.Models.Entities;
using FarmProduceManagement.Models.Enums;
using FarmProduceManagement.Repositories.Interfaces;
using FarmProduceManagement.Services.Interfaces;

namespace FarmProduceManagement.Services.Implementations
{
    public class FarmerService : IFarmerService
    {
        private readonly IFarmerRepository _farmerRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;

        public FarmerService(IFarmerRepository farmerRepository, IWebHostEnvironment webHostEnvironment, IRoleRepository roleRepository, IUserRepository userRepository)
        {
            _farmerRepository = farmerRepository;
            _webHostEnvironment = webHostEnvironment;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }

        public BaseResponse<FarmerDto> Create(CreateFarmerRequestModel model)
        {
            var farmerExist = _farmerRepository.Get(f => f.User.Email == model.Email);
            //BaseEntity farmerExist = _farmerRepository.Get(f => f.User.Email == model.Email);
            if (farmerExist != null)
            {
                return new BaseResponse<FarmerDto>
                {
                    Message = "Invalid details",
                    Status = false,
                };
            }

            var profilePicture = UploadFile(model.ProfilePicture);
            var userId = Guid.NewGuid().ToString();

            var user = new User
            {
                Id = userId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Password = model.Password,
                Address = model.Address,
                Email = model.Email,
                ProfilePicture = profilePicture,

                RoleId = _roleRepository.Get(a => a.RoleName == "Farmer").Id,
                CreatedBy = userId,
            };

            var farmer = new Farmer
            {
                CreatedBy = user.Id,
                RegistrationNumber = GenerateFarmerRegNum(),
               // Wallet = model.Wallet,
                UserId = user.Id,
                User = user,
                FarmerRegStatus = FarmerRegStatus.Pending,
            };
            // _userRepository.Create(user);
            _farmerRepository.Create(farmer);
            _farmerRepository.Save();

            return new BaseResponse<FarmerDto>
            {
                Message = "Farmer successfully added",
                Status = true,
                Data = new FarmerDto
                {
                    Id = farmer.Id,
                    RegistrationNumber = farmer.RegistrationNumber,
                    Wallet = farmer.Wallet,
                    FirstName = farmer.User.FirstName,
                    LastName = farmer.User.LastName,
                    Email = farmer.User.Email,
                    PhoneNumber = farmer.User.PhoneNumber,
                    ProfilePicture = farmer.User.ProfilePicture,
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

        public BaseResponse<FarmerDto> Delete(string id)
        {
            var farmer = _farmerRepository.Get(d => d.Id == id && d.FarmerRegStatus == Models.Enums.FarmerRegStatus.Approved);
            if (farmer != null)
            {
                farmer.IsDeleted = true;
                _farmerRepository.Update(farmer);
                _farmerRepository.Save();

                return new BaseResponse<FarmerDto>
                {
                    Message = "successful",
                    Status = true
                };
            }
            return new BaseResponse<FarmerDto>
            {
                Message = "Farmer does not exist",
                Status = false
            };
        }

        public BaseResponse<FarmerDto> Get(string id)
        {
            var farmer = _farmerRepository.Get(g => g.Id == id && g.FarmerRegStatus == Models.Enums.FarmerRegStatus.Approved);
            if (farmer != null)
            {

                return new BaseResponse<FarmerDto>
                {
                    Message = "successful",
                    Status = true,
                    Data = new FarmerDto
                    {
                        Id = farmer.Id,
                        RegistrationNumber = farmer.RegistrationNumber,
                        Wallet = farmer.Wallet,
                        FirstName = farmer.User.FirstName,
                        LastName = farmer.User.LastName,
                        Email = farmer.User.Email,
                        PhoneNumber = farmer.User.PhoneNumber,
                        ProfilePicture = farmer.User.ProfilePicture,
                        Address = farmer.User.Address
                    },
                };
            }
            return new BaseResponse<FarmerDto>
            {
                Message = "Farmer is not found",
                Status = false
            };
        }


        public BaseResponse<IEnumerable<FarmerDto>> GetAll()
        {
            var farmers = _farmerRepository.GetAll();
            if (farmers.Count() == 0)
            {
                return new BaseResponse<IEnumerable<FarmerDto>>
                {
                    Message = "No Farmer found",
                    Status = false
                };
            }

            var approvedFarmer = farmers.Where(x => x.FarmerRegStatus == FarmerRegStatus.Approved);
            return new BaseResponse<IEnumerable<FarmerDto>>
            {
                Message = "Successful",
                Status = true,
                Data = farmers.Select(f => new FarmerDto
                {
                    Id = f.Id,
                    RegistrationNumber = f.RegistrationNumber,
                    Wallet = f.Wallet,
                    FirstName = f.User.FirstName,
                    LastName = f.User.LastName,
                    Email = f.User.Email,
                    PhoneNumber = f.User.PhoneNumber,
                    Address = f.User.Address,
                    ProfilePicture = f.User.ProfilePicture,
                })
            };
        }


        public BaseResponse<List<FarmerDto>> ApprovePendingFarmers()
        {
            // var approvedFarmer = GetPendingFarmers();
            var approvedFarmer = _farmerRepository.GetAll(x => x.FarmerRegStatus == FarmerRegStatus.Pending);

            foreach (var item in approvedFarmer)
            {
                item.FarmerRegStatus = FarmerRegStatus.Approved;

            }
            _farmerRepository.Save();

            //    for(int i = 0; i < approvedFarmer.Data.Count(); i++)
            //    {
            //         approvedFarmer.Data[i].FarmerRegStatus = FarmerRegStatus.Approved;
            //          _farmerRepository.Save();
            //    }

            return new BaseResponse<List<FarmerDto>>
            {
                Message = "Successful",
                Status = true,
                Data = approvedFarmer.Select(f => new FarmerDto
                {
                    Id = f.Id,
                    RegistrationNumber = f.RegistrationNumber,
                    Wallet = f.Wallet,
                    FirstName = f.User.FirstName,
                    LastName = f.User.LastName,
                    Email = f.User.Email,
                    PhoneNumber = f.User.PhoneNumber,
                    Address = f.User.Address,
                    ProfilePicture = f.User.ProfilePicture,
                }).ToList()
            };
        }
        public BaseResponse<List<FarmerDto>> GetPendingFarmers()
        {
            var penderFarmers = _farmerRepository.GetAll();
            var farmers = penderFarmers.Where(x => x.FarmerRegStatus == FarmerRegStatus.Pending).ToList();
            if (farmers.Count() == 0)
            {
                return new BaseResponse<List<FarmerDto>>
                {
                    Message = "No Farmer found",
                    Status = false
                };
            }

            return new BaseResponse<List<FarmerDto>>
            {
                Message = "Successful",
                Status = true,
                Data = farmers.Select(f => new FarmerDto
                {
                    Id = f.Id,
                    RegistrationNumber = f.RegistrationNumber,
                    Wallet = f.Wallet,
                    FirstName = f.User.FirstName,
                    LastName = f.User.LastName,
                    Email = f.User.Email,
                    PhoneNumber = f.User.PhoneNumber,
                    Address = f.User.Address,
                    ProfilePicture = f.User.ProfilePicture,
                }).ToList()
            };
        }


        public BaseResponse<FarmerDto> Update(string id, UpdateFarmerRequestModel model)
        {
            var farmer = _farmerRepository.Get(a => a.Id == id && a.FarmerRegStatus == FarmerRegStatus.Approved);
            if (farmer is not null)
            {
                var profilePicture = UploadFile(model.ProfilePicture);

                farmer.User.FirstName = model.FirstName;
                farmer.User.Address = model.Address;
                farmer.User.LastName = model.LastName;
                farmer.User.ProfilePicture = profilePicture;
                farmer.User.Email = model.Email;
                // farmer.User.Password = model.Password;
                //farmer.Wallet = model.Wallet;
                farmer.User.PhoneNumber = model.PhoneNumber;

                _farmerRepository.Update(farmer);
                _farmerRepository.Save();

                return new BaseResponse<FarmerDto>
                {
                    Message = "Farmer Updated Successfully",
                    Status = true,
                    Data = new FarmerDto
                    {
                        ProfilePicture = farmer.User.ProfilePicture,
                        FirstName = farmer.User.FirstName,
                        LastName = farmer.User.LastName,
                        PhoneNumber = farmer.User.PhoneNumber,
                        Email = farmer.User.Email,
                        Address = farmer.User.Address,
                    }
                };
            }
            return new BaseResponse<FarmerDto>
            {
                Message = "Unable to Update",
                Status = false,
            };
        }
        private string GenerateFarmerRegNum()
        {
            return "FPM/FAR/00" + $"{_farmerRepository.GetAll().Count() + 1}";
        }


    }
}