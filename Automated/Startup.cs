using Intermec.Printer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Automated
{
    public class Startup
    {
        public static UI.Canvas display;
        public static UI.Keypad keypad;
        public static UI.Canvas.Text headerLine;
        public static UI.Canvas.Text separatorLine;
        public static UI.Canvas.Text firstLine;
        public static UI.Canvas.Text secondLine;
        public static UI.Canvas.Text footerLine;
        public static UI.Canvas.Text countLine;

        public static string currentDate;

        public static Color white = new Color(255, 255, 255, 255);
        public static Color black = new Color(0, 0, 0, 255);

        public static int Main(string[] args)
        {
            display = new UI.Canvas();

            UI.Canvas.Rectangle background = new UI.Canvas.Rectangle(0, 0, display.Width, display.Height, black);

            display += background;

            currentDate = DateTime.Today.ToString("dd/MM/yyyy");

            SetInitialScreen();

            keypad = new UI.Keypad();
            keypad.KeyDown += new UI.Keypad.KeyEventHandler(KeyHandlerDown);

            display.Run();
            display.Dispose();
            keypad.Dispose();
            return 0;
        }

        public static void InitializeDisplay(string header, string first, string second, string footer)
        {
            display.Clear();

            headerLine = new UI.Canvas.Text(20, 20, header, "Univers", 24, white);
            separatorLine = new UI.Canvas.Text(0, 50, new string('-', 100), "Univers", 10, white);
            firstLine = new UI.Canvas.Text(20, 100, first, "Univers", 32, white);
            secondLine = new UI.Canvas.Text(20, 180, second, "Univers", 32, white);
            footerLine = new UI.Canvas.Text(10, 270, footer, "Univers", 14, white);

            display += headerLine;
            display += separatorLine;
            display += firstLine;
            display += secondLine;
            display += footerLine;
        }

        public static void KeyHandlerDown(Object o, UI.Keypad.KeyEventArgs evArgs)
        {
            int keyCode;

            keyCode = evArgs.KeyChar;

            if (keyCode == 43)
            {
                Environment.Exit(0);
            }
            else if (keyCode == 1 && !BarcodeScanner.barcodeIsSet)
            {
                BarcodeScanner.Scan();
            }
            else if (!CountSetter.countSet)
            {
                CountSetter.SetCount(keyCode);
            }
            else
            {
                if (keyCode == 10)
                {
                    Printer.Print();
                }
                else if(keyCode == 9)
                {
                    BarcodeScanner.barcodeIsSet = false;
                    CountSetter.ResetCount();
                    SetInitialScreen();
                }
            }

        }

        public static void SetInitialScreen()
        {
            Startup.InitializeDisplay("ДАТА " + currentDate,"Натисни F1", "и сканирай", "");
        }
    }
}
