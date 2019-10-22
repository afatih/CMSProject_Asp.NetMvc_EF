using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EddarsCms.Common
{
    public static class Helper
    {
        public static string ToString(this DateTime? dt, string format) => dt == null ? "n/a" : ((DateTime)dt).ToString(format);
    }
}
