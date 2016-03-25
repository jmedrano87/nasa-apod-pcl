using System;

namespace apod_api
{
    class Program
    {
        static void Main(string[] args)
        {
            APOD_API apod_api = new APOD_API();

            apod_api.sendRequest();

            Console.WriteLine("Title: " + apod_api.apod.title);
            Console.WriteLine("Date: " + apod_api.apod.date);
            Console.WriteLine("Url: " + apod_api.apod.url);
        }
    }
}
