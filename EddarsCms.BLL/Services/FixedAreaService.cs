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

    public class FixedAreaService : IFixedAreaService
    {
        IRepository<FixedArea> fixedAreaRepo;
        IUnitOfWork uow;

        public FixedAreaService()
        {
            fixedAreaRepo = Resource.UoW.GetRepository<FixedArea>();
            uow = Resource.UoW;
        }

        public ServiceResult AddOrUpdate(FixedAreaDto dto)
        {
            Expression<Func<FixedArea, bool>> exp = p => p.LanguageId == dto.LanguageId;

            var selectedLang = fixedAreaRepo.Get(exp);
            if (selectedLang.Count == 0)
            {
                fixedAreaRepo.Add(EntityFromDto(dto));
                var result = uow.Save();
                return result;
            }
            else
            {
                var entity = fixedAreaRepo.Get(exp).SingleOrDefault();
                entity.IletisimFormBaslik = dto.IletisimFormBaslik;
                entity.IletisimIsim = dto.IletisimIsim;
                entity.IletisimKonu = dto.IletisimKonu;
                entity.IletisimTelefon = dto.IletisimTelefon;
                entity.IletisimMesaj = dto.IletisimMesaj;
                entity.IletisimGonder = dto.IletisimGonder;
                entity.IletisimBilgiBaslik = dto.IletisimBilgiBaslik;
                entity.IletisimAdresBaslik = dto.IletisimAdresBaslik;
                entity.AnaSayfa = dto.AnaSayfa;
                entity.AnaSayfaKurumsal = dto.AnaSayfaKurumsal;
                entity.AnaSayfaHizmetlerimiz = dto.AnaSayfaHizmetlerimiz;
                entity.AnaSayfaUrunlerimiz = dto.AnaSayfaUrunlerimiz;
                entity.AnaSayfaIletisim = dto.AnaSayfaIletisim;
                entity.AnaSayfaUrunlerBaslik = dto.AnaSayfaUrunlerBaslik;
                entity.AnaSayfaBlogBaslik = dto.AnaSayfaBlogBaslik;
                entity.FooterEnSonBloglar = dto.FooterEnSonBloglar;
                entity.FooterHaberdarOl = dto.FooterHaberdarOl;
                entity.FooterHaberdarOlAciklama = dto.FooterHaberdarOlAciklama;
                entity.FooterHaberdarOlEPosta = dto.FooterHaberdarOlEPosta;
                entity.FooterInstagram = dto.FooterInstagram;

                entity.AnaSayfaInsanKaynaklari = dto.AnaSayfaInsanKaynaklari;
                entity.AnaSayfaHaberOku = dto.AnaSayfaHaberOku;

                entity.InsanKaynaklariBaslik = dto.InsanKaynaklariBaslik;
                entity.InsanKaynaklariAd = dto.InsanKaynaklariAd;
                entity.InsanKaynaklariSoyad = dto.InsanKaynaklariSoyad;
                entity.InsanKaynaklariMail = dto.InsanKaynaklariMail;
                entity.InsanKaynaklariTelefon = dto.InsanKaynaklariTelefon;
                entity.InsanKaynaklariDosyaSec = dto.InsanKaynaklariDosyaSec;
                entity.InsanKaynaklariMesaj = dto.InsanKaynaklariMesaj;
                entity.InsanKaynaklariGonder = dto.InsanKaynaklariGonder;
                entity.InsanKaynaklariIcerik = dto.InsanKaynaklariIcerik;




                var result = uow.Save();
                return result;

            }
        }

        public ServiceResult<FixedAreaDto> Get(int langId)
        {
            try
            {
                var result = fixedAreaRepo.Get(x => x.LanguageId == langId).SingleOrDefault();
                if (result != null)
                {
                    return new ServiceResult<FixedAreaDto>(ProcessStateEnum.Success, "", DtoFromEntity(result));
                }
                else
                {
                    return new ServiceResult<FixedAreaDto>(ProcessStateEnum.Warning, "", new FixedAreaDto());
                }
            }
            catch (Exception e)
            {

                return new ServiceResult<FixedAreaDto>(ProcessStateEnum.Error, e.Message, new FixedAreaDto());
            }

        }

        #region Mappings
        public FixedArea EntityFromDto(FixedAreaDto dto)
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FixedAreaDto, FixedArea>();
            });

            IMapper iMapper = config.CreateMapper();
            var entity = iMapper.Map<FixedAreaDto, FixedArea>(dto);
            return entity;

        }

        public FixedAreaDto DtoFromEntity(FixedArea entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FixedArea, FixedAreaDto>();
            });

            IMapper iMapper = config.CreateMapper();
            var dto = iMapper.Map<FixedArea, FixedAreaDto>(entity);
            return dto;
        }

        public List<FixedAreaDto> DtoFromEntity(List<FixedArea> dtos)
        {
            List<FixedAreaDto> list = new List<FixedAreaDto>();
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
