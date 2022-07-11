using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rbac.Application
{
    public class UserShowDto
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        /// <summary>
        /// 最后一次登录时间
        /// </summary>
        public DateTime? LastLoginDate { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 最后一次登录的IP
        /// </summary>
        public string LastLoginIP { get; set; }

        /// <summary>
        /// 是否锁定
        /// </summary>
        public byte IsLock { get; set; }
        /// <summary>
        /// 删除状态
        /// </summary>
        public bool DelState { get; set; }
    }
}
