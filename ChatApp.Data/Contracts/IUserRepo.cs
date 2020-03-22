using ChatApp.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Data.Contracts
{
    public interface IUserRepo : ICoreRepo<User>
    {
        IEnumerable<User> GetAllUsersWithRelationShips();
    }
}
