using System;
using System.Collections.Generic;

using System.Net;
using System.Net.Http;
using System.Net.Http.Json;

using IVWZRT_HFT_2022231.Models;

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

        // CRUD
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

        // Player queries
        public IEnumerable<PlayerRankInfo> PlayersKD(string rank, string endpoint)
        {
            IEnumerable<PlayerRankInfo> playerInfos = null;
            HttpResponseMessage response = _client.GetAsync(endpoint + "/PlayersWithGreaterKD/" + rank).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                playerInfos = response.Content.ReadAsAsync<IEnumerable<PlayerRankInfo>>().GetAwaiter().GetResult();
            }
            else
            {
                var error = response.Content.ReadAsAsync<RestExceptionInfo>().GetAwaiter().GetResult();
                throw new ArgumentException(error.Msg);
            }
            return playerInfos;
        }
        public int TopThree(string username, string endpoint)
        {
            int times = 0;
            HttpResponseMessage response = _client.GetAsync(endpoint + "/NumTimesTopThree/" + username).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                times = response.Content.ReadAsAsync<int>().GetAwaiter().GetResult();
            }
            else
            {
                var error = response.Content.ReadAsAsync<RestExceptionInfo>().GetAwaiter().GetResult();
                throw new ArgumentException(error.Msg);
            }
            return times;
        }

        // Match queries
        public float AvgLength(string gameMode, string endpoint)
        {
            float avg = -1.0f;
            HttpResponseMessage response = _client.GetAsync(endpoint + "/AvgLengthOfGame/" + gameMode).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                avg = response.Content.ReadAsAsync<float>().GetAwaiter().GetResult();
            }
            else
            {
                var error = response.Content.ReadAsAsync<RestExceptionInfo>().GetAwaiter().GetResult();
                throw new ArgumentException(error.Msg);
            }
            return avg;
        }
        public IEnumerable<string> MostRamparts(string endpoint)
        {
            IEnumerable<string> maps = null;
            HttpResponseMessage response = _client.GetAsync(endpoint + "/MapsWithMostRamparts").GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                maps = response.Content.ReadAsAsync<IEnumerable<string>>().GetAwaiter().GetResult();
            }
            else
            {
                var error = response.Content.ReadAsAsync<RestExceptionInfo>().GetAwaiter().GetResult();
                throw new ArgumentException(error.Msg);
            }
            return maps;
        }
        public IEnumerable<Match> LongestMatches(string endpoint)
        {
            IEnumerable<Match> matches = null;
            HttpResponseMessage response = _client.GetAsync(endpoint + "/LongestMatchesInDiamond").GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                matches = response.Content.ReadAsAsync<IEnumerable<Match>>().GetAwaiter().GetResult();
            }
            else
            {
                var error = response.Content.ReadAsAsync<RestExceptionInfo>().GetAwaiter().GetResult();
                throw new ArgumentException(error.Msg);
            }
            return matches;
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