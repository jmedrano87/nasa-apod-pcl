using System;
using System.IO;
using System.Threading.Tasks;

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
            Stream responseStream = await Util.GetHttpResponseStream(new Uri(api_url));
            myAPOD =  Util.JsonToApod(responseStream);
        }
        public async Task<Uri> GetUri(bool hd = false)
        {
            await sendRequest();

            return (hd && !(myAPOD.media_type == "video")) ? myAPOD.hdurl : myAPOD.url;
        }
        public async Task<Uri> GetUri(DateTime date, bool hd = false)
        {
            Date = date;

            return await GetUri(hd);
        }
        public async Task<Uri> GetPrevUri(bool hd = false)
        {
            return await GetUri(date.AddDays(-1), hd);          
        }
        public async Task<Uri> GetNextUri(bool hd = false)
        {
            return await GetUri(date.AddDays(1), hd);
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
        private APOD myAPOD;
        public APOD Apod{ get { return myAPOD; } }
    }
}