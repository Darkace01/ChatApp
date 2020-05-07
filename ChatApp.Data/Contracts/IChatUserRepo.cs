using ChatApp.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Data.Contracts
{
    public interface IChatUserRepo : ICoreRepo<ChatUser>
    {
        IEnumerable<ChatUser> GetAllChatWithRelationships();
        ChatUser GetChatWithRelationships(int id);
    }
}
