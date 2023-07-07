using Firebase.Database;
using Firebase.Database.Query;
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
    public partial class Detail : ContentPage
    {
        public static FirebaseClient firebase = new FirebaseClient("https://stagi-cd67d-default-rtdb.firebaseio.com/");
        public string user = Application.Current.Properties["email"] as string;


        string offre_id = Application.Current.Properties["idoff"] as string;
        string offre_titre = Application.Current.Properties["titre"] as string;
        string offre_entreprise = Application.Current.Properties["entreprise"] as string;
        string offre_adress = Application.Current.Properties["adress"] as string;
        string offre_date = Application.Current.Properties["date"] as string;
        string offre_etat = Application.Current.Properties["etat"] as string;

        string offre_descp = Application.Current.Properties["descpost"]as string;
        string offre_diplome = Application.Current.Properties["diplome"] as string;
        string offre_hskils=  Application.Current.Properties["hskils"] as string;
        string offre_sskils = Application.Current.Properties["softskils"] as string;
        string offre_userm = Application.Current.Properties["userm"] as string;
        offresRepository repo = new offresRepository();
        UserRepository userrepo = new UserRepository();
        string role;



        public Detail()
        {
        
            InitializeComponent();
            Titre.Text = offre_titre;
            Entreprise.Text = offre_entreprise;
            Adress.Text = offre_adress;
            descPost.Text =offre_descp;
            diplome.Text =offre_diplome;
            hSkils.Text =offre_hskils;
            SSkils.Text =offre_sskils;
            etat.Source = offre_etat;


          
            


    }
        protected override async void OnAppearing()
        {
            var offre = await  repo.Getoffre(offre_id);
            role = await userrepo.GetUserRole(user);
            if (role == "Entreprise")
            {
                postuler.IsEnabled = false;
            }





        }
        /* protected override async void OnAppearing()
         {
             var offre = await offresRepository.GetOffre(id);



         }*/
         private async void Like(object sender, EventArgs e)
         {


             string value = like.Source.ToString();
            if (value == "File: coeurVide.png")
            {
                like.Source = "coeur.png";
                 var liked = new liked()
                 {
                     idOffre = offre_id,
                     usermail = user
                 };
                 var data =await firebase
                  .Child("liked")
                  .PostAsync(liked);
               // var isLiked = await repo.addLike(offre_id, user, offre_titre,offre_entreprise,offre_adress,offre_descp,offre_diplome,offre_hskils,offre_sskils,offre_userm,offre_etat,offre_date);
               
                

            }

            else
            {
                like.Source = "coeurVide.png";
            }





         }

         private void goToPostule(object sender, EventArgs e)
         {

             Navigation.PushAsync(new postuler());
         }
    }
}