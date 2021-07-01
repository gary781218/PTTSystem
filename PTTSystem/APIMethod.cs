using Newtonsoft.Json;
using PTTSystem.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PTTSystem
{
    class APIMethod
    {
        public async Task<T> Get<T>(String uri, Object headers = null)
        {
            PropertyInfo[] headersProps = headers.GetType().GetProperties();

            HttpClient client = new HttpClient();
            if (headersProps.Length > 0)
            {
                foreach (var prop in headersProps)
                {
                    client.DefaultRequestHeaders.Add(prop.Name, prop.GetValue(headers).ToString());
                }
            }

            //使用 async 方法從網路 url 上取得回應
            var response = await client.GetAsync(uri).ConfigureAwait(false);

            //如果 httpstatus code 不是 200 時會直接丟出 expection
            response.EnsureSuccessStatusCode();

            // 將 response 內容 轉為 string
            string result = await response.Content.ReadAsStringAsync();

            T model = JsonConvert.DeserializeObject<T>(result);

            return model;
        }

        public async Task<T> Post<T>(string uri, BodyModels body = null, Object headers = null)
        {
            HttpResponseMessage response;
            HttpContent content = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("", "") });
            PropertyInfo[] headersProps = headers.GetType().GetProperties();

            HttpClient client = new HttpClient();
            if (headersProps.Length > 0)
            {
                foreach (var prop in headersProps)
                {
                    client.DefaultRequestHeaders.Add(prop.Name, prop.GetValue(headers).ToString());
                }
            }

            if (body != null)
            {
                var dataContent = new MultipartFormDataContent();
                Stream fileStream = new FileStream(body.filemodels.filepath, FileMode.Open, FileAccess.Read);
                dataContent.Add(new StreamContent(fileStream), body.filemodels.filetype, body.filemodels.filename);

                foreach (var para in body.parameters)
                {
                    dataContent.Add(new StringContent($"{para.Value}"), $"{para.Key}");
                }
                response = await client.PostAsync(uri, dataContent).ConfigureAwait(false);
            }
            else
            {
                response = await client.PostAsync(uri, content).ConfigureAwait(false);
            }

            //如果 httpstatus code 不是 200 時會直接丟出 expection
            response.EnsureSuccessStatusCode();
            string result = await response.Content.ReadAsStringAsync();

            T model = JsonConvert.DeserializeObject<T>(result);

            return model;
        }

        public async void Delete(string uri, Object headers)
        {
            PropertyInfo[] headersProps = headers.GetType().GetProperties();

            HttpClient client = new HttpClient();
            if (headersProps.Length > 0)
            {
                foreach (var prop in headersProps)
                {
                    client.DefaultRequestHeaders.Add(prop.Name, prop.GetValue(headers).ToString());
                }
            }

            var response = await client.DeleteAsync(uri);

            //如果 httpstatus code 不是 200 時會直接丟出 expection
            response.EnsureSuccessStatusCode();
            string result = await response.Content.ReadAsStringAsync();
        }



        public T upload<T>(string filePath)
        {
            //建立 WebRequest 並指定目標的 uri
            WebRequest request = WebRequest.Create("https://api.imgur.com/3/image");
            //WebRequest request = WebRequest.Create("https://api.imgur.com/3/upload");
            request.Headers.Add(HttpRequestHeader.Authorization, "Bearer 5d14027e993fac88b5e46b0658ec947113979daa");
            request.Method = "POST";
            object postData = new { image = File.ReadAllBytes(filePath) };
            //將需 post 的資料內容轉為 stream 
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(postData);
                streamWriter.Write(json);
                streamWriter.Flush();
            }
            request.ContentType = "application/json; charset=utf-8";
            T model;
            using (var httpResponse = (HttpWebResponse)request.GetResponse())
            //使用 GetResponseStream 方法從 server 回應中取得資料，stream 必需被關閉
            //使用 stream.close 就可以直接關閉 WebResponse 及 stream，但同時使用 using 或是關閉兩者並不會造成錯誤，養成習慣遇到其他情境時就比較不會出錯
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                model = JsonConvert.DeserializeObject<T>(result);
                Console.WriteLine(result);
            }
            return model;
        }
    }
}
