using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Better_Choices_1.TemplateForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class labeled_date : ContentView
    {
        DatePicker date;
        Label label;
        public labeled_date(string label_text)
        {
            InitializeComponent();
            date = new DatePicker();
            date.VerticalOptions = Xamarin.Forms.LayoutOptions.Center;
            date.HorizontalOptions = Xamarin.Forms.LayoutOptions.FillAndExpand;
            //date.HorizontalOptions = Xamarin.Forms.LayoutOptions.Center;
            label = new Label();
            label.Text = label_text;
            label.VerticalOptions = Xamarin.Forms.LayoutOptions.Center;
            base_stack.Children.Add(label);
            base_stack.Children.Add(date);
            date.FontSize = 8;
            base_stack.Orientation = Xamarin.Forms.StackOrientation.Horizontal;
            base_stack.HorizontalOptions = Xamarin.Forms.LayoutOptions.FillAndExpand;
        }
        public DateTime Date { get { return date.Date; } }
        public void let_text(string str_) { label.Text = str_; }
    }
}