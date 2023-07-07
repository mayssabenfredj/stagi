using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Database;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Firebase.Database.Query;
using System.Linq;

namespace satgi.Data
{
    class UserRepository
    {
        public static FirebaseClient firebase = new FirebaseClient("https://stagi-cd67d-default-rtdb.firebaseio.com/");


        public async Task<bool> SaveUser(string mail , string uname , string pass,string r)
         {
            var allUsers = await GetAllUser();
            
            var sum = allUsers.Count()+1;

            var data = await firebase.Child(nameof(User)).PostAsync(new User() {id=sum, email = mail, username=uname , password = pass , role= r  });

             if (string.IsNullOrEmpty(data.Key))
             {
                 return true;
                
             }
             return false;
         }
       

        public static async Task<List<User>> GetAllUser()
        {
            try
            { 
                var userlist = (await firebase
                .Child("User")
                .OnceAsync<User>()).Select(item =>
                new User
                {
                    id =item.Object.id,
                    email = item.Object.email,
                    username =item.Object.username,
                    password = item.Object.password,
                    role=item.Object.role
                }).ToList();
                return userlist;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error:{e}");
                return null;
            }
        }

        public async Task<User> GetUser(String email)
        {
            try
            {
                var allUsers = await GetAllUser();
                await firebase
                .Child("User")
                .OnceAsync<User>();
                return allUsers.Where(a => a.email == email).FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error:{e}");
                return null;
            }
        }

        public async Task<string> GetUserRole(String email)
        {
            try
            {
                var allUsers = await GetAllUser();
                await firebase
                .Child("User")
                .OnceAsync<User>();
               var  user = allUsers.Where(a => a.email == email).FirstOrDefault();
                return user.role;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error:{e}");
                return null;
            }
        }


        /*  Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

          Match match = regex.Match(mail);
          if (match.Success)
          { emailEror.Text = "Email invalide"; }


          if (string.IsNullOrEmpty(mail))
          {
              await DisplayAlert("Ops...", "Please Enter Your Email !", "Ok");
          }

          if (string.IsNullOrEmpty(pass))
          {
              await DisplayAlert("Ops...", "Please Enter A Password !", "Ok");
          }
          if (pass.Length < 3)
          {
              await DisplayAlert("Ops...", "Your Password Must Havr 6 Caracter !", "Ok");
          }*/





    }
}
