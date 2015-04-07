using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJECT_SCRATCHPAD
{
    class Model
    {
        public static List<PROJECT_SCRATCHPAD.BLL.Assignment> AssingmentSelectAll(string url, string token, int courseId)
        {
            return DAL.Assignment.SelectAll(url, token, courseId);
        }

    }
}
