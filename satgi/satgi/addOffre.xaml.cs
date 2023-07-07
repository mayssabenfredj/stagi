
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
    public partial class addOffre : ContentPage
    {
        offresRepository repository = new offresRepository();
        public string  user = Application.Current.Properties["email"] as string;

        public addOffre()
        {
            InitializeComponent();
        }

        private async void saveOffre(object sender, EventArgs e)
        {
            string titre = Titre.Text;
            string entre = Entreprise.Text;
            string uName = user;
            string adress = Adress.Text;
            
           
            /********Get Desc*******/
            string descpost = DescPost.Text;
            string diplome = Diplome.Text;
            string hskils = HSkils.Text;
            string sskils = SSkils.Text;
        
            var offre = await repository.AddOffre(titre,entre,adress,descpost,diplome,hskils,sskils, uName);

            if (offre)
            {
                await DisplayAlert("Add Success", "", "Ok");
                
            }
            else
                await DisplayAlert("Error", "Add Fail", "OK");

        }


    }
    }
