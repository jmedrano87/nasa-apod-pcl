using System;
using System.IO;
using System.Threading.Tasks;

namespace ApodPcl
{
    /// <summary>
    /// This class handles communicating with NASA's API to retreive details
    /// about the current Astronomy Picture of the Day.
    /// </summary>
    public sealed class API
    {
        /// <summary>
        /// Initializes an instance of <see cref="API"/>.
        /// <para>
        /// <see cref="API_key"/> is set to <paramref name="key"/>
        /// and <see cref="Date"/> is set to <see cref="DateTime.Today"/>.
        /// </para>
        /// </summary>
        /// <param name="key">The api key to use when communicating with
        /// NASA's API.</param>
        public API(string key)
        {
            date = DateTime.Today;
            api_key = key;
        }
        /// <summary>
        /// Initializes an instance of <see cref="API"/>.
        /// <para>
        /// <see cref="API_key"/> is set to <paramref name="key"/>
        /// and <see cref="Date"/> is set to <paramref name="date"/>.
        /// </para>
        /// </summary>
        /// <param name="key">The api key to use when communicating with
        /// NASA's API.</param>
        /// <param name="date">The date to request the APOD for.
        /// <see cref="Date"/> is set to this value.</param>
        public API(DateTime date, string key)
        {
            Date = date;
            api_key = key;
        }
        /// <summary>
        /// Sends a request to NASA's API for the day <see cref="Date"/>
        /// and using the api key <see cref="API_key"/>.
        /// <para>
        /// The response is used to populate the fields of <see cref="apod"/>.
        /// </para>
        /// </summary>
        public async Task sendRequest()
        {
            generateURL();
            try
            {
                Stream responseStream = await Util.GetHttpResponseStream(new Uri(api_url));
                myAPOD = Util.JsonToApod(responseStream);
            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                myAPOD = Util.Exception2Apod(ex);
            }
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
        /// <summary>
        /// Forms the url to use for for requests to NASA's API.
        /// </summary>
        private void generateURL()
        {
            string  api = "https://api.nasa.gov/planetary/apod";
            api_url = api + "?api_key=" + api_key + "&date=" + date.ToString("yyyy-MM-dd");
        }
        /// <summary>
        /// The date to request the Astronomy Picture of the Day for.
        /// <para>Valid dates are between 1995-06-16 and today.</para>
        /// </summary>
        public DateTime Date
        {
            set
            {
                DateTime min = new DateTime(1995, 06, 16);
                date = (value > DateTime.Today) ? DateTime.Today : ((value < min) ? min : value);
            }
        }
        /// <summary>
        /// The api key to supply to NASA's API when sending requests.
        /// <para>Obtain a key from http://api.nasa.gov. </para>
        /// </summary>
        public string API_key { set { api_key = value; } }
        private string api_key;
        private string api_url;
        private DateTime date;
        private APOD myAPOD;
        /// <summary>
        /// An object for holding data about the returned Astrononmy Picture of the Day.
        /// </summary>
        public APOD Apod { get { return myAPOD; } }
    }
}