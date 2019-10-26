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
    public class ContactInfoService : IContactInfoService
    {
        IRepository<ContactInfo> contactInfRepo;
        IUnitOfWork uow;

        public ContactInfoService()
        {
            contactInfRepo = Resource.UoW.GetRepository<ContactInfo>();
            uow = Resource.UoW;
        }




        public ServiceResult AddOrUpdate(ContactInfoDto dto)
        {
            if (dto.Id == 0)
            {
                contactInfRepo.Add(EntityFromDto(dto));
                var result = uow.Save();
                return result;
            }
            else
            {
                Expression<Func<ContactInfoDto, bool>> exp = p => p.Id == dto.Id;
                var entity = contactInfRepo.Get(x => x.Id == dto.Id).SingleOrDefault();
                entity.UpdatedDate = dto.UpdatedDate;
                entity.Image = dto.Image;
                entity.Name = dto.Name;
                entity.Phone1 = dto.Phone1;
                entity.Phone2 = dto.Phone2;
                entity.Adress = dto.Adress;
                entity.MapLocation = dto.MapLocation;
                entity.EMail = dto.EMail;
                entity.Fax = dto.Fax;
                entity.Description = dto.Description;
                entity.SeoTitle = dto.SeoTitle;
                entity.SeoDescription = dto.SeoDescription;

                var result = uow.Save();
                return result;

            }
        }

        public ServiceResult<ContactInfoDto> Get()
        {
            try
            {
                var result = contactInfRepo.Get(x => x.Id > 0).SingleOrDefault();
                if (result != null)
                {
                    return new ServiceResult<ContactInfoDto>(ProcessStateEnum.Success, "", DtoFromEntity(result));
                }
                else
                {
                    return new ServiceResult<ContactInfoDto>(ProcessStateEnum.Error, "", new ContactInfoDto());
                }
            }
            catch (Exception e)
            {

                return new ServiceResult<ContactInfoDto>(ProcessStateEnum.Error, e.Message, new ContactInfoDto());
            }

        }

        #region Mappings
        public ContactInfo EntityFromDto(ContactInfoDto dto)
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ContactInfoDto, ContactInfo>();
            });

            IMapper iMapper = config.CreateMapper();
            var entity = iMapper.Map<ContactInfoDto, ContactInfo>(dto);
            return entity;

        }

        public ContactInfoDto DtoFromEntity(ContactInfo entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ContactInfo, ContactInfoDto>();
            });

            IMapper iMapper = config.CreateMapper();
            var dto = iMapper.Map<ContactInfo, ContactInfoDto>(entity);
            return dto;
        }

        public List<ContactInfoDto> DtoFromEntity(List<ContactInfo> dtos)
        {
            List<ContactInfoDto> list = new List<ContactInfoDto>();
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
