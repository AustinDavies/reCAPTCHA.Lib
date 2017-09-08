using reCAPTCHA.Lib.DataModels;
using reCAPTCHA.Lib.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace reCAPTCHA.Lib.Clients {

	internal class ReCaptchaVerificationClient {

		public string SendRequest( ReCaptchaVerificationRequest requestObject ) {
			var request = (HttpWebRequest)WebRequest.Create( requestObject.requestURL );
			var postData = $@"secret={requestObject.secretKey}";
			if ( !String.IsNullOrEmpty( requestObject.userResponse ) ) { postData += $@"&response={requestObject.userResponse}"; }
			if ( !String.IsNullOrEmpty( requestObject.userIP ) ) { postData += $@"&remoteip={requestObject.userIP}"; }
			var data = Encoding.ASCII.GetBytes( postData );
			request.Method = "POST";
			request.ContentType = "application/x-www-form-urlencoded";
			request.ContentLength = data.Length;
			using ( var stream = request.GetRequestStream() ) {
				stream.Write( data, 0, data.Length );
			}
			var response = (HttpWebResponse)request.GetResponse();
			string responseString;
			using ( var streamReader = new StreamReader( response.GetResponseStream() ) ) {
				responseString = streamReader.ReadToEnd();
			}
			return responseString;
		}

		public string SendRequest( ReCaptchaVerificationRequest requestObject, HttpClient httpClient ) {
			var parameters = new Dictionary<string, string> { { ReCaptchaRequestConsts.SECRET, requestObject.secretKey } };
			if ( !String.IsNullOrEmpty( requestObject.userResponse ) ) { parameters.Add( ReCaptchaRequestConsts.RESPONSE, requestObject.userResponse ); }
			if ( !String.IsNullOrEmpty( requestObject.userIP ) ) { parameters.Add( ReCaptchaRequestConsts.REMOTE_IP, requestObject.userIP ); }
			var content = new FormUrlEncodedContent( parameters );
			var response = httpClient.PostAsync( requestObject.requestURL, content ).Result;
			var responseString = response.Content.ReadAsStringAsync().Result;
			return responseString;
		}
	}
}
