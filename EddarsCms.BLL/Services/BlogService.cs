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
    public class BlogService : IBlogService
    {
        IRepository<Blog> blogRepo;
        IUnitOfWork uow;

        public BlogService()
        {
            blogRepo = Resource.UoW.GetRepository<Blog>();
            uow = Resource.UoW;
        }
        public ServiceResult Add(BlogDto dto)
        {
            blogRepo.Add(EntityFromDto(dto));
            var result = uow.Save();
            return result;
            
        }
        public ServiceResult<List<BlogDto>> GetAll()
        {
            try
            {
                Expression<Func<Blog, bool>> exp = p => p.Id > 0;
                var blogs = DtoFromEntity(blogRepo.Get(exp));
                return new ServiceResult<List<BlogDto>>(ProcessStateEnum.Success, "İşleminiz başarılı", blogs.OrderBy(x => x.RowNumber).ToList());

            }
            catch (Exception e)
            {
                return new ServiceResult<List<BlogDto>>(ProcessStateEnum.Error, e.Message, new List<BlogDto>());
                throw;
            }
        }
        public ServiceResult<BlogDto> Get(int id)
        {
            try
            {
                Expression<Func<Blog, bool>> exp = p => p.Id == id;
                var blog = DtoFromEntity(blogRepo.Get(exp).SingleOrDefault());
                return new ServiceResult<BlogDto>(ProcessStateEnum.Success, "İşlem başarılı", blog);

            }
            catch (Exception e)
            {
                return new ServiceResult<BlogDto>(ProcessStateEnum.Error, e.Message, new BlogDto());
            }
            
        }
        public ServiceResult Delete(int id)
        {
            Expression<Func<Blog, bool>> exp = p => p.Id == id;
            blogRepo.HardDelete(exp);
            var result = uow.Save();
            return result;
        }
        public ServiceResult Update(BlogDto dto)
        {
            Expression<Func<Blog, bool>> exp = p => p.Id == dto.Id;
            var blog = blogRepo.Get(exp).SingleOrDefault();
            blog.AcceptComment = dto.AcceptComment;
            blog.BlogBegin = dto.BlogBegin;
            blog.Caption = dto.Caption;
            blog.Content = dto.Content;
            blog.Image = dto.Image;
            blog.LanguageId = dto.LanguageId;
            //kaldıılabilir burdaki işlem;
            blog.RowNumber = dto.RowNumber;
            blog.SeoDescription = dto.SeoDescription;
            blog.SeoKeywords = dto.SeoKeywords;
            blog.SeoTitle = dto.SeoTitle;
            //blog.State = dto.State;
            blog.UpdatedDate = dto.UpdatedDate;
            blog.Url = dto.Url;
            var result = uow.Save();
            return result;
        }

        public ServiceResult ChangeState(int id, bool state)
        {
            Expression<Func<Blog, bool>> exp = p => p.Id == id;
            var blog = blogRepo.Get(exp).SingleOrDefault();
            blog.State = state;
            var result = uow.Save();
            return result;
        }


        #region Mappings

        public Blog EntityFromDto (BlogDto dto)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BlogDto, Blog>();
            });

            IMapper iMapper = config.CreateMapper();
            var blog = iMapper.Map<BlogDto, Blog>(dto);
            return blog;


            //Blog blog = new Blog()
            //{
            //    AcceptComment = dto.AcceptComment,
            //    BlogBegin = dto.BlogBegin,
            //    Caption = dto.Caption,
            //    Content = dto.Content,
            //    CreatedDate = dto.CreatedDate,
            //    Image = dto.Image,
            //    LanguageId = dto.LanguageId,
            //    RowNumber = dto.RowNumber,
            //    SeoDescription = dto.SeoDescription,
            //    SeoKeywords = dto.SeoKeywords,
            //    SeoTitle = dto.SeoTitle,
            //    State = dto.DefaultState,
            //    UpdatedDate = dto.UpdatedDate,
            //    Url = dto.Url

            //};
            //return blog;            
        }

        public BlogDto DtoFromEntity (Blog blog)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Blog, BlogDto>();
            });

            IMapper iMapper = config.CreateMapper();
            var blogDto = iMapper.Map<Blog, BlogDto>(blog);
            return blogDto;


            //BlogDto dto = new BlogDto()
            //{
            //    Id = blog.Id,
            //    AcceptComment = blog.AcceptComment,
            //    BlogBegin = blog.BlogBegin,
            //    Caption = blog.Caption,
            //    Content = blog.Content,
            //    CreatedDate = blog.CreatedDate,
            //    Image = blog.Image,
            //    LanguageId = blog.LanguageId,
            //    RowNumber = blog.RowNumber,
            //    SeoDescription = blog.SeoDescription,
            //    SeoKeywords = blog.SeoKeywords,
            //    SeoTitle = blog.SeoTitle,
            //    State = blog.State,
            //    UpdatedDate = blog.UpdatedDate,
            //    Url = blog.Url
            //};
            //return dto;
        }

        public List<BlogDto> DtoFromEntity (List<Blog> blogs)
        {
            List<BlogDto> list = new List<BlogDto>();
            if (blogs!=null)
            {
                if (blogs.Count>0)
                {
                    foreach (var blog in blogs)
                    {
                        list.Add(DtoFromEntity(blog));
                    }

                }
            }
            return list;
        }

        

        #endregion


    }
}
