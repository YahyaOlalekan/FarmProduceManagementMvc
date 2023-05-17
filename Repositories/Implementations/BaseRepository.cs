using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmProduceManagement.AppDbContext;
using FarmProduceManagement.Models.Entities;
using FarmProduceManagement.Repositories.Interfaces;

namespace FarmProduceManagement.Repositories.Implementations
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity, new()
    {
        protected Context _context;

        public T Create(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public T Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return entity;
        }
        public T Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return entity;
        }
    }
}