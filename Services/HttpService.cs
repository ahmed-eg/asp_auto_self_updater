using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace AspAutoSelfUpdater.Services
{
    class HttpService
    {
        public static HttpResponseMessage HttpGet(string url, string Token = null)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromMinutes(10);
                    if (!string.IsNullOrEmpty(Token))
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Token}");
                    }
                    var result = client.GetAsync(url);
                    result.Wait(Convert.ToInt32(TimeSpan.FromMinutes(10).TotalMilliseconds));
                    return result.Result;
                }
            }
            catch //(Exception ex)
            {
                return null;
            }
        }

        public static bool HttpGetDownloadFIle(string url, string fullPath, string Token = null)
        {
            try
            {
                using (var c = new System.Net.WebClient())
                {
                    if (!string.IsNullOrEmpty(Token))
                    {
                        c.Headers.Clear();
                        c.Headers.Add("Authorization", $"Bearer {Token}");
                    }
                    c.DownloadFile(url, fullPath);
                    return true;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static HttpResponseMessage HttpPost(string url, object body, string Token = null)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromMinutes(10);
                    if (string.IsNullOrEmpty(Token))
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Token}");
                    }
                    var content = new StringContent(JsonConvert.SerializeObject(body),
                        System.Text.Encoding.UTF8, "application/json");
                    var result = client.PostAsync(url, content);
                    result.Wait(Convert.ToInt32(TimeSpan.FromMinutes(10).TotalMilliseconds));
                    return result.Result;
                }
            }
            catch //(Exception ex)
            {
                return null;
            }
        }

    }
}
