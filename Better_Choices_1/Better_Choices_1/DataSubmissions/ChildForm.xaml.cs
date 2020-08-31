using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Better_Choices_1.TemplateForms;

namespace Better_Choices_1.DataSubmissions
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChildForm : ContentView
    {
        StackLayout date_parent;
        labeled_date start_date;
        Recurring habit_;
        Habit_Data habit_data;
        public void let_habit(Recurring habit)
        {
            habit_ = habit;
            start_date.Date = habit.date_started;
            this.setup_around_habit(habit);
        }
        public void let_habit(Habit_Data habit)
        {
            habit_data = habit;
            start_date.Date = habit.date_run;
            this.setup_around_habit(habit);
        }

        public virtual void setup_around_habit(Recurring habit)
        {

        }
        public virtual void setup_around_habit(Habit_Data habit)
        {

        }

        public Grid Common_ { get { return Common; }}
        public ChildForm()
        {
            InitializeComponent();
            start_date = new labeled_date("Start date:");
            //date_parent = new StackLayout();
            Common_.Children.Add(start_date, 0, 0);
            //date_parent.Children.Add(start_date);
            // Common.Children.Insert(0,Common_);
            //Common.Orientation = Xamarin.Forms.StackOrientation.Horizontal;
        }

        
        public void let_text(string str) { start_date.let_text(str); }
        public virtual void refresh(){}
        public virtual DateTime end_date(){ return default(DateTime); }
        public virtual DateTime get_start_date() { return start_date.Date; }
        public virtual DateTime get_end_date() { return start_date.Date; }
        public virtual string helper_str() { return ""; }
        public virtual string type() { return "One Time"; }
        public virtual string frequency_1() { return "Default"; }
        public virtual double how_common_1() { return 1; }
        public virtual string frequency_2() { return "Default"; }

        public virtual double how_common_2() { return 1; }
    }
}