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
    public class ReferanceService:IReferanceService
    {
        IRepository<Referance> referanceRepo;
        IUnitOfWork uow;

        public ReferanceService()
        {
            referanceRepo = Resource.UoW.GetRepository<Referance>();
            uow = Resource.UoW;
        }



        public ServiceResult Add(ReferanceDto dto)
        {
            referanceRepo.Add(EntityFromDto(dto));
            var result = uow.Save();
            return result;
        }

        public ServiceResult Delete(int id)
        {
            Expression<Func<Referance, bool>> exp = p => p.Id == id;
            referanceRepo.HardDelete(exp);
            var result = uow.Save();
            return result;
        }

        public ServiceResult<ReferanceDto> Get(int id)
        {
            try
            {
                Expression<Func<Referance, bool>> exp = p => p.Id == id;
                var result = DtoFromEntity(referanceRepo.Get(exp).SingleOrDefault());
                return new ServiceResult<ReferanceDto>(ProcessStateEnum.Success, "İşmeniniz başarılı", result);
            }
            catch (Exception e)
            {
                return new ServiceResult<ReferanceDto>(ProcessStateEnum.Error, e.Message, new ReferanceDto());
            }

        }

        public ServiceResult<List<ReferanceDto>> GetAll()
        {
            try
            {
                Expression<Func<Referance, bool>> exp = p => p.Id > 0;
                var result = DtoFromEntity(referanceRepo.Get(exp));
                return new ServiceResult<List<ReferanceDto>>(ProcessStateEnum.Success, "İşmeniniz başarılı", result.OrderBy(x => x.RowNumber).ToList());
            }
            catch (Exception e)
            {
                return new ServiceResult<List<ReferanceDto>>(ProcessStateEnum.Success, e.Message, new List<ReferanceDto>());
            }
        }

        public ServiceResult Update(ReferanceDto dto)
        {
            Expression<Func<Referance, bool>> exp = p => p.Id == dto.Id;
            var Referance = referanceRepo.Get(exp).SingleOrDefault();
            Referance.LanguageId = dto.LanguageId;
            Referance.Image = dto.Image;
            Referance.Name = dto.Name;
            Referance.Description= dto.Description;
            Referance.UpdatedDate = dto.UpdatedDate;
            Referance.RowNumber = dto.RowNumber;
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
                        Expression<Func<Referance, bool>> exp = p => p.Id == item.Id;
                        var entity = referanceRepo.Get(exp).SingleOrDefault();
                        entity.RowNumber = item.RowNumber;
                    }

                    result = uow.Save();
                }
            }

            return result;
        }


        public ServiceResult<List<ReferanceDto>> GetByLangId(int id)
        {
            try
            {
                Expression<Func<Referance, bool>> exp = p => p.LanguageId == id;
                var result = DtoFromEntity(referanceRepo.Get(exp));
                return new ServiceResult<List<ReferanceDto>>(ProcessStateEnum.Success, "İşmeniniz başarılı", result.OrderBy(x => x.RowNumber).ToList());
            }
            catch (Exception e)
            {
                return new ServiceResult<List<ReferanceDto>>(ProcessStateEnum.Success, e.Message, new List<ReferanceDto>());
            }
        }


        #region Mappings
        public Referance EntityFromDto(ReferanceDto dto)
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ReferanceDto, Referance>();
            });

            IMapper iMapper = config.CreateMapper();
            var entity = iMapper.Map<ReferanceDto, Referance>(dto);
            return entity;

        }

        public ReferanceDto DtoFromEntity(Referance entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Referance, ReferanceDto>();
            });

            IMapper iMapper = config.CreateMapper();
            var dto = iMapper.Map<Referance, ReferanceDto>(entity);
            return dto;
        }

        public List<ReferanceDto> DtoFromEntity(List<Referance> dtos)
        {
            List<ReferanceDto> list = new List<ReferanceDto>();
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
