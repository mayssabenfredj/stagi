using Android.Content;
using Android.Speech;
using Firebase.Database;
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
    public partial class offres : ContentPage
    {
        public static FirebaseClient firebase = new FirebaseClient("https://stagi-cd67d-default-rtdb.firebaseio.com/");
        List<Offres> offrelist = new List<Offres>();

        public offres()
        {
            InitializeComponent();
           



        }

        protected override async void OnAppearing()
        {

           offrelist = (await firebase
                 .Child("Offres")
                 .OnceAsync<Offres>()).Select(item =>
                 new Offres
                 {
                     idOffre = item.Object.idOffre,
                     titre = item.Object.titre,
                     entrprise = item.Object.entrprise,
                     adress = item.Object.adress,
                     etat = item.Object.etat,
                     date=item.Object.date,
                     descPost = item.Object.descPost,
                     profileDiplome = item.Object.profileDiplome,
                     profileHardS = item.Object.profileHardS,
                     profileSoftS = item.Object.profileSoftS,

                     useremail = item.Object.useremail


                 }).ToList();
            
            OffresList.ItemsSource = offrelist;

         




        }
        
        

       
        private void record(object sender, EventArgs e)
        {
           /* Intent intent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
            intent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);
            intent.PutExtra(RecognizerIntent.ExtraPrompt, "Start Speaking");
            
            StartActivity(intent, 100);*/


        }

       
        private void goToAdd(object sender, EventArgs e)
        {
            Navigation.PushAsync(new addOffre());

        }

        private void goToProfil(object sender, EventArgs e)
        {
            Navigation.PushAsync(new profil());

        }





        private async  void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

            var frame = (Frame)sender;
            var off = (Offres)frame.BindingContext;
            Application.Current.Properties["idoff"] = off.idOffre;
            Application.Current.Properties["titre"] = off.titre;
            Application.Current.Properties["entreprise"] = off.entrprise;
            Application.Current.Properties["adress"] = off.adress;
            Application.Current.Properties["date"] = off.date.ToString();
            Application.Current.Properties["etat"] = off.etat;

            Application.Current.Properties["descpost"] = off.descPost;
            Application.Current.Properties["diplome"] = off.profileDiplome;
            Application.Current.Properties["hskils"] = off.profileHardS;
            Application.Current.Properties["softskils"] = off.profileSoftS;
           
            Application.Current.Properties["userm"] = off.useremail;

            await Navigation.PushAsync(new Detail());
        }

        private void goToPost(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Postulations());
        }

        private async void logout(object sender, EventArgs e)
        {
            Application.Current.Properties.Clear();

            // Redirect to the login page
            await Navigation.PushAsync(new login());

            // Remove all previous pages from the navigation stack
            // to prevent the user from going back to the previous pages
            NavigationPage.SetHasNavigationBar(this, false); // Hide navigation bar on this page
            NavigationPage.SetHasBackButton(this, false); // Hide back button on this page
            await Navigation.PopToRootAsync(); // Remove all previous pages from the stack
        }

        private async  void searchText_TextChanged(object sender, TextChangedEventArgs e)
        {
            var filtre = (await firebase
                 .Child("Offres")
                 .OnceAsync<Offres>()).Select(item =>
                 new Offres
                 {
                     idOffre = item.Object.idOffre,
                     titre = item.Object.titre,
                     entrprise = item.Object.entrprise,
                     adress = item.Object.adress,
                     etat = item.Object.etat,
                     date = item.Object.date,
                     descPost = item.Object.descPost,
                     profileDiplome = item.Object.profileDiplome,
                     profileHardS = item.Object.profileHardS,
                     profileSoftS = item.Object.profileSoftS,

                     useremail = item.Object.useremail


                 }).Where(a => a.titre.Contains(e.NewTextValue.ToString())).ToList(); ;
            OffresList.ItemsSource = filtre;
        }
    }
}