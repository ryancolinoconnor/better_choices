using System;
using Xamarin.Forms.Xaml;

using Better_Choices_1.TemplateForms;

namespace Better_Choices_1.DataSubmissions
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Bulk_Purchase : ChildForm
    {
        Better_Choices_1.DataSubmissions.ControlTemplates.FrequencySelector freq_;
        Better_Choices_1.DataSubmissions.ControlTemplates.FrequencySelector freq_2;
        labeled_date end_date_picker;
        Recurring habit_;
        public Bulk_Purchase(): base()
        {
            InitializeComponent();
            freq_ = new Better_Choices_1.DataSubmissions.ControlTemplates.FrequencySelector();
            Frequency_Selector.Content = freq_.Content;
            freq_2 = new Better_Choices_1.DataSubmissions.ControlTemplates.FrequencySelector();
            Frequency_Selector_2.Content = freq_2.Content;
            Common_Holder.Children.Insert(0, Common_);
            end_date_picker = new labeled_date("End Date:");
            Common_.Children.Add(end_date_picker,1,0);

        }
        public override void setup_around_habit(Recurring habit)
        {
            habit_ = habit;
            end_date_picker.Date = habit_.date_ended;
            freq_.let_data(habit_.how_common, habit_.frequency);
            freq_2.let_data(habit_.how_common_2, habit_.frequency_2);



        }

        public override string type()
        {
            return "Bulk Purchase";
        }

        public override void refresh() { freq_.refresh();freq_2.refresh(); }
        public override DateTime end_date() { return end_date_picker.Date; }
        public override DateTime get_end_date() { return end_date_picker.Date; }
        public override string frequency_1() { return freq_.frequency(); }
        public override double how_common_1() { return freq_.how_common(); }
        public override string frequency_2() { return freq_2.frequency(); }
        public override double how_common_2() { return freq_2.how_common(); }
    }
}