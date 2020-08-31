using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.XForms.Buttons;
using Syncfusion.XForms.ComboBox;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Extensions;

namespace Better_Choices_1.DataSubmissions
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Base_Data_Entry : ContentView
    {
        ChildForm child_form;
        Dictionary<String, SfRadioButton> radiogroupcontrollers;
        Recurring habit_;
        Habit_Data habit_data;

        StackLayout all_content;
        Entry nameEntry;
        StackLayout radiogrouplayout;
        Home parent_;
        private void InitializeBase()
        {
            // ChildForm child_form = new ChildForm();
            // OnAppearing();
            // radiogroupLayout = new StackLayout();
            habit_ = null;
            habit_data = null;
            all_content = new StackLayout();
            Base_Stack.Children.Insert(1, all_content);

            child_form = new One_Time();
            Variable_Content.Content = child_form.Content;
            SfRadioGroupKey radioGroup = new SfRadioGroupKey();
            SfRadioButton one_time = new SfRadioButton();
            one_time.IsChecked = true;
            one_time.Text = "One Time";
            nameEntry = new Entry();
            nameEntry.Placeholder = "I made the decision to";
            radiogrouplayout = new StackLayout();
            radiogrouplayout.Orientation = Xamarin.Forms.StackOrientation.Horizontal;
            radiogroupLayout.HorizontalOptions = Xamarin.Forms.LayoutOptions.FillAndExpand;
            all_content.Children.Insert(0, nameEntry);
            all_content.Children.Insert(1, radiogroupLayout);


            SfRadioButton recurring = new SfRadioButton();
            recurring.Text = "Recurring";
            SfRadioButton bulk = new SfRadioButton();
            bulk.Text = "Bulk Purchase";

            one_time.GroupKey = radioGroup;
            recurring.GroupKey = radioGroup;
            bulk.GroupKey = radioGroup;
            bulk.HorizontalOptions = Xamarin.Forms.LayoutOptions.FillAndExpand;
            radiogroupLayout.HorizontalOptions = Xamarin.Forms.LayoutOptions.FillAndExpand;
            radiogroupLayout.Children.Add(one_time);
            radiogroupLayout.Children.Add(recurring);
            radiogroupLayout.Children.Add(bulk);
            radiogroupcontrollers = new Dictionary<string, SfRadioButton> { };
            foreach (SfRadioButton child in radiogroupLayout.Children)
            {
                radiogroupcontrollers[child.Text] = child;
            }
            radiogroupLayout.HorizontalOptions = Xamarin.Forms.LayoutOptions.FillAndExpand;
            one_time.StateChanged += RadioButton_StateChanged;
            recurring.StateChanged += RadioButton_StateChanged;
            bulk.StateChanged += RadioButton_StateChanged;
            nameEntry.Text = string.Empty;
            typePicker.IsVisible = false;


            moneySaved.TextChanged += let_refresh;
            nameEntry.TextChanged += let_refresh;
            this.set_helper();


            //Add_stash.Clicked += btnPopupButton_Clicked;
            //child_form.refresh();

        }
        public Base_Data_Entry()
        {
            InitializeComponent();

            this.InitializeBase();
        }
        public void set_habit(Recurring habit)
        {
            habit_ = habit;
            radiogroupcontrollers[habit.Type].IsChecked = true;
            child_form.let_habit(habit);
            moneySaved.Text = Convert.ToString(habit.money_saved_2);
            moneySaved2.Text = Convert.ToString(habit.money_saved_1);
            nameEntry.Text = habit.Name;
            
        }
        public void set_habit(Habit_Data habit)
        {
            habit_data = habit;
            radiogroupcontrollers["One Time"].IsChecked = true;
            child_form.let_habit(habit);
            moneySaved2.Text = Convert.ToString(habit.money_saved);
            //moneySaved2.Text = Convert.ToString(habit.money_saved_1);
            nameEntry.Text = "Habit";

        }

        public Base_Data_Entry(Recurring habit)
        {
            InitializeComponent();
            this.InitializeBase();
            this.set_habit(habit);
        }
        public Base_Data_Entry(Habit_Data habit)
        {
            InitializeComponent();
            this.InitializeBase();
            this.set_habit(habit);
        }


        public Base_Data_Entry(string stash_id, Home parent)
        {
            InitializeComponent();
            this.set_stash(stash_id);
            parent_ = parent;
            this.InitializeBase();
        }
        public void let_refresh(object sender, EventArgs e)
        {
            this.downstream_refresh();
        }

        public void visible_(bool visible)
        {
            Variable_Content.IsVisible = visible;
            all_content.IsVisible = visible;//Xamarin.Forms.Binding.
            moneySaved.IsVisible = visible;
            moneySaved2.IsVisible = visible;
            buttonholder.IsVisible = visible;
            this.set_helper();
        }
        async void OnButtonClicked(object sender, EventArgs e)
        {

            Recurring Habit_ = this.GetHabit();
            int job_id = await App.Database.SaveItemAsync(Habit_);
            this.save_Habit_Data(Habit_);

            parent_.refresh();
            this.refresh();



        }
        void Clear_OnButtonClicked(object sender, EventArgs e)
        {
            parent_.refresh();
            this.refresh();
        


        }
        string stash_id;
        public void set_stash(string stash)
        {
            stash_id = stash;
        }

        public bool form_complete()
        {
            return true;
        }
        public Habit_Data GetHabitData()
        {
            double money_saved_2 = 0.0;
            double.TryParse(moneySaved.Text, out money_saved_2);
            double money_saved_1 = 0.0;
            double.TryParse(moneySaved2.Text, out money_saved_1);

            habit_data.date_run = child_form.get_start_date();
            habit_data.money_saved = money_saved_1 - money_saved_2;
            return habit_data;
         }
        public Recurring GetHabit()
        {
            double money_saved_2 = 0.0;
            double.TryParse(moneySaved.Text, out money_saved_2);
            double money_saved_1 = 0.0;
            double.TryParse(moneySaved2.Text, out money_saved_1);

            double how_common_1 = child_form.how_common_1();
            double how_common_2 = child_form.how_common_2();

            string frequency_1 = child_form.frequency_1();
            string frequency_2 = child_form.frequency_2();
            double savings = new utils_data.FrequencyTranslator().get_savings(
            money_saved_1, frequency_1, how_common_1,
            money_saved_2, frequency_2, how_common_2
            );
            Recurring Habit_;
            DateTime date_ended = child_form.end_date();
            if (habit_ != null)
            {

                habit_.Name = nameEntry.Text;
                habit_.date_started = child_form.get_start_date();
                habit_.money_saved = savings;
                habit_.date_ended = child_form.get_end_date();
                habit_.how_common = child_form.how_common_1();
                habit_.frequency = child_form.frequency_1();
                habit_.Type = child_form.type();
                habit_.frequency_2 = child_form.frequency_2();
                habit_.how_common_2 = child_form.how_common_2();
                habit_.money_saved_1 = money_saved_1;
                habit_.money_saved_2 = money_saved_2;

                Habit_ = habit_;
            }
            else
            {
                Habit_ = new Recurring
                {
                    Name = nameEntry.Text,
                    date_started = child_form.get_start_date(),
                    money_saved = savings,
                    date_ended = child_form.get_end_date(),
                    how_common = child_form.how_common_1(),
                    frequency = child_form.frequency_1(),
                    Type = child_form.type(),
                    frequency_2 = child_form.frequency_2(),
                    how_common_2 = child_form.how_common_2(),
                    money_saved_1 = money_saved_1,
                    money_saved_2 = money_saved_2,

                };
                Habit_.stash_to_use = App.Database.GetStashesID(stash_id);

            }
            return Habit_;
            
            
        }
        public void set_helper()
        {
            double money_saved_2 = 0.0;
            double.TryParse(moneySaved.Text, out money_saved_2);
            double money_saved_1 = 0.0;
            double.TryParse(moneySaved2.Text, out money_saved_1);

            double how_common_1 = child_form.how_common_1();
            double how_common_2 = child_form.how_common_2();

            string str1 = child_form.frequency_1();
            string str2 = child_form.frequency_2();
            if (str1 == "One Time")
            {
                Calculator.IsVisible = false;
                return;
            }
            Calculator.IsVisible = true;
            if (str2 == "Default")
            {
                str2 = str1;

            }
            if (str1 != "Default")
            {
                Better_Choices_1.utils_data.FrequencyTranslator freq = new utils_data.FrequencyTranslator();
                string helper_str = freq.helper_string(
                    money_saved_1, str1, how_common_1,
                    money_saved_2, str2, how_common_2
                    );
                Calculator.Text = helper_str;
                
            }
            else
            {
                Calculator.Text = "Periodic Savings will be calculated with more data";
            }

        }
        public async void save_Habit_Data(Recurring Habit_)
        {
            App.Database.save_habit_data(Habit_);
        }
        public void downstream_refresh()
        {
            this.set_helper();
        }
        public void refresh()
        {
            nameEntry.Text = null;
            moneySaved.Text = null;
            moneySaved2.Text = null;
            child_form.refresh();
            

        }
        async void Create_Stash(object sender, EventArgs e)
        {


            await Navigation.PushPopupAsync(new TemplateForms.MyPopupPage(new Money_Stashes.Base_Data_Entry(parent_)));
            parent_.refresh();
            //await PopupNavigation.PushAsync();
            //await PopupNavigation.PushAsync(new PopUp());
            //string stash_name = await App.Current.MainPage.DisplayPromptAsync("Stash Creator", "Please Enter a Name");
            //Stash stash = new Stash { Name = stash_name };
            //App.Database.SaveStashAsync(stash);
            //this.refresh();
        }

        private void RadioButton_StateChanged(object sender, StateChangedEventArgs e)
        {
            
            if (e.IsChecked.HasValue && e.IsChecked.Value)
            {
                
                if ((sender as SfRadioButton).Text == "One Time")
                {
                    child_form = new One_Time();
                    Variable_Content.Content = child_form.Content;
                }
                 if ((sender as SfRadioButton).Text == "Recurring")
                {
                    child_form = new Recurring_View();
                    Variable_Content.Content = child_form.Content;
                }
                if ((sender as SfRadioButton).Text == "Bulk Purchase")
                {
                    child_form = new Bulk_Purchase();
                    Variable_Content.Content = child_form.Content;
                }
                this.set_helper();
            if (habit_ != null)
                {
                    child_form.let_habit(habit_);
                }
            }
        }

        private void moneySaved_TextChanged(object sender, TextChangedEventArgs e)
        {
           this.set_helper();
        }
    }
}