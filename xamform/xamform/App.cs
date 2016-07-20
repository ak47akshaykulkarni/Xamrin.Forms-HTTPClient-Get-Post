using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Http;

namespace xamform
{
    public class App : Application
    {
        string baseUrl = "http://ihcdev.azurewebsites.net/api/";
        public App()
        {
            var btnGet = new Button
            {
                Text = "Get User",
                TranslationX = 1.0
            };
            var btnPost = new Button
            {
                Text = "Login (Post method)",
                TranslationX = 1.0
            };
            var mEmail = new Entry
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Placeholder = "Username"
            };
            var mPassword = new Entry
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Placeholder = "Password",
                IsPassword = true
            };
            var listedhere = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center
            };

            btnGet.Clicked +=async (sender, e) =>
            {
                string apiUrl = "User/GetShortList";
                var result = await GetMemberListAsyncGet( apiUrl);
                listedhere.Text = result;
            };
            btnPost.Clicked += async (sender, e) =>
            {
                if (!string.IsNullOrEmpty(mEmail.Text) && !string.IsNullOrEmpty(mPassword.Text))
                {
                    Dictionary<string, string> formParam = new Dictionary<string, string>();
                    formParam.Add("Email", mEmail.Text);
                    formParam.Add("Password", mPassword.Text);
                    string apiUrl = "Account/Authenticate";
                    var result = await AuthenticateAsyncPost(formParam,apiUrl);
                    if (result)
                    {
                        listedhere.Text = "Welcome logged in";
                        await MainPage.Navigation.PushAsync(new loggedin());
                    }
                    else
                    {
                        listedhere.Text = "Authentication failed";
                    }
                }
                else
                {
                    mEmail.Placeholder = mPassword.Placeholder = "Invalid";
                }
            };

            MainPage = new NavigationPage(new ContentPage
            {
                Title = "Login Form",
            Content = new ScrollView
                {
                    Content = new StackLayout
                    {
                        Padding = 50,
                        VerticalOptions = LayoutOptions.Start,
                        Children = {
                        mEmail,
                        mPassword,
                        btnGet,
                        btnPost,
                        listedhere
                    }
                    }
                }
            });
        }
        private async Task<String> GetMemberListAsyncGet(string apiUrl)
        {
            string urls = baseUrl + apiUrl;
            HttpClient webclient = new HttpClient();
            string asad = await webclient.GetStringAsync(urls);
            List<results.CResult> ress = new List<results.CResult>();
            dynamic deserializedDynamic = JsonConvert.DeserializeObject<results>(asad);
            ress = deserializedDynamic.Result;
            results.CResult res1 = ress[0];
            string full = res1.Salutation + res1.FirstName + res1.MiddleName + res1.LastName;
            string sss = ress[0].FirstName;
            int coder = deserializedDynamic.Code;
            return full;
        }

        private async Task<bool> AuthenticateAsyncPost(Dictionary<string, string> param,string apiUrl)
        {
            //string urls = "http://MyWebApi/api/Account/Authenticate";
            string urls = baseUrl + apiUrl;
            string returnable="";

            using (var httpClient = new HttpClient())
            {
                var encodedContent = new FormUrlEncodedContent(param);
                var response = await httpClient.PostAsync(urls, encodedContent).ConfigureAwait(false);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseContent = await response.Content.ReadAsStringAsync ().ConfigureAwait (false);
                    returnable = responseContent;
                    Authenticate.AResult ress = new Authenticate.AResult();
                    dynamic deserializedDynamic = JsonConvert.DeserializeObject<Authenticate>(returnable);
                    ress = deserializedDynamic.Result;
                    bool isAuthenticated = ress.Autheticated;
                    return isAuthenticated;
                }
            }
            return false;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
