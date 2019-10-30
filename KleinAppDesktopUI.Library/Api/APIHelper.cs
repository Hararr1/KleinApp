using KleinAppDataManager.Library.DataAccess;
using KleinAppDesktopUI.Library.Models;
using KleinMessage.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace KleinAppDesktopUI.Library.Api
{
    public class APIHelper : IAPIHelper
    {
        private HttpClient apiClient;
        private ILoggedInUserModel loggedInUser;
        public APIHelper(ILoggedInUserModel loggedInUser)
        {
            InitializeClient();
            this.loggedInUser = loggedInUser;
        }
        private void InitializeClient()
        {
            string api = ConfigurationManager.AppSettings["api"];

            apiClient = new HttpClient();
            apiClient.BaseAddress = new Uri(api);
            apiClient.DefaultRequestHeaders.Accept.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        //Register new account
        public async Task<bool> Register(string email, string password, string confirmpassword, string FirstName, string LastName)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Email",email),
                new KeyValuePair<string, string>("Password",password),
                new KeyValuePair<string, string>("ConfirmPassword",confirmpassword)
            });
            using (HttpResponseMessage response = await apiClient.PostAsync("/api/Account/Register", data))
            {
                if (response.IsSuccessStatusCode)
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
        // authenicate method to get token
        public async Task<AuthenticatedUser> Authenticate(string username, string password)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type","password"),
                new KeyValuePair<string, string>("username",username),
                new KeyValuePair<string, string>("password",password)
            });

            using (HttpResponseMessage response = await apiClient.PostAsync("/Token", data))
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
        // give more information
        public async Task<LoggedInUserModel> GetLoggedInUserInfo(string token)
        {
            apiClient.DefaultRequestHeaders.Clear();
            apiClient.DefaultRequestHeaders.Accept.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            apiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            using (HttpResponseMessage response = await apiClient.GetAsync("api/User/GetInfo"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<LoggedInUserModel>();
                    loggedInUser.CreatedDate = result.CreatedDate;
                    loggedInUser.EmailAddress = result.EmailAddress;
                    loggedInUser.FirstName = result.FirstName;
                    loggedInUser.LastName = result.LastName;
                    loggedInUser.UserId = result.UserId;
                    loggedInUser.Token = token;
                    return (LoggedInUserModel)loggedInUser;

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
            apiClient.DefaultRequestHeaders.Clear();
            apiClient.DefaultRequestHeaders.Accept.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            apiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            using (HttpResponseMessage response = await apiClient.GetAsync("api/User/GetId"))
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

        //ChangePassword
        public async Task<bool> ChangePassword(string token, string oldPassword, string newPassword, string confirmPassword)
        {
            // POST api/Account/ChangePassword
            //need ChangePasswordBindingModel better is do new class in this library? or inherit from api ?

            apiClient.DefaultRequestHeaders.Clear();
            apiClient.DefaultRequestHeaders.Accept.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            apiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var data = new FormUrlEncodedContent(new[]
           {
                new KeyValuePair<string, string>("OldPassword",oldPassword),
                new KeyValuePair<string, string>("NewPassword",newPassword),
                new KeyValuePair<string, string>("ConfirmPassword",confirmPassword)
            });

            using (HttpResponseMessage response = await apiClient.PostAsync("api/Account/ChangePassword", data))
            {
                bool output = false;

                if (response.IsSuccessStatusCode)
                {
                    output = true;
                }

                return output;
            }
        }
    }
}
