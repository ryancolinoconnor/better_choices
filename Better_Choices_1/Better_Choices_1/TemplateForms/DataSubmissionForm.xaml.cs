using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Better_Choices_1.TemplateForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DataSubmissionForm : ContentView
    {
        public DataSubmissionForm()
        {
            InitializeComponent();
        }
        public virtual void SaveForm() { }
    }
}