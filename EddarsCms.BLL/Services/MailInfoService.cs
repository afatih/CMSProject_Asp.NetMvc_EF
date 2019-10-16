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
    public class MailInfoService : IMailInfoService
    {
        IRepository<MailInfo> mailInfoRepo;
        IUnitOfWork uow;

        public MailInfoService()
        {
            mailInfoRepo = Resource.UoW.GetRepository<MailInfo>();
            uow = Resource.UoW;
        }


        public ServiceResult AddOrUpdate(MailInfoDto dto)
        {
            if (dto.Id == 0)
            {
                mailInfoRepo.Add(EntityFromDto(dto));
                var result = uow.Save();
                return result;
            }
            else
            {
                Expression<Func<MailInfoDto, bool>> exp = p => p.Id == dto.Id;
                var entity = mailInfoRepo.Get(x => x.Id == dto.Id).SingleOrDefault();
                entity.UpdatedDate = dto.UpdatedDate;
                entity.SenderMail = dto.SenderMail;
                entity.SenderMailPass = dto.SenderMailPass;
                entity.ReceiverMail = dto.ReceiverMail;
                entity.Caption = dto.Caption;
                entity.Host = dto.Host;
                entity.Port = dto.Port;
                var result = uow.Save();
                return result;

            }
        }

        public ServiceResult<MailInfoDto> Get()
        {
            try
            {


                var result = mailInfoRepo.Get(x => x.Id > 0).SingleOrDefault();
                if (result != null)
                {
                    return new ServiceResult<MailInfoDto>(ProcessStateEnum.Success, "", DtoFromEntity(result));
                }
                else
                {
                    return new ServiceResult<MailInfoDto>(ProcessStateEnum.Error, "", new MailInfoDto());
                }
            }
            catch (Exception e)
            {

                return new ServiceResult<MailInfoDto>(ProcessStateEnum.Error, e.Message, new MailInfoDto());
            }

        }

        #region Mappings
        public MailInfo EntityFromDto(MailInfoDto dto)
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MailInfoDto, MailInfo>();
            });

            IMapper iMapper = config.CreateMapper();
            var entity = iMapper.Map<MailInfoDto, MailInfo>(dto);
            return entity;

        }

        public MailInfoDto DtoFromEntity(MailInfo entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MailInfo, MailInfoDto>();
            });

            IMapper iMapper = config.CreateMapper();
            var dto = iMapper.Map<MailInfo, MailInfoDto>(entity);
            return dto;
        }

        public List<MailInfoDto> DtoFromEntity(List<MailInfo> dtos)
        {
            List<MailInfoDto> list = new List<MailInfoDto>();
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
