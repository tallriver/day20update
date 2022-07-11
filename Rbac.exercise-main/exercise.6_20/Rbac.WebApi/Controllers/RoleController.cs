using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rbac.Application;
using Rbac.Entity;
using System.Collections.Generic;

namespace Rbac.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        public RoleController(IRoleService role)
        {
            Role = role;
        }

        public IRoleService Role { get; }

        [HttpGet]
        public List<Role> GetRoleAll()
        {
            return Role.GetAll();
        }

        [HttpPost]
        public bool AddRole(Role role)
        {
            return Role.Add(role);
        }

        /// <summary>
        /// 获取角色菜单表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<RoleMenuDto> GetRoleMenuDto(int roleid)
        {
            return Role.GetRoleMenuDto(roleid);
        }

        /// <summary>
        /// 角色菜单表添加前的删除功能
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        [HttpDelete]
        public bool DelMenuRole(int roleid)
        {
            return Role.DelMenuRole(roleid);
        }
    }
}
