using Intermec.Printer;
using System;
using System.IO;
using System.Text;
using System.Threading;

namespace Automated
{
    public static class BarcodeScanner
    {
        public static bool barcodeIsSet;
        public static string scannedBarcode;

        public static void Scan()
        { 
            Communication.USBHost usbHost = new Communication.USBHost("/dev/ttyUSB0");

            usbHost.Open();

            if (usbHost.IsOpen)
            {
                FileStream fileStream = usbHost.GetStream();

                try
                {
                    int length;

                    while (true)
                    {
                        Byte[] bytes = new Byte[256];

                        if ((length = fileStream.Read(bytes, 0, bytes.Length)) > 0)
                        {
                            scannedBarcode = Encoding.ASCII.GetString(bytes, 0, length).Trim();

                            barcodeIsSet = true;

                            CountSetter.SetCountScreen();
                            break;
                        }
                        else
                        {
                            Thread.Sleep(1000);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                usbHost.Close();
            }

            usbHost.Dispose();
        }

    }
}
