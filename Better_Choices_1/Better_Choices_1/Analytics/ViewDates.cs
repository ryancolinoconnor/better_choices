using System;
using System.Collections.Generic;
using System.Text;

namespace Better_Choices_1.Analytics
{
    
    class ViewDates
    {
        public List<Better_Choices_1.Analytics.Date_Value> Data { get; set; }
        public ViewDates() {
            this.Data = App.Database.money_saved_per_date();
        }
        public ViewDates(string filter = "",DateTime? start_date=null,DateTime? end_date=null)
        {
            this.Data = App.Database.money_saved_per_date(filter,start_date,end_date);
        }
    }
}
