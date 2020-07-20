using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Rg.Plugins.Popup.Services;
namespace Better_Choices_1.TemplateForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyPopupPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        DataSubmissionForm content_;
        public MyPopupPage(DataSubmissionForm content)
        {
            content_ = content;
            InitializeComponent();
            save_form.Clicked += save;
            cancel.Clicked += close;
            popupContent.Content = content.Content;
        }
        void close(object sender, EventArgs e)
        {
              
            PopupNavigation.Instance.PopAsync(true);
        }
        void save(object sender, EventArgs e)
        {
            content_.SaveForm();
            PopupNavigation.Instance.PopAsync(true);
        }
    }

}