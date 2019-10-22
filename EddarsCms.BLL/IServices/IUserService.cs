using Core.Results;
using EddarsCms.Dto.BasicDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EddarsCms.BLL.IServices
{
    public interface IUserService
    {
        ServiceResult Add(UserDto dto);
        ServiceResult<List<UserDto>> GetAll();
        ServiceResult<UserDto> Get(int id);
        ServiceResult<UserDto> GetUserByNamePassword(UserDto dto);
        ServiceResult<UserDto> Get(string email);
        ServiceResult Delete(int id);
        ServiceResult Update(UserDto dto);
    }
}
