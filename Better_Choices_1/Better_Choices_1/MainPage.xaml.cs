using System;
using System.Collections.Generic;
using Syncfusion.SfNavigationDrawer.XForms;
using Xamarin.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel;
namespace Better_Choices_1
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            // hamburgerButton.Image = (FileImageSource)ImageSource.FromFile("hamburger_icon.png");
            List<string> list = new List<string>();
            list.Add("Home");
            list.Add("Recurring Entries");
            list.Add("Stashes");

            list.Add("Entries");
            listView.ItemsSource = list;
            navigationDrawer.ContentView = new Home().Content;
            navigationDrawer.VerticalOptions = Xamarin.Forms.LayoutOptions.EndAndExpand;
            navigationDrawer.HeightRequest = 1000;
            headerLabel.Text = "Home";
        }



        private void Handle_ItemSelected(object s, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem.ToString() == "Home")
            {
                navigationDrawer.ContentView = new Home().Content;
                headerLabel.Text = "Home";
            }
            else if (e.SelectedItem.ToString() == "Recurring Entries")
            {
                navigationDrawer.ContentView = new Job_Entry().Content;
                headerLabel.Text = "Recurring Entries";
            }
            else if (e.SelectedItem.ToString() == "Entries")
            {
                navigationDrawer.ContentView = new base_form.EntryForm().Content;
                headerLabel.Text = "Entries";
            }

            else if (e.SelectedItem.ToString() == "Remainders")
            {
                //navigationDrawer.ContentView = new Remainders().Content;
                //headerLabel.Text = "Remainders";
            }
            else if (e.SelectedItem.ToString() == "Stashes")
            {
                navigationDrawer.ContentView = new DataForms.StashView().Content;
                headerLabel.Text = "Recurring Entries";
                // navigationDrawer.ContentView = new ToDoList().Content;
                // headerLabel.Text = "ToDoList";
            }
            navigationDrawer.ToggleDrawer();
        }

        void hamburgerButton_Clicked(object sender, EventArgs e)
        {
            navigationDrawer.ToggleDrawer();
        }

        private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // Your codes here
            navigationDrawer.ToggleDrawer();
        }
    }
}

//protected override async void OnAppearing()
//{

// base.OnAppearing();
//listView.ItemsSource = await App.Database.GetPeopleAsync();
//}

//async void OnButtonClicked(object sender, EventArgs e)
//{
//   if (!string.IsNullOrWhiteSpace(nameEntry.Text))
// {
//    await App.Database.SavePersonAsync(new Habit
//   {
//      Name = nameEntry.Text,
//      Type = typePicker.Items[typePicker.SelectedIndex],
//      date_started = dateEntry.Date,
//     money_saved = Convert.ToDouble(moneySaved.Text),


// });

//                nameEntry.Text  = string.Empty;
//              listView.ItemsSource = await App.Database.GetPeopleAsync();
//       }
//}
//public void OnMore(object sender, EventArgs e)
 //      {
  //          var mi = ((MenuItem)sender);
   //         DisplayAlert("More Context Action", mi.CommandParameter + " more context action", "OK");
    //    }

//        public void OnDelete(object sender, EventArgs e)
 //       {
   //         var mi = ((MenuItem)sender);
    //        DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");
      //  }
        

    //}
//}
