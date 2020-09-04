using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SortVisualizer
{
    public class Essentials
    {
        

        public static int COMP = 0;
        public static int ACC = 0;

        #region finalize
        //Finalize
        public static void Finalize()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Press any Key to continue...");
            Console.ReadKey();
        }
        #endregion

        #region dump
        //Dump
        public static void Dump(List<int> list)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("\t");
            list.ForEach(i => Console.Write("{0}\t", i));
            Console.Write("\n");
            Console.ResetColor();

        }
        public static void Dump(string[] list)
        {
            List<string> l = list.ToList();
            l.ForEach(i => Console.Write("{0}\t", i));
            Console.Write("\n");

        }

        public static void Dump(string[] list, bool TwoLines) {
            if (TwoLines) {
                string[] arr1 = new string[(int)Math.Ceiling(list.Length/2.0)];
                string[] arr2 = new string[(int)Math.Floor(list.Length / 2.0)];

                Array.Copy(list, 0, arr1, 0, arr1.Length);
                Array.Copy(list, arr1.Length, arr2, 0, arr2.Length);

                Dump(arr1);
                Dump(arr2);
            }
            else
            {
                Dump(list);
            }
        }

        public static void Dump(int[] list)
        {
            Console.Write("\t");
            List<int> l = list.ToList();
            Console.ForegroundColor = ConsoleColor.Magenta;
            l.ForEach(i => Console.Write("{0}\t", i));
            Console.Write("\n");
            Console.ResetColor();

        }

        #endregion

        #region error
        public static void Error() {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error");
            Console.ResetColor();
        }

        public static void Error(string s)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error: " + s);
            Console.ResetColor();
        }

        #endregion

        #region result
        //Result
        public static void Result(int a, int c) {
            COMP = c; 
            ACC = a;
            Console.WriteLine("It took the Algorithm {0} comparisons and {1} array accesses", c, a);
        }

        public static void Result(int tries) {
            Console.WriteLine("It took the Algorithm {0} tries.", tries);
        }

        #endregion

        public static bool isSorted(List<int> data) {

            if (data.SequenceEqual(data.OrderBy(d => d))) {
                return true;
            }

            return false;
        }

        #region color
        //Color Managment
        public static Color HsvToRgb(double h, double s, double v)
        {
            int hi = (int)Math.Floor(h / 60.0) % 6;
            double f = (h / 60.0) - Math.Floor(h / 60.0);

            double p = v * (1.0 - s);
            double q = v * (1.0 - (f * s));
            double t = v * (1.0 - ((1.0 - f) * s));

            Color ret;

            switch (hi)
            {
                case 0:
                    ret = GetRgb(v, t, p);
                    break;
                case 1:
                    ret = GetRgb(q, v, p);
                    break;
                case 2:
                    ret = GetRgb(p, v, t);
                    break;
                case 3:
                    ret = GetRgb(p, q, v);
                    break;
                case 4:
                    ret = GetRgb(t, p, v);
                    break;
                case 5:
                    ret = GetRgb(v, p, q);
                    break;
                default:
                    ret = Color.FromArgb(0xFF, 0x00, 0x00, 0x00);
                    break;
            }
            return ret;
        }
        public static Color GetRgb(double r, double g, double b)
        {
            return Color.FromArgb(255, (byte)(r * 255.0), (byte)(g * 255.0), (byte)(b * 255.0));
        }

        public static int RandomHue() {
            Random r = new Random();
            return r.Next(0,360);
        }

        #endregion

    }
}
