using Core.Results;
using EddarsCms.Dto.BasicDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EddarsCms.BLL.IServices
{
    public interface IHumanResourceService
    {
        ServiceResult<List<HumanResourceDto>> GetAll();
        ServiceResult<HumanResourceDto> Get(int id);
        ServiceResult Delete(int id);
        ServiceResult Add(HumanResourceDto dto);
    }
}
