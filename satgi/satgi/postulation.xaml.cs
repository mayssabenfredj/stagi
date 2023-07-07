using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;
using Plugin.XamarinFormsSaveOpenPDFPackage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace satgi
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class postulation : ContentPage
    {
        public string idpost = Application.Current.Properties["idpost"] as string;
        public string user = Application.Current.Properties["usrm"] as string;
        public string lettre = Application.Current.Properties["lettre"] as string;
        public string cv = Application.Current.Properties["cv"] as string;
        public string cvname = Application.Current.Properties["cvname"] as string;
        public string cvpath = Application.Current.Properties["cvpath"] as string;
        public string off = Application.Current.Properties["offre"] as string;
        public static FirebaseClient firebase = new FirebaseClient("https://stagi-cd67d-default-rtdb.firebaseio.com/");
        public FirebaseStorage firebaseStorage = new FirebaseStorage("stagi-cd67d.appspot.com");
        public string source;
        public string filepath;
        public Stream stream;
        public MemoryStream memorystrem;
        public string cvfile;
        FileResult file;
        string cvn, cvp;
        public postulation()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            /* post = (await firebase
                      .Child(nameof(Postulation))
                      .OnceAsync<Postulation>()).FirstOrDefault(c => c.Object.offre_id == idoff);*/
            //BindingContext = post;
            filename.Text = cvname;
            myEditor.Text = lettre;


        }
        private async void SelectImageButton_Clicked(object sender, EventArgs e)
        {

            file = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Pdf,
                PickerTitle = "Pick A CV"
            });
            if (file != null)
            {
                filename.Text = file.FileName;
                add.Source = "done.png";
                add.HeightRequest = 90;
                filepath = file.FullPath;
                stream = await file.OpenReadAsync();
                using (memorystrem = new MemoryStream())
                {
                    await stream.CopyToAsync(memorystrem);
                    await CrossXamarinFormsSaveOpenPDFPackage.Current.SaveAndView(filename.Text, "application/pdf", memorystrem, PDFOpenContext.InApp);


                }
                cvfile = await firebaseStorage.Child("Cv").Child(Path.GetFileName(file.FullPath)).PutAsync(stream);
                cvn = file.FileName;
                cvp = file.FullPath;


            }
            else
            {
                cvfile = cv;
                cvn = cvname;
                cvp = cvpath;
            }

           
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            var reponse = await DisplayAlert("Info", "You Want To Delete Postulation ", "OK", "Cancel");
            if (reponse)
            {
                var postDeleted = (await firebase
                          .Child(nameof(Postulation))
                          .OnceAsync<Postulation>()).Where(c => c.Object.offre_id == off && c.Object.user_id == user).FirstOrDefault();
                await firebase
                       .Child("Postulation").Child(postDeleted.Key).DeleteAsync();
                if (true)
                {
                    await DisplayAlert("Sucess", "Postulation Deleted", "OK");
                    await Navigation.PushAsync(new Main());
                }
                else
                {
                    await DisplayAlert("Sorry", "Postulation notDeleted", "OK");
                }
            }

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            
            var reponse =await DisplayAlert("Info", "You Want To Updatet Postulation ", "OK", "Cancel");
            if (reponse)
            {
                string lettre = myEditor.Text;
                var postUpdated = (await firebase
                           .Child(nameof(Postulation))
                           .OnceAsync<Postulation>()).Where(c => c.Object.offre_id == off && c.Object.user_id == user).FirstOrDefault();

                await firebase
                    .Child("Postulation").Child(postUpdated.Key)
                    .PutAsync(new Postulation() { offre_id = off, user_id = user, lettre = lettre, cv_name = cvn, cv_path = cvp, CV = cvfile });
                if (true)
                {
                    await DisplayAlert("Sucess", "Postulation Updated", "OK");
                    await Navigation.PushAsync(new Main());
                }
                else
                {
                    await DisplayAlert("Sorry", "Postulation not Updated", "OK");
                }
            }
           




        }
    }
}