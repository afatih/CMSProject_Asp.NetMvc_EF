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
    public class DutyService:IDutyService
    {

        IRepository<Duty> dutyRepo;
        IUnitOfWork uow;

        public DutyService()
        {
            dutyRepo = Resource.UoW.GetRepository<Duty>();
            uow = Resource.UoW;
        }


        public ServiceResult Add(DutyDto dto)
        {
            dutyRepo.Add(EntityFromDto(dto));
            var result = uow.Save();
            return result;

        }

        public ServiceResult Delete(int id)
        {
            Expression<Func<Duty, bool>> exp = p => p.Id == id;
            dutyRepo.HardDelete(exp);
            var result = uow.Save();
            return result;
        }

        public ServiceResult<DutyDto> Get(int id)
        {
            try
            {
                Expression<Func<Duty, bool>> exp = p => p.Id == id;
                var result = DtoFromEntity(dutyRepo.Get(exp).SingleOrDefault());
                return new ServiceResult<DutyDto>(ProcessStateEnum.Success, "İşmeniniz başarılı", result);
            }
            catch (Exception e)
            {
                return new ServiceResult<DutyDto>(ProcessStateEnum.Error, e.Message, new DutyDto());
            }


        }

        public ServiceResult<List<DutyDto>> GetAll()
        {
            try
            {
                Expression<Func<Duty, bool>> exp = p => p.Id > 0;
                var result = DtoFromEntity(dutyRepo.Get(exp));
                return new ServiceResult<List<DutyDto>>(ProcessStateEnum.Success, "İşleminiz Başarılı", result.OrderBy(x => x.RowNumber).ToList());
            }
            catch (Exception e)
            {
                return new ServiceResult<List<DutyDto>>(ProcessStateEnum.Success, e.Message, new List<DutyDto>());
            }
        }

        public ServiceResult Update(DutyDto dto)
        {
            Expression<Func<Duty, bool>> exp = p => p.Id == dto.Id;
            var duty = dutyRepo.Get(exp).SingleOrDefault();
            duty.RowNumber = dto.RowNumber;
            duty.LanguageId = dto.LanguageId;
            duty.SeoDescription = dto.SeoDescription;
            duty.SeoTitle = dto.SeoTitle;
            //duty.State = dto.State;
            duty.Url = dto.Url;
            duty.Caption = dto.Caption;
            duty.Content = dto.Content;
            duty.UpdatedDate = dto.UpdatedDate;
            duty.ImageBig = dto.ImageBig;
            duty.ImageCover = dto.ImageCover;
            duty.Description = dto.Description;
            var result = uow.Save();
            return result;
        }

        public ServiceResult ChangeState(int id, bool state)
        {
            Expression<Func<Duty, bool>> exp = p => p.Id == id;
            var duty = dutyRepo.Get(exp).SingleOrDefault();
            duty.State = state;
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
                        Expression<Func<Duty, bool>> exp = p => p.Id == item.Id;
                        var entity = dutyRepo.Get(exp).SingleOrDefault();
                        entity.RowNumber = item.RowNumber;
                    }

                    result = uow.Save();
                }
            }

            return result;
        }

        public ServiceResult<List<DutyDto>> GetByLangId(int id)
        {
            try
            {
                Expression<Func<Duty, bool>> exp = p => p.LanguageId == id;
                var result = DtoFromEntity(dutyRepo.Get(exp));
                return new ServiceResult<List<DutyDto>>(ProcessStateEnum.Success, "İşmeniniz başarılı", result.OrderBy(x => x.RowNumber).ToList());
            }
            catch (Exception e)
            {
                return new ServiceResult<List<DutyDto>>(ProcessStateEnum.Success, e.Message, new List<DutyDto>());
            }
        }


        #region Mappings

        public Duty EntityFromDto(DutyDto dto)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DutyDto, Duty>();
            });

            IMapper iMapper = config.CreateMapper();
            var language = iMapper.Map<DutyDto, Duty>(dto);
            return language;

        }

        public DutyDto DtoFromEntity(Duty entitiy)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Duty, DutyDto>();
            });

            IMapper iMapper = config.CreateMapper();
            var language = iMapper.Map<Duty, DutyDto>(entitiy);
            return language;
        }

        public List<DutyDto> DtoFromEntity(List<Duty> dutys)
        {
            List<DutyDto> list = new List<DutyDto>();
            if (dutys != null)
            {
                if (dutys.Count > 0)
                {
                    foreach (var duty in dutys)
                    {
                        list.Add(DtoFromEntity(duty));
                    }
                }
            }
            return list;
        }


        #endregion
    }
}
