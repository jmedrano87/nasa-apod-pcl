using System;
using System.Text;
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
            api_url = api + "?api_key=" + api_key;
        }
        public async void sendRequest()
        {
            WebRequest request = WebRequest.Create(api);
            api_response = await request.GetResponseAsync(); 
            Stream responseStream = api_response.GetResponseStream();
            sr = new StreamReader(responseStream);

            myAPOD = JsonConvert.DeserializeObject<APOD>(sr.ReadToEnd());
        }
        private string api_key = "DEMO_KEY";
        private string api = "https://api.nasa.gov/planetary/apod";
        private string api_url;
        private WebResponse api_response;
        private StreamReader sr;
        private APOD myAPOD;
        public APOD apod { get { return myAPOD; } }
    }
}