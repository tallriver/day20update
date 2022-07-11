using AutoMapper;
using Rbac.Entity;
using Rbac.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rbac.Application
{
    public class MenuService : BaseService<Menu,MenuAddDto>,IMenuService
    {
        public IMenuRepository MenuRepository { get; }
        public IMenuRoleRepository MenuRole { get; }

        public MenuService(IMenuRepository menuRepository, IMapper mapper, IMenuRoleRepository menuRole) :base(menuRepository,mapper)
        {
            MenuRepository = menuRepository;
            MenuRole = menuRole;
        }

        public List<MenuDto> GetMenuAll()
        {
            var list=MenuRepository.GetInfoAll();
            List<MenuDto> result=new List<MenuDto>();

            var menudto = list.Where(m => m.ParentId == 0).Select(m => new MenuDto
            {
                MenuId = m.MenuId,
                MenuName = m.MenuName,
                LinkUrl = m.LinkUrl,
            }).ToList();

            GetNodes(menudto);

            return menudto;
        }


        private void GetNodes(List<MenuDto> menus)
        {
            var list = MenuRepository.GetInfoAll();

            foreach (var item in menus)
            {
                var obj = list.Where(m => m.ParentId == item.MenuId).Select(m => new MenuDto
                {
                    MenuId = m.MenuId,
                    MenuName = m.MenuName,
                    LinkUrl = m.LinkUrl,
                }).ToList();

                item.Children.AddRange(obj);

                GetNodes(obj);
            }
        }

        public List<MenuListDto> GetList()
        {
            var list = MenuRepository.GetInfoAll();
            List<MenuListDto> result = new List<MenuListDto>();

            var menudto = list.Where(m => m.ParentId == 0).Select(m => new MenuListDto
            {
                value = m.MenuId,
                label = m.MenuName,
            }).ToList();

            GetNodesList(menudto);

            return menudto;
        }

        private void GetNodesList(List<MenuListDto> menus)
        {
            var list = MenuRepository.GetInfoAll();

            foreach (var item in menus)
            {
                var obj = list.Where(m => m.ParentId == item.value).Select(m => new MenuListDto
                {
                    value = m.MenuId,
                    label = m.MenuName,
                }).ToList();

                item.children.AddRange(obj);

                GetNodesList(obj);
            }
        }

        public bool Create(Menu menu)
        {
            menu.CreateTime = DateTime.Now;
            return MenuRepository.Add(menu);
        }

        public bool UpdMenu(Menu menu)
        {
            return MenuRepository.UpdMenu(menu);
        }

        public List<MenuShowDto> GetMenuShow()
        {
            return Mapper.Map<List<MenuShowDto>>(MenuRepository.GetInfoAll());
        }

        public bool SaveMenuRole(MenuRoleDto menuRole)
        {
            var ids = menuRole.MenuId.Select(m => new MenuRole { MenuId = m, RoleId = menuRole.RoleId }).ToList();
            return MenuRole.AddAll(ids);
        }



        public List<MenuDto> Get()
        {
            var list = MenuRepository.GetInfoAll();
            List<MenuDto> result = new List<MenuDto>();

            var menulist = list.Where(m => m.ParentId == 0).Select(m=>new MenuDto
            {
                MenuName = m.MenuName,
                MenuId = m.MenuId,
                LinkUrl = m.LinkUrl,
            }).ToList();

            GetChlidren(menulist);
            return menulist;
        }

        public void GetChlidren(List<MenuDto> menus)
        {
            var list = MenuRepository.GetInfoAll();
            foreach (var item in menus)
            {
                var menulist = list.Where(m => m.ParentId == item.MenuId).Select(m => new MenuDto
                {
                    MenuName = m.MenuName,
                    MenuId = m.MenuId,
                    LinkUrl = m.LinkUrl,
                }).ToList();
                
                item.Children.AddRange(menulist);
                GetChlidren(menulist);
            }
        }
    }
}
