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
    public interface ISliderService:IService<SliderDto>
    {
        ServiceResult Reorder(List<ReorderDto> list);
        ServiceResult ChangeState(int id, bool state);

        
    }
}
