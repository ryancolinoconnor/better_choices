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

namespace Better_Choices_1
{
    public partial class Home : ContentPage
    {
        Button Add_stash;
        SfComboBox combobox;
        StackLayout combobox_holder;
        Better_Choices_1.Analytics.Forecast forecast;
        ContentView forc_;
        query_attrs _query_selector;
        public Home()
        {
            InitializeComponent();
            combobox = new SfComboBox();
            combobox.DataSource = App.Database.GetStashesNames();
            
            data_entry = new DataSubmissions.Base_Data_Entry(combobox.Text, this);
            Example1.Content = data_entry.Content;
            





            Add_stash = new Button();
            Add_stash.Text = "+";
            combobox.HorizontalOptions = Xamarin.Forms.LayoutOptions.FillAndExpand;
            // combobox.WidthRequest = 260;
            Add_stash.HorizontalOptions = Xamarin.Forms.LayoutOptions.Start;
            combobox_holder = new StackLayout();
            combobox_holder.Orientation = Xamarin.Forms.StackOrientation.Horizontal;
            combobox.MaximumDropDownHeight = 150;
            combobox_holder.Children.Add(combobox);
            combobox.ItemPadding = 0;
            Add_stash.Padding = 0;
            combobox_holder.Children.Add(Add_stash);
            combobox.SelectionChanged += comboBox1_SelectedItemChanged;


            Add_stash.BackgroundColor = Xamarin.Forms.Color.Transparent;
            combobox_holder.HorizontalOptions = Xamarin.Forms.LayoutOptions.Center;
            Add_stash.HorizontalOptions = Xamarin.Forms.LayoutOptions.Start;
            Add_stash.Clicked += Create_Stash;
            MainGrid.RowDefinitions.Insert(1, new RowDefinition { 
                Height = new GridLength(5,GridUnitType.Auto) });

            _query_selector = new query_attrs(DateTime.Today.AddDays(-7), DateTime.Today.AddDays(7), this);
            MainGrid.Children.Add(_query_selector, 1, 1);
            MainGrid.Children.Add(combobox, 1, 2);
            MainGrid.Children.Add(Add_stash, 2, 2);

            StackLayout hider = new StackLayout();
            hider.BackgroundColor = Xamarin.Forms.Color.LightBlue;
            hider.Orientation = Xamarin.Forms.StackOrientation.Horizontal;
            hider.HorizontalOptions = Xamarin.Forms.LayoutOptions.FillAndExpand;

            Label new_dec = new Label();
            new_dec.Text = "New entry";
            Switch switchControl = new Switch { IsToggled = true };
            switchControl.Toggled += switch_OnToggled;
            switchControl.IsToggled = false;
            new_dec.TextColor = Xamarin.Forms.Color.White;
            new_dec.FontSize = 20;
            hider.Children.Add(new_dec);
            hider.Children.Add(switchControl);
            new_dec.HorizontalOptions = Xamarin.Forms.LayoutOptions.End;
            switchControl.HorizontalOptions = Xamarin.Forms.LayoutOptions.EndAndExpand;


            forecast = new Analytics.Forecast();
            StackLayout forecast_hider = new StackLayout();
            forecast_hider.BackgroundColor = Xamarin.Forms.Color.LightBlue;
            forecast_hider.Orientation = Xamarin.Forms.StackOrientation.Horizontal;
            forecast_hider.HorizontalOptions = Xamarin.Forms.LayoutOptions.FillAndExpand;


            Label show_forecast = new Label();
            show_forecast.TextColor = Xamarin.Forms.Color.White;
            show_forecast.FontSize = 20;
            show_forecast.Text = "Plot Data";
            Switch forecast_switchControl = new Switch { IsToggled = true };
            forecast_switchControl.Toggled += show_plot;
            forecast_switchControl.IsToggled = false;

            

            forecast_hider.Children.Add(show_forecast);
            forecast_hider.Children.Add(forecast_switchControl);
            show_forecast.HorizontalOptions = Xamarin.Forms.LayoutOptions.End;
            forecast_switchControl.HorizontalOptions = Xamarin.Forms.LayoutOptions.EndAndExpand;
            main_form.Children.Insert(1, hider);
            main_form.Children.Insert(3, forecast_hider);
            main_form.Children.Insert(4, forecast);

            forecast.HeightRequest = 300;
            forecast_hider.MinimumHeightRequest = hider.Height;
            this.nullcb();
            OnAppearing();

        }
        void switch_OnToggled(object sender, ToggledEventArgs e)
        {
            // Perform an action after examining e.Value
            //this.visible_();;
            data_entry.visible_((sender as Switch).IsToggled);
            Example1.IsVisible = (sender as Switch).IsToggled;

        }
        void show_plot(object sender, ToggledEventArgs e)
        {
            // Perform an action after examining e.Value
            //this.visible_();;
            forecast.IsVisible = (sender as Switch).IsToggled;
            // data_entry.visible_((sender as Switch).IsToggled);
            // Example1.IsVisible = (sender as Switch).IsToggled;

        }

