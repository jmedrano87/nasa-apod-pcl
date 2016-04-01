using System;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace apod_api
{
    public sealed class APOD_API
    {
        public APOD_API(string key = "DEMO_KEY")
        {
            date = DateTime.Today;
            api_key = key;
        }
        public APOD_API(DateTime date, string key = "DEMO_KEY")
        {
            this.date = date;
            api_key = key;
        }
        public async Task sendRequest()
        {
            generateURL();
            WebRequest request = WebRequest.Create(api_url);
            getResponseTask = request.GetResponseAsync();
            WebResponse responseContent = await getResponseTask;
            sr = new StreamReader(responseContent.GetResponseStream());
            myAPOD = JsonConvert.DeserializeObject<APOD>(sr.ReadToEnd());

            sr.Dispose();
            responseContent.Dispose();
        }
        private void generateURL()
        {
            string  api = "https://api.nasa.gov/planetary/apod";
            api_url = api + "?api_key=" + api_key + "&date=" + date.ToString("yyyy-MM-dd");
        }
        public string API_key { set { api_key = value; } }
        public DateTime Date { set { date = value; } }
        private string api_key;
        private string api_url;
        private DateTime date;
        private Task<WebResponse> getResponseTask;
        private StreamReader sr;
        private APOD myAPOD;
        public APOD apod { get { return myAPOD; } }
    }
}