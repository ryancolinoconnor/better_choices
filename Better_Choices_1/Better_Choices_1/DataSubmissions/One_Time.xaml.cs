using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Better_Choices_1.DataSubmissions
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class One_Time : ChildForm
    {
        public One_Time(): base()
        {
            InitializeComponent();
            Common_Holder.Children.Add(Common_);
            this.let_text("Event Date: ");
        }
        public virtual string helper_str() { return "Save ###"; }
        public virtual int get_frequencies() { return 1; }
        public override string frequency_1() { return "One Time"; }
    }
}