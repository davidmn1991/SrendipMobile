﻿using System;

namespace PorpoiseMobileApp.Client
		//Production http://api.porpoise.com
		//Test http://porpoiseapi.click

		//Production API KEY api_key_uS4bcabwKM4ZQrQT8RHUQgtt

		//Test API KEY api_key_or8L7EFAuUDQy7xbOELvpQtt
		private const string apiAddress = "http://srendipapi.us-east-1.elasticbeanstalk.com";
        //private const string apiAddress = "http://api.porpoise.com";
        //Production api_key_uS4bcabwKM4ZQrQT8RHUQgtt
        private const string API_KEY = "api_key_uS4bcabwKM4ZQrQT8RHUQgtt";
			var headers = this.buildDefaultHeaders();

		public async Task<LoginResponse<Object>> Login(string email, string password)
		{
			var requestEmployeeUrl = "" + apiAddress + "/login";

			var signupModel = new LoggedRequestModel
			{
				email = email,

				password = password


			};
			try
			{

				var result = await PerformPost<LoggedRequestModel, LoginResponse<Object>>(requestEmployeeUrl, signupModel, IRequestFormat.JSON, false, x =>
				{
					var error = x.Content.ReadAsStringAsync().Result;
					throw new PorpoiseException(error);
				});

				if (result != null)
				{
					return result;
				}

				throw new Exception("Please connect to the internet");
			}


			catch (Exception ex)
			{
				throw new PorpoiseException(Resource.PorpoiseConnectionError);
			}
		}

		public async Task<LoginResponse<Object>> LoginOauth(string email, string provider)
		{
			var LoginUrl = "" + apiAddress + "/login";

			var LoginOauthModel = new LoginOauthRequestModel
			{
				email = email,

                provider = provider


			};
			try
			{

				var result = await PerformPost<LoginOauthRequestModel, LoginResponse<Object>>(LoginUrl, LoginOauthModel, IRequestFormat.JSON, false, x =>
				{
					var error = x.Content.ReadAsStringAsync().Result;
					throw new PorpoiseException(error);
				});

				if (result != null)
				{
					return result;
				}

				throw new Exception("Please connect to the internet");
			}


			catch (Exception ex)
			{
				throw new PorpoiseException(Resource.PorpoiseConnectionError);
			}
		}

		public async Task<EmailVerificationResponseModel<Object>> EmailVerification(string email, string provider_id)
		{
			var requestUrl = "" + apiAddress + "/users/check-register";

			var EmailVerification = new EmailVerificationRequestModel
			{
				email = email,

				provider_id = provider_id


			};
			try
			{

				var result = await PerformPost<EmailVerificationRequestModel, EmailVerificationResponseModel<Object>>(requestUrl, EmailVerification, IRequestFormat.JSON, false, x =>
				{
					var error = x.Content.ReadAsStringAsync().Result;
					throw new PorpoiseException(error);
				});

				if (result != null)
				{
					return result;
				}

				throw new Exception("Please connect to the internet");
			}


			catch (Exception ex)
			{
				throw new PorpoiseException(Resource.PorpoiseConnectionError);
			}
		}

		public async Task<ResponseModel<Object>> UploadLocation(double latitude, double longitude)
		{
			var requestUrl = "" + apiAddress + "/users/location";

			var UploadLocation= new UploadLocationRequestModel
			{
				latitude = latitude,

                longitude = longitude

			};
			try
			{

				var result = await PerformPost<UploadLocationRequestModel, ResponseModel<Object>>(requestUrl, UploadLocation, IRequestFormat.JSON, false, x =>
				{
					var error = x.Content.ReadAsStringAsync().Result;
					throw new PorpoiseException(error);
				});

				if (result != null)
				{
					return result;
				}

				throw new Exception("Please connect to the internet");
			}


			catch (Exception ex)
			{
				throw new PorpoiseException(Resource.PorpoiseConnectionError);
			}
		}

		public async Task<ResponseModel<Object>> UploadImageProfile(string image_url)
		{
			var requestUrl = "" + apiAddress + "/users/profile/update";

			var UploadImage = new UploadImageRequestModel
			{
				image_url = image_url

			};
			try
			{

				var result = await PerformPut<UploadImageRequestModel, ResponseModel<Object>>(requestUrl, UploadImage, IRequestFormat.JSON, false, x =>
				{
					var error = x.Content.ReadAsStringAsync().Result;
					throw new PorpoiseException(error);
				});

				if (result != null)
				{
					return result;
				}

				throw new Exception("Please connect to the internet");
			}


			catch (Exception ex)
			{
				throw new PorpoiseException(Resource.PorpoiseConnectionError);
			}
		}

		public async Task<ResponseModel<Object>> RegisterUser(string email, string password, string first_name, string last_name, string bio, string image_url, string gender, string birth_date)
		{
			var requestUrl = "" + apiAddress + "/users/register";

			var signupModel = new RegisterUserRequestModel
			{
				email = email,

				password = password,

				first_name = first_name,

				last_name = last_name,

                bio = bio,

                birth_date = birth_date,

                image_url = image_url,

                gender = gender


			};
			try
			{

				var result = await PerformPost<RegisterUserRequestModel, ResponseModel<Object>>(requestUrl, signupModel, IRequestFormat.JSON, false, x =>
				{
					var error = x.Content.ReadAsStringAsync().Result;
					throw new PorpoiseException(error);
				});

				if (result != null)
				{
					return result;
				}

				throw new Exception("Please connect to the internet");
			}


			catch (Exception ex)
			{
				throw new PorpoiseException(Resource.PorpoiseConnectionError);
			}
		}

		public async Task<ResponseModel<Object>> RegisterOauthUser(string email, string first_name, string last_name, string gender, string birth_date, UserLoginProviderRequestModel user_login_provider)
		{
			var requestUrl = "" + apiAddress + "/users/register";

			var signupModel = new RegisterOauthUserRequestModel
			{
				email = email,

                first_name = first_name,

                last_name = last_name,

                gender = gender,

                birth_date = birth_date,

				user_login_provider = user_login_provider

			};
			try
			{

				var result = await PerformPost<RegisterOauthUserRequestModel, ResponseModel<Object>>(requestUrl, signupModel, IRequestFormat.JSON, false, x =>
				{
					var error = x.Content.ReadAsStringAsync().Result;
					throw new PorpoiseException(error);
				});

				if (result != null)
				{
					return result;
				}

				throw new Exception("Please connect to the internet");
			}


			catch (Exception ex)
			{
				throw new PorpoiseException(Resource.PorpoiseConnectionError);
			}
		}

		public async Task<ResponseModel<Object>> RegisterUserEvent(string email, string password, string first_name, string last_name, string bio, string image_url, string gender, string birth_date, UserEventRequestModel @event)
		{
			var requestUrl = "" + apiAddress + "/users/register";

			var signupModel = new RegisterUserEventRequestModel
			{
				email = email,

				password = password,

				first_name = first_name,

				last_name = last_name,

				bio = bio,

				birth_date = birth_date,

				image_url = image_url,

				gender = gender,

				@event = @event

			};
			try
			{

				var result = await PerformPost<RegisterUserEventRequestModel, ResponseModel<Object>>(requestUrl, signupModel, IRequestFormat.JSON, false, x =>
				{
					var error = x.Content.ReadAsStringAsync().Result;
					throw new PorpoiseException(error);
				});

				if (result != null)
				{
					return result;
				}

				throw new Exception("Please connect to the internet");
			}


			catch (Exception ex)
			{
				throw new PorpoiseException(Resource.PorpoiseConnectionError);
			}
		}

		public async Task<SignupResponseModel> Signup(string email, string company_name, string first_name, string last_name)
		{
            var requestUrl = "" + apiAddress + "/v1/signup";

            var signupModel = new SignupRequestModel
            {
                email = email,

                company_name = company_name,

                first_name = first_name,
			};
			try
			{

				var result = await PerformPost<SignupRequestModel, SignupResponseModel>(requestUrl, signupModel, IRequestFormat.JSON, false, x =>
				{
					var error = x.Content.ReadAsStringAsync().Result;
					throw new PorpoiseException(error);
				});

                if (result != null)
                {
                    AccountInfo.Message_webservice = result.Message;
                }
                else
                {
                    AccountInfo.Message_webservice = "Please connect to the internet";
                }


                if (result != null && result.Successful)
				{


					return result;
				}

                throw new Exception("Please connect to the internet");
			}

			catch (PorpoiseException pe)
			{
				return new SignupResponseModel(false, pe.Message);
			}
			catch (Exception ex)
			{
				throw new PorpoiseException(Resource.PorpoiseConnectionError);
			}
		}

		public async Task<ResponseModel<Object>> AddBubble(int bubble_id, string description, string type)
		{
			var url = "" + apiAddress + "/users/add-bubble";

			var AddBubble = new AddBubbleRequestModel
			{

				bubble_id = bubble_id,

				description = description,

				type = type
			};
			try
			{

				var result = await PerformPost<AddBubbleRequestModel, ResponseModel<Object>>(url, AddBubble, IRequestFormat.JSON, false, x =>
				{
					var error = x.Content.ReadAsStringAsync().Result;
					throw new PorpoiseException(error);
				});

				if (result != null)
				{
					return result;
				}

				throw new Exception("Please connect to the internet");
			}


			catch (Exception ex)
			{
				throw new PorpoiseException(Resource.PorpoiseConnectionError);
			}
		}

		public async Task<ResponseModel<Object>> SaveProfile(string user_dipped_id)
		{
			var url = "" + apiAddress + "/users/dipped";

			var SaveProfile = new SaveProfileRequestModel
			{

				user_dipped_id = user_dipped_id

			

			};
			try
			{

				var result = await PerformPost<SaveProfileRequestModel, ResponseModel<Object>>(url, SaveProfile, IRequestFormat.JSON, false, x =>
				{
					var error = x.Content.ReadAsStringAsync().Result;
					throw new PorpoiseException(error);
				});

				if (result != null)
				{
					return result;
				}

				throw new Exception("Please connect to the internet");
			}


			catch (Exception ex)
			{
				throw new PorpoiseException(Resource.PorpoiseConnectionError);
			}
		}

		public async Task<ResponseModel<Object>> AddBubbleFile(int bubble_id, string type, List<Bubbledetails> bubble_details)
		{
			var url = "" + apiAddress + "/users/add-bubble";

			var AddBubble = new AddBubbleFileRequestModel
			{

				bubble_id = bubble_id,

				type = type,

                bubble_details = bubble_details
			
			};
			try
			{

				var result = await PerformPost<AddBubbleFileRequestModel, ResponseModel<Object>>(url, AddBubble, IRequestFormat.JSON, false, x =>
				{
					var error = x.Content.ReadAsStringAsync().Result;
					throw new PorpoiseException(error);
				});

				if (result != null)
				{
					return result;
				}

				throw new Exception("Please connect to the internet");
			}


			catch (Exception ex)
			{
				throw new PorpoiseException(Resource.PorpoiseConnectionError);
			}
		}


		public async Task<ForgotPasswordResponseModel> ForgotPassword(string email)
        {
            var requestEmployeeUrl = "" + apiAddress + "/webapp/request_password_reset";

            var ForgotPasswordModel = new ForgotPasswordRequestModel
            {
                email_address_data = email
            };
            try
            {

                var result = await PerformPost<ForgotPasswordRequestModel, ForgotPasswordResponseModel>(requestEmployeeUrl, ForgotPasswordModel, IRequestFormat.JSON, false, x =>
                {
                    var error = x.Content.ReadAsStringAsync().Result;
                    throw new PorpoiseException(error);
                });

                if (result != null)
                {
                    AccountInfo.Message_webservice = result.Message;
                    Debug.WriteLine("ForgotPasswordModel");
                    Debug.WriteLine("Message:"+ AccountInfo.Message_webservice);
                }
                else
                {
                    AccountInfo.Message_webservice = "Please connect to the internet";
                    Debug.WriteLine("ForgotPasswordModel");
                    Debug.WriteLine("Message:" + AccountInfo.Message_webservice);
                }


                if (result != null && result.Successful)
                {
                    Debug.WriteLine("ForgotPasswordModel");
                    Debug.WriteLine("result Successful");

                    return result;
                }
                Debug.WriteLine("ForgotPasswordModel - throw new Exception");
                throw new Exception("Please connect to the internet");
            }

            catch (PorpoiseException pe)
            {
                Debug.WriteLine("ForgotPasswordModel - PorpoiseException");
                return new ForgotPasswordResponseModel(false, pe.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ForgotPasswordModel - Exception");
                throw new PorpoiseException(Resource.PorpoiseConnectionError);
            }

        }

        public async Task<ResponseModel<Object>> Confirmcode(string mobilecode){
            {

                mobile_code = mobilecode

			try
			{

				var result = await PerformPost<CodeConfirmationRequestModel, ResponseModel<Object>>(requestCodeURL, confirmationModel, IRequestFormat.JSON, false, x =>
				{
					var error = x.Content.ReadAsStringAsync().Result;
					throw new PorpoiseException(error);
				});

				if (result != null)
				{
					return result;
				}

				throw new Exception("Please connect to the internet");
			}

			
			catch (Exception ex)
			{
				throw new PorpoiseException(Resource.PorpoiseConnectionError);
			}


		public async Task<string> GetCompanyName() {

			try
			{





				return null;
			}


        /// <seealso cref="Client(bool)" cref="buildTestHeaders()"/>

		public async Task<AllProfilesResponseModel<Object>> GetNearProfiles(double latitude, double logitude, int page,int items)

		public async Task<ProfileDetailsResponseModel<Object>> GetProfilesDetails(string id)

		public async Task<SavedProfilesResponseModel<Object>> SavedProfiles()

		public async Task<ProfileDetailsResponseModel<Object>> GetMyProfilesDetails()

		public async Task<OtherProfileResponseModel<Object>> GetOtherProfile(double latitude, double longitude)

		public async Task<BubblesListResponseModel<Object>> GetBubblesList()




























		/// <summary>

			var url = "" + apiAddress + "/v1/set_tutorial_to_complete/";

            var requestModel = new LoginTutorialRequestModel
			{
                employee_uid = employeeUid,
                tutorial = tutorial

			};
			try
			{
                var result = await PerformPost<LoginTutorialRequestModel, Client.ResponseModel<Object>>(url, requestModel, IRequestFormat.JSON, false, x =>
				{
					var error = x.Content.ReadAsStringAsync().Result;
					throw new PorpoiseException(error);
				});
				if (result.Successful)
				{
					Debug.WriteLine("Result Success " + result.Successful);
				}
				return result;

			}
            Debug.WriteLine("PERFORMING SIGN IN: "+loginUrl);

				Debug.WriteLine("MESSAGE: " + result.Message + " Email: "+email+" Password "+password);

        public async Task<SetNewPasswordResponseModel> SetupNewPassword(string password, string uid)
        {

			try {


				var requestModel = new SetNewPasswordRequestModel
				{
                    Password= password,

				}; 

                var result = await PerformPost<SetNewPasswordRequestModel, SetNewPasswordResponseModel>(newPasswordURL, requestModel, IRequestFormat.JSON, false, x =>
				{
					var error = x.Content.ReadAsStringAsync().Result;
					throw new PorpoiseException(error);
				});

				if (result != null && result.Successful)
				{
					AccountInfo.Password = password;
                    //AccountInfo.Email = email;
					AccountInfo.Token = result.Token;

					return result;
				}
				return result;
			catch (PorpoiseException pe)
			{
				return new SetNewPasswordResponseModel(false, pe.Message);
			}
			catch (Exception ex)
			{
				throw new PorpoiseException(Resource.PorpoiseConnectionError);
			}
                    //AccountInfo.Logout();
                    Debug.WriteLine("ERROR = " + error);
             //throw new PorpoiseException(ex.Message);

                return null;
                    Debug.WriteLine("RESPONSE IS SUCCESSFUL");
                    Debug.WriteLine("RESPONSE UNSUCCESSFUL "+response.ToString());
                var temp = await PerformAction<T>(x => x.GetAsync(path), xml: xml, errorHandler: errorHandler);

			var url = "" + apiAddress + "/v1/flag_post/";
			
            var requestModel = new ReportPostRequestModel
			{
                post_uid = postId.Value,
                user_uid = flaggedByUserId.Value

			};
			try
			{
                var result = await PerformPost<ReportPostRequestModel, Client.ResponseModel<Object>>(url, requestModel, IRequestFormat.JSON, false, x =>
				{
					var error = x.Content.ReadAsStringAsync().Result;
					throw new PorpoiseException(error);
				});
                if (result.Successful)
                {
                    Debug.WriteLine("Result Success " + result.Successful);
                }

			}
			
			catch (Exception ex)
			{
                return null;
			}

			var url = "" + apiAddress + "/v1/flag_user/";

            var requestModel = new ReportUserRequestModel
			{
                post_uid = postId.Value,
                user_uid = userId.Value,

			};
			try
			{
				var result = await PerformPost<ReportUserRequestModel, Client.ResponseModel<Object>>(url, requestModel, IRequestFormat.JSON, false, x =>
				{
					var error = x.Content.ReadAsStringAsync().Result;
					throw new PorpoiseException(error);
				});
				if (result.Successful)
				{
					Debug.WriteLine("Result Success " + result.Successful);
				}
				return result;

			}

                var requestModel = new InviteCoworkerRequestModel
                {
                    employee_uid = employeeUid.Value,

				};

				var result = await PerformPost<InviteCoworkerRequestModel, Client.ResponseModel<Object>>(url, requestModel, IRequestFormat.JSON, false, x =>
				{
					var error = x.Content.ReadAsStringAsync().Result;
					throw new PorpoiseException(error);
				});
				if (result.Successful)
				{
					Debug.WriteLine("Result Success " + result.Successful);
				}
				return result;
        {
            Debug.WriteLine("PERFORMING POST "+format.ToString());
                    Debug.WriteLine("FORMAT IS JSON");
                    Debug.WriteLine("PATH: " + path);
                    Debug.WriteLine("BODY: " + body.ToString());
                    var result = await PerformAction<TResult>(x => x.PostAsJsonAsync(path, body), requiresAuthentication, errorHandler);
                    Debug.WriteLine("RESULT: "+result.ToString());

		private async Task<TResult> PerformPut<TRequest, TResult>(string path, TRequest body, IRequestFormat format = IRequestFormat.JSON, bool requiresAuthentication = false, Action<HttpResponseMessage> errorHandler = null)
		{
			Debug.WriteLine("PERFORMING POST " + format.ToString());
					Debug.WriteLine("FORMAT IS JSON");
					Debug.WriteLine("PATH: " + path);
					Debug.WriteLine("BODY: " + body.ToString());
					var result = await PerformAction<TResult>(x => x.PutAsJsonAsync(path, body), requiresAuthentication, errorHandler);
					Debug.WriteLine("RESULT: " + result.ToString());

		private Tuple<string, string>[] buildTestHeaders() {

            List<Tuple<string, string>> headers = new List<Tuple<string, string>>();

            headers.Add(new Tuple<string, string>("X-Api-Key", API_KEY));
            headers.Add(new Tuple<string, string>("X-Porp-ApiVersion", "1.0"));
#if __ANDROID__
			headers.Add(new Tuple<string, string>("X-Porp-Platform", "ANDROID_PORPOISE_APP"));
#elif __IOS__
            headers.Add(new Tuple<string, string>("X-Porp-Platform", "IOS_PORPOISE_APP"));
#else
            headers.Add(new Tuple<string, string>("X-Porp-Platform", "PORPOISE_APP"));
#endif



			//headers.Add(new Tuple<string, string>("X-Porp-ApiVersion", "1.0"));

			// headers.Add(new Tuple<string, string>("X-Porp-Platform", "PORPOISE_APP"));

			if (!string.IsNullOrEmpty(AccountInfo.token_id))

			return headers.ToArray();



        #endregion
    }