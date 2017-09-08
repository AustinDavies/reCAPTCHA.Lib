using System;
using System.Collections.Generic;
using System.Net.Http;

namespace reCAPTCHA.Lib.DataModels {
	public class ReCaptchaClientSettings {
		private const string KEY_VERIFICATION_SECRET = "verification-secret";
		private Dictionary<string, string> reCaptchaKeyStorage { get; set; }

		internal HttpClient httpClient { get; private set; }
		internal bool IsUsingHttpClient { get { return this.httpClient != null; } }

		public ReCaptchaClientSettings() {
			this.reCaptchaKeyStorage = new Dictionary<string, string>();
		}

		public ReCaptchaClientSettings SetVerificationClientSecret(string secret ) {
			this.reCaptchaKeyStorage[KEY_VERIFICATION_SECRET] = secret;
			return this;
		}

		public ReCaptchaClientSettings SetHttpClientReference(HttpClient httpClient ) {
			this.httpClient = httpClient;
			return this;
		}

		internal string GetVerificationClientSecret() {
			return this.reCaptchaKeyStorage[KEY_VERIFICATION_SECRET];
		}
	}
}
