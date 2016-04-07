using System;
using System.Diagnostics;

namespace ApodPcl
{
    class Program
    {
        static void Main(string[] args)
        {
            string key = config.Key;
            API apod_api = new API(key);
            int action;
            DateTime oldestDate = new DateTime(1995, 06, 25);
            do
            {
                apod_api.sendRequest().Wait();
                Console.WriteLine("Title: " + apod_api.Apod.title);
                Console.WriteLine("Date: " + apod_api.Apod.date.ToShortDateString());
                Console.WriteLine("Media type: " + apod_api.Apod.media_type);
                printMenu(apod_api.Apod.media_type == "image");

                action = int.Parse(Console.ReadLine());

                switch (action)
                {
                    case 0:
                        Console.WriteLine("Exiting . . .");
                        break;
                    case 1:
                        Console.WriteLine(apod_api.Apod.explanation);
                        break;
                    case 2:
                        if (apod_api.Apod.url != null)
                        {
                            Console.WriteLine("Opening . . .");
                            Process.Start(apod_api.Apod.url.ToString());
                        }
                        break;
                    case 3:
                        if (apod_api.Apod.hdurl != null)
                        {
                            Console.WriteLine("Opening . . .");
                            Process.Start(apod_api.Apod.hdurl.ToString());
                        }
                        break;
                    case 4:
                        if (apod_api.Apod.date.AddDays(-1) >= oldestDate)
                            apod_api.Date = apod_api.Apod.date.AddDays(-1);
                        else
                            Console.WriteLine("Can't go that far back.");
                        break;
                    case 5:
                        if (apod_api.Apod.date.AddDays(1) <= DateTime.Today)
                            apod_api.Date = apod_api.Apod.date.AddDays(1);
                        else
                            Console.WriteLine("Can't travel to the future!");
                        break;
                    default:
                        Console.WriteLine("Invalid selection.");
                        break;
                }
            }
            while (action != 0);
        }
        static void printMenu(bool isImage)
        {

            Console.WriteLine("[1] Read explanation.");
            Console.WriteLine("[2] View " + ((isImage) ? "low quality picture." : "video."));
            Console.WriteLine((isImage) ? "[3] View high quality picture." : "[x] N/A");
            Console.WriteLine("[4] Previous Day.");
            Console.WriteLine("[5] Next Day.");
            Console.WriteLine("[0] Quit.");
            Console.Write("Enter selection: ");
        }
    }
}
