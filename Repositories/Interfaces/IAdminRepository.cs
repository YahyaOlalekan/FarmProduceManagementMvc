using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Entities;

namespace FarmProduceManagement.Repositories.Interfaces
{
    public interface IAdminRepository  : IBaseRepository<Admin>
    {
        Admin GetAdmin();
        decimal GetCompanyWallet();
    }
}