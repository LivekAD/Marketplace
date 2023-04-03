using Marketplace.DAL.Interfaces;
using Marketplace.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.DAL.Repositories
{
    public class BidRepository : IBaseRepository<Bid>
    {
        private readonly AppDBContext _db;

        public BidRepository(AppDBContext db)
        {
            _db = db;
        }

        public async Task Create(Bid entity)
        {
            await _db.Bids.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Bid entity)
        {
            _db.Bids.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Bid> GetAll()
        {
            return _db.Bids;
        }

        public async Task<Bid> Update(Bid entity)
        {
            _db.Bids.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
