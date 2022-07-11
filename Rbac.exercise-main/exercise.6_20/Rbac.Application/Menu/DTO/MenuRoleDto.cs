using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rbac.Application
{
    public class MenuRoleDto
    {
        public int Id { get; set; }
        public int[] MenuId { get; set; }
        public int RoleId { get; set; }

    }
}
