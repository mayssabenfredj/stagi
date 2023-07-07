using Firebase.Database;
using Firebase.Storage;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace satgi
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Postulations : ContentPage
    {
        public static FirebaseClient firebase = new FirebaseClient("https://stagi-cd67d-default-rtdb.firebaseio.com/");
        public FirebaseStorage firebaseStorage = new FirebaseStorage("stagi-cd67d.appspot.com");
        public MemoryStream memorystrem;

        public string idoffpos = Application.Current.Properties["idoffpos"] as string;

        public string titre = Application.Current.Properties["titre"] as string;




        public Postulations()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {

            var allpost = (await firebase
                  .Child(nameof(Postulation))
                  .OnceAsync<Postulation>()).Select(item =>
                  new Postulation
                  {
                      idPost = item.Object.idPost,
                      lettre = item.Object.lettre,
                      CV = item.Object.CV,
                      cv_name=item.Object.cv_name,
                      offre_id=item.Object.offre_id



                  }).Where(c => c.offre_id == idoffpos).ToList();
            if (allpost.Count() == 0)
            {
                PostsList.IsVisible = false;
                scroll.IsVisible = false;
                Im1.IsVisible = true;

            }
            else
            {
                PostsList.IsVisible = true;
                scroll.IsVisible = true;
                Im1.IsVisible = false;
                PostsList.ItemsSource = allpost;
            }

            PostsList.ItemsSource = allpost;


        }
        private Stream DownloadPdfStream(string URL)
        {
            //Initialize WebClient
            WebClient webClient = new WebClient();
            // Initialize Uri
            var uri = new System.Uri(URL);
            //Returns the document stream from the given URL
            return webClient.OpenRead(uri);
            

        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var webClient = new WebClient();

            var frame = (Frame)sender;
            var post = (Postulation)frame.BindingContext;
            string file = post.CV;
           // var cv =  await firebaseStorage.Child("Cv").Child(file).GetDownloadUrlAsync();
                    await Launcher.OpenAsync(file);

/*var cv =  await firebaseStorage.Child("Cv").Child(post.cv_name).GetDownloadUrlAsync();
  var Fi = cv.ToString();
  WebView webView = new WebView();
  webView.Source = "https://drive.google.com/viewerng/viewer?url={0}" + Fi;
  //"https://firebasestorage.googleapis.com/v0/b/stagi-cd67d.appspot.com/o/Cv%2FCV%20.pdf";
  //string.Format( "https://drive.google.com/viewerng/viewer?url={0}" , "https://firebasestorage.googleapis.com/v0/b/stagi-cd67d.appspot.com/o/Cv%2FCV%20.pdf");
  Content = webView;
  // string pdfurl = cv;
  // byte[] pdfBytes = webClient.DownloadData(pdfurl);
  /*using (memorystrem = new MemoryStream())
 {
      await stream.CopyToAsync(memorystrem);
      await CrossXamarinFormsSaveOpenPDFPackage.Current.SaveAndView(filename.Text, "application/pdf", memorystrem, PDFOpenContext.InApp);


  }
  */
            /* var fileUrl = await firebase
                             .Child(post.cv_path)
                             .OnceSingleAsync<string>();
             var f= await firebaseStorage.Child("Cv").Child(fileUrl).GetDownloadUrlAsync();
             string fa = f.ToString();*/

            // UrlWebViewSource Url = (UrlWebViewSource)file;
            //   var stream = new MemoryStream(Encoding.UTF8.GetBytes(file));




            /* await Browser.OpenAsync(file, new BrowserLaunchOptions
             {
                 LaunchMode = BrowserLaunchMode.SystemPreferred,
                 TitleMode = BrowserTitleMode.Show,
                 PreferredToolbarColor = Color.AliceBlue,
                 PreferredControlColor = Color.Violet,
                 Flags = BrowserLaunchFlags.PresentAsFormSheet

             });

           //  Launcher.OpenAsync(file);

             // UrlWebViewSource Url = (UrlWebViewSource)file;
             //Stream pdfStream =  DownloadPdfStream(file);


             /*   WebView webView = new WebView();
                webView.Source = Url;
                Content = webView;

               /*  var httpClien = new HttpClient();
                 httpClien.GetStreamAsync()
                 var frame = (Frame)sender;
                 var post = (Postulation)frame.BindingContext;
                 string file = post.CV;
                 var pdfRef = firebaseStorage.Child("CV");
                 var localFile = await File.Create(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "<your-pdf-file>.pdf");
                 await pdfRef.
               */
        }
    }
}