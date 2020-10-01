using Microsoft.PowerBI.Api;
using Microsoft.PowerBI.Api.Models;
using Microsoft.Rest;
using Newtonsoft.Json;
using PowerBIEmbed.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;

namespace PowerBIEmbed.Controllers
{
    public class HomeController : Controller
    {
        private static string token = string.Empty;
        private static HttpClient _httpClient;

        private static readonly string _azureAuthUrl = "https://login.windows.net/";
        private static readonly string _grantType = "password";
        private static readonly string _scope = "openid";
        private static readonly string _resource = "https://analysis.windows.net/powerbi/api";
        private static readonly string _clientId = "use client id";
        private static readonly string _clientSecret = "use key secret";
        private static readonly string _userName = "email azure";
        private static readonly string _password = "password";

        /*
         * ACESSO AO GET TOKEN 
         */
        private async Task<MicrosoftAuthResponse> GetTokenPowerBi()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_azureAuthUrl);
            var request = new HttpRequestMessage(HttpMethod.Post, "/common/oauth2/token");

            var keyValues = new List<KeyValuePair<string, string>>();
            keyValues.Add(new KeyValuePair<string, string>("grant_type", _grantType));
            keyValues.Add(new KeyValuePair<string, string>("scope", _scope));
            keyValues.Add(new KeyValuePair<string, string>("resource", _resource));
            keyValues.Add(new KeyValuePair<string, string>("client_id", _clientId));
            keyValues.Add(new KeyValuePair<string, string>("client_secret", _clientSecret));
            keyValues.Add(new KeyValuePair<string, string>("username", _userName));
            keyValues.Add(new KeyValuePair<string, string>("password", _password));


            request.Content = new FormUrlEncodedContent(keyValues);
            var response = await _httpClient.SendAsync(request);

            var responseString = await response.Content.ReadAsStringAsync();

            var authResponse = JsonConvert.DeserializeObject<MicrosoftAuthResponse>(responseString);

            return authResponse;

        }
        public async Task<ActionResult> AccessTokenPowerBI()
        {
            PowerBiReport report = null;
            var responseAcessToken = await GetTokenPowerBi();
            
            // Step 2: Get Report details
            // Pass on the access token received from previous step
            _httpClient = new HttpClient();
            //_httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {responseAcessToken.access_token}");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", responseAcessToken.access_token);

            var reportsRequest = await _httpClient.GetAsync("https://api.powerbi.com/v1.0/myorg/reports");
            if(reportsRequest.IsSuccessStatusCode)
            {
                var reportsResult = JsonConvert.DeserializeObject<PBIReportsResponse>(await reportsRequest.Content.ReadAsStringAsync());
                report = reportsResult.value.FirstOrDefault(r => r.Id.Equals("use the id report embeded to power BI"));                
            }
            else
            {

            }
            return Json(new
            {
                accessToken = responseAcessToken.access_token,
                reportEmbedUrl = report.EmbedUrl,
                reportId = report.Id
            }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Index()
        {
            
            return View();
        }

     
    }
}