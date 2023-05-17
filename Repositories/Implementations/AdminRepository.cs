using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FarmProduceManagement.AppDbContext;
using FarmProduceManagement.Models.Entities;
using FarmProduceManagement.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FarmProduceManagement.Repositories.Implementations
{
    public class AdminRepository : BaseRepository<Admin>, IAdminRepository
    {
        public AdminRepository(Context context)
        {
            _context = context;
        }

        public Admin GetAdmin()
        {
            
            var role = _context.Roles.FirstOrDefault(r => r.RoleName == "Admin");
            var user = _context.Users.FirstOrDefault(u => u.RoleId == role.Id);
            return _context.Admin
           .Include(a => a.User)
           .FirstOrDefault(a => a.UserId == user.Id && a.IsDeleted == false);
        }

        public decimal GetCompanyWallet()
        {
            var role = _context.Roles.FirstOrDefault(r => r.RoleName == "Admin");
            var user = _context.Users.FirstOrDefault(u => u.RoleId == role.Id);
            var admin = _context.Admin.FirstOrDefault(a => a.UserId == user.Id);
            return admin.CompanyWallet;
        }
    }
}