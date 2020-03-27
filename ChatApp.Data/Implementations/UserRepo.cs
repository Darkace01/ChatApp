using ChatApp.Core;
using ChatApp.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Data.Implementations
{
    public class UserRepo : CoreRepo<User>, IUserRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<User> _DbSet;
        public UserRepo(ApplicationDbContext ctx) : base(ctx){
            this._context = ctx;
            this._DbSet = this._context.Set<User>();
        }

        public IEnumerable<User> GetAllUsersWithRelationShips(){
            return _DbSet;
                
        }
    }
}
