using System;
using System.Collections.Generic;
using System.Text;

namespace Better_Choices_1.utils_data
{
    class FrequencyTranslator
    {
        Dictionary<string, double> translations;
        public FrequencyTranslator()
        {
            translations = new Dictionary<string, double> {
                {"Hours",(1.0/24.0)/365.0},
                {"Days",(1.0/365.0)},
                {"Weeks",(7.0/365.0)},
                { "Months", (1.0/12.0)},
                { "Default",1.0},
                { "One Time",1.0}

            };

        }
        public DateTime AddDate(Recurring habit_, DateTime occurence)
        {
            switch (habit_.frequency)
            {
                case "Hours":
                    return occurence.AddHours(habit_.how_common);
                case "Days":
                    return occurence.AddDays(habit_.how_common);
                case "Weeks":
                    return occurence.AddDays(7 * habit_.how_common);
                case "Months":
                    return occurence.AddMonths(Convert.ToInt32(habit_.how_common));
                case "Default":
                    //
                    return occurence.AddDays(1);
                case "One Time":
                    //
                    return occurence.AddDays(1);

            }
            // to prevent an infinite recursion loop
            return occurence.AddDays(1);

        }
        public double annualization_factor(string frequency,double how_common)
        {
            return translations[frequency] * how_common;
        }
        public double get_savings(double money_1, string frequency_1, double how_common_1,
                                    double money_2, string frequency_2, double how_common_2)
        {
            double savings = ((money_1 / (how_common_1 * translations[frequency_1]))) - (((money_2 / (how_common_2 * translations[frequency_2]))));
            savings = savings * translations[frequency_1] / how_common_1;
            return savings;
        }
        public string helper_string(double money_1, string frequency_1, double how_common_1,
                                    double money_2, string frequency_2, double how_common_2)
        {
            translations = new Dictionary<string, double> {
                {"Hours",(1.0/24.0)/365.0},
                {"Days",(1.0/365.0)},
                {"Weeks",(7.0/365.0)},
                { "Months", (1.0/12.0)},
                { "Default",1.0},
                { "One Time",1.0}

            };
            string output = "Every  " + Convert.ToString(how_common_1) + " " + frequency_1 + " I will Save: ";
            double savings = this.get_savings(money_1, frequency_1, how_common_1, money_2, frequency_2, how_common_2);


            output += Convert.ToString(savings); 
            return output;
        }
    }
}
