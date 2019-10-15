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
    public class NewsService:INewsService
    {
        IRepository<News> NewsRepo;
        IUnitOfWork uow;

        public NewsService()
        {
            NewsRepo = Resource.UoW.GetRepository<News>();
            uow = Resource.UoW;
        }



        public ServiceResult Add(NewsDto dto)
        {
            NewsRepo.Add(EntityFromDto(dto));
            var result = uow.Save();
            return result;
        }

        public ServiceResult Delete(int id)
        {
            Expression<Func<News, bool>> exp = p => p.Id == id;
            NewsRepo.HardDelete(exp);
            var result = uow.Save();
            return result;
        }

        public ServiceResult<NewsDto> Get(int id)
        {
            try
            {
                Expression<Func<News, bool>> exp = p => p.Id == id;
                var result = DtoFromEntity(NewsRepo.Get(exp).SingleOrDefault());
                return new ServiceResult<NewsDto>(ProcessStateEnum.Success, "İşmeniniz başarılı", result);
            }
            catch (Exception e)
            {
                return new ServiceResult<NewsDto>(ProcessStateEnum.Error, e.Message, new NewsDto());
            }

        }

        public ServiceResult<List<NewsDto>> GetAll()
        {
            try
            {
                Expression<Func<News, bool>> exp = p => p.Id > 0;
                var result = DtoFromEntity(NewsRepo.Get(exp));
                return new ServiceResult<List<NewsDto>>(ProcessStateEnum.Success, "İşmeniniz başarılı", result.OrderBy(x => x.RowNumber).ToList());
            }
            catch (Exception e)
            {
                return new ServiceResult<List<NewsDto>>(ProcessStateEnum.Success, e.Message, new List<NewsDto>());
            }
        }

        public ServiceResult Update(NewsDto dto)
        {
            Expression<Func<News, bool>> exp = p => p.Id == dto.Id;
            var News = NewsRepo.Get(exp).SingleOrDefault();
            News.LanguageId = dto.LanguageId;
            News.Caption = dto.Caption;
            News.Image = dto.Image;
            News.Content = dto.Content;
            News.UpdatedDate = dto.UpdatedDate;
            News.RowNumber = dto.RowNumber;
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
                        Expression<Func<News, bool>> exp = p => p.Id == item.Id;
                        var entity = NewsRepo.Get(exp).SingleOrDefault();
                        entity.RowNumber = item.RowNumber;
                    }

                    result = uow.Save();
                }
            }

            return result;
        }

        public ServiceResult ChangeState(int id, bool state)
        {
            Expression<Func<News, bool>> exp = p => p.Id == id;
            var News = NewsRepo.Get(exp).SingleOrDefault();
            News.State = state;
            var result = uow.Save();
            return result;
        }

        public ServiceResult<List<NewsDto>> GetByLangId(int id)
        {
            try
            {
                Expression<Func<News, bool>> exp = p => p.LanguageId == id;
                var result = DtoFromEntity(NewsRepo.Get(exp));
                return new ServiceResult<List<NewsDto>>(ProcessStateEnum.Success, "İşmeniniz başarılı", result.OrderBy(x => x.RowNumber).ToList());
            }
            catch (Exception e)
            {
                return new ServiceResult<List<NewsDto>>(ProcessStateEnum.Success, e.Message, new List<NewsDto>());
            }
        }


        #region Mappings
        public News EntityFromDto(NewsDto dto)
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<NewsDto, News>();
            });

            IMapper iMapper = config.CreateMapper();
            var entity = iMapper.Map<NewsDto, News>(dto);
            return entity;

        }

        public NewsDto DtoFromEntity(News entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<News, NewsDto>();
            });

            IMapper iMapper = config.CreateMapper();
            var dto = iMapper.Map<News, NewsDto>(entity);
            return dto;
        }

        public List<NewsDto> DtoFromEntity(List<News> dtos)
        {
            List<NewsDto> list = new List<NewsDto>();
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
