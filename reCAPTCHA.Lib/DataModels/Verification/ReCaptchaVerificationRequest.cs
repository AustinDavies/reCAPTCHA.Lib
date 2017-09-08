using reCAPTCHA.Lib.Interfaces;

namespace reCAPTCHA.Lib.DataModels {

	internal class ReCaptchaVerificationRequest : IReCaptchaRequest {

		private const string RECAPTCHA_VERIFICATION_ENDPOINT = @"https://www.google.com/recaptcha/api/siteverify";

		private string _secretKey { get; set; }
		private string _userResponse { get; set; }
		private string _userIP { get; set; }

		public string requestURL { get; }
		public string secretKey { get { return this._secretKey; } }

		public string userResponse { get { return this._userResponse; } }
		public string userIP { get { return this._userIP; } }

		public ReCaptchaVerificationRequest() {
			this.requestURL = RECAPTCHA_VERIFICATION_ENDPOINT;
		}

		public ReCaptchaVerificationRequest SetSecretKey(string secret ) {
			this._secretKey = secret;
			return this;
		}

		public ReCaptchaVerificationRequest SetUserResponse( string token ) {
			this._userResponse = token;
			return this;
		}

		public ReCaptchaVerificationRequest SetUserIpAddress( string ip ) {
			this._userIP = ip;
			return this;
		}
	}
}
