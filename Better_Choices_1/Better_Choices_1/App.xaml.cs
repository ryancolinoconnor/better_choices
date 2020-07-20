using System;
using Xamarin.Forms;

using Xamarin.Forms.Xaml;
using System.IO;
using Better_Choices_1;
namespace Better_Choices_1
{
    public partial class App : Application
    {
        static Database database;
        public static Database Database
        {
            get
            {
                if(database == null)
                {
                    database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "people.db3"));
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();
 
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
