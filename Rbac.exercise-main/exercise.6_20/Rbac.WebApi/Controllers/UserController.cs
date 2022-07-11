using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rbac.Application;

namespace Rbac.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController(IUserService userService)
        {
            UserService = userService;
        }

        public IUserService UserService { get; }

        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ResaultDto AddUserInfo(UserDto user)
        {
            return UserService.AddUserInfo(user);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public TokenDto Login(LoginDto user)
        {
            return UserService.Login(user);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UserPage(int pageindex = 1, int pagesize = 10)
        {
            return Ok(UserService.UserPage(pageindex,pagesize));
        }
    }
}
