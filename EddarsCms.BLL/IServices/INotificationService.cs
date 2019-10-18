using Core.Results;
using EddarsCms.Dto.BasicDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EddarsCms.BLL.IServices
{
    public interface INotificationService
    {
        ServiceResult<NotificationDto> Get(int id);
        ServiceResult<List<NotificationDto>> GetAll();
        ServiceResult Delete(int id);
        ServiceResult DeleteAll();

    }
}
