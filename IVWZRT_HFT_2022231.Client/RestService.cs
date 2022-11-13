using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace IVWZRT_HFT_2022231.Client
{
    public class RestService
    {
        public RestService(string baseurl, string pingableEndpoint)
        {
            bool isOk = false;
            do
            {
                isOk = Ping(baseurl + pingableEndpoint);
            } while (isOk == false);
            Init(baseurl);
        }

        public List<T> Get<T>(string endpoint)
        {
            List<T> items = new List<T>();
            HttpResponseMessage response = _client.GetAsync(endpoint).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                items = response.Content.ReadAsAsync<List<T>>().GetAwaiter().GetResult();
            }
            else
            {
                var error = response.Content.ReadAsAsync<RestExceptionInfo>().GetAwaiter().GetResult();
                throw new ArgumentException(error.Msg);
            }
            return items;
        }
        public T GetSingle<T>(string endpoint)
        {
            T item = default(T);
            HttpResponseMessage response = _client.GetAsync(endpoint).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                item = response.Content.ReadAsAsync<T>().GetAwaiter().GetResult();
            }
            else
            {
                var error = response.Content.ReadAsAsync<RestExceptionInfo>().GetAwaiter().GetResult();
                throw new ArgumentException(error.Msg);
            }
            return item;
        }
        public T Get<T>(int id, string endpoint)
        {
            T item = default(T);
            HttpResponseMessage response = _client.GetAsync(endpoint + "/" + id.ToString()).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                item = response.Content.ReadAsAsync<T>().GetAwaiter().GetResult();
            }
            else
            {
                var error = response.Content.ReadAsAsync<RestExceptionInfo>().GetAwaiter().GetResult();
                throw new ArgumentException(error.Msg);
            }
            return item;
        }
        public void Post<T>(T item, string endpoint)
        {
            HttpResponseMessage response =
                _client.PostAsJsonAsync(endpoint, item).GetAwaiter().GetResult();

            if (!response.IsSuccessStatusCode)
            {
                var error = response.Content.ReadAsAsync<RestExceptionInfo>().GetAwaiter().GetResult();
                throw new ArgumentException(error.Msg);
            }
            response.EnsureSuccessStatusCode();
        }
        public void Delete(int id, string endpoint)
        {
            HttpResponseMessage response =
                _client.DeleteAsync(endpoint + "/" + id.ToString()).GetAwaiter().GetResult();

            if (!response.IsSuccessStatusCode)
            {
                var error = response.Content.ReadAsAsync<RestExceptionInfo>().GetAwaiter().GetResult();
                throw new ArgumentException(error.Msg);
            }

            response.EnsureSuccessStatusCode();
        }
        public void Put<T>(T item, string endpoint)
        {
            HttpResponseMessage response =
                _client.PutAsJsonAsync(endpoint, item).GetAwaiter().GetResult();

            if (!response.IsSuccessStatusCode)
            {
                var error = response.Content.ReadAsAsync<RestExceptionInfo>().GetAwaiter().GetResult();
                throw new ArgumentException(error.Msg);
            }

            response.EnsureSuccessStatusCode();
        }

        private void Init(string baseurl)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(baseurl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue
                ("application/json"));
            try
            {
                _client.GetAsync("").GetAwaiter().GetResult();
            }
            catch (HttpRequestException)
            {
                throw new ArgumentException("Endpoint is not available!");
            }

        }
        private bool Ping(string url)
        {
            try
            {
                WebClient wc = new WebClient();
                wc.DownloadData(url);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private HttpClient _client;
    }
    public class RestExceptionInfo
    {
        public RestExceptionInfo()
        {

        }

        public string Msg { get; set; }
    }
}