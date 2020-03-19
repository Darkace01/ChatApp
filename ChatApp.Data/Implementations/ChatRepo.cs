using ChatApp.Core;
using ChatApp.Data.Contracts;
using ChatApp.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ChatApp.Data.Implementations
{
    public class ChatRepo : CoreRepo<Chat>, IChatRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Chat> _DbSet;
        public ChatRepo(ApplicationDbContext ctx) : base(ctx)
        {
            this._context = ctx;
            this._DbSet = this._context.Set<Chat>();
        }

        public IEnumerable<Chat> GetAllChatWithRelationships(){
            return _DbSet
                .Include( c => c.Users)
                .ThenInclude(c => c.User)
                .Include( c => c.Messages);
        
        }

        public Chat GetChatWithRelationships(int id)
        {
            return _DbSet.Where(c => c.Id == id)
                .Include(c => c.Users)
                .ThenInclude(c => c.User)
                .Include(c=>c.Messages)
                .FirstOrDefault();
        }
    }
}
