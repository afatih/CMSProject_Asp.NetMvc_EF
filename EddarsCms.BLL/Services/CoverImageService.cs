using AutoMapper;
using Core.DAL;
using Core.Results;
using EddarsCms.BLL.IServices;
using EddarsCms.DAL;
using EddarsCms.Dto.BasicDtos;
using EddarsCms.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EddarsCms.BLL.Services
{
    public class CoverImageService:ICoverImageService
    {
        IRepository<CoverImage> coverImageRepo;
        IUnitOfWork uow;

        public CoverImageService()
        {
            coverImageRepo = Resource.UoW.GetRepository<CoverImage>();
            uow = Resource.UoW;
        }




        public ServiceResult AddOrUpdate(CoverImageDto dto)
        {
            if (dto.Id == 0)
            {
                coverImageRepo.Add(EntityFromDto(dto));
                var result = uow.Save();
                return result;
            }
            else
            {
                Expression<Func<CoverImageDto, bool>> exp = p => p.Id == dto.Id;
                var entity = coverImageRepo.Get(x => x.Id == dto.Id).SingleOrDefault();
                entity.Blog = dto.Blog;
                entity.Duty = dto.Duty;
                entity.Product = dto.Product;
                entity.HumanResource = dto.HumanResource;
                entity.Contact = dto.Contact;
                entity.News= dto.News;

                var result = uow.Save();
                return result;

            }
        }

        public ServiceResult<CoverImageDto> Get()
        {
            try
            {
                var result = coverImageRepo.Get(x => x.Id > 0).SingleOrDefault();
                if (result != null)
                {
                    return new ServiceResult<CoverImageDto>(ProcessStateEnum.Success, "", DtoFromEntity(result));
                }
                else
                {
                    return new ServiceResult<CoverImageDto>(ProcessStateEnum.Error, "", new CoverImageDto());
                }
            }
            catch (Exception e)
            {

                return new ServiceResult<CoverImageDto>(ProcessStateEnum.Error, e.Message, new CoverImageDto());
            }

        }

        #region Mappings
        public CoverImage EntityFromDto(CoverImageDto dto)
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CoverImageDto, CoverImage>();
            });

            IMapper iMapper = config.CreateMapper();
            var entity = iMapper.Map<CoverImageDto, CoverImage>(dto);
            return entity;

        }

        public CoverImageDto DtoFromEntity(CoverImage entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CoverImage, CoverImageDto>();
            });

            IMapper iMapper = config.CreateMapper();
            var dto = iMapper.Map<CoverImage, CoverImageDto>(entity);
            return dto;
        }

        public List<CoverImageDto> DtoFromEntity(List<CoverImage> dtos)
        {
            List<CoverImageDto> list = new List<CoverImageDto>();
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
