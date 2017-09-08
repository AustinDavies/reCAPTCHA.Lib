using reCAPTCHA.Lib.DataModels;
using reCAPTCHA.Lib.Types;
using Newtonsoft.Json.Linq;
using System;

namespace reCAPTCHA.Lib.Factories {
	internal class ReCaptchaResponseBuilder {

		public ReCaptchaVerificationResponse BuildVerificationResponse(string jsonResponse ) {
			var token = JToken.Parse( jsonResponse );
			return new ReCaptchaVerificationResponse() {
				Success = (bool)token[ReCaptchaResponseConsts.SUCCESS],
				ChallengeTimeStamp = (DateTime)token[ReCaptchaResponseConsts.CHALLENGE_TIMESTAMP],
				HostName = (string)token[ReCaptchaResponseConsts.HOSTNAME]
			};
		}
	}
}
