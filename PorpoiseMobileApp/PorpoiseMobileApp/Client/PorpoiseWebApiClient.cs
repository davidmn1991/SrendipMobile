using System;using System.Collections.Generic;using System.Diagnostics;using System.IO;using System.Net.Http;using System.Net.Http.Formatting;using System.Net.Http.Headers;using System.Text;using System.Threading.Tasks;using System.Xml.Serialization;using PorpoiseMobileApp.Models;

namespace PorpoiseMobileApp.Client{	public class PorpoiseWebApiClient : IDisposable, IPorpoiseWebApiClient	{        private static XmlSerializer errorSerializer = new XmlSerializer(typeof(Error));		private string baseAddress;
		//Production http://api.porpoise.com
		//Test http://porpoiseapi.click

		//Production API KEY api_key_uS4bcabwKM4ZQrQT8RHUQgtt

		//Test API KEY api_key_or8L7EFAuUDQy7xbOELvpQtt
		private const string apiAddress = "http://srendipapi.us-east-1.elasticbeanstalk.com";
        //private const string apiAddress = "http://api.porpoise.com";
        //Production api_key_uS4bcabwKM4ZQrQT8RHUQgtt
        private const string API_KEY = "api_key_uS4bcabwKM4ZQrQT8RHUQgtt";		public PorpoiseWebApiClient()		{			this.baseAddress = apiAddress;		}		private HttpClient Client(bool xml = false)		{			//HttpClientHandler handler = new HttpClientHandler();			var client = new HttpClient();
			var headers = this.buildDefaultHeaders();			client.BaseAddress = new Uri(baseAddress);			client.DefaultRequestHeaders.Accept.Clear();			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(xml ? "text/xml" : "application/json"));			//client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(new ProductHeaderValue("PorpoiseMobile")));			foreach (var header in headers)			{				client.DefaultRequestHeaders.Add(header.Item1, header.Item2);			}            return client;		}		public  async Task<RequestEmployeeResponseModel> PerformRequestEmployee(string Fullname, string CompanyName, string companyEmail)		{			var requestEmployeeUrl = "" + apiAddress + "/v1/request_as_employee";			var requestModel = new RequestEmployeeRequestModel			{				fullname = Fullname,				company_name = CompanyName,				work_email = companyEmail			};			try			{				var result = await PerformPost<RequestEmployeeRequestModel, RequestEmployeeResponseModel>(requestEmployeeUrl, requestModel, IRequestFormat.JSON, false, x =>				{					var error = x.Content.ReadAsStringAsync().Result;					throw new PorpoiseException(error);				});				if (result != null && result.Successful)				{                    return result;				}				if (!result.Successful && result.Message.Equals("user_not_found"))				{					throw new PorpoiseException(Resource.AccountDoesNotExistError);				}				if (!result.Successful && result.Message.Equals("invalid_login"))				{					throw new PorpoiseException(Resource.LoginError);				}				else				{					Debug.WriteLine("MESSAGE: " + result.Message);					throw new PorpoiseException(Resource.PorpoiseConnectionError);				}			}			catch (PorpoiseException pe)			{				return new RequestEmployeeResponseModel(false, pe.Message);			}			catch (Exception ex)			{				throw new PorpoiseException(Resource.PorpoiseConnectionError);			}		}

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

                first_name = first_name,                last_name = last_name
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

        public async Task<ResponseModel<Object>> Confirmcode(string mobilecode){            var requestCodeURL = "" + apiAddress + "/v1/verify_signup";            var confirmationModel = new CodeConfirmationRequestModel
            {

                mobile_code = mobilecode            };

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
			}        }


