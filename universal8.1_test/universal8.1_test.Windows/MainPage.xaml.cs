using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
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
        }
        private void nextBtn_Trig(object sender, RoutedEventArgs e)
        {
            if (myAPI.apod.date != DateTime.Now)
            {
                myAPI.setDate(myAPI.apod.date.AddDays(1));
                updateImg();
            }
        }
        private void prevBtn_Trig(object sender, RoutedEventArgs e)
        {
            myAPI.setDate(myAPI.apod.date.AddDays(-1));
            updateImg();
        }
        private async void updateImg()
        {
            await myAPI.sendRequest();
            apod_image.Source = new BitmapImage(new Uri(myAPI.apod.url));
        }
        apod_api.APOD_API myAPI = new apod_api.APOD_API();
    }
}
