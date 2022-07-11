using Rbac.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rbac.Repository
{
    public class UserRoleRepository: BaseRepository<UserRole,int>,IUserRoleRepository
    {
        public UserRoleRepository(MyDbContext db)
        {
            DbContext = db;
        }
    }
}
