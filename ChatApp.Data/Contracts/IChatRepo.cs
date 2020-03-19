using ChatApp.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Data.Contracts
{
    public interface IChatRepo : ICoreRepo<Chat>
    {
        IEnumerable<Chat> GetAllChatWithRelationships();
        Chat GetChatWithRelationships(int id);
    }
}
