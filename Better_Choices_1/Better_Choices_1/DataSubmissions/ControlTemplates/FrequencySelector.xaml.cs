using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Syncfusion.XForms.ComboBox;

namespace Better_Choices_1.DataSubmissions.ControlTemplates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrequencySelector : ContentView
    {
        SfComboBox combobox;
        string default_freq = "frequency";
        public FrequencySelector()
        {
            InitializeComponent();
            combobox = new SfComboBox();
            combobox.DataSource = new List<String> { "Hours", "Days", "Weeks", "Months" };
            combobox.HorizontalOptions = Xamarin.Forms.LayoutOptions.StartAndExpand;
            this.nullcb();
            //combobox.WidthRequest = 115;
            combobox.VerticalOptions = Xamarin.Forms.LayoutOptions.Center;
            combobox.HeightRequest = freq_num.Height;
            combobox.TextSize = 6;
            combobox_holder.Children.Add(combobox);
            combobox.SelectionChanged += comboBox1_SelectedItemChanged;
            combobox.VerticalOptions = Xamarin.Forms.LayoutOptions.Center;
        }
        private void nullcb (){
            combobox.Text = default_freq;
            combobox.TextColor = Xamarin.Forms.Color.Gray;
            }
        public void refresh()
        {
            this.nullcb();
            freq_num.Text = null;
        }
        int orginalindex = 0;
        public string frequency() {
            if (combobox.Text == default_freq)
            { return "Default"; }
            return combobox.Text; 
        
        }

        public void let_data(double how_common,string frequency)
        {
            freq_num.Text = Convert.ToString(how_common);
            combobox.Text = frequency;


        }
        public double how_common() {
            double output = 0.0;
            double.TryParse(freq_num.Text,out output);
            return output; }
        private void comboBox1_SelectedItemChanged(object sender, EventArgs e)
        {
            if (combobox.SelectedIndex < 0)
            {
                combobox.Text = "Select one of the answers";
                combobox.SelectedIndex = combobox.SelectedIndex;
                this.nullcb();
            }
            else
            {
                combobox.TextColor = Xamarin.Forms.Color.Black;
                orginalindex = combobox.SelectedIndex;
            }
        }
    }
}