using System;
using System.Collections.Generic;
using Syncfusion.XForms.Buttons;
using Xamarin.Forms;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Extensions;


namespace Better_Choices_1.base_form
{
    public partial class EntryForm : ContentPage
    {
        public EntryForm()
        {
            InitializeComponent();
            OnAppearing();

        }
        Habit_Data Selected_;
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            this.refresh();


        }
        private async void refresh()
        {
            db_view.ItemsSource = await App.Database.GetEntriesAsync();

        }

        private void db_view_Clicked(object s, SelectedItemChangedEventArgs e)
        {
            try
            {
                Selected_ = (Habit_Data)e.SelectedItem;
            }
            catch (Exception)
            {
                //pass;
            }
        }
        async void OnButtonClicked(object sender, EventArgs e)
        {

                this.refresh(); 
         }
        


        public void OnMore(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("More Context Action", mi.CommandParameter + " more context action", "OK");
        }

        public void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");
        }
        async void DeleteRecord(object sender, EventArgs e)
        {
            var button = sender as Button;
            var obj_ = button.BindingContext as Habit_Data;
            await App.Database.DeleteItemAsync(obj_);
            this.refresh();

        }

        async void ModifyRecord(object sender, EventArgs e)
        {
            var obj_ = (sender as Button).BindingContext as Habit_Data;
            await Navigation.PushPopupAsync(new TemplateForms.MyPopupPage(new DataSubmissions.habit_data_modification(obj_)));
            this.refresh();

        }

    }

}
