using System;
using System.Threading.Tasks;
using System.Net;

namespace ApodPcl
{
    public sealed class API
    {
        public API(string key)
        {
            date = DateTime.Today;
            api_key = key;
        }
        public API(DateTime date, string key)
        {
            Date = date;
            api_key = key;
        }
        public async Task sendRequest()
        {
            generateURL();
            HttpWebRequest request = Util.CreateRequest(new Uri(api_url));
            getResponseTask = request.GetResponseAsync();
            WebResponse responseContent = await getResponseTask;
            myAPOD =  Util.JsonToApod(responseContent.GetResponseStream());

            responseContent.Dispose();
        }
        private void generateURL()
        {
            string  api = "https://api.nasa.gov/planetary/apod";
            api_url = api + "?api_key=" + api_key + "&date=" + date.ToString("yyyy-MM-dd");
        }
        public DateTime Date
        {
            set
            {
                DateTime min = new DateTime(1995, 06, 16);
                date = (value > DateTime.Today) ? DateTime.Today : ((value < min) ? min : value);
            }
        }
        public string API_key { set { api_key = value; } }
        private string api_key;
        private string api_url;
        private DateTime date;
        private Task<WebResponse> getResponseTask;
        private APOD myAPOD;
        public APOD Apod{ get { return myAPOD; } }
    }
}