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
    public partial class Recurring_View : ChildForm
    {
        labeled_date end_date_picker;
        Better_Choices_1.DataSubmissions.ControlTemplates.FrequencySelector freq_;
        public Recurring_View(): base()
        {
            InitializeComponent();
            freq_ = new Better_Choices_1.DataSubmissions.ControlTemplates.FrequencySelector();
            Frequency_Selector.Content = freq_.Content;
            Common_Holder.Children.Insert(0,Common_);
            end_date_picker = new labeled_date("End Date:");
            Common_.Children.Add(end_date_picker,1,0);

        }
        
        public override void refresh() { freq_.refresh();}
        public override DateTime end_date() { return end_date_picker.Date; }

        public override DateTime get_end_date() { return end_date_picker.Date; }
        public override string frequency_1() { return freq_.frequency(); }
        public override double how_common_1() { return freq_.how_common(); }

        public override string frequency_2() { return freq_.frequency(); }
        public override double how_common_2() { return freq_.how_common(); }
    }
}