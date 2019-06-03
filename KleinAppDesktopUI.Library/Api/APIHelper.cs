using KleinAppDataManager.Library.DataAccess;
using KleinAppDesktopUI.Library.Models;
using KleinMessage.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KleinAppDesktopUI.Library.Api
{
    public class APIHelper : IAPIHelper
    {
        private HttpClient ApiClient;
        private ILoggedInUserModel _loggedInUser;
        public APIHelper(ILoggedInUserModel loggedInUser)
        {
            InitializeClient();
            _loggedInUser = loggedInUser;
        }
        private void InitializeClient()
        {
            string api = ConfigurationManager.AppSettings["api"];

            ApiClient = new HttpClient();
            ApiClient.BaseAddress = new Uri(api);
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        

        public async Task<bool> Register(string email, string password, string confirmpassword, string FirstName, string LastName)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Email",email),
                new KeyValuePair<string, string>("Password",password),
                new KeyValuePair<string, string>("ConfirmPassword",confirmpassword)
            });
            using (HttpResponseMessage response = await ApiClient.PostAsync("/api/Account/Register", data))
            {
                if(response.IsSuccessStatusCode)
                {

                    //we need a token to save user to data table
                    
                    AuthenticatedUser giveToken = await Authenticate(email, password);

                    try
                    {
                        string id = await GetIdInfo(giveToken.Access_Token);
                        UserData user = new UserData();
                        user.SaveUser(id, FirstName, LastName, giveToken.UserName);
                        return true;
                    }
                    catch (Exception e)
                    {

                        throw new Exception(e.Message);
                    }



                    
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }

            }

        }


        public async Task<AuthenticatedUser> Authenticate(string username, string password)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type","password"),
                new KeyValuePair<string, string>("username",username),
                new KeyValuePair<string, string>("password",password)
            });

            using (HttpResponseMessage response = await ApiClient.PostAsync("/Token", data))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<AuthenticatedUser>();
                  
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<LoggedInUserModel> GetLoggedInUserInfo(string token)
        {
            ApiClient.DefaultRequestHeaders.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            ApiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
           

            using (HttpResponseMessage response = await ApiClient.GetAsync("api/User/GetInfo"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<LoggedInUserModel>();
                    _loggedInUser.CreatedDate = result.CreatedDate;
                    _loggedInUser.EmailAddress = result.EmailAddress;
                    _loggedInUser.FirstName = result.FirstName;
                    _loggedInUser.LastName = result.LastName;
                    _loggedInUser.UserId = result.UserId;
                    _loggedInUser.Token = token;
                    return (LoggedInUserModel)_loggedInUser;

                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
                
            }

        }

        //Get ID 
        public async Task<string> GetIdInfo(string token)
        {
            ApiClient.DefaultRequestHeaders.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            ApiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");


            using (HttpResponseMessage response = await ApiClient.GetAsync("api/User/GetId"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<string>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }

            }

        }

    }
}
