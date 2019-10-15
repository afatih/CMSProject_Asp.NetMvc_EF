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
    public class PageService : IPageService
    {
        IRepository<Page> pageRepo;
        IUnitOfWork uow;

        public PageService()
        {
            pageRepo = Resource.UoW.GetRepository<Page>();
            uow = Resource.UoW;
        }


        public ServiceResult Add(PageDto dto)
        {
            pageRepo.Add(EntityFromDto(dto));
            var result = uow.Save();
            return result;

        }

        public ServiceResult Delete(int id)
        {
            Expression<Func<Page, bool>> exp = p => p.Id == id;
            pageRepo.HardDelete(exp);
            var result = uow.Save();
            return result;
        }

        public ServiceResult<PageDto> Get(int id)
        {
            try
            {
                Expression<Func<Page, bool>> exp = p => p.Id == id;
                var result = DtoFromEntity(pageRepo.Get(exp).SingleOrDefault());
                return new ServiceResult<PageDto>(ProcessStateEnum.Success, "İşmeniniz başarılı", result);
            }
            catch (Exception e)
            {
                return new ServiceResult<PageDto>(ProcessStateEnum.Error, e.Message, new PageDto());
            }


        }

        public ServiceResult<List<PageDto>> GetAll()
        {
            try
            {
                Expression<Func<Page, bool>> exp = p => p.Id > 0;
                var result = DtoFromEntity(pageRepo.Get(exp));
                return new ServiceResult<List<PageDto>>(ProcessStateEnum.Success, "İşleminiz Başarılı", result.OrderBy(x => x.RowNumber).ToList());
            }
            catch (Exception e)
            {
                return new ServiceResult<List<PageDto>>(ProcessStateEnum.Success, e.Message, new List<PageDto>());
            }
        }

        public ServiceResult Update(PageDto dto)
        {
            Expression<Func<Page, bool>> exp = p => p.Id == dto.Id;
            var page = pageRepo.Get(exp).SingleOrDefault();
            page.RowNumber = dto.RowNumber;
            page.LanguageId = dto.LanguageId;
            page.SeoDescription = dto.SeoDescription;
            page.SeoTitle = dto.SeoTitle;
            //page.State = dto.State;
            page.Url = dto.Url;
            page.Caption = dto.Caption;
            page.Content = dto.Content;
            page.UpdatedDate = dto.UpdatedDate;
            page.ImageBig = dto.ImageBig;
            page.ImageCover= dto.ImageCover;
            var result = uow.Save();
            return result;
        }

        public ServiceResult ChangeState(int id, bool state)
        {
            Expression<Func<Page, bool>> exp = p => p.Id == id;
            var page = pageRepo.Get(exp).SingleOrDefault();
            page.State = state;
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
                        Expression<Func<Page, bool>> exp = p => p.Id == item.Id;
                        var entity = pageRepo.Get(exp).SingleOrDefault();
                        entity.RowNumber = item.RowNumber;
                    }

                    result = uow.Save();
                }
            }

            return result;
        }

        public ServiceResult<List<PageDto>> GetByLangId(int id)
        {
            try
            {
                Expression<Func<Page, bool>> exp = p => p.LanguageId == id;
                var result = DtoFromEntity(pageRepo.Get(exp));
                return new ServiceResult<List<PageDto>>(ProcessStateEnum.Success, "İşmeniniz başarılı", result.OrderBy(x => x.RowNumber).ToList());
            }
            catch (Exception e)
            {
                return new ServiceResult<List<PageDto>>(ProcessStateEnum.Success, e.Message, new List<PageDto>());
            }
        }


        #region Mappings

        public Page EntityFromDto(PageDto dto)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PageDto, Page>();
            });

            IMapper iMapper = config.CreateMapper();
            var language = iMapper.Map<PageDto, Page>(dto);
            return language;


            //Page page = new Page()
            //{
            //    Caption = dto.Caption,
            //    Content = dto.Content,
            //    LanguageId = dto.LanguageId,
            //    //CreatedDate = dto.CreatedDate,
            //    RowNumber = dto.RowNumber,
            //    SeoDescription = dto.SeoDescription,
            //    SeoKeywords = dto.SeoKeywords,
            //    SeoTitle = dto.SeoTitle,
            //    State = dto.DefaultState,
            //    UpdatedDate = dto.UpdatedDate,
            //    Url = dto.Url,
            //};
            //return page;
        }

        public PageDto DtoFromEntity(Page entitiy)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Page, PageDto>();
            });

            IMapper iMapper = config.CreateMapper();
            var language = iMapper.Map<Page, PageDto>(entitiy);
            return language;


            //PageDto pageDto = new PageDto()
            //{
            //    Id = entitiy.Id,
            //    Caption = entitiy.Caption,
            //    Content = entitiy.Content,
            //    LanguageId = entitiy.LanguageId,
            //    RowNumber = entitiy.RowNumber,
            //    SeoDescription = entitiy.SeoDescription,
            //    SeoKeywords = entitiy.SeoKeywords,
            //    SeoTitle = entitiy.SeoTitle,
            //    State = entitiy.State,
            //    Url = entitiy.Url
            //};
            //return pageDto;
        }

        public List<PageDto> DtoFromEntity(List<Page> pages)
        {
            List<PageDto> list = new List<PageDto>();
            if (pages != null)
            {
                if (pages.Count > 0)
                {
                    foreach (var page in pages)
                    {
                        list.Add(DtoFromEntity(page));
                    }
                }
            }
            return list;
        }


        #endregion
    }
}