        async void Create_Stash(object sender, EventArgs e)
        {


            await Navigation.PushPopupAsync(new TemplateForms.MyPopupPage(new Money_Stashes.Base_Data_Entry()));
            this.refresh();
            //await PopupNavigation.PushAsync();
            //await PopupNavigation.PushAsync(new PopUp());
            //string stash_name = await App.Current.MainPage.DisplayPromptAsync("Stash Creator", "Please Enter a Name");
            //Stash stash = new Stash { Name = stash_name };
            //App.Database.SaveStashAsync(stash);
            //this.refresh();
        }

        Habit Selected_;
        DataSubmissions.Base_Data_Entry data_entry;
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            data_entry = new DataSubmissions.Base_Data_Entry(combobox.Text, this);
            Example1.Content = data_entry.Content;
            this.refresh();

        }
        public async void refresh()
        {
            jobs_view.ItemsSource = App.Database.GetNamedJobsAsync(combobox.Text,
                _query_selector.start_date()
                ,_query_selector.end_date());

            Aggregation.Text = "$" + Convert.ToString(Math.Round(
                App.Database.money_saved(
                    combobox.Text, _query_selector.start_date(),
                    _query_selector.end_date()
                    ),2)) + " saved ";
            data_entry.refresh();
            forecast.Content = new Analytics.Forecast((combobox.Text),_query_selector.start_date(),
                                    _query_selector.end_date()).Content;

            data_entry.set_stash((combobox.Text));
            //data_entry = new DataSubmissions.Base_Data_Entry();
            //Example1.Content = data_entry.Content;

        }
        private async void nullcb()
        {
            combobox.Text = "";
            combobox.SelectedIndex = combobox.SelectedIndex;
            combobox.TextColor = Xamarin.Forms.Color.Gray;
        }
        int originalindex = 0;
        private void comboBox1_SelectedItemChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            
            if (combobox.Text == "General Savings" | combobox.Text=="")
            {

                this.nullcb();
            }
            else
            {
                combobox.TextColor = Xamarin.Forms.Color.Black;
                // forecast.Content = new Analytics.Forecast((combobox.Text)).Content;
                // jobs_view.ItemsSource = App.Database.GetNamedJobsAsync(combobox.Text,_query_selector.start_date(),
                //    _query_selector.end_date());
                // data_entry.set_stash((combobox.Text));
                this.refresh();
            }

        }

        async void OnButtonClicked(object sender, EventArgs e)
        {

            Habit Habit_ = data_entry.GetHabit();
            int job_id = await App.Database.SaveItemAsync(Habit_);
            data_entry.save_Habit_Data(Habit_);
            // habit_data_.Job_ID = job_id;
            
            this.refresh();
            
            
            
        }


        async void DeleteRecord(object sender, EventArgs e)
        {
            var button = sender as Button;
            var obj_ = button.BindingContext as Named_Habit_Data;
            await App.Database.DeleteNamedHabitAsync(obj_);
            this.refresh();

        }


    }

}
