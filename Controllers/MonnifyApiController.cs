using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MonnifyApp.LogService;
using System.Net.Http.Headers;
using System.Net;
using MonnifyApp.Models;
using System.Text;
using Newtonsoft.Json;

namespace MonnifyApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonnifyApiController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;
        private readonly ILogWriter _logger;


        public MonnifyApiController(IHttpClientFactory clientFactory, IConfiguration config, ILogWriter logger)
        {
            _clientFactory = clientFactory;
            _config = config;
            _logger = logger;
        }

        [HttpPost]
        [Route("api/MonnifyApp/PostInitializeTransaction")]
        public async Task<object> PostInitializeTransaction([FromBody] InitializeTransct it)
        {
            var baseurl = _config.GetSection("BaseAddress").Value.ToString();
            var InitTr = _config.GetSection("PostInitiateTransaction").Value.ToString();
            var Auth = _config.GetSection("token").Value;
            InitializeTransct innt = it;
            object lookupInfo = new object();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + Auth);
                client.Timeout = new System.TimeSpan(0, 0, 1, 0);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //var resp = await client.PostAsync("v1/merchant/transactions/init-transaction" , innt);
                //HttpResponseMessage resp = await client.PostAsync(InitTr + it, new StringContent("",Encoding.UTF8, "application/json"));
                //var InitiateTransactionResp = await resp.Content.ReadAsStringAsync();


                var stringContent = new StringContent(JsonConvert.SerializeObject(it), Encoding.UTF8, "application/json");
                var resp = await client.PostAsync(InitTr, stringContent);
                var InitiateTransactionResp = await resp.Content.ReadAsStringAsync();


                try
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        _logger.LogWrite(baseurl + Environment.NewLine +
                           "REQUEST URL ENDPOINT :" + (InitTr) + Environment.NewLine +
                           "REQUEST :" + ("Method Call: api/MonnifyApp/PostInitializeTransaction") + Environment.NewLine +
                             client.Timeout + DateTime.Now, "Request");

                        _logger.LogWrite(baseurl + Environment.NewLine +
                            "REQUEST URL ENDPOINT :" + (InitTr) + Environment.NewLine +
                              "REQUEST :" + ("Method Call: api/BuyPower/GetAddressLookup") + Environment.NewLine +
                              "RESPONSE :" + InitiateTransactionResp + Environment.NewLine + client.Timeout + DateTime.Now, "Response");
                    }
                    else
                    {
                        _logger.LogWrite(baseurl + Environment.NewLine +
                           "REQUEST URL ENDPOINT :" + (InitTr) + Environment.NewLine +
                             "REQUEST :" + ("Method Call: api/BuyPower/GetAddressLookup") + Environment.NewLine +
                             "RESPONSE :" + InitiateTransactionResp + Environment.NewLine + client.Timeout + DateTime.Now, "Error");
                        return InitiateTransactionResp;
                    }
                }
                catch (WebException e)
                {
                    if (e.Status == WebExceptionStatus.ProtocolError)
                    {

                        HttpWebResponse response = (System.Net.HttpWebResponse)e.Response;
                        if (response.StatusCode == HttpStatusCode.NotFound)
                            return null;
                        if (response.StatusCode == HttpStatusCode.Unauthorized)
                            return null;

                        if (response.StatusCode == HttpStatusCode.Forbidden)
                            return null;

                        if (response.StatusCode == HttpStatusCode.BadRequest)
                            return null;
                        else
                            return null;
                    }
                    else
                    {
                        return null;
                    }
                }
                return InitiateTransactionResp;
            }
        }


        [HttpPost]
        [Route("api/MonnifyApp/PostPayWithBank")]
        public async Task<object> PostPayWithBank([FromBody] PayWithBnk pwb)
        {
            var baseurl = _config.GetSection("BaseAddress").Value.ToString();
            var PayBnk = _config.GetSection("PostPayBank").Value.ToString();
            var Auth = _config.GetSection("token").Value;
            PayWithBnk pnnb = pwb;


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + Auth);
                client.Timeout = new System.TimeSpan(0, 0, 1, 0);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var stringContent = new StringContent(JsonConvert.SerializeObject(pwb), Encoding.UTF8, "application/json");
                var resp = await client.PostAsync(PayBnk, stringContent);
                var PayWithBankResp = await resp.Content.ReadAsStringAsync();

                try
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        _logger.LogWrite(baseurl + Environment.NewLine +
                           "REQUEST URL ENDPOINT :" + (PayBnk) + Environment.NewLine +
                           "REQUEST :" + ("Method Call: api/MonnifyApp/PostInitializeTransaction") + Environment.NewLine +
                             client.Timeout + DateTime.Now, "Request");

                        _logger.LogWrite(baseurl + Environment.NewLine +
                            "REQUEST URL ENDPOINT :" + (PayBnk) + Environment.NewLine +
                              "REQUEST :" + ("Method Call: api/BuyPower/GetAddressLookup") + Environment.NewLine +
                              "RESPONSE :" + PayWithBankResp + Environment.NewLine + client.Timeout + DateTime.Now, "Response");
                    }
                    else
                    {
                        _logger.LogWrite(baseurl + Environment.NewLine +
                           "REQUEST URL ENDPOINT :" + (PayBnk) + Environment.NewLine +
                             "REQUEST :" + ("Method Call: api/BuyPower/GetAddressLookup") + Environment.NewLine +
                             "RESPONSE :" + PayWithBankResp + Environment.NewLine + client.Timeout + DateTime.Now, "Error");
                        return PayWithBankResp;
                    }
                }
                catch (WebException e)
                {
                    if (e.Status == WebExceptionStatus.ProtocolError)
                    {

                        HttpWebResponse response = (System.Net.HttpWebResponse)e.Response;
                        if (response.StatusCode == HttpStatusCode.NotFound)
                            return null;
                        if (response.StatusCode == HttpStatusCode.Unauthorized)
                            return null;

                        if (response.StatusCode == HttpStatusCode.Forbidden)
                            return null;

                        if (response.StatusCode == HttpStatusCode.BadRequest)
                            return null;
                        else
                            return null;
                    }
                    else
                    {
                        return null;
                    }
                }
                return PayWithBankResp;
            }
        }



        [HttpPost]
        [Route("api/MonnifyApp/PostPayWithCardNoOTP")]
        public async Task<object> PostPayWithCardNoOTP([FromBody] PayWithCardNoOTP pcn)
        {
            var baseurl = _config.GetSection("BaseAddress").Value.ToString();
            var PayWc = _config.GetSection("PayWithCardNoOTP").Value.ToString();
            var Auth = _config.GetSection("token").Value;
            PayWithCardNoOTP pnnb = pcn;


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + Auth);
                client.Timeout = new System.TimeSpan(0, 0, 1, 0);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var stringContent = new StringContent(JsonConvert.SerializeObject(pcn), Encoding.UTF8, "application/json");
                var resp = await client.PostAsync(PayWc, stringContent);
                var PayWithCardNoOTPResp = await resp.Content.ReadAsStringAsync();

                try
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        _logger.LogWrite(baseurl + Environment.NewLine +
                           "REQUEST URL ENDPOINT :" + (PayWc) + Environment.NewLine +
                           "REQUEST :" + ("Method Call: api/MonnifyApp/PostInitializeTransaction") + Environment.NewLine +
                             client.Timeout + DateTime.Now, "Request");

                        _logger.LogWrite(baseurl + Environment.NewLine +
                            "REQUEST URL ENDPOINT :" + (PayWc) + Environment.NewLine +
                              "REQUEST :" + ("Method Call: api/BuyPower/GetAddressLookup") + Environment.NewLine +
                              "RESPONSE :" + PayWithCardNoOTPResp + Environment.NewLine + client.Timeout + DateTime.Now, "Response");
                    }
                    else
                    {
                        _logger.LogWrite(baseurl + Environment.NewLine +
                           "REQUEST URL ENDPOINT :" + (PayWc) + Environment.NewLine +
                             "REQUEST :" + ("Method Call: api/BuyPower/GetAddressLookup") + Environment.NewLine +
                             "RESPONSE :" + PayWithCardNoOTPResp + Environment.NewLine + client.Timeout + DateTime.Now, "Error");
                        return PayWithCardNoOTPResp;
                    }
                }
                catch (WebException e)
                {
                    if (e.Status == WebExceptionStatus.ProtocolError)
                    {

                        HttpWebResponse response = (System.Net.HttpWebResponse)e.Response;
                        if (response.StatusCode == HttpStatusCode.NotFound)
                            return null;
                        if (response.StatusCode == HttpStatusCode.Unauthorized)
                            return null;

                        if (response.StatusCode == HttpStatusCode.Forbidden)
                            return null;

                        if (response.StatusCode == HttpStatusCode.BadRequest)
                            return null;
                        else
                            return null;
                    }
                    else
                    {
                        return null;
                    }
                }
                return PayWithCardNoOTPResp;
            }
        }



        [HttpPost]
        [Route("api/MonnifyApp/PostAuthorizeOTP")]
        public async Task<object> PostAuthorizeOTP([FromBody] AuthorizeOTP atp)
        {
            var baseurl = _config.GetSection("BaseAddress").Value.ToString();
            var AutOtp = _config.GetSection("AuthorizeOTP").Value.ToString();
            var Auth = _config.GetSection("token").Value;
            AuthorizeOTP pnnb = atp;


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + Auth);
                client.Timeout = new System.TimeSpan(0, 0, 1, 0);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var stringContent = new StringContent(JsonConvert.SerializeObject(atp), Encoding.UTF8, "application/json");
                var resp = await client.PostAsync(AutOtp, stringContent);
                var AuthorizeOTPResp = await resp.Content.ReadAsStringAsync();



                try
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        _logger.LogWrite(baseurl + Environment.NewLine +
                           "REQUEST URL ENDPOINT :" + (AutOtp) + Environment.NewLine +
                           "REQUEST :" + ("Method Call: api/MonnifyApp/PostInitializeTransaction") + Environment.NewLine +
                             client.Timeout + DateTime.Now, "Request");

                        _logger.LogWrite(baseurl + Environment.NewLine +
                            "REQUEST URL ENDPOINT :" + (AutOtp) + Environment.NewLine +
                              "REQUEST :" + ("Method Call: api/BuyPower/GetAddressLookup") + Environment.NewLine +
                              "RESPONSE :" + AuthorizeOTPResp + Environment.NewLine + client.Timeout + DateTime.Now, "Response");
                    }
                    else
                    {
                        _logger.LogWrite(baseurl + Environment.NewLine +
                           "REQUEST URL ENDPOINT :" + (AutOtp) + Environment.NewLine +
                             "REQUEST :" + ("Method Call: api/BuyPower/GetAddressLookup") + Environment.NewLine +
                             "RESPONSE :" + AuthorizeOTPResp + Environment.NewLine + client.Timeout + DateTime.Now, "Error");
                        return AuthorizeOTPResp;
                    }
                }
                catch (WebException e)
                {
                    if (e.Status == WebExceptionStatus.ProtocolError)
                    {

                        HttpWebResponse response = (System.Net.HttpWebResponse)e.Response;
                        if (response.StatusCode == HttpStatusCode.NotFound)
                            return null;
                        if (response.StatusCode == HttpStatusCode.Unauthorized)
                            return null;

                        if (response.StatusCode == HttpStatusCode.Forbidden)
                            return null;

                        if (response.StatusCode == HttpStatusCode.BadRequest)
                            return null;
                        else
                            return null;
                    }
                    else
                    {
                        return null;
                    }
                }
                return AuthorizeOTPResp;
            }
        }


        [HttpPost]
        [Route("api/MonnifyApp/Post3DSsecureAuthentication")]
        public async Task<object> Post3DSsecureAuthentication([FromBody] Secure3DSAuth atp)
        {
            var baseurl = _config.GetSection("BaseAddress").Value.ToString();
            var Sec3D = _config.GetSection("Secure3DSAuth").Value.ToString();
            var Auth = _config.GetSection("token").Value;
            Secure3DSAuth pnnb = atp;


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + Auth);
                client.Timeout = new System.TimeSpan(0, 0, 1, 0);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var stringContent = new StringContent(JsonConvert.SerializeObject(atp), Encoding.UTF8, "application/json");
                var resp = await client.PostAsync(Sec3D, stringContent);
                var Secure3DSAuthResp = await resp.Content.ReadAsStringAsync();


                try
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        _logger.LogWrite(baseurl + Environment.NewLine +
                           "REQUEST URL ENDPOINT :" + (Sec3D) + Environment.NewLine +
                           "REQUEST :" + ("Method Call: api/MonnifyApp/PostInitializeTransaction") + Environment.NewLine +
                             client.Timeout + DateTime.Now, "Request");

                        _logger.LogWrite(baseurl + Environment.NewLine +
                            "REQUEST URL ENDPOINT :" + (Sec3D) + Environment.NewLine +
                              "REQUEST :" + ("Method Call: api/BuyPower/GetAddressLookup") + Environment.NewLine +
                              "RESPONSE :" + Secure3DSAuthResp + Environment.NewLine + client.Timeout + DateTime.Now, "Response");
                    }
                    else
                    {
                        _logger.LogWrite(baseurl + Environment.NewLine +
                           "REQUEST URL ENDPOINT :" + (Sec3D) + Environment.NewLine +
                             "REQUEST :" + ("Method Call: api/BuyPower/GetAddressLookup") + Environment.NewLine +
                             "RESPONSE :" + Secure3DSAuthResp + Environment.NewLine + client.Timeout + DateTime.Now, "Error");
                        return Secure3DSAuthResp;
                    }
                }
                catch (WebException e)
                {
                    if (e.Status == WebExceptionStatus.ProtocolError)
                    {

                        HttpWebResponse response = (System.Net.HttpWebResponse)e.Response;
                        if (response.StatusCode == HttpStatusCode.NotFound)
                            return null;
                        if (response.StatusCode == HttpStatusCode.Unauthorized)
                            return null;

                        if (response.StatusCode == HttpStatusCode.Forbidden)
                            return null;

                        if (response.StatusCode == HttpStatusCode.BadRequest)
                            return null;
                        else
                            return null;
                    }
                    else
                    {
                        return null;
                    }
                }
                return Secure3DSAuthResp;
            }


        }




    }
}
