using System;
using System.Collections.Generic;
using System.Text;

namespace Better_Choices_1.Analytics
{
    public class Date_Value
    {
        public Date_Value() { }
        public Date_Value(string Date_,double value_) {
            this.Date = Date_;
            this.value = value_;
        }
        public string Date { get; set; }
        public double value { get; set; }
        public static Date_Value operator +(Date_Value a, double b) => new Date_Value(a.Date, b + a.value);
        public static Date_Value operator +(double b, Date_Value a) => new Date_Value(a.Date, b + a.value);
    }
}
