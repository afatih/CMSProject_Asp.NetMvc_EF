using Core.Results;
using EddarsCms.Dto.BasicDtos;
using EddarsCms.Dto.OtherDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EddarsCms.BLL.IServices
{
    public interface IPageService:IService<PageDto>
    {
        ServiceResult ChangeState(int id, bool state);
    }
}
