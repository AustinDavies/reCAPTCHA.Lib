using System;

namespace reCAPTCHA.Lib.DataModels {
	public class ReCaptchaVerificationResponse {
		public bool Success { get; set; }
		public DateTime ChallengeTimeStamp { get; set; }
		public string HostName { get; set; }

		public ReCaptchaVerificationResponse() { }

		public bool IsValid(string hostOrigin ) {
			bool isDateValid = DateTime.UtcNow >= ChallengeTimeStamp;
			return (HostName == hostOrigin && Success && isDateValid);
		}
	}
}
