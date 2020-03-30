using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Better_Choices_1
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await App.Database.GetPeopleAsync();
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nameEntry.Text))
            {
                await App.Database.SavePersonAsync(new Habit
                {
                    Name = nameEntry.Text,
                    Type = typePicker.Items[typePicker.SelectedIndex],
                    date_started = dateEntry.Date,
                    money_saved = Convert.ToDouble(moneySaved.Text),

                    
                });

                nameEntry.Text  = string.Empty;
                listView.ItemsSource = await App.Database.GetPeopleAsync();
            }
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
            // await App.Database.DeleteHabitAsync(
            //     Convert.ToInt32(listView.SelectedItem.ToString()));
            // listView.ItemsSource = await App.Database.GetPeopleAsync();
            /// int id =  App.Database.SearchAsync(listView.SelectedItem.ToString());
            
            //id_display.Text = new Habit(listView.SelectedItem).ID;

        }
    }
}
