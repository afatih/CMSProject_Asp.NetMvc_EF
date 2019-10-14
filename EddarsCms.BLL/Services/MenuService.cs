using AutoMapper;
using Core.DAL;
using Core.Results;
using EddarsCms.BLL.IServices;
using EddarsCms.DAL;
using EddarsCms.Dto.BasicDtos;
using EddarsCms.Dto.OtherDtos;
using EddarsCms.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EddarsCms.BLL.Services
{
    public class MenuService : IMenuService
    {
        IRepository<Menu> MenuRepo;
        IUnitOfWork uow;

        public MenuService()
        {
            MenuRepo = Resource.UoW.GetRepository<Menu>();
            uow = Resource.UoW;
        }



        public ServiceResult Add(MenuDto dto)
        {
            MenuRepo.Add(EntityFromDto(dto));
            var result = uow.Save();
            return result;
        }

        public ServiceResult Delete(int id)
        {
            Expression<Func<Menu, bool>> exp = p => p.Id == id;
            MenuRepo.HardDelete(exp);
            var result = uow.Save();
            return result;
        }

        public ServiceResult<MenuDto> Get(int id)
        {
            try
            {
                Expression<Func<Menu, bool>> exp = p => p.Id == id;
                var result = DtoFromEntity(MenuRepo.Get(exp).SingleOrDefault());
                return new ServiceResult<MenuDto>(ProcessStateEnum.Success, "İşmeniniz başarılı", result);
            }
            catch (Exception e)
            {
                return new ServiceResult<MenuDto>(ProcessStateEnum.Error, e.Message, new MenuDto());
            }

        }

        public ServiceResult<List<MenuDto>> GetAll()
        {
            try
            {
                Expression<Func<Menu, bool>> exp = p => p.Id > 0;
                var result = DtoFromEntity(MenuRepo.Get(exp));
                if (result != null)
                {
                    if (result.Count > 0)
                    {
                        foreach (var menu in result)
                        {
                            if (menu.MainId != 0)
                            {
                                Expression<Func<Menu, bool>> expMain = p => p.Id == menu.MainId;
                                var mainMenu = MenuRepo.Get(expMain).SingleOrDefault();
                                if (mainMenu!=null)
                                {
                                    menu.MainCaption = mainMenu.Caption;
                                }

                            }
                            else
                            {
                                menu.MainCaption = "Ana Menü";
                            }

                        }
                    }

                }
                return new ServiceResult<List<MenuDto>>(ProcessStateEnum.Success, "İşmeniniz başarılı", result.OrderBy(x => x.RowNumber).ToList());
            }
            catch (Exception e)
            {
                return new ServiceResult<List<MenuDto>>(ProcessStateEnum.Error, e.Message, new List<MenuDto>());
            }
        }

        public ServiceResult Update(MenuDto dto)
        {
            Expression<Func<Menu, bool>> exp = p => p.Id == dto.Id;
            var Menu = MenuRepo.Get(exp).SingleOrDefault();
            Menu.LanguageId = dto.LanguageId;
            Menu.MainId = dto.MainId;
            Menu.Caption = dto.Caption;
            Menu.Url = dto.Url;
            Menu.RowNumber = dto.RowNumber;
            Menu.OpenNewTab = dto.OpenNewTab;
            Menu.UpdatedDate = dto.UpdatedDate;
            var result = uow.Save();
            return result;
        }

        public ServiceResult Reorder(List<ReorderDto> list)
        {
            var result = new ServiceResult(ProcessStateEnum.Error, "İşlem Başarısız");
            if (list != null)
            {
                if (list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        Expression<Func<Menu, bool>> exp = p => p.Id == item.Id;
                        var entity = MenuRepo.Get(exp).SingleOrDefault();
                        entity.RowNumber = item.RowNumber;
                    }
                    result = uow.Save();
                }
            }
            return result;
        }

        public ServiceResult ChangeState(int id, bool state)
        {
            Expression<Func<Menu, bool>> exp = p => p.Id == id;
            var Menu = MenuRepo.Get(exp).SingleOrDefault();
            Menu.State = state;
            var result = uow.Save();
            return result;
        }


        #region Mappings
        public Menu EntityFromDto(MenuDto dto)
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MenuDto, Menu>();
            });

            IMapper iMapper = config.CreateMapper();
            var entity = iMapper.Map<MenuDto, Menu>(dto);
            return entity;

        }

        public MenuDto DtoFromEntity(Menu entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Menu, MenuDto>();
            });

            IMapper iMapper = config.CreateMapper();
            var dto = iMapper.Map<Menu, MenuDto>(entity);
            return dto;
        }

        public List<MenuDto> DtoFromEntity(List<Menu> dtos)
        {
            List<MenuDto> list = new List<MenuDto>();
            if (dtos != null)
            {
                if (dtos.Count > 0)
                {
                    foreach (var dto in dtos)
                    {
                        list.Add(DtoFromEntity(dto));
                    }
                }
            }
            return list;
        }

        #endregion
    }
}
