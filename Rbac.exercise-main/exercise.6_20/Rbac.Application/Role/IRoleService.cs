﻿using Rbac.Entity;
using Rbac.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rbac.Application
{
    public interface IRoleService: IBaseService<Role, Role>
    {
        List<RoleMenuDto> GetRoleMenuDto(int roleid);
        bool DelMenuRole(int roleid);
    }
}
