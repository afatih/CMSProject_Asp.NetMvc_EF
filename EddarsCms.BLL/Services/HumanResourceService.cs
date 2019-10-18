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
    public class HumanResourceService:IHumanResourceService
    {
        IRepository<HumanResource> HumanResourceRepo;
        IRepository<Notification> notRepo;
        IUnitOfWork uow;

        public HumanResourceService()
        {
            uow = Resource.UoW;
            HumanResourceRepo = uow.GetRepository<HumanResource>();
            notRepo = uow.GetRepository<Notification>();
     
        }

        public ServiceResult Add(HumanResourceDto dto)
        {
            HumanResourceRepo.Add(EntityFromDto(dto));
            var result = uow.Save();
            if (result.State == ProcessStateEnum.Success)
            {
                Notification not = new Notification()
                {
                    Caption = "Yeni iş başvurusu var",
                    Date = dto.Date,
                    Description = "Kullanıcı Adı ve Soyadı: " + dto.Name+" "+dto.Surname + ", Tarih:" + dto.Date.ToString("dd MMMM yyyy"),
                    Icon = "bg-green icon-notification glyph-icon icon-user"
                };
                notRepo.Add(not);
                var result2 = uow.Save();
            }
            return result;
        }


        public ServiceResult Delete(int id)
        {
            Expression<Func<HumanResource, bool>> exp = p => p.Id == id;
            HumanResourceRepo.HardDelete(exp);
            var result = uow.Save();
            return result;
        }

        public ServiceResult<HumanResourceDto> Get(int id)
        {
            try
            {
                Expression<Func<HumanResource, bool>> exp = p => p.Id == id;
                var result = DtoFromEntity(HumanResourceRepo.Get(exp).SingleOrDefault());
                return new ServiceResult<HumanResourceDto>(ProcessStateEnum.Success, "İşmeniniz başarılı", result);
            }
            catch (Exception e)
            {
                return new ServiceResult<HumanResourceDto>(ProcessStateEnum.Error, e.Message, new HumanResourceDto());
            }

        }

        public ServiceResult<List<HumanResourceDto>> GetAll()
        {
            try
            {
                Expression<Func<HumanResource, bool>> exp = p => p.Id > 0;
                var result = DtoFromEntity(HumanResourceRepo.Get(exp));
                return new ServiceResult<List<HumanResourceDto>>(ProcessStateEnum.Success, "İşmeniniz başarılı", result.OrderByDescending(x => x.Id).ToList());
            }
            catch (Exception e)
            {
                return new ServiceResult<List<HumanResourceDto>>(ProcessStateEnum.Success, e.Message, new List<HumanResourceDto>());
            }
        }

        #region Mappings
        public HumanResource EntityFromDto(HumanResourceDto dto)
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<HumanResourceDto, HumanResource>();
            });

            IMapper iMapper = config.CreateMapper();
            var entity = iMapper.Map<HumanResourceDto, HumanResource>(dto);
            return entity;

        }

        public HumanResourceDto DtoFromEntity(HumanResource entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<HumanResource, HumanResourceDto>();
            });

            IMapper iMapper = config.CreateMapper();
            var dto = iMapper.Map<HumanResource, HumanResourceDto>(entity);
            return dto;
        }

        public List<HumanResourceDto> DtoFromEntity(List<HumanResource> dtos)
        {
            List<HumanResourceDto> list = new List<HumanResourceDto>();
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
