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
    public partial class profil : TabbedPage
    {
        offresRepository repo = new offresRepository();
        UserRepository userrepo = new UserRepository();
        public static FirebaseClient firebase = new FirebaseClient("https://stagi-cd67d-default-rtdb.firebaseio.com/");
        string role;
        List<Offres> list = new List<Offres>();
        List<Offres> list1 = new List<Offres>();
        string key;



        public string user = Application.Current.Properties["email"] as string;


        public profil()
        {
            InitializeComponent();
                        
            
        }
        protected override async void OnAppearing()
        {
            
             role = await  userrepo.GetUserRole(user);
            if (role=="Student")
            {

                Im3.IsVisible = false;
               // Im4.IsVisible = false;
               // Im2.IsVisible = false;
                B1.IsVisible = false;
                C1.Title = "Offres Liked";
                C3.Title = "Offres Postulé";
                var off = (await firebase
                  .Child("liked")
                  .OnceAsync<liked>()).Select(item =>
                  new liked
                  {
                      idOffre = item.Object.idOffre,
                      usermail = item.Object.usermail
                  }).Where(a => a.usermail == user).ToList();
                for (int i=0; i <off.Count;i++)
                {
                 var offre = await repo.Getoffr(off[i].idOffre);
                    list.Add(offre);

                }
                 if (list.Count() == 0)
                {
                    userOffresList.IsVisible = false;
                    scroll.IsVisible = false;
                    Im1.IsVisible = true;

                }
                else
                {
                    userOffresList.IsVisible = true;
                    scroll.IsVisible = true;
                    Im1.IsVisible = false;
                    userOffresList.ItemsSource = list;
                    
                }


                var post = (await firebase
                  .Child("Postulation")
                  .OnceAsync<Postulation>()).Select(item =>
                  new Postulation
                  {
                      idPost = item.Object.idPost,
                      lettre = item.Object.lettre,
                      CV = item.Object.CV,
                      cv_name = item.Object.cv_name,
                      offre_id = item.Object.offre_id,
                      user_id=item.Object.user_id,
                      cv_path=item.Object.cv_path,
                      
                     
                  }).Where(a => a.user_id == user).ToList();
                for (int i = 0; i < post.Count; i++)
                {                    
                    var offre = await repo.Getoffr(post[i].offre_id);
                    list1.Add(offre);
                    Application.Current.Properties["idpost"] = post[i].idPost;
                    Application.Current.Properties["lettre"] = post[i].lettre;
                    Application.Current.Properties["cv"] = post[i].CV;
                    Application.Current.Properties["cvname"] = post[i].cv_name;
                    Application.Current.Properties["offre"] = post[i].offre_id;
                    Application.Current.Properties["usrm"] = post[i].user_id;
                    Application.Current.Properties["cvpath"] = post[i].cv_path;
                    



                }

                if (list1.Count() == 0)
                {
                    userOffresListD.IsVisible = false;
                    scroll3.IsVisible = false;
                    Im3.IsVisible = true;

                }
                else
                {
                    userOffresListD.IsVisible = true;
                    scroll3.IsVisible = true;
                    Im3.IsVisible = false;
                    userOffresListD.ItemsSource = list1;
                }
               

            }


            if (role== "Entreprise")
            {
                C1.Title = "Offres In Progress";
                C3.Title = "Offres Done";
               

                var off = await repo.GetOffreEtat(user, "progres.png");
                if (off.Count() == 0)
                {
                userOffresList.IsVisible = false;
                scroll.IsVisible = false;
                Im1.IsVisible = true;

                 }
                else
                {
                userOffresList.IsVisible = true;
                scroll.IsVisible = true;
                Im1.IsVisible = false;
                userOffresList.ItemsSource = off;
                }

           
                 var offd = await repo.GetOffreEtat(user, "done.png");
                if (offd.Count() == 0)
                {
                userOffresListD.IsVisible = false;
                scroll3.IsVisible = false;
                Im3.IsVisible = true;

                }
                else
                {
                userOffresListD.IsVisible = true;
                scroll3.IsVisible = true;
                Im3.IsVisible = false;
                userOffresListD.ItemsSource = offd;
                }
            


            }
          
        }

        private async void TapGestureRecognizer_Tapped2(object sender, EventArgs e)
        {
            var frame = (Frame)sender;
            var offre = (Offres)frame.BindingContext;
            Application.Current.Properties["idoffpos"] = offre.idOffre;
            Application.Current.Properties["titre"] = offre.titre;
            Application.Current.Properties["userm"] = offre.useremail;
            if (role == "Student")
            {
                Application.Current.Properties["idoffpos"] = offre.idOffre;
                await Navigation.PushAsync(new postulation());
            } 
            if (role== "Entreprise") { 
                
            

            await Navigation.PushAsync(new Postulations());
            }
        }
        private async void TapGestureRecognizer_Tapped1(object sender, EventArgs e)
        {
            if (role == "Student")
            {
                var frame = (Frame)sender;
                var off = (Offres)frame.BindingContext;
                Application.Current.Properties["idoff"] = off.idOffre;
                Application.Current.Properties["idoff"] = off.idOffre;
                Application.Current.Properties["titre"] = off.titre;
                Application.Current.Properties["entreprise"] = off.entrprise;
                Application.Current.Properties["adress"] = off.adress;
                Application.Current.Properties["date"]=off.date;
                Application.Current.Properties["etat"] = off.etat;
                Application.Current.Properties["descpost"] = off.descPost;
                Application.Current.Properties["diplome"] = off.profileDiplome;
                Application.Current.Properties["hskils"] = off.profileHardS;
                Application.Current.Properties["softskils"] = off.profileSoftS;
                
                Application.Current.Properties["userm"] = off.useremail;

                await Navigation.PushAsync(new Detail());
            }
            if (role == "Entreprise")
            {
                var frame = (Frame)sender;
                var offre = (Offres)frame.BindingContext;
                Application.Current.Properties["idoffpos"] = offre.idOffre;
                Application.Current.Properties["titre"] = offre.titre;
                Application.Current.Properties["userm"] = offre.useremail;

                await Navigation.PushAsync(new Postulations());
            }
        }

        private void goToAdd(object sender, EventArgs e)
        {
            Navigation.PushAsync(new addOffre());
        }
       

        private void goToDetail(object sender, EventArgs e)
        {
           
            
            if (role == "Entreprise")
            {
               
                Navigation.PushAsync(new DetailOffreE());
            }
            else
            {
                 Navigation.PushAsync(new Detail());
            }
           
        }
       
    }
}