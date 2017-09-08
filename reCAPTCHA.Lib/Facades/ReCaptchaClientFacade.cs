using reCAPTCHA.Lib.Clients;
using reCAPTCHA.Lib.DataModels;
using reCAPTCHA.Lib.Factories;
using System.Net.Http;
using System.Threading.Tasks;

namespace reCAPTCHA.Lib.Facades {

	public interface IReCaptchaClientFacade {

		/// <summary>
		/// Requests the validity of a user's response token.
		/// </summary>
		/// <param name="gResponse">The user response token provided by reCAPTCHA.</param>
		/// <returns>Class:ReCaptchaVerificationResponse</returns>
		Task<ReCaptchaVerificationResponse> VerifyUserReCaptchaAsync( string gResponse );

		/// <summary>
		/// Requests the validity of a user's response token.
		/// </summary>
		/// <param name="gResponse">The user response token provided by reCAPTCHA.</param>
		/// <param name="userIP">The IP address of the end user.</param>
		/// <returns>Class:ReCaptchaVerificationResponse</returns>
		Task<ReCaptchaVerificationResponse> VerifyUserReCaptchaAsync( string gResponse, string userIP );
	}

	public class ReCaptchaClientFacade : IReCaptchaClientFacade {

		private ReCaptchaClientSettings clientSettings { get; set; }
		private HttpClient httpClient { get; }
		private ReCaptchaResponseBuilder responseBuilder { get; set; }
		
		private ReCaptchaVerificationClient verificationClient { get; set; }

		public ReCaptchaClientFacade(ReCaptchaClientSettings settings) {
			this.responseBuilder = new ReCaptchaResponseBuilder();
			this.verificationClient = new ReCaptchaVerificationClient();
			this.clientSettings = settings;
			if ( settings.IsUsingHttpClient ) { this.httpClient = settings.httpClient; }
		}

		public Task<ReCaptchaVerificationResponse> VerifyUserReCaptchaAsync( string gResponse ) {
			return Task.Run(()=> {
				var verificationRequest = new ReCaptchaVerificationRequest()
					.SetSecretKey( clientSettings.GetVerificationClientSecret() )
					.SetUserResponse( gResponse );
				var response = ( httpClient == null ) ? this.verificationClient.SendRequest( verificationRequest ) :
					this.verificationClient.SendRequest( verificationRequest, this.httpClient );
				return this.responseBuilder.BuildVerificationResponse( response );
			} );

		}

		public Task<ReCaptchaVerificationResponse> VerifyUserReCaptchaAsync( string gResponse, string userIP ) {
			return Task.Run( () => {
				var verificationRequest = new ReCaptchaVerificationRequest()
					.SetSecretKey( clientSettings.GetVerificationClientSecret() )
					.SetUserResponse( gResponse )
					.SetUserIpAddress( userIP );
				var response = ( httpClient == null ) ? this.verificationClient.SendRequest( verificationRequest ) :
					this.verificationClient.SendRequest( verificationRequest, this.httpClient );
				return this.responseBuilder.BuildVerificationResponse( response );
			} );
		}

		~ReCaptchaClientFacade() {
			if ( this.verificationClient != null ) { this.verificationClient = null; }
		}
	}
}
