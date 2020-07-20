using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.XForms.Buttons;
using Syncfusion.XForms.ComboBox;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Better_Choices_1.Money_Stashes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Base_Data_Entry : Better_Choices_1.TemplateForms.DataSubmissionForm
    {

        Button Add_stash;
        SfComboBox combobox;
        public Base_Data_Entry()
        {
            InitializeComponent();
            //ChildForm child_form = new ChildForm();
            // OnAppearing();

            nameEntry.Text = string.Empty;
            combobox = new SfComboBox();
            combobox.DataSource = App.Database.GetStashesNames();


        }
        public override void  SaveForm() {
            Stash stash = this.GetStash();
            App.Database.SaveItemAsync(stash);
            this.refresh();

        }
        public bool form_complete()
        {
            return true;
        }
        public Stash GetStash()
        {
            var stash_ = new Stash
            {
                Name = nameEntry.Text,
                date_started = dateEntry.Date,
                target_date = end_dateEntry.Date,
                target_savings =  Convert.ToDouble(target_savings.Text),

            };

            return stash_;
        }

        public async void refresh()
        {
            nameEntry.Text = null;


        }

        async void Create_Stash(object sender, EventArgs e)
        {
    

        }


    }
}