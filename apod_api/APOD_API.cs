using System;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace apod_api
{
    /// <summary>
    /// This class handles communicating with NASA's API to retreive details
    /// about the current Astronomy Picture of the Day.
    /// </summary>
    public sealed class APOD_API
    {
        /// <summary>
        /// Initializes an instance of <see cref="APOD_API"/>.
        /// <para>
        /// <see cref="API_key"/> is set to <paramref name="key"/>
        /// and <see cref="Date"/> is set to <see cref="DateTime.Today"/>.
        /// </para>
        /// </summary>
        /// <param name="key">The api key to use when communicating with
        /// NASA's API.</param>
        public APOD_API(string key)
        {
            date = DateTime.Today;
            api_key = key;
        }
        /// <summary>
        /// Initializes an instance of <see cref="APOD_API"/>.
        /// <para>
        /// <see cref="API_key"/> is set to <paramref name="key"/>
        /// and <see cref="Date"/> is set to <paramref name="date"/>.
        /// </para>
        /// </summary>
        /// <param name="key">The api key to use when communicating with
        /// NASA's API.</param>
        /// <param name="date">The date to request the APOD for.
        /// <see cref="Date"/> is set to this value.</param>
        public APOD_API(DateTime date, string key)
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
            WebRequest request = WebRequest.Create(api_url);
            getResponseTask = request.GetResponseAsync();
            WebResponse responseContent = await getResponseTask;
            sr = new StreamReader(responseContent.GetResponseStream());
            myAPOD = JsonConvert.DeserializeObject<APOD>(sr.ReadToEnd());

            sr.Dispose();
            responseContent.Dispose();
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
        private Task<WebResponse> getResponseTask;
        private StreamReader sr;
        private APOD myAPOD;
        /// <summary>
        /// An object for holding data about the returned Astrononmy Picture of the Day.
        /// </summary>
        public APOD apod { get { return myAPOD; } }
    }
}