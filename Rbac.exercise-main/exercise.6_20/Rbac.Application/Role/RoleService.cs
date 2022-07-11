using AutoMapper;
using Rbac.Entity;
using Rbac.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rbac.Application
{
    public class RoleService: BaseService<Role, Role>,IRoleService
    {
        public RoleService(IRoleRepository role, IMapper mapper,IMenuRoleRepository menuRole) : base(role, mapper)
        {
            Role = role;
            MenuRole = menuRole;
        }

        public IRoleRepository Role { get; }
        public IMenuRoleRepository MenuRole { get; }

        public List<RoleMenuDto> GetRoleMenuDto(int roleid)
        {
            return Mapper.Map<List<RoleMenuDto>>(MenuRole.GetByWhereAll(m=>m.RoleId==roleid));
        }

        /// <summary>
        /// 角色菜单表添加前的删除功能
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public bool DelMenuRole(int roleid)
        {
            return MenuRole.DelAll(m => m.RoleId == roleid);
        }

    }
}
