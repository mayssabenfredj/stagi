using satgi.Data;
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
    public partial class login : ContentPage
    {
        UserRepository userRepository =  new UserRepository();
        public login()
        {
            InitializeComponent();
        }
        private void goToRegister(object sender, EventArgs e)
        {
            Navigation.PushAsync(new register());

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
        private async void goToHome(object sender, EventArgs e)
        {
            string email = mail.Text;
           
            string pass = password.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pass))
                await DisplayAlert("Empty Values", "Please enter Email and Password", "OK");
            else
            {
                //call GetUser function which we define in Firebase helper class    
                var user = await userRepository.GetUser(email);
                //firebase return null valuse if user data not found in database    
                if (user != null)
                    if (email == user.email && pass == user.password)
                    {
                        //await App.Current.MainPage.DisplayAlert("Login Success", "", "Ok");
                        //Navigate to Wellcom page after successfuly login    
                        //pass user email to welcom page
                        Application.Current.Properties["email"] = email;
                        Application.Current.Properties["role"] = user.role;
                        Application.Current.Properties["id"] = user.id;
                        await Navigation.PushAsync(new Main());
                        Navigation.RemovePage(this);


                    }
                    else
                        await DisplayAlert("Login Fail", "Please enter correct Email and Password", "OK");
                else
                    await DisplayAlert("Login Fail", "User not found", "OK");
            }
        }



     
    }
}