		public async Task<string> GetCompanyName() {

			try
			{
				var employee = await this.GetEmployee();				if (employee != null && employee.Successful) {

					HourLog latestPost = employee.LatestPost;					if (latestPost != null) {

						return latestPost.CompanyName;										}								}
				return null;
			}			catch (Exception ex) {

				Debug.WriteLine(ex.Message);				return null;						}				}		/// <summary>		/// Get specified employee. The auth.Token that is passed in the http header defines 		/// which user is logged in and get's their information without passing an Id		/// </summary>		/// <param ></param>		/// <returns></returns>
        /// <seealso cref="Client(bool)" cref="buildTestHeaders()"/>		public async Task<EmployeeResponseModel> GetEmployee()		{			try			{				var url = apiAddress+ "/v1/get_profile_data/";				var result = await PerformGet<EmployeeResponseModel>(url, false, x =>				{					var error = x.Content.ReadAsStringAsync().Result;					throw new PorpoiseException(Resource.PorpoiseConnectionError);				});				return result;			}			catch (Exception ex)			{				Debug.WriteLine("GET EMPLOYEE API CLIENT ERROR: "+ex.Message);				AccountInfo.Logout();				return null;				//throw new PorpoiseException(ex.Message);			}		}		/// <summary>		/// Get all goals that belong to the specified Company.		/// </summary>		/// <param name="companyId"></param>		/// <returns></returns>		public async Task<GoalResponseModel> GetCompanyGoals()		{			try			{				var url = apiAddress+"/v1/get_all_goals";				var result = await PerformGet<GoalResponseModel>(url, false, x =>				{					var error = x.Content.ReadAsStringAsync().Result;					throw new PorpoiseException(error);				});				return result;			}			catch (Exception ex)			{				Debug.WriteLine("GET COMPANY GOALS API CLIENT ERROR: "+ex.Message);				throw;			}		}

		public async Task<AllProfilesResponseModel<Object>> GetNearProfiles(double latitude, double logitude, int page,int items)		{			try			{				var url = apiAddress + "/users/near-me/"+latitude+"/"+logitude+"/"+page+"/"+items;				var result = await PerformGet<AllProfilesResponseModel<Object>>(url, false, x =>				{					var error = x.Content.ReadAsStringAsync().Result;					throw new PorpoiseException(error);				});				return result;			}			catch (Exception ex)			{				Debug.WriteLine("GET COMPANY GOALS API CLIENT ERROR: " + ex.Message);				throw;			}		}

		public async Task<ProfileDetailsResponseModel<Object>> GetProfilesDetails(string id)		{			try			{				var url = apiAddress + "/users/" + id;				var result = await PerformGet<ProfileDetailsResponseModel<Object>>(url, false, x =>				{					var error = x.Content.ReadAsStringAsync().Result;					throw new PorpoiseException(error);				});				return result;			}			catch (Exception ex)			{				Debug.WriteLine("GET COMPANY GOALS API CLIENT ERROR: " + ex.Message);				throw;			}		}

		public async Task<SavedProfilesResponseModel<Object>> SavedProfiles()		{			try			{				var url = apiAddress + "/users/dipped";				var result = await PerformGet<SavedProfilesResponseModel<Object>>(url, false, x =>				{					var error = x.Content.ReadAsStringAsync().Result;					throw new PorpoiseException(error);				});				return result;			}			catch (Exception ex)			{				Debug.WriteLine("GET COMPANY GOALS API CLIENT ERROR: " + ex.Message);				throw;			}		}

		public async Task<ProfileDetailsResponseModel<Object>> GetMyProfilesDetails()		{			try			{				var url = apiAddress + "/users/profile";				var result = await PerformGet<ProfileDetailsResponseModel<Object>>(url, false, x =>				{					var error = x.Content.ReadAsStringAsync().Result;					throw new PorpoiseException(error);				});				return result;			}			catch (Exception ex)			{				Debug.WriteLine("GET COMPANY GOALS API CLIENT ERROR: " + ex.Message);				throw;			}		}

		public async Task<OtherProfileResponseModel<Object>> GetOtherProfile(double latitude, double longitude)		{			try			{				var url = apiAddress + "/users/next/"+latitude+"/"+longitude;				var result = await PerformGet<OtherProfileResponseModel<Object>>(url, false, x =>				{					var error = x.Content.ReadAsStringAsync().Result;					throw new PorpoiseException(error);				});				return result;			}			catch (Exception ex)			{				Debug.WriteLine("GET COMPANY GOALS API CLIENT ERROR: " + ex.Message);				throw;			}		}

