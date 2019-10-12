using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EddarsCms.Dto.BasicDtos
{
    public class BlogCommentDto:DtoBase
    {
        public int BlogId { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public int CommentStatus { get; set; }
    }
}
