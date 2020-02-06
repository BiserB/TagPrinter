using Intermec.Printer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Automated
{
    public class CountSetter
    {
        private static readonly Dictionary<int, int> KEYS = new Dictionary<int, int>{
            {49, 1}, {16, 2}, {51, 3}, {13, 4}, {53, 5}, {14, 6}, {55, 7}, {15, 8}, {57, 9}, {48, 0},
            { 10, -10},     // ENTER
            { 9, -9},       // C
            { 43, -8},      // red button
            { 8, -7}        // minus
        };

        public static int count;
        public static bool countSet;

        private static int tensDigit;
        private static int onesDigit;
        private static bool tensSet;
        private static bool onesSet;

        private static string countString = "Брой - {0} {1}";

        public static void SetCount(int keyCode)
        {
            int keyValue = KEYS[keyCode];

            if (keyValue >= 0)
            {
                if (!tensSet)
                {
                    tensDigit = keyValue;
                    Startup.countLine.Data = String.Format(countString, tensDigit.ToString(), "_");
                    tensSet = true;
                }
                else if (!onesSet)
                {
                    onesDigit = keyValue;
                    Startup.countLine.Data = String.Format(countString, tensDigit.ToString(), onesDigit.ToString());
                    onesSet = true;
                }
            }
            else
            {
                if (keyValue == -9)
                {
                    ResetCount();
                }
                else if (keyValue == -10)
                {
                    if (onesSet)
                    {
                        count = tensDigit * 10 + onesDigit;
                    }
                    else
                    {
                        count = tensDigit;
                    }

                    if (count != 0)
                    {
                        countSet = true;
                        Printer.SetPrintScreen();
                    }
                }
            }

        }

        public static void ResetCount()
        {
            count = 0;
            countSet = false;
            tensDigit = 0;
            onesDigit = 0;
            tensSet = false;
            onesSet = false;
            Startup.countLine.Data = String.Format(countString, "_", "_");
        }

        public static void SetCountScreen()
        {
            Startup.display.Clear();

            Startup.headerLine.Data = "ВЪВЕДИ БРОЙ";
            Startup.display += Startup.headerLine;
            Startup.display += Startup.separatorLine;
            Startup.countLine = new UI.Canvas.Text(10, 100, String.Format(countString, "_", "_"), "Univers", 32, Startup.white);
            Startup.display += Startup.countLine;

            Startup.footerLine.Data = "C - изтрий     |     Enter - запис";
            Startup.display += Startup.footerLine;
        }
    }
}
