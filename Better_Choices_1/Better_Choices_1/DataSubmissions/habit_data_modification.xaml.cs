using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Better_Choices_1.DataSubmissions
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class  habit_data_modification : Better_Choices_1.TemplateForms.DataSubmissionForm
   
    {
        Habit_Data habit_data_;
        Base_Data_Entry data_entry;
        public habit_data_modification(Habit_Data habit_data)
        {
            habit_data_ = habit_data;
            InitializeComponent();
            data_entry = new DataSubmissions.Base_Data_Entry(habit_data);
            maincontent.Content = data_entry.Content;
        }
        public habit_data_modification(Recurring habit)
        {
            habit_data_ = null;
            InitializeComponent();
            data_entry = new DataSubmissions.Base_Data_Entry(habit);
            maincontent.Content = data_entry.Content;

        }
        public override void SaveForm()
        {
            if (habit_data_ != null)
            {
                var Habit_ = data_entry.GetHabitData();
                int job_id = App.Database.SaveItemAsync(Habit_).Id;
            }
            else
            {
                var Habit_ = data_entry.GetHabit();
                int job_id = App.Database.SaveItemAsync(Habit_).Id;
            }
            
            // data_entry.save_Habit_Data(Habit_);
            base.SaveForm();
        }

    }
}