		public async Task<BubblesListResponseModel<Object>> GetBubblesList()		{			try			{				var url = apiAddress + "/bubbles";				var result = await PerformGet<BubblesListResponseModel<Object>>(url, false, x =>				{					var error = x.Content.ReadAsStringAsync().Result;					throw new PorpoiseException(error);				});				return result;			}			catch (Exception ex)			{				Debug.WriteLine("GET COMPANY GOALS API CLIENT ERROR: " + ex.Message);				throw;			}		}




























		/// <summary>		/// Get all organisations that exist in the system		/// </summary>		/// <returns></returns>        public async Task<OrganisationResponseModel> GetAllOrganisations()		{			try			{				var url = apiAddress+"/v1/get_all_organizations";				var result = await PerformGet<OrganisationResponseModel>(url, false, x =>				{					var error = x.Content.ReadAsStringAsync().Result;					throw new PorpoiseException(error);				});				return result;			}			catch (Exception ex)			{				Debug.WriteLine("GET ALL ORGANISATIONS API CLIENT ERROR: "+ex.Message);				throw;			}		}		/// Create Log Hours Posts		/// </summary>		/// <param name="requestModel" type="LogHoursRequestModel"></param>		/// <returns></returns>		public async Task<LogHoursResponseModel> PostLogHours(LogHoursRequestModel requestModel)		{			var url = apiAddress+"/v1/save_new_post";			var result = await PerformPost<LogHoursRequestModel, LogHoursResponseModel>(url, requestModel, IRequestFormat.JSON, false, x =>			{				var error = x.Content.ReadAsStringAsync().Result;				throw new PorpoiseException(error);			});			if (result != null)			{				return result;			}			else			{				return null;			}		}		public async Task<LogHoursResponseModel> UpdateLogHours(LogHoursRequestModel requestModel)		{			var url = apiAddress+"/v1/save_post_details";			var result = await PerformPost<LogHoursRequestModel, LogHoursResponseModel>(url, requestModel, IRequestFormat.JSON, false, x =>			{				var error = x.Content.ReadAsStringAsync().Result;				throw new PorpoiseException(error);			});			if (result != null)			{                Debug.WriteLine(result.ToString());				return result;			}			else			{                Debug.WriteLine(result.ToString());				return null;			}		}		public async Task<GetPostResponseModel> GetPost(Guid postId)		{			try			{				var url = apiAddress+"/v1/get_post_details?post_uid="+postId+"";				var result = await PerformGet<GetPostResponseModel>(url, false, x =>				{					var error = x.Content.ReadAsStringAsync().Result;					throw new PorpoiseException(error);				});				return result;			}			catch (Exception ex)			{				Debug.WriteLine("GET POST DETAILS API CLIENT ERROR: "+ex.Message);				throw;			}		}        public async Task<ResponseModel<Object>> FlagTutorial(Guid employeeUid, string tutorial){

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

			}            catch(Exception ex){                throw;            }        }		/// <summary>		/// Perform the sign in function based on the email and password passed		/// </summary>		/// <param name="email"></param>		/// <param name="password"></param>		/// <returns></returns>		public async Task<LoginResponseModel> PerformSignIn(string email, string password)		{			var loginUrl = ""+apiAddress+"/v1/login/";
            Debug.WriteLine("PERFORMING SIGN IN: "+loginUrl);			var requestModel = new LoginRequestModel			{	Username = email,				Password = password			};			try			{				var result = await PerformPost<LoginRequestModel, LoginResponseModel>(loginUrl, requestModel, IRequestFormat.JSON, false, x =>				{					var error = x.Content.ReadAsStringAsync().Result;					throw new PorpoiseException(error);				});

				Debug.WriteLine("MESSAGE: " + result.Message + " Email: "+email+" Password "+password);				if (result != null && result.Successful)				{					AccountInfo.Password = password;					AccountInfo.Email = email;					AccountInfo.Token = result.Token;					return result;				}				return result;			}			catch (PorpoiseException pe)			{				return new LoginResponseModel(false, pe.Message);			}			catch (Exception ex)			{				throw new PorpoiseException(Resource.PorpoiseConnectionError);			}		}

        public async Task<SetNewPasswordResponseModel> SetupNewPassword(string password, string uid)
        {

			try {


				var requestModel = new SetNewPasswordRequestModel
				{
                    Password= password,                    uid = uid

				};                 var newPasswordURL = "" + apiAddress + "/v1/set_new_password/";

                var result = await PerformPost<SetNewPasswordRequestModel, SetNewPasswordResponseModel>(newPasswordURL, requestModel, IRequestFormat.JSON, false, x =>
				{
					var error = x.Content.ReadAsStringAsync().Result;
					throw new PorpoiseException(error);
				});

				if (result != null && result.Successful)
				{
					AccountInfo.Password = password;
                    //AccountInfo.Email = email;                    Debug.WriteLine("ASSIGNING TOKEN TO HEADER: "+result.Token.ToString());
					AccountInfo.Token = result.Token;

					return result;
				}
				return result;                        }
			catch (PorpoiseException pe)
			{
				return new SetNewPasswordResponseModel(false, pe.Message);
			}
			catch (Exception ex)
			{
				throw new PorpoiseException(Resource.PorpoiseConnectionError);
			}                }		public async Task<PostsActivityResponseModel> GetPosts(bool onlyForUser)		{						Debug.WriteLine("WebApiClient - GetPosts(bool onlyForUser)");			string url = "";			if (onlyForUser)			{				url = ""+apiAddress+"/v1/get_user_posts";				Debug.WriteLine("if (onlyForUser)");			}			else			{				url = ""+apiAddress+"/v1/get_all_posts";				Debug.WriteLine("else (onlyForUser)");			}			try			{				Debug.WriteLine("PRE PerformGet<PostsActivityResponseModel>");				var result = await PerformGet<PostsActivityResponseModel>(url, false, x =>				{					var error = x.Content.ReadAsStringAsync().Result;
                    //AccountInfo.Logout();
                    Debug.WriteLine("ERROR = " + error);					throw new PorpoiseException(error);				});				Debug.WriteLine("POST PerformGet<PostsActivityResponseModel>");				if (result != null && result.Successful)				{					Debug.WriteLine("IF result.Successful");					return result;				}                else if (result != null && !result.Successful){					Debug.WriteLine("else if result.Successful");                    return null;                }				else				{//AccountInfo.Logout();					Debug.WriteLine("if result.Successful");					return new PostsActivityResponseModel(false);				}			}			catch (Exception ex)			{//AccountInfo.Logout();			    Debug.WriteLine("catch (Exception ex)"+ex.Message);
             //throw new PorpoiseException(ex.Message);

                return null;			}		}		public async Task<EmployeeResponseModel> getEmployeeRow(Guid userId)		{			try			{				var url = ""+apiAddress+"/v1/get_employee_row?user_uid="+userId+"";				Debug.WriteLine("RETRIEVING EMPLOYEE INFO: "+url);				var result = await PerformGet<EmployeeResponseModel>(url, false, x =>				{					Debug.WriteLine("RETRIEVING EMPLOYEE INFORMATION " + x.ToString());					var error = x.Content.ReadAsStringAsync().Result;					throw new PorpoiseException(error);				});				return result;			}			catch (Exception ex)			{				throw;			}		}		#region Util methods 		private async Task<T> PerformAction<T>(Func<HttpClient, Task<HttpResponseMessage>> action, bool requiresAuthentication = false, Action<HttpResponseMessage> errorHandler = null, bool xml = false)		{			var client = Client(xml);			{				Debug.WriteLine("TRYING TO GET RESPONSE; URL: ");				HttpResponseMessage response = await action(client).ConfigureAwait(false);				Debug.WriteLine("RESPONSE ");				if (response.IsSuccessStatusCode)				{
                    Debug.WriteLine("RESPONSE IS SUCCESSFUL");					T result;					if (!xml)					{						string content = await response.Content.ReadAsStringAsync();						Debug.WriteLine(content);						result = await response.Content.ReadAsAsync<T>();					}					else					{						XmlSerializer serializer = new XmlSerializer(typeof(T));						try						{							string content = await response.Content.ReadAsStringAsync();							result = (T)serializer.Deserialize(new StringReader(content));						}						catch						{							if (errorHandler != null)							{								errorHandler(response);							}							return default(T);						}					}					return result;				}				else				{
                    Debug.WriteLine("RESPONSE UNSUCCESSFUL "+response.ToString());					if (errorHandler != null)					{						errorHandler(response);					}					return default(T);				}			}		}		private async Task<T> PerformGet<T>(string path, bool xml = false, Action<HttpResponseMessage> errorHandler = null)		{			try			{                Debug.WriteLine("PATH  PerformGet()" + path);
                var temp = await PerformAction<T>(x => x.GetAsync(path), xml: xml, errorHandler: errorHandler);				return temp;			}			catch (Exception ex)			{				Debug.WriteLine("catch  PerformGet()" + ex.Message);				throw;			}		}		private Task<T> PerformGet<T>(string url, params object[] parameters)		{			return PerformGet<T>(string.Format(url, parameters), errorHandler: HandleError);		}		public async Task<GiveWelldoneResponseModel> GiveWelldone(Guid? postId)		{			var url = ""+apiAddress+"/v1/give_well_done?post_uid="+postId+"";			var result = await PerformGet<GiveWelldoneResponseModel>(url, false, x =>			{								var error = x.Content.ReadAsStringAsync().Result;				throw new PorpoiseException(error);			});			return result;		}		public async Task<RemoveWelldoneResponseModel> RemoveWelldone(Guid? postId)		{			var url = ""+apiAddress+"/v1/remove_well_done?post_uid="+postId+"";			var result = await PerformGet<RemoveWelldoneResponseModel>(url, false, x =>			{				var error = x.Content.ReadAsStringAsync().Result;				throw new PorpoiseException(error);			}			);			return result;		}		public async Task<DeletePostResponseModel> DeletePost(Guid? postId)		{			var url = ""+apiAddress+"/v1/delete_post?post_uid="+postId+"";			var result = await PerformGet<DeletePostResponseModel>(url, false, x =>			{				var error = x.Content.ReadAsStringAsync().Result;				throw new PorpoiseException(error);			});			return result;		}        //ReportInnappropriate Post        public async Task<ResponseModel<Object>> ReportPost(Guid? postId, Guid? flaggedByUserId){

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
                }                return result;

			}
			
			catch (Exception ex)
			{
                return null;
			}        }        //Report user        public async Task<ResponseModel<Object>> ReportUser(Guid? postId, Guid? userId, string reason){

			var url = "" + apiAddress + "/v1/flag_user/";

            var requestModel = new ReportUserRequestModel
			{
                post_uid = postId.Value,
                user_uid = userId.Value,                reason = reason

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

			}            catch(Exception ex){                Debug.WriteLine(ex);                return null;            }        }        public async Task<ResponseModel<Object>> InviteCoWorker(Guid? employeeUid, string name, string email){            try{               var url = "" + apiAddress + "/v1/invite_coworker/";

                var requestModel = new InviteCoworkerRequestModel
                {
                    employee_uid = employeeUid.Value,                    name = name,                    email = email

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
				return result;            }            catch(Exception ex){                Debug.WriteLine(ex);                return null;            }        }		private async Task<TResult> PerformPost<TRequest, TResult>(string path, TRequest body, IRequestFormat format = IRequestFormat.JSON, bool requiresAuthentication = false, Action<HttpResponseMessage> errorHandler = null)
        {
            Debug.WriteLine("PERFORMING POST "+format.ToString());			switch (format)			{				case IRequestFormat.JSON:
                    Debug.WriteLine("FORMAT IS JSON");
                    Debug.WriteLine("PATH: " + path);
                    Debug.WriteLine("BODY: " + body.ToString());
                    var result = await PerformAction<TResult>(x => x.PostAsJsonAsync(path, body), requiresAuthentication, errorHandler);
                    Debug.WriteLine("RESULT: "+result.ToString());					return result;				case IRequestFormat.XML:					return await PerformAction<TResult>(x => x.PostAsync(path, body, new XmlMediaTypeFormatter() { UseXmlSerializer = true }), requiresAuthentication, errorHandler, true);				case IRequestFormat.STRING:					return await PerformAction<TResult>(async x =>					{						var content = body.ToString();						try						{							return await x.PostAsync(path, new StringContent(content));						}						catch						{							throw;						}					}, requiresAuthentication, errorHandler);			}			var temp = default(TResult);			return temp;		}

		private async Task<TResult> PerformPut<TRequest, TResult>(string path, TRequest body, IRequestFormat format = IRequestFormat.JSON, bool requiresAuthentication = false, Action<HttpResponseMessage> errorHandler = null)
		{
			Debug.WriteLine("PERFORMING POST " + format.ToString());			switch (format)			{				case IRequestFormat.JSON:
					Debug.WriteLine("FORMAT IS JSON");
					Debug.WriteLine("PATH: " + path);
					Debug.WriteLine("BODY: " + body.ToString());
					var result = await PerformAction<TResult>(x => x.PutAsJsonAsync(path, body), requiresAuthentication, errorHandler);
					Debug.WriteLine("RESULT: " + result.ToString());					return result;				case IRequestFormat.XML:					return await PerformAction<TResult>(x => x.PutAsync(path, body, new XmlMediaTypeFormatter() { UseXmlSerializer = true }), requiresAuthentication, errorHandler, true);				case IRequestFormat.STRING:					return await PerformAction<TResult>(async x =>					{						var content = body.ToString();						try						{							return await x.PostAsync(path, new StringContent(content));						}						catch						{							throw;						}					}, requiresAuthentication, errorHandler);			}			var temp = default(TResult);			return temp;		}

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

			if (!string.IsNullOrEmpty(AccountInfo.token_id))			{				headers.Add(new Tuple<string, string>("Authorization", "Bearer " + AccountInfo.token_id));				Debug.WriteLine("TOKEN VALUE: " + AccountInfo.token_id);			}

			return headers.ToArray();                }		private Tuple<string, string>[] buildDefaultHeaders()		{			List<Tuple<string, string>> headers = new List<Tuple<string, string>>();			headers.Add(new Tuple<string, string>("X-Api-Key", API_KEY));			headers.Add(new Tuple<string, string>("X-Porp-ApiVersion", "1.0"));#if __ANDROID__			headers.Add(new Tuple<string, string>("X-Porp-Platform", "ANDROID_PORPOISE_APP"));#elif __IOS__            headers.Add(new Tuple<string, string>("X-Porp-Platform", "IOS_PORPOISE_APP"));#else			headers.Add(new Tuple<string, string>("X-Porp-Platform", "PORPOISE_APP"));#endif			if (!string.IsNullOrEmpty(AccountInfo.token_id))			{				headers.Add(new Tuple<string, string>("Authorization", "Bearer " + AccountInfo.token_id));				Debug.WriteLine("TOKEN VALUE: "+ AccountInfo.token_id);			}			return headers.ToArray();		}		static readonly char[] padding = { '=' };		private string ToBase64(string src)		{			byte[] toEncodeAsBytes = Encoding.UTF8.GetBytes(src);			return System.Convert.ToBase64String(toEncodeAsBytes).TrimEnd(padding).Replace('+', '-').Replace('/', '_');		}		private void HandleError(HttpResponseMessage obj)		{			var str = obj.Content.ReadAsStringAsync().Result;			throw new PorpoiseException(errorSerializer.Deserialize(new StringReader(str)) as Error);		}		public void Dispose()		{		}



        #endregion
    }	public enum IRequestFormat	{		JSON, XML, FORMS,		STRING	}}