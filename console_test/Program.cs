using System;
using System.Net;

namespace apod_api
{
    class Program
    {
        static void Main(string[] args)
        {
            APOD_API apod_api = new APOD_API();
            int action;
            DateTime oldestDate = new DateTime(1995, 06, 25);
            WebClient dl = new WebClient();

            do
            {
                apod_api.sendRequest().Wait();
                Console.WriteLine("Title: " + apod_api.apod.title);
                Console.WriteLine("Date: " + apod_api.apod.date.ToShortDateString());
                Console.WriteLine("[1] Standard");
                Console.WriteLine("[2] HD");
                Console.WriteLine("[3] Previous Day");
                Console.WriteLine("[4] Next Day");
                Console.WriteLine("[0] Quit.");
                Console.Write("Enter selection: ");

                action = int.Parse(Console.ReadLine());

                switch (action)
                {
                    case 0:
                        Console.WriteLine("Exiting . . .");
                        break;
                    case 1:
                        Console.WriteLine("Downloading standard photo . . .");
                        try {
                            dl.DownloadFile(apod_api.apod.url, "apod_"
                                + apod_api.apod.date.ToString("yyyy-MM-dd") + "_lq.jpg");
                            Console.WriteLine("Finished downloading.");
                        }
                        catch (WebException ex) {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 2:
                        Console.WriteLine("Downloading hd photo . . .");
                        try {
                            dl.DownloadFile(apod_api.apod.hdurl, "apod_"
                                + apod_api.apod.date.ToString("yyyy-MM-dd") + "_hq.jpg");
                            Console.WriteLine("Finished downloading.");
                        }
                        catch (WebException ex) {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 3:
                        if (apod_api.apod.date.AddDays(-1) >= oldestDate)
                            apod_api.Date = apod_api.apod.date.AddDays(-1);
                        else
                            Console.WriteLine("Can't go that far back.");
                        break;
                    case 4:
                        if (apod_api.apod.date.AddDays(1) <= DateTime.Today)
                            apod_api.Date = apod_api.apod.date.AddDays(1);
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
    }
}
