using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.Geolocation;
using Windows.Storage;
using Windows.UI.Xaml;
using BeatThisBurger.ViewModels;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BeatThisBurger.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewBurgerView 
    {
        public NewBurgerView()
        {
            InitializeComponent();
            Data = new DataContextWrapper<NewBurgerViewModel>(this);
        }

        public DataContextWrapper<NewBurgerViewModel> Data { get; private set; }


        private static async Task<DeviceInformation> FindCameraDeviceByPanelAsync()
        {
            // Get available devices for capturing pictures
            var allVideoDevices = await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);

            // If there is no device mounted on the desired panel, return the first device found
            return allVideoDevices.FirstOrDefault();
        }
        StorageFile media = null;
        private async void TakePhotoClick(object sender, RoutedEventArgs e)
        {
            // Attempt to get the back camera if one is available, but use any camera device if not
            var cameraDevice = await FindCameraDeviceByPanelAsync();

            if (cameraDevice == null)
            {
                Debug.WriteLine("No camera device found!");
                return;
            }

            // Create MediaCapture and its settings
            var _mediaCapture = new MediaCapture();

             media = await ApplicationData.Current.LocalFolder.CreateFileAsync(Guid.NewGuid().ToString() + ".jpg");

            var settings = new MediaCaptureInitializationSettings { VideoDeviceId = cameraDevice.Id };

            // Initialize MediaCapture
            try
            {
                await _mediaCapture.InitializeAsync(settings);
                await _mediaCapture.CapturePhotoToStorageFileAsync(ImageEncodingProperties.CreateJpeg(), media);

                _mediaCapture.Dispose();
            }
            catch (UnauthorizedAccessException)
            {
                Debug.WriteLine("The app was denied access to the camera");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception when initializing MediaCapture with {0}", ex.ToString());
            }
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);


            try
            {
                // Request permission to access location
                var accessStatus = await Geolocator.RequestAccessAsync();

                switch (accessStatus)
                {
                    case GeolocationAccessStatus.Allowed:

                        // Get cancellation token
                        var _cts = new CancellationTokenSource();
                        CancellationToken token = _cts.Token;

                        
                        // If DesiredAccuracy or DesiredAccuracyInMeters are not set (or value is 0), DesiredAccuracy.Default is used.
                        Geolocator geolocator = new Geolocator { DesiredAccuracyInMeters = 200};

                        // Carry out the operation
                        Geoposition pos = await geolocator.GetGeopositionAsync().AsTask(token);

                        Data.ViewModel.Place.Latitude = pos.Coordinate.Point.Position.Latitude;
                        Data.ViewModel.Place.Longitude= pos.Coordinate.Point.Position.Longitude;
                        Data.ViewModel.Place.RefreshLocation();
                            break;

                    case GeolocationAccessStatus.Denied:
                        break;

                    case GeolocationAccessStatus.Unspecified:
                        break;
                }
            }
            catch (TaskCanceledException)
            {
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }

        }
    }
}
