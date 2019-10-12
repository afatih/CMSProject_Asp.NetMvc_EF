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
    public class CategoryService : ICategoryService
    {
        IRepository<Category> _cr;
        IUnitOfWork _uow;

        public CategoryService()
        {
            _cr = Resource.UoW.GetRepository<Category>();
            _uow = Resource.UoW;
        }


        public ServiceResult Add(CategoryDto categoryDto)
        {
            _cr.Add(EntityFromDto(categoryDto));
            var result = _uow.Save();
            return result;
            
        }

        public ServiceResult<CategoryDto> Get(int id)
        {
            throw new NotImplementedException();
        }

        public ServiceResult Delete(int id)
        {
            throw new NotImplementedException();
        }


        public ServiceResult<List<CategoryDto>> GetAll()
        {
            try
            {
                Expression<Func<Category, bool>> exp = p => p.Id > 0;
                var categories = DtoFromEntity(_cr.Get(exp));
                return new ServiceResult<List<CategoryDto>>(ProcessStateEnum.Success, "İşlem başarılı", categories);
            }
            catch (Exception e)
            {
                return new ServiceResult<List<CategoryDto>>(ProcessStateEnum.Error, e.Message , new List<CategoryDto>());
            }
           

            
        }

        public ServiceResult Update(CategoryDto dto)
        {
            throw new NotImplementedException();
        }



        #region Mappings
        public Category EntityFromDto(CategoryDto categoryDto)
        {
            Category category = new Category()
            {
                Name = categoryDto.Name,
                Number = categoryDto.Number

            };
            return category;
        }

        public CategoryDto DtoFromEntity(Category category)
        {
            CategoryDto categoryDto = new CategoryDto()
            {
                Id = category.Id,
                Name = category.Name,
                Number = category.Number
            };
            return categoryDto;
        }

        public List<CategoryDto> DtoFromEntity(List<Category> categories)
        {
            List<CategoryDto> list = new List<CategoryDto>();
            if (categories!=null)
            {
                if (categories.Count>0)
                {
                    foreach (var category in categories)
                    {
                        list.Add(DtoFromEntity(category));

                    }
                }
            }
            return list;
        }

       


        #endregion



    }
}
