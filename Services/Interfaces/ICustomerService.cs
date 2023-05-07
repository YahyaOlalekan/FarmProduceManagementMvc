using FarmProduceManagement.Models.Dtos;

namespace FarmProduceManagement.Services.Interfaces
{
    public interface ICustomerService
    {
        BaseResponse<CustomerDto> Create(CreateCustomerRequestModel model);
        BaseResponse<CustomerDto> Update(string id, UpdateCustomerRequestModel model);
        BaseResponse<CustomerDto> Get(string id);
        //BaseResponse<IEnumerable<CustomerDto>> GetAll(Func<CustomerDto, bool> expression);
        BaseResponse<IEnumerable<CustomerDto>> GetAll();
        BaseResponse<CustomerDto> Delete(string id);
       
    }
}
