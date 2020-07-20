using System;
using System.Collections.Generic;
using Syncfusion.XForms.Buttons;
using Xamarin.Forms;

namespace Better_Choices_1
{
    public partial class Job_Entry : ContentPage
    {
        public Job_Entry()
        {
            InitializeComponent();
            OnAppearing();

        }
        Habit Selected_;
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            this.refresh();


        }
        private async void refresh()
        {
            db_view.ItemsSource = await App.Database.GetPeopleAsync();

        }

        private void db_view_Clicked(object s, SelectedItemChangedEventArgs e)
        {
            try
            {
                Selected_ = (Habit)e.SelectedItem;
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
        public void DeleteRecord(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");
        }
    }

}
