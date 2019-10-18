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
    public class ProductService : IProductService
    {
        IRepository<Product> productRepo;
        IRepository<Category> categoryRepo;
        IUnitOfWork uow;

        public ProductService()
        {
            productRepo = Resource.UoW.GetRepository<Product>();
            categoryRepo = Resource.UoW.GetRepository<Category>();
            uow = Resource.UoW;
        }



        public ServiceResult Add(ProductDto dto)
        {
            productRepo.Add(EntityFromDto(dto));
            var result = uow.Save();
            return result;
        }

        public ServiceResult Delete(int id)
        {
            Expression<Func<Product, bool>> exp = p => p.Id == id;
            productRepo.HardDelete(exp);
            var result = uow.Save();
            return result;
        }

        public ServiceResult<ProductDto> Get(int id)
        {
            try
            {
                Expression<Func<Product, bool>> exp = p => p.Id == id;
                var result = DtoFromEntity(productRepo.Get(exp).SingleOrDefault());
                return new ServiceResult<ProductDto>(ProcessStateEnum.Success, "İşmeniniz başarılı", result);
            }
            catch (Exception e)
            {
                return new ServiceResult<ProductDto>(ProcessStateEnum.Error, e.Message, new ProductDto());
            }

        }

        public ServiceResult<List<ProductDto>> GetAll()
        {
            try
            {

                var products = productRepo.Get(x => x.Id >= 0);
                var categories = categoryRepo.Get(x => x.Id >= 0);

                var result = (from p1 in products
                              join c in categories on p1.MainCatId equals c.Id into p
                              from c in p.DefaultIfEmpty()
                              select new ProductDto
                              {
                                  Id = p1.Id,
                                  MainProdId = p1.MainProdId,
                                  MainCatName= c == null ? "" : c.Name,
                                  MainCatId = p1.MainCatId,
                                  Name = p1.Name,
                                  ImageBig = p1.ImageBig,
                                  ImageSmall = p1.ImageSmall,
                                  LanguageId = p1.LanguageId,
                                  RowNumber = p1.RowNumber,
                                  SeoDescription = p1.SeoDescription,
                                  SeoTitle = p1.SeoTitle,
                                  State = p1.State,
                                  UpdatedDate = p1.UpdatedDate,
                                  Video1 = p1.Video1,
                                  Video2 = p1.Video2,
                                  Video3 = p1.Video3,
                                  Caption = p1.Caption,
                                  Content = p1.Content,
                                  Description = p1.Description
                              });

               
                return new ServiceResult<List<ProductDto>>(ProcessStateEnum.Success, "İşmeniniz başarılı", result.OrderBy(x => x.RowNumber).ToList());
            }
            catch (Exception e)
            {
                return new ServiceResult<List<ProductDto>>(ProcessStateEnum.Error, e.Message, new List<ProductDto>());
            }
        }

        public ServiceResult Update(ProductDto dto)
        {
            Expression<Func<Product, bool>> exp = p => p.Id == dto.Id;
            var Product = productRepo.Get(exp).SingleOrDefault();
            Product.LanguageId = dto.LanguageId;
            Product.UpdatedDate = dto.UpdatedDate;
            Product.RowNumber = dto.RowNumber;

            Product.Name = dto.Name;
            Product.MainCatId = dto.MainCatId;
            Product.MainProdId = dto.MainProdId;
            Product.SeoTitle = dto.SeoTitle;
            Product.SeoDescription = dto.SeoDescription;
            Product.ImageBig = dto.ImageBig;
            Product.ImageSmall = dto.ImageSmall;
            Product.Image3 = dto.Image3;
            Product.Image4 = dto.Image4;
            Product.Video1 = dto.Video1;
            Product.Video2 = dto.Video2;
            Product.Video3 = dto.Video3;
            Product.Content = dto.Content;
            Product.Description = dto.Description;
            Product.Caption = dto.Caption;
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
                        Expression<Func<Product, bool>> exp = p => p.Id == item.Id;
                        var entity = productRepo.Get(exp).SingleOrDefault();
                        entity.RowNumber = item.RowNumber;
                    }
                    result = uow.Save();
                }
            }
            return result;
        }

        public ServiceResult ChangeState(int id, bool state)
        {
            Expression<Func<Product, bool>> exp = p => p.Id == id;
            var Product = productRepo.Get(exp).SingleOrDefault();
            Product.State = state;
            var result = uow.Save();
            return result;
        }

        public ServiceResult<List<ProductDto>> GetByLangId(int id)
        {
            try
            {
                Expression<Func<Product, bool>> exp = p => p.LanguageId == id;

                var products = productRepo.Get(exp);
                var categories = categoryRepo.Get(x => x.Id >= 0);

                var result = (from p1 in products
                              join c in categories on p1.MainCatId equals c.Id into p
                              from c in p.DefaultIfEmpty()
                              select new ProductDto
                              {
                                  Id = p1.Id,
                                  MainProdId = p1.MainProdId,
                                  MainCatName = c == null ? "" : c.Name,
                                  MainCatId = p1.MainCatId,
                                  Name = p1.Name,
                                  ImageBig = p1.ImageBig,
                                  ImageSmall = p1.ImageSmall,
                                  LanguageId = p1.LanguageId,
                                  RowNumber = p1.RowNumber,
                                  SeoDescription = p1.SeoDescription,
                                  SeoTitle = p1.SeoTitle,
                                  State = p1.State,
                                  UpdatedDate = p1.UpdatedDate,
                                  Video1 = p1.Video1,
                                  Video2 = p1.Video2,
                                  Video3 = p1.Video3,
                                  Caption = p1.Caption,
                                  Content = p1.Content,
                                  Description = p1.Description
                              });
                return new ServiceResult<List<ProductDto>>(ProcessStateEnum.Success, "İşmeniniz başarılı", result.OrderBy(x => x.RowNumber).ToList());
            }
            catch (Exception e)
            {
                return new ServiceResult<List<ProductDto>>(ProcessStateEnum.Success, e.Message, new List<ProductDto>());
            }
        }


        #region Mappings
        public Product EntityFromDto(ProductDto dto)
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductDto, Product>();
            });

            IMapper iMapper = config.CreateMapper();
            var entity = iMapper.Map<ProductDto, Product>(dto);
            return entity;

        }

        public ProductDto DtoFromEntity(Product entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDto>();
            });

            IMapper iMapper = config.CreateMapper();
            var dto = iMapper.Map<Product, ProductDto>(entity);
            return dto;
        }

        public List<ProductDto> DtoFromEntity(List<Product> dtos)
        {
            List<ProductDto> list = new List<ProductDto>();
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
