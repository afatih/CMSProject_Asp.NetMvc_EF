using Core.Results;
using EddarsCms.Dto.BasicDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EddarsCms.BLL.IServices
{
    public interface IBlogCommentService
    {
        ServiceResult Add(BlogCommentDto dto);
        ServiceResult<List<BlogCommentDto>> GetAll();
        ServiceResult<BlogCommentDto> Get(int id);
        ServiceResult Delete(int id);
        ServiceResult<List<BlogCommentDto>> GetByLangId(int id);
        ServiceResult ChangeState(int id, bool state);
        //ServiceResult Reorder(List<BlogCommentDto> list);
    }
}
