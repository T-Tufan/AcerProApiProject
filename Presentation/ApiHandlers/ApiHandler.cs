using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace Presentation.ApiHandlers
{
    public class ApiHandler:IApiHandler
    {
        private readonly IHttpContextAccessor _accessor;

        public ApiHandler(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string GetToken()
        {
            var securityToken = _accessor.HttpContext.Request.Cookies["security-token"];
            if (securityToken is null)
            {
                return "";
            }
            return securityToken;
        }

        public T GetAPI<T>(string url)
        {
            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.ContentType = "application/json";
                httpRequest.Method = "GET";
                //httpRequest.Headers.Add("Authorization", "Bearer " + token);
                var response = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var model = JsonConvert.DeserializeObject<T>(result);
                    response.Close();
                    return model;
                }
            }
            catch (Exception ex)
            {
                string str = "";
                var model = JsonConvert.DeserializeObject<T>(str);
                return model;
            }
        }
        public string PostApiString(dynamic dynamicModel, string Url)
        {
            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(Url);
                httpRequest.Method = "POST";
                httpRequest.ContentType = "application/json";
                string JsonData = JsonConvert.SerializeObject(dynamicModel);
                byte[] byteArray = Encoding.UTF8.GetBytes(JsonData);
                httpRequest.ContentLength = byteArray.Length;
                Stream dataStream = httpRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                var response = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    response.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                throw new NotImplementedException();
            }
        }
        public T PostAPIWithModel<T>(dynamic dynamicModel, string url)
        {
            var Token = GetToken();
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.ContentType = "application/json";
            httpRequest.Method = "POST";
            string JsonData = JsonConvert.SerializeObject(dynamicModel);
            byte[] byteArray = Encoding.UTF8.GetBytes(JsonData);
            httpRequest.ContentLength = byteArray.Length;
            httpRequest.Headers.Add("Authorization", "Bearer " + Token);
            Stream dataStream = httpRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            var response = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                T model = JsonConvert.DeserializeObject<T>(result);
                response.Close();
                return model;
            }
        }

        public string PostAPIWithTokenModel<T>(dynamic dynamicModel, string url/*, string Token*/)
        {
            var Token = GetToken();
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.ContentType = "application/json";
            httpRequest.Method = "POST";
            string JsonData = JsonConvert.SerializeObject(dynamicModel);
            byte[] byteArray = Encoding.UTF8.GetBytes(JsonData);
            httpRequest.ContentLength = byteArray.Length;
            httpRequest.Headers.Add("Authorization", "Bearer " + Token);
            Stream dataStream = httpRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            var response = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                //T model = JsonConvert.DeserializeObject<T>(result);
                response.Close();
                return result;
            }
        }


        public string PostAPITokenWithModel(dynamic dynamicModel, string url)
        {

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.ContentType = "application/json";
            httpRequest.Method = "POST";
            string JsonData = JsonConvert.SerializeObject(dynamicModel);
            byte[] byteArray = Encoding.UTF8.GetBytes(JsonData);
            httpRequest.ContentLength = byteArray.Length;
            Stream dataStream = httpRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    //var  model = JsonConvert.DeserializeObject(result);
                    response.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {

                return "400";
            }


        }
    }
}
