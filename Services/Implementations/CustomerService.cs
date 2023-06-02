using FarmProduceManagement.Models.Dtos;
using FarmProduceManagement.Models.Entities;
using FarmProduceManagement.Models.Enums;
using FarmProduceManagement.Repositories.Implementations;
using FarmProduceManagement.Repositories.Interfaces;
using FarmProduceManagement.Services.Interfaces;

namespace FarmProduceManagement.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;

        public CustomerService(ICustomerRepository customerRepository, IWebHostEnvironment webHostEnvironment, IRoleRepository roleRepository, IUserRepository userRepository)
        {
            _customerRepository = customerRepository;
            _webHostEnvironment = webHostEnvironment;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }

        public BaseResponse<CustomerDto> Create(CreateCustomerRequestModel model)
        {
            BaseEntity customerExist = _customerRepository.Get(c => c.User.Email == model.Email);
            if (customerExist != null)
            {
                return new BaseResponse<CustomerDto>
                {
                    Message = "Email already exists",
                    Status = false,
                };
            }

           BaseEntity phoneNumber = _customerRepository.Get(c => c.User.PhoneNumber == model.PhoneNumber);
            if (phoneNumber != null)
            {
                return new BaseResponse<CustomerDto>
                {
                    Message = "Phone number already exists",
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
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Address = model.Address,
                Email = model.Email,
                ProfilePicture = profilePicture,

                RoleId = _roleRepository.Get(a => a.RoleName == "Customer").Id,
                CreatedBy = userId,
            };

            var customer = new Customer
            {
                CreatedBy = user.Id,
                RegistrationNumber = GenerateCustomerRegNum(),
                UserId = user.Id,
                User = user,
            };
            // _userRepository.Create(user);
            _customerRepository.Create(customer);
            _customerRepository.Save();

            return new BaseResponse<CustomerDto>
            {
                Message = "Customer successfully added",
                Status = true,
                Data = new CustomerDto
                {
                    Id = customer.Id,
                    RegistrationNumber = customer.RegistrationNumber,
                    // Wallet = farmer.Wallet,
                    FirstName = customer.User.FirstName,
                    LastName = customer.User.LastName,
                    Email = customer.User.Email,
                    PhoneNumber = customer.User.PhoneNumber,
                    ProfilePicture = customer.User.ProfilePicture,
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

        public BaseResponse<CustomerDto> Delete(string id)
        {
            var customer = _customerRepository.Get(d => d.Id == id);        
            if (customer != null)
            {
                customer.IsDeleted = true;
                _customerRepository.Update(customer);
                _customerRepository.Save();

                return new BaseResponse<CustomerDto>
                {
                    Message = "successful",
                    Status = true
                };
            }
            return new BaseResponse<CustomerDto>
            {
                Message = "Customer does not exist",
                Status = false
            };
        }

        public BaseResponse<CustomerDto> Get(string id)
        {
            var customer = _customerRepository.Get(g => g.Id == id || g.UserId == id);
            if (customer != null)
            {

                return new BaseResponse<CustomerDto>
                {
                    Message = "successful",
                    Status = true,
                    Data = new CustomerDto
                    {
                        Id = customer.Id,
                        RegistrationNumber = customer.RegistrationNumber,
                        Wallet = customer.Wallet,
                        FirstName = customer.User.FirstName,
                        LastName = customer.User.LastName,
                        Email = customer.User.Email,
                        PhoneNumber = customer.User.PhoneNumber,
                        ProfilePicture = customer.User.ProfilePicture,
                        Address = customer.User.Address
                    },
                };
            }
            return new BaseResponse<CustomerDto>
            {
                Message = "Customer is not found",
                Status = false
            };
        }



        public BaseResponse<CustomerDto> Update(string id, UpdateCustomerRequestModel model)
        {
            var customer = _customerRepository.Get(a => a.Id == id );
            if (customer is not null)
            {
                var profilePicture = UploadFile(model.ProfilePicture);

                customer.User.FirstName = model.FirstName;
                customer.User.Address = model.Address;
                customer.User.LastName = model.LastName;
                customer.User.ProfilePicture = profilePicture;
                customer.User.Email = model.Email;
                customer.User.PhoneNumber = model.PhoneNumber;

                _customerRepository.Update(customer);
                _customerRepository.Save();

                return new BaseResponse<CustomerDto>
                {
                    Message = "Customer Updated Successfully",
                    Status = true,
                    Data = new CustomerDto
                    {
                        ProfilePicture = customer.User.ProfilePicture,
                        FirstName = customer.User.FirstName,
                        LastName = customer.User.LastName,
                        PhoneNumber = customer.User.PhoneNumber,
                        Email = customer.User.Email,
                        Address = customer.User.Address,
                    }
                };
            }
            return new BaseResponse<CustomerDto>
            {
                Message = "Unable to Update",
                Status = false,
            };
        }
        private string GenerateCustomerRegNum()
        {
            return "FPM/CUS/00" + $"{_customerRepository.GetAll().Count() + 1}";
        }

       
        public BaseResponse<IEnumerable<CustomerDto>> GetAll()
        {
            var customers = _customerRepository.GetAll();
            if (customers == null)
            {
                return new BaseResponse<IEnumerable<CustomerDto>>
                {
                    Message = "No Customer found",
                    Status = false
                };
            }


            return new BaseResponse<IEnumerable<CustomerDto>>
            {
                Message = "Successful",
                Status = true,
                Data = customers.Select(c => new CustomerDto
                {
                    Id = c.Id,
                    RegistrationNumber = c.RegistrationNumber,
                    FirstName = c.User.FirstName,
                    LastName = c.User.LastName,
                    Email = c.User.Email,
                    PhoneNumber = c.User.PhoneNumber,
                    Address = c.User.Address,
                    ProfilePicture = c.User.ProfilePicture,
                })
            };
        }



       
    }
}
