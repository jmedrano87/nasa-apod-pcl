using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

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

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
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
            Uri uri = await myAPI.GetUri();

            if (myAPI.Apod.media_type == "image")
            {
                apod_image.Source = new BitmapImage(uri);
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
                apod_video.Navigate(uri);
                apod_video.Visibility = Visibility.Visible;

            }
        }
            ApodPcl.API myAPI = new ApodPcl.API(config.Key);
    }
}
