using Marketplace.DAL.Interfaces;
using Marketplace.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;


namespace Marketplace.DAL.Repositories
{
    public class ProductPhotoRepository : IBaseRepository<ProductPhoto>
    {
        private readonly AppDBContext _db;

        public ProductPhotoRepository(AppDBContext db)
        {
            _db = db;
        }

        public async Task Create(ProductPhoto entity)
        {
            await _db.ProductPhotos.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(ProductPhoto entity)
        {
            _db.ProductPhotos.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<ProductPhoto> GetAll()
        {
            return _db.ProductPhotos;
        }

        public async Task<ProductPhoto> Update(ProductPhoto entity)
        {
            _db.ProductPhotos.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
