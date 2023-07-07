using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;
using Plugin.Media.Abstractions;
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
    public partial class postuler : ContentPage
    {
       FileResult file;
        public static FirebaseClient firebase = new FirebaseClient("https://stagi-cd67d-default-rtdb.firebaseio.com/");
        public FirebaseStorage firebaseStorage = new FirebaseStorage("stagi-cd67d.appspot.com");

        string offre_id = Application.Current.Properties["idoff"] as string;
        public string user = Application.Current.Properties["email"] as string;
        public string source;
        public string filepath;
        public Stream stream;
        public MemoryStream memorystrem;
        public string cvfile;
       /* public FileStream stream;*/




        public postuler()
        {
            InitializeComponent();
           

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
                    await CrossXamarinFormsSaveOpenPDFPackage.Current.SaveAndView(filename.Text, "application/pdf",memorystrem,PDFOpenContext.InApp);
                    
                    
                }
                 cvfile = await firebaseStorage.Child("Cv").Child(Path.GetFileName(file.FullPath)).PutAsync(stream);



            }

            /*source = file.FullPath;
            stream = File.OpenRead(file.FileName);
            /*cv = ImageSource.FromStream.*/
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string lettre = myEditor.Text;

         /*   var cvfile = await firebaseStorage.Child("Cv").PutAsync(await file.OpenReadAsync());*/
            


            /*var cvfile = await firebaseStorage.Child("Cv").Child(Path.GetFileName(file.Path)).PutAsync(stream);*/
            var data = await firebase.Child(nameof(Postulation)).PostAsync(new Postulation() { offre_id = offre_id, user_id = user, lettre = lettre, cv_name=file.FileName,cv_path=file.FullPath, CV = cvfile });

          



        }




    

       /* private  async Task<string> Upload(Stream stream, string v)
        {
            var cvfile = await firebaseStorage.Child("Cv").Child(v).PutAsync(stream);
            return cvfile;
        }*/
    }
}