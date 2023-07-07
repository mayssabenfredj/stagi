using satgi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace satgi
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class register : ContentPage
    {
        UserRepository userRepository = new UserRepository();
        int num = 0;


        public register()
        {
            InitializeComponent();
           

        }
        private void goToLogin(object sender, EventArgs e)
        {
            Navigation.PushAsync(new login());

        }
        
        private void Showpass(object sender, EventArgs e)
        {
            if (password.IsPassword = !password.IsPassword)
            {
                image.Source = "hidepass.png";
            }
            else
            {
                image.Source = "showpass.png";

            }

        }

   
        private async void registerUser(object sender, EventArgs e)
        {
             num++ ;  
            string mail = email.Text;
            string name = username.Text;
            string pass = password.Text;
            string role = "";

                if (student.IsChecked)
            {
                role = "Student";
            }
            if (entreprise.IsChecked)
            {
                role = "Entreprise";
            }
            try
            {
                var user = await userRepository.GetUser(mail);
               
                
                    if (user != null && mail == user.email)
                    {
                        await DisplayAlert("Warning", "User already Exist", "Ok");
                    }
                    else
                    {
                        var isSaved = await userRepository.SaveUser(mail, name, pass, role);
                        if (isSaved)
                        {
                            await DisplayAlert("Ops...", "User Not Saved", "Ok");
                        }
                        else
                        {
                            await Navigation.PushAsync(new login());

                        }

                    }
                
                   
                
               
            }
            catch (Exception ex)
            {
               
               
             await DisplayAlert("Warning", "Eroor", "Ok");
              
            }

        }


    }
}