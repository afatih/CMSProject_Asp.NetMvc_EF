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
using System.Text;
using System.Threading.Tasks;

namespace EddarsCms.BLL.Services
{
    public class NotificationService : INotificationService
    {
        IRepository<Notification> notRepo;
        IUnitOfWork uow;

        public NotificationService()
        {
            uow = Resource.UoW;
            notRepo = uow.GetRepository<Notification>();
        }

        public ServiceResult DeleteAll()
        {
            //Bu kısım hepsini silecek şekilde güncellenecek....
            notRepo.HardDelete(x => x.Id < 12);
            var result = uow.Save();
            return result;
        }


        public ServiceResult<List<NotificationDto>> GetAll()
        {
            try
            {
                var result = DtoFromEntity(notRepo.Get(x => x.Id > 0).OrderByDescending(x=>x.Id).ToList());
                if (result!=null)
                {
                    if (result.Count>0)
                    {
                        //çekilen bildirimlerin ne zaman once geldiği hesaplanıyor
                        foreach (var notification in result)
                        {
                            if (notification.Date != null)
                            {
                                TimeSpan TS = DateTime.Now - notification.Date;
                                int hour = TS.Hours;
                                int mins = TS.Minutes;
                                int secs = TS.Seconds;
                                int day = TS.Days;
                                if (day > 0)
                                {
                                    notification.DateString = notification.Date.ToString("dd MMMM yyyy");
                                }
                                else if (hour > 0)
                                {
                                    notification.DateString = TS.Hours + " saat önce";
                                }
                                else if (mins > 0)
                                {
                                    notification.DateString = TS.Minutes + " dakika önce";
                                }
                                else if (secs < 0 || secs > 0)
                                {
                                    if (secs > 0)
                                    {
                                        notification.DateString = TS.Seconds + " saniye önce";
                                    }
                                    else
                                    {
                                        //(60 + TS.Seconds) oalbilir
                                        notification.DateString = (-1 * TS.Seconds) + " saniye önce";
                                    }

                                }
                                else
                                {
                                    notification.DateString = notification.Date.ToString("dd MMMM yyyy");
                                }
                            }
                        }
                    
                    }
                }
                return new ServiceResult<List<NotificationDto>>(ProcessStateEnum.Success, "İşlem başarılı", result);

            }
            catch (Exception e)
            {

                return new ServiceResult<List<NotificationDto>>(ProcessStateEnum.Error, e.Message, new List<NotificationDto>());
            }
            

        }
       
        public ServiceResult<NotificationDto> Get(int id)
        {
            try
            {
                var notification = DtoFromEntity(notRepo.Get(x => x.Id == id)).SingleOrDefault();
                if (notification.Date != null)
                {
                    TimeSpan TS = DateTime.Now - notification.Date;
                    int hour = TS.Hours;
                    int mins = TS.Minutes;
                    int secs = TS.Seconds;
                    int day = TS.Days;
                    if (day > 0)
                    {
                        notification.DateString = notification.Date.ToString("dd MMMM yyyy");
                    }
                    else if (hour > 0)
                    {
                        notification.DateString = TS.Hours + " saat önce";
                    }
                    else if (mins > 0)
                    {
                        notification.DateString = TS.Minutes + " dakika önce";
                    }
                    else if (secs < 0 || secs > 0)
                    {
                        if (secs > 0)
                        {
                            notification.DateString = TS.Seconds + " saniye önce";
                        }
                        else
                        {
                            //(60 + TS.Seconds) oalbilir
                            notification.DateString = (-1 * TS.Seconds) + " saniye önce";
                        }

                    }
                    else
                    {
                        notification.DateString = notification.Date.ToString("dd MMMM yyyy");
                    }
                }
                return new ServiceResult<NotificationDto>(ProcessStateEnum.Success, "İşlem başarılı", notification);

            }
            catch (Exception e)
            {

                return new ServiceResult<NotificationDto>(ProcessStateEnum.Error, e.Message, new NotificationDto());
            }
        }

        public ServiceResult Delete(int id)
        {
            notRepo.HardDelete(x => x.Id == id);
            var result = uow.Save();
            return result;
        }


        #region Mappings

        public Notification EntityFromDto(NotificationDto dto)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<NotificationDto, Notification>();
            });

            IMapper iMapper = config.CreateMapper();
            var blog = iMapper.Map<NotificationDto, Notification>(dto);
            return blog;
        
        }

        public NotificationDto DtoFromEntity(Notification blog)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Notification, NotificationDto>();
            });

            IMapper iMapper = config.CreateMapper();
            var blogDto = iMapper.Map<Notification, NotificationDto>(blog);
            return blogDto;

        }

        public List<NotificationDto> DtoFromEntity(List<Notification> blogs)
        {
            List<NotificationDto> list = new List<NotificationDto>();
            if (blogs != null)
            {
                if (blogs.Count > 0)
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
