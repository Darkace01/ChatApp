using ChatApp.Core;
using ChatApp.Data.Contracts;
using ChatApp.Data.Implementations;
using ChatApp.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly UnitOfWork _uow;
        public UserService(IUnitOfWork uow)
        {
            this._uow = uow as UnitOfWork;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _uow.UserRepo.GetAll();
        }

        public IEnumerable<User> GetAllPossibleFriends(string userId)
        {
            return  _uow.UserRepo.GetAllUsersWithRelationShips().Where(u => u.Id != userId);
        }
    }
}
