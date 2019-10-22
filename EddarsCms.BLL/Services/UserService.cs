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
    public class UserService : IUserService
    {
        IRepository<User> userRepo;
        IUnitOfWork uow;

        public UserService()
        {
            userRepo = Resource.UoW.GetRepository<User>();
            uow = Resource.UoW;
        }
        public ServiceResult Add(UserDto dto)
        {
            userRepo.Add(EntityFromDto(dto));
            var result = uow.Save();
            return result;

        }
        public ServiceResult<List<UserDto>> GetAll()
        {
            try
            {
                Expression<Func<User, bool>> exp = p => p.Id > 0;
                var users = DtoFromEntity(userRepo.Get(exp));
                return new ServiceResult<List<UserDto>>(ProcessStateEnum.Success, "İşleminiz başarılı", users);

            }
            catch (Exception e)
            {
                return new ServiceResult<List<UserDto>>(ProcessStateEnum.Error, e.Message, new List<UserDto>());
            }
        }
        public ServiceResult<UserDto> Get(int id)
        {
            try
            {
                var user = DtoFromEntity(userRepo.Get(x => x.Id == id).SingleOrDefault());
                return new ServiceResult<UserDto>(ProcessStateEnum.Success, "İşlem başarılı", user);

            }
            catch (Exception e)
            {
                return new ServiceResult<UserDto>(ProcessStateEnum.Error, e.Message, new UserDto());
            }

        }
        public ServiceResult Delete(int id)
        {
            Expression<Func<User, bool>> exp = p => p.Id == id;
            userRepo.HardDelete(exp);
            var result = uow.Save();
            return result;
        }
        public ServiceResult Update(UserDto dto)
        {
            Expression<Func<User, bool>> exp = p => p.Id == dto.Id;
            var user = userRepo.Get(exp).SingleOrDefault();
            user.Name = dto.Name;
            user.Surname = dto.Surname;
            user.UserName = dto.UserName;
            user.Password = dto.Password;
            user.EMail = dto.EMail;
            var result = uow.Save();
            return result;
        }


        public ServiceResult<UserDto> GetUserByNamePassword(UserDto dto)
        {

            try
            {
                Expression<Func<User, bool>> exp = p => p.UserName == dto.UserName && p.Password == dto.Password;
                var user = DtoFromEntity(userRepo.Get(exp)).SingleOrDefault();
                return new ServiceResult<UserDto>(ProcessStateEnum.Success, "İşlem başarılı", user);

            }
            catch (Exception e)
            {
                return new ServiceResult<UserDto>(ProcessStateEnum.Error, e.Message, new UserDto());
            }
        }

        public ServiceResult<UserDto> Get(string email)
        {
            try
            {
                Expression<Func<User, bool>> exp = p => p.EMail == email;
                var user = DtoFromEntity(userRepo.Get(exp)).SingleOrDefault();
                return new ServiceResult<UserDto>(ProcessStateEnum.Success, "İşlem başarılı", user);

            }
            catch (Exception e)
            {
                return new ServiceResult<UserDto>(ProcessStateEnum.Error, e.Message, new UserDto());
            }
        }


        #region Mappings

        public User EntityFromDto(UserDto dto)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDto, User>();
            });

            IMapper iMapper = config.CreateMapper();
            var user = iMapper.Map<UserDto, User>(dto);
            return user;



        }

        public UserDto DtoFromEntity(User user)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDto>();
            });

            IMapper iMapper = config.CreateMapper();
            var userDto = iMapper.Map<User, UserDto>(user);
            return userDto;


        }

        public List<UserDto> DtoFromEntity(List<User> users)
        {
            List<UserDto> list = new List<UserDto>();
            if (users != null)
            {
                if (users.Count > 0)
                {
                    foreach (var user in users)
                    {
                        list.Add(DtoFromEntity(user));
                    }

                }
            }
            return list;
        }










        #endregion
    }
}
