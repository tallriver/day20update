using Rbac.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Rbac.Repository;
using System.Security.Cryptography;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using IdentityModel;
using Microsoft.AspNetCore.Http;

namespace Rbac.Application
{
    public class UserService: BaseService<User, UserDto>, IUserService
    {
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor accessor;
        private readonly IUserRoleRepository userRoleRepository;

        public UserService(IUserRepository userRepository,IMapper mapper, IConfiguration configuration, IHttpContextAccessor accessor, IUserRoleRepository userRoleRepository) : base(userRepository,mapper)
        {
            UserRepository = userRepository;
            this.mapper = mapper;
            this.configuration = configuration;
            this.accessor = accessor;
            this.userRoleRepository = userRoleRepository;
        }

        public IUserRepository UserRepository { get; }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ResaultDto AddUserInfo(UserDto user)
        {
            if (UserRepository.GetByWhere(m => m.UserName == user.UserName.Trim().ToUpper()) != null)
            {
                return new ResaultDto { Code = false, Msg = "该用户名已存在" };
            }
            else
            {
                user.LastLoginDate = null;
                user.CreateTime = DateTime.Now;
                user.Password = Md5(user.Password.Trim());
                user.UserName = user.UserName.Trim().ToUpper();

                var entity = mapper.Map<User>(user);
                UserRepository.Add(entity);

                //用户角色中间表的添加
                var list = user.RoleId.Select(m => new UserRole
                {
                    RoleId=m,
                    UserId=entity.UserId,
                }).ToList();
                userRoleRepository.AddAll(list);

                return new ResaultDto { Code = true, Msg = "用户添加成功" };
            }
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        private string Md5(string val)
        {
            return string.Join("", MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(val)).Select(x => x.ToString("x2")));
        }

        //string code=Request.Cookies.TryGetValue("userFavorite", out string value);
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public TokenDto Login(LoginDto user)
        {
            var code=accessor.HttpContext.Request.Cookies["Code"];
            if (user.CheckCode != code.Trim().ToLower())
            {
                return new TokenDto { Code = false, Msg = "验证码错误" };
            }
            var list = UserRepository.GetByWhere(m => m.UserName == user.UserName.Trim());
            if (list==null)
            {
                return new TokenDto { Code = false, Msg = "该用户名不存在" };
            }
            if (list.Password.ToLower() != Md5(user.PassWord.Trim().ToLower()))
            {
                return new TokenDto { Code = false, Msg = "密码输入有误" };
            }
            //生成Token令牌
            IList<Claim> claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Id, user.UserName)
            };

            //JWT密钥
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfig:key"]));

            //算法，签名证书
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //过期时间
            DateTime expires = DateTime.UtcNow.AddHours(10);


            //Payload负载
            var token = new JwtSecurityToken(
                issuer: configuration["JwtConfig:Issuer"], //发布者、颁发者
                audience: configuration["JwtConfig:Audience"],  //令牌接收者
                claims: claims, //自定义声明信息
                notBefore: DateTime.UtcNow,  //创建时间
                expires: expires,   //过期时间
                signingCredentials: cred
                );

            var handler = new JwtSecurityTokenHandler();

            //生成令牌
            string jwt = handler.WriteToken(token);

            return new TokenDto { Code = true, Msg = "登录成功", Token = jwt };
        }


         public Tuple<List<UserShowDto>,int> UserPage(int pageindex=1,int pagesize=10)
        {
            var list =mapper.Map<List<UserShowDto>>(UserRepository.GetQueryable()).OrderBy(m => m.UserId).Skip((pageindex - 1) * pagesize).Take(pagesize).ToList();
            var TotalCount = UserRepository.GetQueryable().Count();
            return new Tuple<List<UserShowDto>, int>(list, TotalCount);
        }
    }
}
