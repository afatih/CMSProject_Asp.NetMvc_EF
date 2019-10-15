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
    public class BranchService:IBranchService
    {
        IRepository<Branch> branchRepo;
        IUnitOfWork uow;

        public BranchService()
        {
            branchRepo = Resource.UoW.GetRepository<Branch>();
            uow = Resource.UoW;
        }

        public ServiceResult Add(BranchDto dto)
        {
            branchRepo.Add(EntityFromDto(dto));
            var result = uow.Save();
            return result;
        }

        public ServiceResult Delete(int id)
        {
            Expression<Func<Branch, bool>> exp = p => p.Id == id;
            branchRepo.HardDelete(exp);
            var result = uow.Save();
            return result;
        }

        public ServiceResult<BranchDto> Get(int id)
        {
            try
            {
                Expression<Func<Branch, bool>> exp = p => p.Id == id;
                var result = DtoFromEntity(branchRepo.Get(exp).SingleOrDefault());
                return new ServiceResult<BranchDto>(ProcessStateEnum.Success, "İşmeniniz başarılı", result);
            }
            catch (Exception e)
            {
                return new ServiceResult<BranchDto>(ProcessStateEnum.Error, e.Message, new BranchDto());
            }

        }

        public ServiceResult<List<BranchDto>> GetAll()
        {
            try
            {
                Expression<Func<Branch, bool>> exp = p => p.Id > 0;
                var result = DtoFromEntity(branchRepo.Get(exp));
                return new ServiceResult<List<BranchDto>>(ProcessStateEnum.Success, "İşmeniniz başarılı", result.OrderBy(x => x.RowNumber).ToList());
            }
            catch (Exception e)
            {
                return new ServiceResult<List<BranchDto>>(ProcessStateEnum.Success, e.Message, new List<BranchDto>());
            }
        }

        public ServiceResult Update(BranchDto dto)
        {
            Expression<Func<Branch, bool>> exp = p => p.Id == dto.Id;
            var Branch = branchRepo.Get(exp).SingleOrDefault();
            Branch.LanguageId = dto.LanguageId;
            Branch.UpdatedDate = dto.UpdatedDate;
            Branch.Image = dto.Image;
            Branch.RowNumber = dto.RowNumber;
            Branch.Name = dto.Name;
            Branch.Phone1 = dto.Phone1;
            Branch.Phone2= dto.Phone2;
            Branch.Adress = dto.Adress;
            Branch.MapLocation= dto.MapLocation;
            Branch.Email= dto.Email;
            Branch.Fax= dto.Fax;
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
                        Expression<Func<Branch, bool>> exp = p => p.Id == item.Id;
                        var entity = branchRepo.Get(exp).SingleOrDefault();
                        entity.RowNumber = item.RowNumber;
                    }

                    result = uow.Save();
                }
            }

            return result;
        }

        public ServiceResult ChangeState(int id, bool state)
        {
            Expression<Func<Branch, bool>> exp = p => p.Id == id;
            var Branch = branchRepo.Get(exp).SingleOrDefault();
            Branch.State = state;
            var result = uow.Save();
            return result;
        }

        public ServiceResult<List<BranchDto>> GetByLangId(int id)
        {
            try
            {
                Expression<Func<Branch, bool>> exp = p => p.LanguageId == id;
                var result = DtoFromEntity(branchRepo.Get(exp));
                return new ServiceResult<List<BranchDto>>(ProcessStateEnum.Success, "İşmeniniz başarılı", result.OrderBy(x => x.RowNumber).ToList());
            }
            catch (Exception e)
            {
                return new ServiceResult<List<BranchDto>>(ProcessStateEnum.Success, e.Message, new List<BranchDto>());
            }
        }


        #region Mappings
        public Branch EntityFromDto(BranchDto dto)
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BranchDto, Branch>();
            });

            IMapper iMapper = config.CreateMapper();
            var entity = iMapper.Map<BranchDto, Branch>(dto);
            return entity;

        }

        public BranchDto DtoFromEntity(Branch entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Branch, BranchDto>();
            });

            IMapper iMapper = config.CreateMapper();
            var dto = iMapper.Map<Branch, BranchDto>(entity);
            return dto;
        }

        public List<BranchDto> DtoFromEntity(List<Branch> dtos)
        {
            List<BranchDto> list = new List<BranchDto>();
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
