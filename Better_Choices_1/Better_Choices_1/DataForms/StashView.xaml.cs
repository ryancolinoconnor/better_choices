using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private async void refresh()
        {
            db_view.ItemsSource = await App.Database.GetStashesAsync();

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