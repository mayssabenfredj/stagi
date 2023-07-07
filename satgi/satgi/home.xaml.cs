using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace satgi
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class home : ContentPage
    {
        public home()
        {
            InitializeComponent();
        }

        private void goToLogin(object sender, EventArgs e)
        {
            Navigation.PushAsync(new login());

        }
        private void goToRegister(object sender, EventArgs e)
        {
            Navigation.PushAsync(new register());
        }
    }
}