
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System;
using Emgu.CV;

using Emgu.CV.CvEnum;
using ZXing;
using ZXing.Windows.Compatibility;
using System.Text.RegularExpressions;

namespace LensInfo1
{
    public partial class QRCamera : Window
    {
        private VideoCapture _capture;
        public event Action<string> QRCodeDecoded; // Event to notify QR code data

        public QRCamera()
        {
            InitializeComponent();
            StartCamera();
        }

        private void StartCamera()
        {
            _capture = new VideoCapture(0);
            _capture.ImageGrabbed += Capture_ImageGrabbed;
            _capture.Start();
        }

        private void Capture_ImageGrabbed(object sender, EventArgs e)
        {
            Mat frame = new Mat();
            _capture.Retrieve(frame);

            if (!frame.IsEmpty)
            {
                // Convert frame to Bitmap
                Bitmap bitmap = frame.ToBitmap();

                // Decode the QR code
                var barcodeReader = new BarcodeReader();
                var result = barcodeReader.Decode(bitmap);

                if (result != null)
                {
                    // Invoke the event with the QR data on the UI thread
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        QRCodeDecoded?.Invoke(result.Text);
                        Close(); // Close the camera window after decoding
                    });
                }
            }
        }

        private BitmapSource ConvertToBitmapSource(Mat mat)
        {
            using (Bitmap bitmap = mat.ToBitmap()) // Use using statement to ensure disposal
            {
                IntPtr hBitmap = bitmap.GetHbitmap();
                try
                {
                    return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                        hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                }
                finally
                {
                    DeleteObject(hBitmap); // Clean up HBitmap
                }
            }
        }

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);

        protected override void OnClosed(EventArgs e)
        {
            // Stop the camera when the window is closed
            _capture?.Stop();
            base.OnClosed(e);
        }
    }
}