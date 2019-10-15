using Core.Results;
using EddarsCms.Dto.BasicDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EddarsCms.BLL.IServices
{
    public interface IContactInfoService
    {
        ServiceResult<ContactInfoDto> Get();
        ServiceResult AddOrUpdate(ContactInfoDto dto);
    }
}
