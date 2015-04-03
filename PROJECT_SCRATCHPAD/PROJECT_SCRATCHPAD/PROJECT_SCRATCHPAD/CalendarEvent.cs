using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJECT_SCRATCHPAD
{
    class CalendarEvent
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Format { get; set; }
        public string Courseid { get; set; }
        public string Groupid { get; set; }
        public string Repeatid { get; set; }
        public string Modulename { get; set; }
        public string Instance { get; set; }
        public string Eventtype { get; set; }
        public string TimeStart { get; set; }
        public string TimeDuration { get; set; }
        public string Visible { get; set; }
        public string Uuid { get; set; }
        public string Sequence { get; set; }
        public string TimeModified { get; set; }
        public string SubscriptionId { get; set; }

        public override string ToString()
        {
            return string.Format("{0}: {1} \r\n Info: {2} \r\n Stat: {3}", Id, Name, Description, TimeStart);
        }
    }
}
