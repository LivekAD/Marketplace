using Marketplace.DAL.Interfaces;
using Marketplace.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.DAL.Repositories
{
    public class ProfileRepository : IBaseRepository<Profile>
    {
        private readonly AppDBContext _dbContext;

        public ProfileRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(Profile entity)
        {
            await _dbContext.Profiles.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<Profile> GetAll()
        {
            return _dbContext.Profiles;
        }

        public async Task Delete(Profile entity)
        {
            _dbContext.Profiles.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Profile> Update(Profile entity)
        {
            _dbContext.Profiles.Update(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }
    }
}
