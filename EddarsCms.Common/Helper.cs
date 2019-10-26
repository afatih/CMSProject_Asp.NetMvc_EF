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

        /// <summary>
        /// Editöre basmadan once db deki içerikteki tırnakları temizliyoruz
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string ChangeQuatitionForEditor(string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                content = content.Replace('\'', '`');
            }

            return content;
        }


        /// <summary>
        /// Db ye kaydetmeden once orjinal tırnak işaretine çeviriyoruz içeriği
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string ChangeQuatitionForDb(string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                content = content.Replace('`', '\'');
            }

            return content;
        }
    }
}
