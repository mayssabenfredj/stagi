using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace satgi
{
    class offresRepository
    {
        public static FirebaseClient firebase = new FirebaseClient("https://stagi-cd67d-default-rtdb.firebaseio.com/");

        public async Task<bool> AddOffre(string t, string en, string a,   string dp, string pd, string phs, string pss, string um)
        {
            try
            {
                var alloffre = await GetAllOffres();

                var sum = alloffre.Count() + 1;

                var offre = new Offres()
                {
                    idOffre = sum.ToString(),
                    titre = t,
                    entrprise = en,
                    adress = a,
                    etat = "progres.png",
                    date = DateTime.Now,
                    
                   
                    descPost = dp,
                    profileDiplome = pd,
                    profileHardS = phs,
                    profileSoftS = pss,
                  
                    useremail = um
                };
                var result = await firebase
                 .Child("Offres")
                 .PostAsync(offre);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error:{e}");
                return false;
            }
        }

        public async Task<List<Offres>> GetAllOffres()
        {

            return (await firebase
                .Child("Offres")
                .OnceAsync<Offres>()).Select(item =>
                new Offres
                {
                    idOffre = item.Object.idOffre,
                    titre = item.Object.titre,
                    entrprise = item.Object.entrprise,
                    adress = item.Object.adress,
                    date = item.Object.date,
                    etat=item.Object.etat,

                    descPost = item.Object.descPost,
                    profileDiplome = item.Object.profileDiplome,
                    profileHardS = item.Object.profileHardS,
                    profileSoftS = item.Object.profileSoftS,
                    useremail = item.Object.useremail


                }).ToList();


        }

        public async Task<Offres> Getoffre(string id)
        {

            var alloffre = await GetAllOffres();
            await firebase
            .Child("Offres")
            .OnceAsync<Offres>();
            return alloffre.Where(a => a.idOffre == id).FirstOrDefault();

        }
        public async Task<List<Offres>> GetOffreUser(string user)
        {

            return (await firebase
               .Child("Offres")
               .OnceAsync<Offres>()).Select(item =>
               new Offres
               {
                   idOffre = item.Object.idOffre,
                   titre = item.Object.titre,
                   entrprise = item.Object.entrprise,
                   adress = item.Object.adress,
                   date = item.Object.date,
                   etat = item.Object.etat,

                   descPost = item.Object.descPost,
                   profileDiplome = item.Object.profileDiplome,
                   profileHardS = item.Object.profileHardS,
                   profileSoftS = item.Object.profileSoftS,
                   useremail = item.Object.useremail


               }).Where(a => a.useremail == user).ToList();

        }
        public async Task<List<Offres>> GetOffreEtat(string user ,string e)
        {

            return (await firebase
                .Child("Offres")
                .OnceAsync<Offres>()).Select(item =>
                new Offres
                {
                    idOffre = item.Object.idOffre,
                    titre = item.Object.titre,
                    entrprise = item.Object.entrprise,
                    adress = item.Object.adress,
                    
                    etat=item.Object.etat,
                   date=item.Object.date,
                    descPost = item.Object.descPost,
                    profileDiplome = item.Object.profileDiplome,
                    profileHardS = item.Object.profileHardS,
                    profileSoftS = item.Object.profileSoftS,
                  
                    useremail = item.Object.useremail


                }).Where(a => a.etat == e && a.useremail==user).ToList();


        }








        public async Task<Offres> Getoffr(string id)
        {

            var alloffre = await GetAllOffres();
          
            return alloffre.Where(a => a.idOffre == id).FirstOrDefault();

        }


        /*  public async Task<bool> addLike(string id , string user, string t, string en, string a, string dp, string pd, string phs, string pss, string um , string e, string d)
          {
              List<string> list = new List<string>();
              list.Add(user);
              var toUpdate = (await firebase
                  .Child("Offres")
                  .OnceAsync<Offres>()).FirstOrDefault(a => a.Object.idOffre == id);



              await firebase
              .Child("Offres")
              .Child(toUpdate.Key).PutAsync(new Offres() {
                  idOffre = id,
                  titre = t,
                  entrprise = en,
                  adress = a,
                  etat = e,
                  date = DateTime.Parse(d),

                  descPost = dp,
                  profileDiplome = pd,
                  profileHardS = phs,
                  profileSoftS = pss,

                  useremail = um,
                  Liked = list });
              return true;

          }*/

        public static async Task<bool> Updateoffre (string id,string t, string en, string a, string e,  string dp, string pd, string phs, string pss, string um)
        {
            try
            {

                var postUpdated = (await firebase
                           .Child(nameof(Offres))
                           .OnceAsync<Offres>()).Where(c => c.Object.idOffre == id && c.Object.useremail == um).FirstOrDefault();

                await firebase
                    .Child("Offres").Child(postUpdated.Key)
                    .PutAsync(new Offres() {
                        idOffre = id,
                        titre = t,
                        entrprise = en,
                        adress = a,
                        etat = e,
                        date = DateTime.Now,

                        descPost = dp,
                        profileDiplome = pd,
                        profileHardS = phs,
                        profileSoftS = pss,

                        useremail = um
                    });
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error:{ex}");
                return false;
            }
        }

        public static async Task<bool> DeleteOffre(string id)
        {
            try
            {


                var toDeleteOffre = (await firebase
                .Child("Offres")
                .OnceAsync<Offres>()).Where(a => a.Object.idOffre == id).FirstOrDefault();
                await firebase.Child("Offres").Child(toDeleteOffre.Key).DeleteAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error:{e}");
                return false;
            }
        }
        public async Task<List<Offres>> GetAll()
        {

            return (await firebase
                .Child("Offres")
                .OnceAsync<Offres>()).Select(item =>
                new Offres
                {
                    idOffre = item.Object.idOffre,
                    titre = item.Object.titre,
                    entrprise = item.Object.entrprise,



                }).ToList();


        }
    }
}
