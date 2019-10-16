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
    public class CategoryService:ICategoryService
    {
        IRepository<Category> CategoryRepo;
        IUnitOfWork uow;

        public CategoryService()
        {
            CategoryRepo = Resource.UoW.GetRepository<Category>();
            uow = Resource.UoW;
        }



        public ServiceResult Add(CategoryDto dto)
        {
            CategoryRepo.Add(EntityFromDto(dto));
            var result = uow.Save();
            return result;
        }

        public ServiceResult Delete(int id)
        {
            Expression<Func<Category, bool>> exp = p => p.Id == id;
            CategoryRepo.HardDelete(exp);
            var result = uow.Save();
            return result;
        }

        public ServiceResult<CategoryDto> Get(int id)
        {
            try
            {
                Expression<Func<Category, bool>> exp = p => p.Id == id;
                var result = DtoFromEntity(CategoryRepo.Get(exp).SingleOrDefault());
                return new ServiceResult<CategoryDto>(ProcessStateEnum.Success, "İşmeniniz başarılı", result);
            }
            catch (Exception e)
            {
                return new ServiceResult<CategoryDto>(ProcessStateEnum.Error, e.Message, new CategoryDto());
            }

        }

        public ServiceResult<List<CategoryDto>> GetAll()
        {
            try
            {

                var categories = CategoryRepo.Get(x => x.Id>=0);

                var result2 = (from cr1 in categories
                               join cr2 in categories on cr1.MainCatId equals cr2.Id into p
                               from cr2 in p.DefaultIfEmpty()
                               select new CategoryDto
                               {
                                   Id = cr1.Id,
                                   MainCatId = cr1.MainCatId,
                                   MainCatName = cr2 == null ? "Ana Menü" : cr2.Name,
                                   Name = cr1.Name,
                                   ImageBig = cr1.ImageBig,
                                   ImageSmall = cr1.ImageSmall,
                                   LanguageId = cr1.LanguageId,
                                   RowNumber = cr1.RowNumber,
                                   SeoDescription = cr1.SeoDescription,
                                   SeoTitle = cr1.SeoTitle,
                                   State = cr1.State,
                                   UpdatedDate = cr1.UpdatedDate,
                                   Video1 = cr1.Video1,
                                   Video2 = cr1.Video2,
                                   Video3 = cr1.Video3
                               });







                return new ServiceResult<List<CategoryDto>>(ProcessStateEnum.Success, "İşmeniniz başarılı", result2.OrderBy(x => x.RowNumber).ToList());
            }
            catch (Exception e)
            {
                return new ServiceResult<List<CategoryDto>>(ProcessStateEnum.Error, e.Message, new List<CategoryDto>());
            }
        }

        public ServiceResult Update(CategoryDto dto)
        {
            Expression<Func<Category, bool>> exp = p => p.Id == dto.Id;
            var Category = CategoryRepo.Get(exp).SingleOrDefault();
            Category.LanguageId = dto.LanguageId;
            Category.UpdatedDate = dto.UpdatedDate;
            Category.RowNumber = dto.RowNumber;

            Category.Name= dto.Name;
            Category.MainCatId = dto.MainCatId;
            Category.SeoTitle = dto.SeoTitle;
            Category.SeoDescription= dto.SeoDescription;
            Category.ImageBig= dto.ImageBig;
            Category.ImageSmall= dto.ImageSmall;
            Category.Video1= dto.Video1;
            Category.Video2= dto.Video2;
            Category.Video3= dto.Video3;
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
                        Expression<Func<Category, bool>> exp = p => p.Id == item.Id;
                        var entity = CategoryRepo.Get(exp).SingleOrDefault();
                        entity.RowNumber = item.RowNumber;
                    }
                    result = uow.Save();
                }
            }
            return result;
        }

        public ServiceResult ChangeState(int id, bool state)
        {
            Expression<Func<Category, bool>> exp = p => p.Id == id;
            var Category = CategoryRepo.Get(exp).SingleOrDefault();
            Category.State = state;
            var result = uow.Save();
            return result;
        }

        public ServiceResult<List<CategoryDto>> GetByLangId(int id)
        {
            try
            {
                Expression<Func<Category, bool>> exp = p => p.LanguageId == id;
                var categories = CategoryRepo.Get(exp);

                var result = (from cr1 in categories
                               join cr2 in categories on cr1.MainCatId equals cr2.Id into p
                               from cr2 in p.DefaultIfEmpty()
                               select new CategoryDto
                               {
                                   Id = cr1.Id,
                                   MainCatId = cr1.MainCatId,
                                   MainCatName = cr2 == null ? "Ana Menü" : cr2.Name,
                                   Name = cr1.Name,
                                   ImageBig = cr1.ImageBig,
                                   ImageSmall = cr1.ImageSmall,
                                   LanguageId = cr1.LanguageId,
                                   RowNumber = cr1.RowNumber,
                                   SeoDescription = cr1.SeoDescription,
                                   SeoTitle = cr1.SeoTitle,
                                   State = cr1.State,
                                   UpdatedDate = cr1.UpdatedDate,
                                   Video1 = cr1.Video1,
                                   Video2 = cr1.Video2,
                                   Video3 = cr1.Video3
                               });
                return new ServiceResult<List<CategoryDto>>(ProcessStateEnum.Success, "İşmeniniz başarılı", result.OrderBy(x => x.RowNumber).ToList());
            }
            catch (Exception e)
            {
                return new ServiceResult<List<CategoryDto>>(ProcessStateEnum.Success, e.Message, new List<CategoryDto>());
            }
        }


        #region Mappings
        public Category EntityFromDto(CategoryDto dto)
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CategoryDto, Category>();
            });

            IMapper iMapper = config.CreateMapper();
            var entity = iMapper.Map<CategoryDto, Category>(dto);
            return entity;

        }

        public CategoryDto DtoFromEntity(Category entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, CategoryDto>();
            });

            IMapper iMapper = config.CreateMapper();
            var dto = iMapper.Map<Category, CategoryDto>(entity);
            return dto;
        }

        public List<CategoryDto> DtoFromEntity(List<Category> dtos)
        {
            List<CategoryDto> list = new List<CategoryDto>();
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
