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
    public class BlogCommentService : IBlogCommentService
    {
        IRepository<BlogComment> blogCommentRepo;
        IRepository<Blog> blogRepo;
        IRepository<Notification> notRepo;
        IUnitOfWork uow;

        public BlogCommentService()
        {
            blogCommentRepo = Resource.UoW.GetRepository<BlogComment>();
            notRepo = Resource.UoW.GetRepository<Notification>();
            blogRepo = Resource.UoW.GetRepository<Blog>();
            uow = Resource.UoW;
        }

        public ServiceResult Add(BlogCommentDto dto)
        {

            blogCommentRepo.Add(EntityFromDto(dto));
            var result = uow.Save();
            if (result.State==ProcessStateEnum.Success)
            {
                Notification not = new Notification()
                {
                    Caption = "Yeni blog yorumu var",
                    Date = dto.Date,
                    Description = "Kullanıcı Adı: " + dto.UserName +", BlogId: "+dto.BlogId+ ", Yorum:" + dto.Comment,
                    Icon = "bg-orange icon-notification glyph-icon icon-user"
                };
                notRepo.Add(not);
                var result2 = uow.Save();
            }
            return result;
        }


        public ServiceResult Delete(int id)
        {
            Expression<Func<BlogComment, bool>> exp = p => p.Id == id;
            blogCommentRepo.HardDelete(exp);
            var result = uow.Save();
            return result;
        }

        public ServiceResult<BlogCommentDto> Get(int id)
        {
            try
            {
                Expression<Func<BlogComment, bool>> exp = p => p.Id == id;
                var result = DtoFromEntity(blogCommentRepo.Get(exp).SingleOrDefault());
                if (result!=null)
                {
                    var blog = blogRepo.Get(x => x.Id == result.BlogId).SingleOrDefault();
                    result.BlogName = blog.Caption;
                }
                return new ServiceResult<BlogCommentDto>(ProcessStateEnum.Success, "İşmeniniz başarılı", result);
            }
            catch (Exception e)
            {
                return new ServiceResult<BlogCommentDto>(ProcessStateEnum.Error, e.Message, new BlogCommentDto());
            }

        }

        public ServiceResult<List<BlogCommentDto>> GetAll()
        {
            try
            {
                var blogComments = blogCommentRepo.Get(x => x.Id > 0);
                var blogs = blogRepo.Get(x => x.Id > 0);
                var result2 = (from bc in blogComments
                               join b in blogs on bc.BlogId equals b.Id
                               select new BlogCommentDto
                               {
                                   BlogName = b.Caption,
                                   UserName = bc.UserName,
                                   UserEmail = bc.UserEmail,
                                   Id = bc.Id,
                                   BlogId = bc.BlogId,
                                   Comment = bc.Comment,
                                   Date = bc.Date,
                                   LanguageId = bc.LanguageId,
                                   RowNumber = bc.RowNumber,
                                   State = bc.State
                               });



                return new ServiceResult<List<BlogCommentDto>>(ProcessStateEnum.Success, "İşmeniniz başarılı", result2.OrderBy(x => x.Date).ToList());
            }
            catch (Exception e)
            {
                return new ServiceResult<List<BlogCommentDto>>(ProcessStateEnum.Success, e.Message, new List<BlogCommentDto>());
            }
        }



        public ServiceResult<List<BlogCommentDto>> GetByLangId(int id)
        {
            try
            {
                var blogComments = blogCommentRepo.Get(x => x.LanguageId == id);
                var blogs = blogRepo.Get(x => x.Id>0);
                var result = (from bc in blogComments
                               join b in blogs on bc.BlogId equals b.Id
                               select new BlogCommentDto
                               {
                                   BlogName = b.Caption,
                                   UserName = bc.UserName,
                                   UserEmail = bc.UserEmail,
                                   Id = bc.Id,
                                   BlogId = bc.BlogId,
                                   Comment = bc.Comment,
                                   Date = bc.Date,
                                   LanguageId = bc.LanguageId,
                                   RowNumber = bc.RowNumber,
                                   State = bc.State
                               });

                return new ServiceResult<List<BlogCommentDto>>(ProcessStateEnum.Success, "İşmeniniz başarılı", result.OrderBy(x => x.RowNumber).ToList());
            }
            catch (Exception e)
            {
                return new ServiceResult<List<BlogCommentDto>>(ProcessStateEnum.Success, e.Message, new List<BlogCommentDto>());
            }
        }


        public ServiceResult ChangeState(int id, bool state)
        {
            Expression<Func<BlogComment, bool>> exp = p => p.Id == id;
            var blog = blogCommentRepo.Get(exp).SingleOrDefault();
            blog.State = state;
            var result = uow.Save();
            return result;
        }

        #region Mappings
        public BlogComment EntityFromDto(BlogCommentDto dto)
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BlogCommentDto, BlogComment>();
            });

            IMapper iMapper = config.CreateMapper();
            var entity = iMapper.Map<BlogCommentDto, BlogComment>(dto);
            return entity;

        }

        public BlogCommentDto DtoFromEntity(BlogComment entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BlogComment, BlogCommentDto>();
            });

            IMapper iMapper = config.CreateMapper();
            var dto = iMapper.Map<BlogComment, BlogCommentDto>(entity);
            return dto;
        }

        public List<BlogCommentDto> DtoFromEntity(List<BlogComment> dtos)
        {
            List<BlogCommentDto> list = new List<BlogCommentDto>();
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
