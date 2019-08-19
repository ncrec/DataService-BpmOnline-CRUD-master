using System;
using System.IO;
using System.Net;
using System.Data.Services.Client;

namespace DataService_BpmOnline_CRUD
{
    class LoginClass
    {
        private readonly static Uri serverUri = new Uri("http://178.206.68.64:84/0/ServiceModel/EntityDataService.svc/");
        private const string baseUri = "http://192.168.225.200:84/";
        public static CookieContainer AuthCookie = new CookieContainer();
        private const string authServiceUri = baseUri + @"/ServiceModel/AuthService.svc/Login";
        private const string selectQueryUri = baseUri + @"/0/DataService/json/SyncReply/SelectQuery";
        private const string deleteQueryUri = baseUri + @"/0/DataService/json/SyncReply/DeleteQuery";
        private const string updateQueryUri = baseUri + @"/0/DataService/json/SyncReply/UpdateQuery";
        private const string insertQueryUri = baseUri + @"/0/DataService/json/SyncReply/InsertQuery";
        public static bool TryLogin(string userName, string userPassword)
        {
            var authRequest = HttpWebRequest.Create(authServiceUri) as HttpWebRequest;
            authRequest.Method = "POST";
            authRequest.ContentType = "application/json";
            authRequest.CookieContainer = AuthCookie;
            using (var requestStream = authRequest.GetRequestStream())
            {
                using (var writer = new StreamWriter(requestStream))
                {
                    writer.Write(@"{
                    ""UserName"":""" + userName + @""",
                    ""UserPassword"":""" + userPassword + @"""
                    }");
                }
            }
            ResponseStatus status = null;
            using (var response = (HttpWebResponse)authRequest.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    string responseText = reader.ReadToEnd();
                    status = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<ResponseStatus>(responseText);
                }
            }
            if (status != null)
            {
                if (status.Code == 0)
                {
                    return true;
                }
                Console.WriteLine(status.Message);
            }
            return false;

        }
        public static void OnSendingRequestCookie(object sender, SendingRequestEventArgs e)
        {
            var req = e.Request as HttpWebRequest;
            req.CookieContainer = AuthCookie;
            CookieCollection cookieCollection = AuthCookie.GetCookies(serverUri);
            string csrfToken = cookieCollection["BPMCSRF"].Value;
            ((HttpWebRequest)e.Request).Headers.Add("BPMCSRF", csrfToken);
            e.Request = req;
        }
        public static void CreateHttpRequest(out HttpWebRequest request, string collectionName, Guid id, string requestMethod)
        {
            request = (HttpWebRequest)HttpWebRequest.Create(serverUri
                        + collectionName + "(guid'" + id + "')");
            request.Method = requestMethod;
            request.Accept = "application/atom+xml";
            request.ContentType = "application/atom+xml;type=entry";
            request.CookieContainer = AuthCookie;
            CookieCollection cookieCollection = AuthCookie.GetCookies(serverUri);
            string csrfToken = cookieCollection["BPMCSRF"].Value;
            request.Headers.Add("BPMCSRF", csrfToken);
        }
        public static void CreateAndSendHttpRequestDataService(out HttpWebRequest request, Configuration.RequestType type)
        {
            string requestUri=String.Empty;
            switch(type)
            {
           
                case Configuration.RequestType.Insert:
                    requestUri = insertQueryUri;
                    break;
                case Configuration.RequestType.Update:
                    requestUri = updateQueryUri;
                    break;
                case Configuration.RequestType.Delete:
                    requestUri = deleteQueryUri;
                    break;
                case Configuration.RequestType.Select:
                    requestUri = selectQueryUri;
                    break;
                default:
                    break;
            }
            request = HttpWebRequest.Create(requestUri) as HttpWebRequest;
            request.Method = ReqestMethod.Post;
            request.ContentType = "application/json";
            request.CookieContainer = LoginClass.AuthCookie;
            CookieCollection cookieCollection = LoginClass.AuthCookie.GetCookies(new Uri(requestUri));
            string csrfToken = cookieCollection["BPMCSRF"].Value;
            request.Headers.Add("BPMCSRF", csrfToken);

        }
    }
}
