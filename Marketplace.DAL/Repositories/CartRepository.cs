using Marketplace.DAL.Interfaces;
using Marketplace.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.DAL.Repositories
{
    public class CartRepository : IBaseRepository<Cart>
    {
        private readonly AppDBContext _db;

        public CartRepository(AppDBContext dbContext)
        {
            _db = dbContext;
        }

        public async Task Create(Cart entity)
        {
            await _db.Carts.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Cart> GetAll()
        {
            return _db.Carts;
        }

        public async Task Delete(Cart entity)
        {
            _db.Carts.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<Cart> Update(Cart entity)
        {
            _db.Carts.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }

    }
}
