using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Syncfusion.XForms.Buttons;
using Xamarin.Forms;
using Syncfusion.XForms.Buttons;
using Syncfusion.XForms.ComboBox;
using Xamarin.Forms;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Extensions;
using Better_Choices_1.TemplateForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Better_Choices_1.DataForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StashView : ContentPage
    {
        public StashView()
        {
            InitializeComponent();
            OnAppearing();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            this.refresh();


        }
        public async void refresh()
        {
            db_view.ItemsSource = await App.Database.GetStashesAsync();

        }
        async void ModifyRecord(object sender, EventArgs e)
        {
            var obj_ = (sender as Button).BindingContext as Stash;
            await Navigation.PushPopupAsync(new TemplateForms.MyPopupPage(new Money_Stashes.Base_Data_Entry(this,obj_)));
            this.refresh();

        }

        async void DeleteRecord(object sender, EventArgs e)
        {
            var button = sender as Button;
            var obj_ = button.BindingContext as Stash;
            await App.Database.DeleteItemAsync(obj_);
            this.refresh();

        }
    }
}