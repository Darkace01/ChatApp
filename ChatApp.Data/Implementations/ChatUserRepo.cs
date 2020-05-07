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
    public class ChatUserRepo : CoreRepo<ChatUser>, IChatUserRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<ChatUser> _DbSet;
        public ChatUserRepo(ApplicationDbContext ctx) : base(ctx)
        {
            this._context = ctx;
            this._DbSet = this._context.Set<ChatUser>();
        }

        public IEnumerable<ChatUser> GetAllChatWithRelationships(){
            return _DbSet
                .Include( c => c.Chat)
                .ThenInclude(c => c.Messages)
                .Include( c => c.User);
        
        }

        public ChatUser GetChatWithRelationships(int id)
        {
            return _DbSet.Where(c => c.ChatId == id)
                .Include( c => c.Chat)
                .ThenInclude(c => c.Messages)
                .Include( c => c.User)
                .FirstOrDefault();
        }
    }
}
