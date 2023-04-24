using Marketplace.DAL.Interfaces;
using Marketplace.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.DAL.Repositories
{
    public class ChatMessageRepository : IBaseRepository<ChatMessage>
    {
        private readonly AppDBContext _db;

        public ChatMessageRepository(AppDBContext db)
        {
            _db = db;
        }

        public async Task Create(ChatMessage entity)
        {
            await _db.ChatMessages.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<ChatMessage> GetAll()
        {
            return _db.ChatMessages;
        }

        public async Task Delete(ChatMessage entity)
        {
            _db.ChatMessages.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<ChatMessage> Update(ChatMessage entity)
        {
            _db.ChatMessages.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
