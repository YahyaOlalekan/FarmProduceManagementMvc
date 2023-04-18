using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FarmProduceManagement.AppDbContext
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Admin> Admin { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Farmer> Farmers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Produce> Produces { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionProduce> TransactionProduces { get; set; }
        public DbSet<FarmerManager> FarmerManagers { get; set; }

    }
}