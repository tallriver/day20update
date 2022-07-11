﻿using Rbac.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rbac.Repository
{
    public class MenuRoleRepository: BaseRepository<MenuRole, int>, IMenuRoleRepository
    {
        public MenuRoleRepository(MyDbContext db)
        {
            DbContext = db;
        }
    }
}
