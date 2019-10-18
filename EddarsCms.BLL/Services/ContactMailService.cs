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
    public class ContactMailService:IContactMailService
    {
        IRepository<ContactMail> contactMailRepo;
        IRepository<Notification> notRepo;
        IUnitOfWork uow;

        public ContactMailService()
        {
            uow = Resource.UoW;
            contactMailRepo = uow.GetRepository<ContactMail>();
            notRepo = uow.GetRepository<Notification>();

        }

        public ServiceResult Add(ContactMailDto dto)
        {
            contactMailRepo.Add(EntityFromDto(dto));
            var result = uow.Save();
            if (result.State == ProcessStateEnum.Success)
            {
                Notification not = new Notification()
                {
                    Caption = "Yeni mail var",
                    Date = dto.Date,
                    Description = "Kullanıcı Adı Soyadı: " + dto.Name+" "+dto.Surname + ", Başlık: " + dto.Caption + ", Mail:" + dto.Content + ", Tarih:" + dto.Date.ToString("dd MMMM yyyy"),
                    Icon = "bg-blue icon-notification glyph-icon icon-user"
                };
                notRepo.Add(not);
                var result2 = uow.Save();
            }
            return result;
        }

        public ServiceResult Delete(int id)
        {
            Expression<Func<ContactMail, bool>> exp = p => p.Id == id;
            contactMailRepo.HardDelete(exp);
            var result = uow.Save();
            return result;
        }

        public ServiceResult<ContactMailDto> Get(int id)
        {
            try
            {
                Expression<Func<ContactMail, bool>> exp = p => p.Id == id;
                var result = DtoFromEntity(contactMailRepo.Get(exp).SingleOrDefault());
                return new ServiceResult<ContactMailDto>(ProcessStateEnum.Success, "İşmeniniz başarılı", result);
            }
            catch (Exception e)
            {
                return new ServiceResult<ContactMailDto>(ProcessStateEnum.Error, e.Message, new ContactMailDto());
            }

        }

        public ServiceResult<List<ContactMailDto>> GetAll()
        {
            try
            {
                Expression<Func<ContactMail, bool>> exp = p => p.Id > 0;
                var result = DtoFromEntity(contactMailRepo.Get(exp));
                return new ServiceResult<List<ContactMailDto>>(ProcessStateEnum.Success, "İşmeniniz başarılı", result.OrderByDescending(x => x.Id).ToList());
            }
            catch (Exception e)
            {
                return new ServiceResult<List<ContactMailDto>>(ProcessStateEnum.Success, e.Message, new List<ContactMailDto>());
            }
        }

        #region Mappings
        public ContactMail EntityFromDto(ContactMailDto dto)
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ContactMailDto, ContactMail>();
            });

            IMapper iMapper = config.CreateMapper();
            var entity = iMapper.Map<ContactMailDto, ContactMail>(dto);
            return entity;

        }

        public ContactMailDto DtoFromEntity(ContactMail entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ContactMail, ContactMailDto>();
            });

            IMapper iMapper = config.CreateMapper();
            var dto = iMapper.Map<ContactMail, ContactMailDto>(entity);
            return dto;
        }

        public List<ContactMailDto> DtoFromEntity(List<ContactMail> dtos)
        {
            List<ContactMailDto> list = new List<ContactMailDto>();
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
