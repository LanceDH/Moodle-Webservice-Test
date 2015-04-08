using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moodle
{
    class Model
    {
        public static List<Moodle.BLL.Assignment> AssingmentSelectAll(string url, string token, int courseId)
        {
            return DAL.Assignment.SelectAll(url, token, courseId);
        }

        public static List<BLL.Grade> GradeSelectAll(string url, string token, int assignmentId)
        {
            return DAL.Grade.SelectAll(url, token, assignmentId);
        }

        public static string GradeSelectOne(string url, string token, int assignmentId, int userId)
        {
            foreach (BLL.Grade grade in DAL.Grade.SelectAll(url, token, assignmentId))
	        {
		        if (grade.UserId == userId)
	            {
		            return grade.Score;
	            }
	        }
            return "-1";
        }

    }
}
