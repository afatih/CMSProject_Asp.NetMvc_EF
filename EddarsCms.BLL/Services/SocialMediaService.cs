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
    public class SocialMediaService:ISocialMediaService
    {
        IRepository<SocialMedia> SocialMediaRepo;
        IUnitOfWork uow;

        public SocialMediaService()
        {
            SocialMediaRepo = Resource.UoW.GetRepository<SocialMedia>();
            uow = Resource.UoW;
        }



        public ServiceResult Add(SocialMediaDto dto)
        {
            SocialMediaRepo.Add(EntityFromDto(dto));
            var result = uow.Save();
            return result;
        }

        public ServiceResult Delete(int id)
        {
            Expression<Func<SocialMedia, bool>> exp = p => p.Id == id;
            SocialMediaRepo.HardDelete(exp);
            var result = uow.Save();
            return result;
        }

        public ServiceResult<SocialMediaDto> Get(int id)
        {
            try
            {
                Expression<Func<SocialMedia, bool>> exp = p => p.Id == id;
                var result = DtoFromEntity(SocialMediaRepo.Get(exp).SingleOrDefault());
                return new ServiceResult<SocialMediaDto>(ProcessStateEnum.Success, "İşmeniniz başarılı", result);
            }
            catch (Exception e)
            {
                return new ServiceResult<SocialMediaDto>(ProcessStateEnum.Error, e.Message, new SocialMediaDto());
            }

        }

        public ServiceResult<List<SocialMediaDto>> GetAll()
        {
            try
            {
                Expression<Func<SocialMedia, bool>> exp = p => p.Id > 0;
                var result = DtoFromEntity(SocialMediaRepo.Get(exp));
                return new ServiceResult<List<SocialMediaDto>>(ProcessStateEnum.Success, "İşmeniniz başarılı", result.OrderBy(x => x.RowNumber).ToList());
            }
            catch (Exception e)
            {
                return new ServiceResult<List<SocialMediaDto>>(ProcessStateEnum.Success, e.Message, new List<SocialMediaDto>());
            }
        }

        public ServiceResult Update(SocialMediaDto dto)
        {
            Expression<Func<SocialMedia, bool>> exp = p => p.Id == dto.Id;
            var socialMedia = SocialMediaRepo.Get(exp).SingleOrDefault();
            socialMedia.LanguageId = dto.LanguageId;
            socialMedia.UpdatedDate = dto.UpdatedDate;
            socialMedia.RowNumber = dto.RowNumber;
            socialMedia.Name = dto.Name;
            socialMedia.Url = dto.Url;
            socialMedia.IconFull = dto.IconFull;
            socialMedia.Icon = dto.Icon;
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
                        Expression<Func<SocialMedia, bool>> exp = p => p.Id == item.Id;
                        var entity = SocialMediaRepo.Get(exp).SingleOrDefault();
                        entity.RowNumber = item.RowNumber;
                    }

                    result = uow.Save();
                }
            }

            return result;
        }


        public ServiceResult<List<SocialMediaDto>> GetByLangId(int id)
        {
            try
            {
                Expression<Func<SocialMedia, bool>> exp = p => p.LanguageId == id;
                var result = DtoFromEntity(SocialMediaRepo.Get(exp));
                return new ServiceResult<List<SocialMediaDto>>(ProcessStateEnum.Success, "İşmeniniz başarılı", result.OrderBy(x => x.RowNumber).ToList());
            }
            catch (Exception e)
            {
                return new ServiceResult<List<SocialMediaDto>>(ProcessStateEnum.Success, e.Message, new List<SocialMediaDto>());
            }
        }


        #region Mappings
        public SocialMedia EntityFromDto(SocialMediaDto dto)
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SocialMediaDto, SocialMedia>();
            });

            IMapper iMapper = config.CreateMapper();
            var entity = iMapper.Map<SocialMediaDto, SocialMedia>(dto);
            return entity;

        }

        public SocialMediaDto DtoFromEntity(SocialMedia entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SocialMedia, SocialMediaDto>();
            });

            IMapper iMapper = config.CreateMapper();
            var dto = iMapper.Map<SocialMedia, SocialMediaDto>(entity);
            return dto;
        }

        public List<SocialMediaDto> DtoFromEntity(List<SocialMedia> dtos)
        {
            List<SocialMediaDto> list = new List<SocialMediaDto>();
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
