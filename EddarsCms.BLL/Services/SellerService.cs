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
    public class SellerService:ISellerService
    {
        IRepository<Seller> SellerRepo;
        IUnitOfWork uow;

        public SellerService()
        {
            SellerRepo = Resource.UoW.GetRepository<Seller>();
            uow = Resource.UoW;
        }



        public ServiceResult Add(SellerDto dto)
        {
            SellerRepo.Add(EntityFromDto(dto));
            var result = uow.Save();
            return result;
        }

        public ServiceResult Delete(int id)
        {
            Expression<Func<Seller, bool>> exp = p => p.Id == id;
            SellerRepo.HardDelete(exp);
            var result = uow.Save();
            return result;
        }

        public ServiceResult<SellerDto> Get(int id)
        {
            try
            {
                Expression<Func<Seller, bool>> exp = p => p.Id == id;
                var result = DtoFromEntity(SellerRepo.Get(exp).SingleOrDefault());
                return new ServiceResult<SellerDto>(ProcessStateEnum.Success, "İşmeniniz başarılı", result);
            }
            catch (Exception e)
            {
                return new ServiceResult<SellerDto>(ProcessStateEnum.Error, e.Message, new SellerDto());
            }

        }

        public ServiceResult<List<SellerDto>> GetAll()
        {
            try
            {
                Expression<Func<Seller, bool>> exp = p => p.Id > 0;
                var result = DtoFromEntity(SellerRepo.Get(exp));
                return new ServiceResult<List<SellerDto>>(ProcessStateEnum.Success, "İşmeniniz başarılı", result.OrderBy(x => x.RowNumber).ToList());
            }
            catch (Exception e)
            {
                return new ServiceResult<List<SellerDto>>(ProcessStateEnum.Success, e.Message, new List<SellerDto>());
            }
        }

        public ServiceResult Update(SellerDto dto)
        {
            Expression<Func<Seller, bool>> exp = p => p.Id == dto.Id;
            var Seller = SellerRepo.Get(exp).SingleOrDefault();
            Seller.LanguageId = dto.LanguageId;
            Seller.UpdatedDate = dto.UpdatedDate;
            Seller.RowNumber = dto.RowNumber;
            Seller.Image = dto.Image;
            Seller.Name = dto.Name;
            Seller.City= dto.City;
            Seller.Adress= dto.Adress;
            Seller.Phone= dto.Phone;
            Seller.AuthorityPlace= dto.AuthorityPlace;
            Seller.Mail= dto.Mail;
            Seller.MapLocation= dto.MapLocation;
            


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
                        Expression<Func<Seller, bool>> exp = p => p.Id == item.Id;
                        var entity = SellerRepo.Get(exp).SingleOrDefault();
                        entity.RowNumber = item.RowNumber;
                    }

                    result = uow.Save();
                }
            }

            return result;
        }

        public ServiceResult ChangeState(int id, bool state)
        {
            Expression<Func<Seller, bool>> exp = p => p.Id == id;
            var Seller = SellerRepo.Get(exp).SingleOrDefault();
            Seller.State = state;
            var result = uow.Save();
            return result;
        }

        public ServiceResult<List<SellerDto>> GetByLangId(int id)
        {
            try
            {
                Expression<Func<Seller, bool>> exp = p => p.LanguageId == id;
                var result = DtoFromEntity(SellerRepo.Get(exp));
                return new ServiceResult<List<SellerDto>>(ProcessStateEnum.Success, "İşmeniniz başarılı", result.OrderBy(x => x.RowNumber).ToList());
            }
            catch (Exception e)
            {
                return new ServiceResult<List<SellerDto>>(ProcessStateEnum.Success, e.Message, new List<SellerDto>());
            }
        }


        #region Mappings
        public Seller EntityFromDto(SellerDto dto)
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SellerDto, Seller>();
            });

            IMapper iMapper = config.CreateMapper();
            var entity = iMapper.Map<SellerDto, Seller>(dto);
            return entity;

        }

        public SellerDto DtoFromEntity(Seller entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Seller, SellerDto>();
            });

            IMapper iMapper = config.CreateMapper();
            var dto = iMapper.Map<Seller, SellerDto>(entity);
            return dto;
        }

        public List<SellerDto> DtoFromEntity(List<Seller> dtos)
        {
            List<SellerDto> list = new List<SellerDto>();
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

