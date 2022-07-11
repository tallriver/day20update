using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rbac.Application
{
    public class UserRoleDto
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public int RoleId { get; set; }
    }

}
