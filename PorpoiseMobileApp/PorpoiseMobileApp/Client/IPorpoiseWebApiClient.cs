using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PorpoiseMobileApp.Models;

namespace PorpoiseMobileApp.Client
{
	public interface IPorpoiseWebApiClient
	{
		void Dispose();

		Task<EmployeeResponseModel> GetEmployee();
		Task<GoalResponseModel> GetCompanyGoals();

		Task<AllProfilesResponseModel<Object>> GetNearProfiles(double latitude, double logitude ,int page,int items);

		Task<ProfileDetailsResponseModel<Object>> GetProfilesDetails(string id);

		Task<ProfileDetailsResponseModel<Object>> GetMyProfilesDetails();

		Task<SavedProfilesResponseModel<Object>> SavedProfiles();

		Task<OtherProfileResponseModel<Object>> GetOtherProfile(double latitude, double longitude);

		Task<BubblesListResponseModel<Object>> GetBubblesList();

		Task<OrganisationResponseModel> GetAllOrganisations();

		Task<PostsActivityResponseModel> GetPosts(bool onlyForUser);

		Task<ResponseModel<Object>> AddBubble(int bubble_id, string description, string type);

		Task<ResponseModel<Object>> SaveProfile(string user_dipped_id);

		Task<ResponseModel<Object>> UploadImageProfile(string image_url);

		Task<ResponseModel<Object>> UploadLocation(double latitude, double longitude);

		Task<ResponseModel<Object>> AddBubbleFile(int bubble_id, string type, List<Bubbledetails> bubble_details);

		Task<GetPostResponseModel> GetPost(Guid postId);
		Task<LoginResponseModel> PerformSignIn(string email, string password);
		Task<LogHoursResponseModel> PostLogHours(LogHoursRequestModel requestModel);
		Task<LogHoursResponseModel> UpdateLogHours(LogHoursRequestModel requestModel);
		//Task<DeleteLogHoursResponseModel> DeleteLogHours(DeleteLogHoursRequestModel requestModel);
		Task<DeletePostResponseModel> DeletePost(Guid? postId);
		Task<GiveWelldoneResponseModel> GiveWelldone(Guid? postId);

		Task<RemoveWelldoneResponseModel> RemoveWelldone(Guid? postId);

		Task<EmployeeResponseModel> getEmployeeRow(Guid userId);

		Task<string> GetCompanyName();

		Task<RequestEmployeeResponseModel> PerformRequestEmployee(string Fullname, string CompanyName, string companyEmail);

        Task<ResponseModel<Object>> ReportPost(Guid? postId, Guid? flaggedByUserId);

        Task<ResponseModel<Object>> ReportUser(Guid? postId, Guid? userId, string reason);

        Task<SignupResponseModel> Signup(string email, string company_name, string first_name, string last_name);

		Task<ResponseModel<Object>> RegisterUser(string email, string password, string first_name, string last_name, string bio, string image_url, string gender, string birth_date);

        Task<ResponseModel<Object>> RegisterUserEvent(string email, string password, string first_name, string last_name, string bio, string image_url, string gender, string birth_date, UserEventRequestModel @event);

		Task<ResponseModel<Object>> RegisterOauthUser(string email, string first_name, string last_name, string gender, string birth_date, UserLoginProviderRequestModel user_login_provider);

		Task<LoginResponse<Object>> Login(string username, string password);

		Task<LoginResponse<Object>> LoginOauth(string email, string provider);

		Task<EmailVerificationResponseModel<Object>> EmailVerification(string email, string provider_id);

		Task<ForgotPasswordResponseModel> ForgotPassword(string email);

        Task<ResponseModel<Object>> Confirmcode(string mobilecode);

        Task<SetNewPasswordResponseModel> SetupNewPassword(string password, string uid);

        Task<ResponseModel<Object>> InviteCoWorker(Guid? employeeUid, string name, string email);

        Task<ResponseModel<Object>> FlagTutorial(Guid employeeUid, string tutorial);

		//Task<Gift> GetGift(Guid giftId);
		//Task<Goal> GetGoal(Guid goalId);
		//Task<User> GetUser(string email, string password);
	}
}
