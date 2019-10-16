using Core.Results;
using EddarsCms.Dto.BasicDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EddarsCms.BLL.IServices
{
    public interface IContactMailService
    {
        ServiceResult<List<ContactMailDto>> GetAll();
        ServiceResult<ContactMailDto> Get(int id);
        ServiceResult Delete(int id);
    }
}
