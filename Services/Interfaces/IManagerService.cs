using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Dtos;

namespace FarmProduceManagement.Services.Interfaces
{
    public interface IManagerService
    {
        BaseResponse<ManagerDto> Create(string loginId, CreateManagerRequestModel model);
        // BaseResponse<ManagerDto> Create(string id, CreateManagerRequestModel model);
        BaseResponse<ManagerDto> Update(string id, UpdateManagerRequestModel model);
        BaseResponse<ManagerDto> Get(string id);
        BaseResponse<IEnumerable<ManagerDto>> GetAll();
        BaseResponse<ManagerDto> Delete(string id);
      //  BaseResponse<ManagerDto> GetByEmail(string email);
        decimal GetCompanyBalance();

                
    }
}