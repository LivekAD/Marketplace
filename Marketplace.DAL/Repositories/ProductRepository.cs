using Marketplace.DAL.Interfaces;
using Marketplace.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.DAL.Repositories
{
    public class ProductRepository : IBaseRepository<Product>
    {
        private readonly AppDBContext _db;

        public ProductRepository(AppDBContext db)
        {
            _db = db;
        }

        public async Task Create(Product entity)
        {
            await _db.Products.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Product entity)
        {
            _db.Products.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Product> GetAll()
        {
            return _db.Products;
        }

        public async Task<Product> Update(Product entity)
        {
            _db.Products.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
