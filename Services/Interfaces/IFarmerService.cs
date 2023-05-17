using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Dtos;

namespace FarmProduceManagement.Services.Interfaces
{
    public interface IFarmerService
    {
        BaseResponse<FarmerDto> Create(CreateFarmerRequestModel model);
        BaseResponse<FarmerDto> Update(string id, UpdateFarmerRequestModel model);
        BaseResponse<FarmerDto> Get(string id);
        // BaseResponse<IEnumerable<FarmerDto>> GetAll(Func<FarmerDto, bool> expression);
        BaseResponse<IEnumerable<FarmerDto>> GetAll();
        BaseResponse<FarmerDto> Delete(string id);
       BaseResponse<IEnumerable<FarmerDto>> GetPendingFarmers();
       BaseResponse<IEnumerable<FarmerDto>> ApprovedFarmers();
       BaseResponse<IEnumerable<FarmerDto>> GetDeclinedFarmers();
       BaseResponse<FarmerDto> VerifyFarmers(ApproveFarmerDto model);
      
    }
}