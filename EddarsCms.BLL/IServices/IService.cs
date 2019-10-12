using Core.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EddarsCms.BLL.IServices
{
    public interface IService<TDto>
    {
        ServiceResult Add(TDto dto);
        ServiceResult<List<TDto>> GetAll();
        ServiceResult<TDto> Get(int id);
        ServiceResult Delete(int id);
        ServiceResult Update(TDto dto);
    }
}
