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
    public class CertificateService:ICertificateService
    {
        IRepository<Certificate> certificateRepo;
        IUnitOfWork uow;

        public CertificateService()
        {
            certificateRepo = Resource.UoW.GetRepository<Certificate>();
            uow = Resource.UoW;
        }



        public ServiceResult Add(CertificateDto dto)
        {
            certificateRepo.Add(EntityFromDto(dto));
            var result = uow.Save();
            return result;
        }

        public ServiceResult Delete(int id)
        {
            Expression<Func<Certificate, bool>> exp = p => p.Id == id;
            certificateRepo.HardDelete(exp);
            var result = uow.Save();
            return result;
        }

        public ServiceResult<CertificateDto> Get(int id)
        {
            try
            {
                Expression<Func<Certificate, bool>> exp = p => p.Id == id;
                var result = DtoFromEntity(certificateRepo.Get(exp).SingleOrDefault());
                return new ServiceResult<CertificateDto>(ProcessStateEnum.Success, "İşmeniniz başarılı", result);
            }
            catch (Exception e)
            {
                return new ServiceResult<CertificateDto>(ProcessStateEnum.Error, e.Message, new CertificateDto());
            }

        }

        public ServiceResult<List<CertificateDto>> GetAll()
        {
            try
            {
                Expression<Func<Certificate, bool>> exp = p => p.Id > 0;
                var result = DtoFromEntity(certificateRepo.Get(exp));
                return new ServiceResult<List<CertificateDto>>(ProcessStateEnum.Success, "İşmeniniz başarılı", result.OrderBy(x => x.RowNumber).ToList());
            }
            catch (Exception e)
            {
                return new ServiceResult<List<CertificateDto>>(ProcessStateEnum.Success, e.Message, new List<CertificateDto>());
            }
        }

        public ServiceResult Update(CertificateDto dto)
        {
            Expression<Func<Certificate, bool>> exp = p => p.Id == dto.Id;
            var certificate = certificateRepo.Get(exp).SingleOrDefault();
            certificate.LanguageId = dto.LanguageId;
            certificate.UpdatedDate = dto.UpdatedDate;
            certificate.RowNumber = dto.RowNumber;
            certificate.Image = dto.Image;
            certificate.Name = dto.Name;
            certificate.Description = dto.Description;
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
                        Expression<Func<Certificate, bool>> exp = p => p.Id == item.Id;
                        var entity = certificateRepo.Get(exp).SingleOrDefault();
                        entity.RowNumber = item.RowNumber;
                    }

                    result = uow.Save();
                }
            }

            return result;
        }

        public ServiceResult ChangeState(int id, bool state)
        {
            Expression<Func<Certificate, bool>> exp = p => p.Id == id;
            var Certificate = certificateRepo.Get(exp).SingleOrDefault();
            Certificate.State = state;
            var result = uow.Save();
            return result;
        }

        public ServiceResult<List<CertificateDto>> GetByLangId(int id)
        {
            try
            {
                Expression<Func<Certificate, bool>> exp = p => p.LanguageId == id;
                var result = DtoFromEntity(certificateRepo.Get(exp));
                return new ServiceResult<List<CertificateDto>>(ProcessStateEnum.Success, "İşmeniniz başarılı", result.OrderBy(x => x.RowNumber).ToList());
            }
            catch (Exception e)
            {
                return new ServiceResult<List<CertificateDto>>(ProcessStateEnum.Success, e.Message, new List<CertificateDto>());
            }
        }


        #region Mappings
        public Certificate EntityFromDto(CertificateDto dto)
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CertificateDto, Certificate>();
            });

            IMapper iMapper = config.CreateMapper();
            var entity = iMapper.Map<CertificateDto, Certificate>(dto);
            return entity;

        }

        public CertificateDto DtoFromEntity(Certificate entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Certificate, CertificateDto>();
            });

            IMapper iMapper = config.CreateMapper();
            var dto = iMapper.Map<Certificate, CertificateDto>(entity);
            return dto;
        }

        public List<CertificateDto> DtoFromEntity(List<Certificate> dtos)
        {
            List<CertificateDto> list = new List<CertificateDto>();
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
