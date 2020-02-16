using Intermec.Printer;

namespace Automated
{
    public class Printer
    {
        public static int Print()
        {
            PrintControl printControl = new PrintControl();
            Drawing drawing = new Drawing();

            drawing.Clear();

            Drawing.Text date = new Drawing.Text(330, 150, "Univers", "ПАРТ " + Startup.currentDate);
            date.Height = 10;
            drawing += date;

            Drawing.Barcode barcode = new Drawing.Barcode(330, 30, "CODE128", BarcodeScanner.scannedBarcode);
            barcode.Height = 80;
            barcode.Text.Enabled = true;
            barcode.Text.FontName = "Univers";
            barcode.Text.Height = 8;

            barcode.BarWidthWide = 3;
            barcode.BarWidthNarrow = 1;
            barcode.WidthMagnification = 1;
            drawing += barcode;

            printControl.PrintFeed(drawing, CountSetter.count);

            printControl.Dispose();
            drawing.Dispose();

            return 0;
        }

        public static void SetPrintScreen()
        {
            string date = "дата- " + Startup.currentDate;
            string label = "код-" + BarcodeScanner.scannedBarcode;
            string count = "брой етикети- " + CountSetter.count.ToString();

            Startup.display.Clear();

            Startup.headerLine.Data = "ГОТОВ за ПЕЧАТ";
            Startup.display += Startup.headerLine;
            Startup.display += Startup.separatorLine;

            Startup.display += new UI.Canvas.Text(10, 70, date, "Univers", 24, Startup.white);
            Startup.display += new UI.Canvas.Text(10, 120, label, "Univers", 24, Startup.white);
            Startup.display += new UI.Canvas.Text(10, 170, count, "Univers", 24, Startup.white);

            Startup.display += new UI.Canvas.Text(10, 240, "Enter за печат", "Univers", 32, Startup.white);
            Startup.display += new UI.Canvas.Text(10, 290, " C  за  отказ", "Univers", 14, Startup.white);
        }
    }
}
