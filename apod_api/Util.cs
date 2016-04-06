using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;

namespace ApodPcl
{
    static internal class Util
    {
        static async internal Task<Stream> GetHttpResponseStream(Uri url)
        {
            CacheControlHeaderValue cacheHeader = new CacheControlHeaderValue();
            cacheHeader.MaxAge = TimeSpan.FromDays(7);
            cacheHeader.MaxStale = true;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.CacheControl = cacheHeader;
            client.DefaultRequestHeaders.Add("User-Agent", "nasa-apod-pcl/beta (.Net PCL)");

            Stream response = await client.GetStreamAsync(url);

            return response;
        }

        static internal APOD JsonToApod(Stream stream)
        {
            DataContractJsonSerializerSettings settings = new DataContractJsonSerializerSettings();
            settings.DateTimeFormat = new System.Runtime.Serialization.DateTimeFormat("yyyy-MM-dd");
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(APOD), settings);
            return (APOD)json.ReadObject(stream);
        }
        static internal APOD Exception2Apod(Exception e)
        {
            APOD apod = new APOD();
            apod.date = DateTime.Now;
            apod.title = "Error! See APOD.explanation";
            apod.explanation = e.Message;

            return apod;
        }
    }
}
