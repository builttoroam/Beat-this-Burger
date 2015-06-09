using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Storage;
using Windows.UI.Xaml;
using BeatThisBurger.ViewModels;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;

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
    }
}
