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
    public partial class query_attrs : ContentView
    {
        DatePicker _start_date;
        DatePicker _end_date;
        Better_Choices_1.Home _parent;
        public Grid Common_ { get { return Common; } }
        public query_attrs(DateTime start_date,DateTime end_date, 
            Better_Choices_1.Home parent)
        {
            _parent = parent;
            InitializeComponent();
            _start_date = new DatePicker();
            _end_date = new DatePicker();

            _start_date.Date = start_date;
            _end_date.Date = end_date;
            Common_.Children.Add(_start_date, 0, 0);
            Common_.Children.Add(_end_date, 1, 0);
            _start_date.FontSize = 6;
            _end_date.FontSize = 6;
            _start_date.DateSelected += this.formchanged;
            _end_date.DateSelected += this.formchanged;

        }
        public DateTime start_date() { return _start_date.Date; }
        public DateTime end_date() { return _end_date.Date; }

        void OnComplete() { this._parent.refresh(); }
        private void formchanged(object sender,
            DateChangedEventArgs e)
        {
            this.OnComplete();
        }

    }
}