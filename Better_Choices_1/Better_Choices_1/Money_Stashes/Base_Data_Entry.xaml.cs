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
        Better_Choices_1.DataForms.StashView _stash_view_parent;
        Better_Choices_1.Home _parent;
        Stash base_stash;
        public Base_Data_Entry(Better_Choices_1.Home parent)
        {
            InitializeComponent();
            //ChildForm child_form = new ChildForm();
            // OnAppearing();

            nameEntry.Text = string.Empty;
            combobox = new SfComboBox();
            combobox.DataSource = App.Database.GetStashesNames();
            this._parent = parent;


        }
        public void build_from_stash(Stash stash_) {

            this.base_stash = stash_;
            nameEntry.Text = this.base_stash.Name;
            dateEntry.Date = this.base_stash.date_started;
            end_dateEntry.Date = this.base_stash.target_date;
            target_savings.Text = Convert.ToString(this.base_stash.target_savings);
            Aggregation.Text = "Update Stash";


        }
        public Base_Data_Entry(Better_Choices_1.DataForms.StashView _stash_view_parent, Stash base_stash)
        {
            InitializeComponent();
            //ChildForm child_form = new ChildForm();
            // OnAppearing();

            nameEntry.Text = string.Empty;
            combobox = new SfComboBox();
            combobox.DataSource = App.Database.GetStashesNames();
            this._stash_view_parent = _stash_view_parent;

            this.build_from_stash(base_stash);
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
            Stash stash_;
            if (this.base_stash != null) {
                stash_ = this.base_stash;
                stash_.Name = nameEntry.Text;
                stash_.date_started = dateEntry.Date;
                stash_.target_date = end_dateEntry.Date;
                stash_.target_savings = Convert.ToDouble(target_savings.Text);
            }
            else
            {
                stash_ = new Stash
                {
                    Name = nameEntry.Text,
                    date_started = dateEntry.Date,
                    target_date = end_dateEntry.Date,
                    target_savings = Convert.ToDouble(target_savings.Text),

                };
            }
            return stash_;
        }

        public async void refresh()
        {
            nameEntry.Text = null;
            if (this._parent != null)
            {
                this._parent.refresh();
            }
            if (this._stash_view_parent != null)
            {
                this._stash_view_parent.refresh();
            }
        }

        async void Create_Stash(object sender, EventArgs e)
        {
    

        }


    }
}