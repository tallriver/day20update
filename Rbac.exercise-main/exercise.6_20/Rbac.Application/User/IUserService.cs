using Rbac.Entity;
using Rbac.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rbac.Application
{
    public interface IUserService: IBaseService<User,UserDto>
    {
        ResaultDto AddUserInfo(UserDto user);
        TokenDto Login(LoginDto user);
        Tuple<List<UserShowDto>, int> UserPage(int pageindex = 1, int pagesize = 10);
    }
}
