using ChatApp.Core;
using ChatApp.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Data.Implementations
{
    public class MessageRepo : CoreRepo<Message>, IMessageRepo
    {
        public MessageRepo(ApplicationDbContext ctx) : base(ctx)
        {

        }
    }
}
