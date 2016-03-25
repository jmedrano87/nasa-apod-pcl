using System;
using System.Net;

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

            int action;
            Console.WriteLine("[1] Standard");
            Console.WriteLine("[2] HD");
            Console.WriteLine("[0] Don't download.");
            Console.Write("Download? ");

            action = int.Parse(Console.ReadLine());
            WebClient dl = new WebClient();

            switch (action)
            {
                case 0:
                    Console.WriteLine("Photo will not be downloaded.");
                    break;
                case 1:
                    Console.WriteLine("Downloading standard photo . . .");
                    dl.DownloadFile(apod_api.apod.url, "apod_" + apod_api.apod.date + ".jpg");
                    Console.WriteLine("Finished downloading.");
                    break;
                case 2:
                    Console.WriteLine("Downloading hd photo . . .");
                    dl.DownloadFile(apod_api.apod.hdurl, "apod_" + apod_api.apod.date + ".jpg");
                    Console.WriteLine("Finished downloading.");
                    break;
                default:
                    Console.WriteLine("Invalid selection.");
                    break;
            }
        }
    }
}
