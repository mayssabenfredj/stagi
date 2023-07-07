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
    public partial class DetailOffreE : ContentPage
    {
        string user = Application.Current.Properties["email"] as string;
        string offre_id = Application.Current.Properties["idoff"] as string;
        string offre_titre = Application.Current.Properties["titre"] as string;
        string offre_entreprise = Application.Current.Properties["entreprise"] as string;
        string offre_adress = Application.Current.Properties["adress"] as string;
        string offre_date = Application.Current.Properties["date"] as string;
        string offre_etat = Application.Current.Properties["etat"] as string;

        string offre_descp = Application.Current.Properties["descpost"] as string;
        string offre_diplome = Application.Current.Properties["diplome"] as string;
        string offre_hskils = Application.Current.Properties["hskils"] as string;
        string offre_sskils = Application.Current.Properties["softskils"] as string;
        string offre_userm = Application.Current.Properties["userm"] as string;
        string et;

        public DetailOffreE()
        {
            InitializeComponent();
            Titre.Text = offre_titre;
            Entreprise.Text = offre_entreprise;
            Adress.Text = offre_adress;
            descPost.Text = offre_descp;
            diplome.Text = offre_diplome;
            hSkils.Text = offre_hskils;
            SSkils.Text = offre_sskils;
            etat.Source = offre_etat;
        }
        private void changeetat(object sender, EventArgs e)
        {
            string value = etat.Source.ToString();
            if (value == "File: progres.png")
            {
                etat.Source = "done.png";
                et = "done.png";
            }
            else
            {
                etat.Source = "progres.png";
                et = "progres.png";
            }
        }

        private async void update(object sender, EventArgs e)
        {
            var reponse = await DisplayAlert("Info", "Are You Sure ", "OK", "Cancel");
            if (reponse)
            {
                var update = await offresRepository.Updateoffre(offre_id, Titre.Text, Entreprise.Text, Adress.Text, et, descPost.Text, diplome.Text, hSkils.Text, SSkils.Text, user);
                if (update)
                {
                    await DisplayAlert("Sucess", "Offre Updated", "OK");
                    await Navigation.PushAsync(new Main());
                }
                else
                {
                    await DisplayAlert("Sorry", "Offre not Updated", "OK");
                }
            }
            }
       
            private async void delete(object sender, EventArgs e)
        {
            var reponse = await DisplayAlert("Info", "Are You Sure ", "OK", "Cancel");
            if (reponse)
            {
                var delet = await offresRepository.DeleteOffre(offre_id);
                if (delet)
                {
                    await DisplayAlert("Sucess", "Offre Deleted", "OK");
                    await Navigation.PushAsync(new Main());
                }
                else
                {
                    await DisplayAlert("Sorry", "Offre not Deleted", "OK");
                }

            }

            }
    }
}