using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization.Json;

namespace ApodPcl
{
    static internal class Util
    {
        static public void SetHeader(HttpWebRequest request, string header, string value)
        {
            PropertyInfo propInfo = request.GetType().GetRuntimeProperty(header.Replace("-", string.Empty));

            if (propInfo != null)
                propInfo.SetValue(request, value, null);
            else
                request.Headers[header] = value;
        }
        static public HttpWebRequest CreateRequest(Uri url)
        {
            HttpWebRequest req = HttpWebRequest.CreateHttp(url);
            SetHeader(req, "User-Agent", "nasa-apod-pcl/beta (.Net PCL)");

            return req;
        }

        static public APOD JsonToApod(Stream stream)
        {
            DataContractJsonSerializerSettings settings = new DataContractJsonSerializerSettings();
            settings.DateTimeFormat = new System.Runtime.Serialization.DateTimeFormat("yyyy-MM-dd");
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(APOD), settings);
            return (APOD)json.ReadObject(stream);
        }
    }
}
