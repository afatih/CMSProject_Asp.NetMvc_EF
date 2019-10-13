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
    public class LanguageService : ILanguageService
    {
        IRepository<Language> languageRep;
        IUnitOfWork uow;

        public LanguageService()
        {
            languageRep = Resource.UoW.GetRepository<Language>();
            uow = Resource.UoW;
        }

        public ServiceResult Add(LanguageDto dto)
        {
            languageRep.Add(EntityFromDto(dto));
            var result = uow.Save();
            return result;
        }

        public ServiceResult Delete(int id)
        {
            Expression<Func<Language, bool>> exp = p => p.Id == id;
            languageRep.HardDelete(exp);
            var result = uow.Save();
            return result;
        }

        public ServiceResult<LanguageDto> Get(int id)
        {
            try
            {
                Expression<Func<Language, bool>> exp = p => p.Id == id;
                var result = DtoFromEntity(languageRep.Get(exp).SingleOrDefault());
                return new ServiceResult<LanguageDto>(ProcessStateEnum.Success, "İşmeniniz başarılı", result);
            }
            catch (Exception e)
            {
                return new ServiceResult<LanguageDto>(ProcessStateEnum.Error, e.Message, new LanguageDto());
            }

        }

        public ServiceResult<List<LanguageDto>> GetAll()
        {
            try
            {
                Expression<Func<Language, bool>> exp = p => p.Id > 0;
                var result = DtoFromEntity(languageRep.Get(exp));
                return new ServiceResult<List<LanguageDto>>(ProcessStateEnum.Success, "İşmeniniz başarılı", result);
            }
            catch (Exception e)
            {
                return new ServiceResult<List<LanguageDto>>(ProcessStateEnum.Success, e.Message, new List<LanguageDto>());
            }
        }

        public ServiceResult Update(LanguageDto dto)
        {
            Expression<Func<Language, bool>> exp = p => p.Id == dto.Id;
            var language = languageRep.Get(exp).SingleOrDefault();
            language.Name = dto.Name;
            language.UpdatedDate = dto.UpdatedDate;
            language.Url = dto.Url;
            language.Image = dto.Image;
            language.RowNumber = dto.RowNumber;
            var result = uow.Save();
            return result;
        }


        #region Mappings
        public Language EntityFromDto(LanguageDto languageDto)
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LanguageDto, Language>();
            });

            IMapper iMapper = config.CreateMapper();
            var language = iMapper.Map<LanguageDto, Language>(languageDto);
            return language;

            //Language language = new Language()
            //{
            //    Name = languageDto.Name,
            //    Url = languageDto.Url,
            //    Image = languageDto.Image ?? "",
            //    //Image = languageDto.Image,

            //};
            //return language;
        }

        public LanguageDto DtoFromEntity(Language language)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Language, LanguageDto>();
            });

            IMapper iMapper = config.CreateMapper();
            var languageDto = iMapper.Map<Language, LanguageDto>(language);
            return languageDto;


            //LanguageDto languageDto = new LanguageDto()
            //{
            //    Id = language.Id,
            //    Name = language.Name,
            //    State = language.State,
            //    Url = language.Url,
            //    Image = language.Image,
            //    RowNumber = language.RowNumber,

            //};
            //return languageDto;
        }

        public List<LanguageDto> DtoFromEntity(List<Language> languages)
        {
            List<LanguageDto> list = new List<LanguageDto>();
            if (languages != null)
            {
                if (languages.Count > 0)
                {
                    foreach (var language in languages)
                    {
                        list.Add(DtoFromEntity(language));

                    }
                }
            }
            return list;
        }



        #endregion
    }
}
