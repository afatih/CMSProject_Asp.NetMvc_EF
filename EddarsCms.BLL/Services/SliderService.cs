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
    public class SliderService : ISliderService
    {
        IRepository<Slider> sliderRepo;
        IUnitOfWork uow;

        public SliderService()
        {
            sliderRepo = Resource.UoW.GetRepository<Slider>();
            uow = Resource.UoW;
        }



        public ServiceResult Add(SliderDto dto)
        {
            sliderRepo.Add(EntityFromDto(dto));
            var result = uow.Save();
            return result;
        }

        public ServiceResult Delete(int id)
        {
            Expression<Func<Slider, bool>> exp = p => p.Id == id;
            sliderRepo.HardDelete(exp);
            var result = uow.Save();
            return result;
        }

        public ServiceResult<SliderDto> Get(int id)
        {
            try
            {
                Expression<Func<Slider, bool>> exp = p => p.Id == id;
                var result = DtoFromEntity(sliderRepo.Get(exp).SingleOrDefault());
                return new ServiceResult<SliderDto>(ProcessStateEnum.Success, "İşmeniniz başarılı", result);
            }
            catch (Exception e)
            {
                return new ServiceResult<SliderDto>(ProcessStateEnum.Error, e.Message, new SliderDto());
            }

        }

        public ServiceResult<List<SliderDto>> GetAll()
        {
            try
            {
                Expression<Func<Slider, bool>> exp = p => p.Id > 0 ;
                var result = DtoFromEntity(sliderRepo.Get(exp));
                return new ServiceResult<List<SliderDto>>(ProcessStateEnum.Success, "İşmeniniz başarılı", result.OrderBy(x => x.RowNumber).ToList());
            }
            catch (Exception e)
            {
                return new ServiceResult<List<SliderDto>>(ProcessStateEnum.Success, e.Message, new List<SliderDto>());
            }
        }

        public ServiceResult Update(SliderDto dto)
        {
            Expression<Func<Slider, bool>> exp = p => p.Id == dto.Id;
            var Slider = sliderRepo.Get(exp).SingleOrDefault();
            Slider.LanguageId = dto.LanguageId;
            Slider.Caption = dto.Caption;
            Slider.Description = dto.Description;
            Slider.ButtonText = dto.ButtonText;
            Slider.UpdatedDate = dto.UpdatedDate;
            Slider.Url = dto.Url;
            Slider.ImageBig = dto.ImageBig;
            Slider.ImageCover = dto.ImageCover;
            Slider.RowNumber = dto.RowNumber;
            Slider.OpenNewTab = dto.OpenNewTab;
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
                        Expression<Func<Slider, bool>> exp = p => p.Id == item.Id;
                        var entity = sliderRepo.Get(exp).SingleOrDefault();
                        entity.RowNumber = item.RowNumber;
                    }

                    result = uow.Save();
                }
            }

            return result;
        }

        public ServiceResult ChangeState(int id, bool state)
        {
            Expression<Func<Slider, bool>> exp = p => p.Id == id;
            var slider = sliderRepo.Get(exp).SingleOrDefault();
            slider.State = state;
            var result = uow.Save();
            return result;
        }

        public ServiceResult<List<SliderDto>> GetByLangId(int id)
        {
            try
            {
                Expression<Func<Slider, bool>> exp = p => p.LanguageId == id;
                var result = DtoFromEntity(sliderRepo.Get(exp));
                return new ServiceResult<List<SliderDto>>(ProcessStateEnum.Success, "İşmeniniz başarılı", result.OrderBy(x => x.RowNumber).ToList());
            }
            catch (Exception e)
            {
                return new ServiceResult<List<SliderDto>>(ProcessStateEnum.Success, e.Message, new List<SliderDto>());
            }
        }


        #region Mappings
        public Slider EntityFromDto(SliderDto dto)
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SliderDto, Slider>();
            });

            IMapper iMapper = config.CreateMapper();
            var entity = iMapper.Map<SliderDto, Slider>(dto);
            return entity;

        }

        public SliderDto DtoFromEntity(Slider entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Slider, SliderDto>();
            });

            IMapper iMapper = config.CreateMapper();
            var dto = iMapper.Map<Slider, SliderDto>(entity);
            return dto;
        }

        public List<SliderDto> DtoFromEntity(List<Slider> dtos)
        {
            List<SliderDto> list = new List<SliderDto>();
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
