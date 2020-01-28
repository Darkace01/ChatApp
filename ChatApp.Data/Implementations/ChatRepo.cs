using ChatApp.Core;
using ChatApp.Data.Contracts;
using ChatApp.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Data.Implementations
{
    public class ChatRepo : CoreRepo<Chat>, IChatRepo
    {
        public ChatRepo(ApplicationDbContext ctx) : base(ctx)
        {

        }
    }
}
