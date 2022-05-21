using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bitcomTickets.Core.Types
{
    public class Month
    {
        public Month() 
        {
            InitCurrentMonth();
        }
        private DateTime today;
        public DateTime FirsDayOfMonth { get; private set; }
        public DateTime LastDayOfMonth { get; private set; }

        private void InitCurrentMonth() 
        {
            today = DateTime.Now;
            int monthDays = DateTime.DaysInMonth(today.Year, today.Month);
            FirsDayOfMonth = new DateTime(today.Year, today.Month,1,0,0,0 );
            LastDayOfMonth = new DateTime(today.Year, today.Month, monthDays, 23, 59, 59);
        }


    }
}
