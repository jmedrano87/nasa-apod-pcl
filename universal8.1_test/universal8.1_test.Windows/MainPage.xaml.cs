using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace universal8._1_test
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            updateImg();
        }
        private void nextBtn_Trig(object sender, RoutedEventArgs e)
        {
            if (myAPI.Apod.date != DateTime.Today)
            {
                myAPI.Date = myAPI.Apod.date.AddDays(1);
                updateImg();
            }
        }
        private void prevBtn_Trig(object sender, RoutedEventArgs e)
        {
            myAPI.Date = myAPI.Apod.date.AddDays(-1);
            updateImg();
        }
        private async void updateImg()
        {
            await myAPI.sendRequest();

            if (myAPI.Apod.media_type == "image")
            {
                apod_image.Source = new BitmapImage(myAPI.Apod.url);
                if (apod_video.Visibility == Visibility.Visible)
                {
                    apod_video.Stop();
                    apod_video.Visibility = Visibility.Collapsed;
                    apod_image.Visibility = Visibility.Visible;
                }
            }
            else if (myAPI.Apod.media_type == "video")
            {
                apod_image.Visibility = Visibility.Collapsed;
                apod_video.Navigate(myAPI.Apod.url);
                apod_video.Visibility = Visibility.Visible;
            }
        }
        ApodPcl.API myAPI = new ApodPcl.API(config.Key);
    }
}
