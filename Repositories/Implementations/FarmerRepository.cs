using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FarmProduceManagement.AppDbContext;
using FarmProduceManagement.Models.Dtos;
using FarmProduceManagement.Models.Entities;
using FarmProduceManagement.Models.Enums;
using FarmProduceManagement.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FarmProduceManagement.Repositories.Implementations
{
    public class FarmerRepository : BaseRepository<Farmer>, IFarmerRepository
    {
        public FarmerRepository(Context context)
        {
            _context = context;
        }

        public Farmer Get(string id)
        {
            return _context.Farmers
            // .Where(a => a.FarmerRegStatus == FarmerRegStatus.Approved)
            .Include(a => a.Transactions)
             .Include(a => a.User)
             .FirstOrDefault(a => a.Id == id && a.IsDeleted == false);
        }

        public Farmer Get(Expression<Func<Farmer, bool>> expression)
        {
            return _context.Farmers
            .Where(a => a.IsDeleted == false)
            // .Where(a => a.FarmerRegStatus == FarmerRegStatus.Approved && a.IsDeleted == false)
            .Include(a => a.Transactions)
             .Include(a => a.User)
            .FirstOrDefault(expression);
        }

        public IEnumerable<Farmer> GetAll(Func<Farmer, bool> expression)
        {
            return _context.Farmers
            .Where(a => !a.IsDeleted)
            // .Where(a => a.IsDeleted == false && a.FarmerRegStatus == FarmerRegStatus.Approved)
            .Include(a => a.User)
            .Include(a => a.Transactions)
            .Where(expression)
            .ToList();
        }



        public IEnumerable<Farmer> GetAll()
        {
            return _context.Farmers
            .Where(a => !a.IsDeleted)
            // .Where(a => a.IsDeleted == false && a.FarmerRegStatus == FarmerRegStatus.Approved)
            .Include(a => a.User)
            .Include(a => a.Transactions)
            .ToList();
        }


        public IEnumerable<Farmer> GetSelected(List<string> ids)
        {
            return _context.Farmers
            .Where(a => ids.Contains(a.Id) && a.IsDeleted == false)
            // .Where(a => ids.Contains(a.Id) && a.IsDeleted == false && a.FarmerRegStatus == FarmerRegStatus.Approved)
            .Include(a => a.User)
            .Include(a => a.Transactions)
            .ToList();
        }



        public IEnumerable<Farmer> GetSelected(Expression<Func<Farmer, bool>> expression)
        {
            return _context.Farmers
            .Where(expression)
            .Include(a => a.Transactions)
            .Include(a => a.User)
            // .Where(a => a.FarmerRegStatus == FarmerRegStatus.Approved)
            .ToList();
        }


    }
}