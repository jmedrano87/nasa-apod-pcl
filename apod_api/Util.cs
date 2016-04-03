﻿using System;
using System.IO;
using System.Net;
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
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "nasa-apod-pcl/beta (.Net PCL)");
            Stream response = await client.GetStreamAsync(url);

            return response;
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