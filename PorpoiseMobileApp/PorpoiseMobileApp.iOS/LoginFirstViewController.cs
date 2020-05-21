
using System;
using System.Diagnostics;
using System.Drawing;
using CoreGraphics;
using Foundation;
using PorpoiseMobileApp.Converters;
using PorpoiseMobileApp.iOS.Services;
using PorpoiseMobileApp.iOS.Utils;
using PorpoiseMobileApp.ViewModels;
using UIKit;
//using Xamarin.Auth;
using System.Json;
using Facebook.LoginKit;
using Facebook.CoreKit;
using System.Collections.Generic;
using WebKit;
using Auth0.OidcClient;
using System.Threading.Tasks;

namespace PorpoiseMobileApp.iOS
{
    public partial class LoginFirstViewController : MvvmViewController<LoginFirstViewModel>
    {
        public LoginFirstViewController (IntPtr handle) : base (handle)
        {
        }

        LoginButton loginView;
        ProfilePictureView pictureView;
        UILabel nameLabel;

        List<string> readPermissions = new List<string> { "public_profile", "email", "user_birthday", "user_gender"};

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);


        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            this.HidesBottomBarWhenPushed = false;
        }

        public async Task UsingOauth()
        {

            var client = new Auth0Client(new Auth0ClientOptions
            {
                Domain = "davidmnc.auth0.com",
                ClientId = "qkthSLxP04EEyU0R3VqoVBFrVyVGv1i4"
            });

            var loginResult = await client.LoginAsync();
        }



        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            loginFacebookButton.TouchUpInside += delegate {

                Console.WriteLine("presing facebook link");

                loginView.SendActionForControlEvents(UIControlEvent.TouchUpInside);

            };



            get_helplbl.TextColor = UIColor.FromRGB(112, 112, 112);

            back_button.SetTitleColor(UIColor.FromRGB(75,171,229),UIControlState.Normal);

            loginButton.BackgroundColor = UIColor.FromRGB(75, 171, 229);




            signup_lbl.TextColor = UIColor.FromRGB(112, 112, 112);

            passwordText.SecureTextEntry = true;

            Profile.CurrentProfile = null;

            AccessToken.CurrentAccessToken = null;

            NSHttpCookieStorage storage = NSHttpCookieStorage.SharedStorage;

            foreach (NSHttpCookie cookie in storage.Cookies)
            {
                if (cookie.Domain == "facebook.com")
                {
                    storage.DeleteCookie(cookie);
                }

            }


            NSUrlCache.SharedCache.RemoveAllCachedResponses();
            var cookies = NSHttpCookieStorage.SharedStorage.Cookies;

            foreach (var c in cookies)
            {
                NSHttpCookieStorage.SharedStorage.DeleteCookie(c);
            }

            var websiteDataTypes = new NSSet<NSString>(new[]
            { 
            //Choose which ones you want to remove
             WKWebsiteDataType.Cookies,
             WKWebsiteDataType.DiskCache,
             WKWebsiteDataType.IndexedDBDatabases,
             WKWebsiteDataType.LocalStorage,
             WKWebsiteDataType.MemoryCache,
             WKWebsiteDataType.OfflineWebApplicationCache,
             WKWebsiteDataType.SessionStorage,
             WKWebsiteDataType.WebSQLDatabases
            });

            WKWebsiteDataStore.DefaultDataStore.FetchDataRecordsOfTypes(websiteDataTypes, (NSArray records) =>
            {
                for (nuint i = 0; i < records.Count; i++)
                {
                    var record = records.GetItem<WKWebsiteDataRecord>(i);

                    WKWebsiteDataStore.DefaultDataStore.RemoveDataOfTypes(record.DataTypes,
                        new[] { record }, () => { Console.Write($"deleted: {record.DisplayName}"); });
                }
            });

            NavigationController.NavigationBarHidden = true;

            this.View.BringSubviewToFront(passwordText);

            this.View.BringSubviewToFront(emailText);

            this.View.BringSubviewToFront(back_button);

            var screenTap = new UITapGestureRecognizer(() =>
            {
                emailText.ResignFirstResponder();
                passwordText.ResignFirstResponder();

            });

            var show_signup = new UITapGestureRecognizer(() =>
            {
                ViewModel.ShowSignUp();

            });

            this.View.AddGestureRecognizer(screenTap);

            this.signup_lbl.AddGestureRecognizer(show_signup);

            this.signup_lbl.UserInteractionEnabled = true;

            back_button.TouchUpInside += (sender, e) => {
                ViewModel.BackIntroduction();
            };

            ViewModel.ForPropertyChange(x => x.ErrorMessage, y =>
            {

                //Debug.WriteLine("PORPOISE LOGO CHANGED");

                var okAlertController = UIAlertController.Create("Error message", ViewModel.ErrorMessage, UIAlertControllerStyle.Alert);

                //Add Action
                okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

                // Present Alert
                PresentViewController(okAlertController, true, null);


            });

            

            loginButton.TouchUpInside += (sender, e) => {
                
                if(!string.IsNullOrEmpty(emailText.Text) && !string.IsNullOrEmpty(passwordText.Text))
                {

                    ViewModel.email = emailText.Text;

                    ViewModel.password = passwordText.Text;

                    ViewModel.Login();
                }

                else if (string.IsNullOrEmpty(emailText.Text))
                {
                    var okAlertController = UIAlertController.Create("Error message", "Please enter email", UIAlertControllerStyle.Alert);

                    //Add Action
                    okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

                    // Present Alert
                    PresentViewController(okAlertController, true, null);

                }

                else if (string.IsNullOrEmpty(passwordText.Text))
                {
                    var okAlertController = UIAlertController.Create("Error message", "Please enter password", UIAlertControllerStyle.Alert);

                    //Add Action
                    okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

                    // Present Alert
                    PresentViewController(okAlertController, true, null);

                }

                //UsingOauth();
            };

            

            Profile.Notifications.ObserveDidChange((sender, e) => {

                if (e.NewProfile == null)
                    return;
                if (AccessToken.CurrentAccessToken != null)
                {
                    var request = new GraphRequest("/me?fields=first_name,last_name,name,email,picture,birthday,gender", null, AccessToken.CurrentAccessToken.TokenString, null, "GET");
                    request.Start((connection, result, error) =>
                    {
                    // Handle if something went wrong with the request
                    if (error != null)
                    {
                        //showAlert("Error", error.Description);
                        return;
                    }

                    //fbReponseFromSDK facebookSDKLoginItem = new fbReponseFromSDK();
                    // Get your profile name
                    var userInfo = result as NSDictionary;
                        if (userInfo["id"] != null)
                        {
                            ViewModel.provider_id = userInfo["id"].ToString();

                            ViewModel.user_login_provider.user_provider_id = userInfo["id"].ToString();

                            Console.WriteLine("id is: " + userInfo["id"].ToString());
                        }
                        if (userInfo["name"] != null)
                        {
                            ViewModel.user_login_provider.user_provider_name = userInfo["name"].ToString();

                            Console.WriteLine("name is: " + userInfo["name"].ToString());
                        }

                        if (userInfo["first_name"] != null)
                        {
                            ViewModel.first_name = userInfo["first_name"].ToString();

                            Console.WriteLine("first_name is: " + userInfo["first_name"].ToString());
                        }

                        if (userInfo["last_name"] != null)
                        {
                            ViewModel.last_name = userInfo["last_name"].ToString();

                            Console.WriteLine("last_name is: " + userInfo["last_name"].ToString());
                        }

                        if (userInfo["email"] != null)
                        {
                            ViewModel.email = userInfo["email"].ToString();

                            Console.WriteLine("email is: " + userInfo["email"].ToString());
                        }

                        if (userInfo["picture"] != null)
                        {
                            Console.WriteLine("profile image is: " + userInfo["picture"].ToString());
                        }

                        if (userInfo["birthday"] != null)
                        {
                            var formatted = DateTime.Parse(userInfo["birthday"].ToString());
                            ViewModel.birth_date = formatted.ToString("yyyy-MM-dd");
                            Console.WriteLine("birthday is: " + userInfo["birthday"].ToString());
                        }

                        if (userInfo["gender"] != null)
                        {
                            if (userInfo["gender"].Equals("male"))
                            {
                                ViewModel.gender = "MALE";
                            }
                            else if (userInfo["gender"].Equals("female"))
                            {
                                ViewModel.gender = "FEMALE";

                            }
                            Console.WriteLine("gender is: " + userInfo["gender"].ToString());
                        }
                        //(Success) Do what you want next :
                        //doneFacbookLogin();

                        //loginView.LoggedOut();

                        ViewModel.user_login_provider.provider = "FACEBOOK";

                        ViewModel.EmailVerification();
                    });
                }

                nameLabel.Text = e.NewProfile.Name;
            });

            // Set the Read and Publish permissions you want to get
            loginView = new LoginButton(new CGRect(0, 0, loginFacebookButton.Frame.Width, loginFacebookButton.Frame.Height))
            {
                //LoginBehavior = LoginBehavior.,
                Permissions = readPermissions.ToArray()
            };

           

            var attributes = new UIStringAttributes
            {
                BackgroundColor = UIColor.FromRGB(75, 171, 229),
             
                ForegroundColor = UIColor.White,
                Font = UIFont.FromName("System Semibold", 17f)
            };
            var titleText = new NSAttributedString("Log In With Facebook", attributes);

            loginView.SetAttributedTitle(titleText, UIControlState.Normal);

            loginView.SetBackgroundImage(null, UIControlState.Normal);

            loginView.BackgroundColor = UIColor.FromRGB(75, 171, 229);
            // Handle actions once the user is logged in
            loginView.Completed += (sender, e) => {
                if (e.Error != null)
                {
                    // Handle if there was an error
                }

                if (e.Result.IsCancelled)
                {
                    // Handle if the user cancelled the login request
                }

                // Handle your successful login
            };

            // Handle actions once the user is logged out
            loginView.LoggedOut += (sender, e) => {

               

                Profile.CurrentProfile = null;
                // Handle your logout
            };

            // The user image profile is set automatically once is logged in
            pictureView = new ProfilePictureView(new CGRect(50, 50, 220, 220));

            // Create the label that will hold user's facebook name
            nameLabel = new UILabel(new RectangleF(20, 319, 280, 21))
            {
                TextAlignment = UITextAlignment.Center,
                BackgroundColor = UIColor.Clear
            };

            // Add views to main view

            Console.WriteLine();

            loginFacebookButton.BackgroundColor = UIColor.FromRGB(75, 171, 229);

            //loginFacebookButton.AddSubview(loginView);

        }

        /*

        partial void Instagram_test(UIButton sender)
        {
            var auth = new OAuth2Authenticator(
            clientId: "2613255312251727",
            scope: "basic",
            authorizeUrl: new Uri("https://api.instagram.com/oauth/authorize?client_id=2613255312251727&redirect_uri=https://socialsizzle.heroku.com/auth/&response_type=code"),
            redirectUrl: new Uri("https://socialsizzle.heroku.com/auth/"));

            

            auth.Completed += Auth_Completed;
            var ui = auth.GetUI();
            PresentViewController(ui, true, null);
        }

        

        private async void Auth_Completed(object sender, AuthenticatorCompletedEventArgs e)
        {
            try
            {
                if (e.IsAuthenticated)
                {
                    var request = new OAuth2Request("GET",
                                                    new Uri("https://graph.facebook.com/me?fields=name,picture,cover,birthday")
                                                    , null, e.Account);
                    var response = await request.GetResponseAsync();

                    
                    var user = JsonValue.Parse(response.GetResponseText());
                    var fbName = user["name"];
                    var fbCover = user["cover"]["source"];
                    var fbProfile = user["picture"]["data"]["url"];

                    //lblName.Text = fbName.ToString();
                    //imgCover.Image = UIImage.LoadFromData(NSData.FromUrl(new NSUrl(fbCover)));
                    //imgProfile.Image = UIImage.LoadFromData(NSData.FromUrl(new NSUrl(fbProfile)));
                }
            }
            catch(Exception ex)
            {


            }

            DismissViewController(true, null);
        }

        /*

        partial void UIButton20291_TouchUpInside(UIButton sender)
        {
            var auth = new OAuth2Authenticator(
             clientId: "467819537190038",
             scope: "",
             authorizeUrl: new Uri("https://api.instagram.com/oauth/authorize/"),
             redirectUrl: new Uri("REDIRECT_URL"));

            auth.Completed += Auth_Completed;
            var ui = auth.GetUI();
            PresentViewController(ui, true, null);
        }*/

    }
}