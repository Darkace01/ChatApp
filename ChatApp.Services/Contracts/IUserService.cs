using ChatApp.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Services.Contracts
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        IEnumerable<User> GetAllPossibleFriends(string userId);
    }
}
