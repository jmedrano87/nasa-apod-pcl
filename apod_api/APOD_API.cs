using System;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace apod_api
{
    public sealed class APOD_API
    {
        public APOD_API()
        {
            date = DateTime.Today;
        }
        public void sendRequest()
        {
            generateURL();
            WebRequest request = WebRequest.Create(api_url);
            api_response = request.GetResponse(); 
            Stream responseStream = api_response.GetResponseStream();
            sr = new StreamReader(responseStream);

            myAPOD = JsonConvert.DeserializeObject<APOD>(sr.ReadToEnd());
        }
        public APOD_API setDate(DateTime newDate)
        {
            date = newDate;

            return this;
        }
        private void generateURL()
        {
            api_url = api + "?api_key=" + api_key + "&date=" + date.ToString("yyyy-MM-dd");
        }
        private string api_key = "DEMO_KEY";
        private string api = "https://api.nasa.gov/planetary/apod";
        private string api_url;
        private DateTime date;
        private WebResponse api_response;
        private StreamReader sr;
        private APOD myAPOD;
        public APOD apod { get { return myAPOD; } }
    }
}