using SortVisualizer.Properties;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SortVisualizer
{
    class Program
    {
        static List<int> dataSet = new List<int>();
        public static int sortModeIndex;
        [STAThread]
        static void Main(string[] args)
        {

            //some winform stuff
            Application.EnableVisualStyles();
            //Console.SetWindowSize((int)Math.Round(Console.WindowWidth*1.5f), Console.WindowHeight);

            //Select Size
            Console.WriteLine("Choose Array Size: [Integer]");

            Console.ForegroundColor = ConsoleColor.Green;
            int arraySize = Convert.ToInt32(Console.ReadLine());
            Console.ResetColor();

            dataSet.Clear();
            for (int i = 1; i <= arraySize; i++) {
                dataSet.Add(i);
            }

            dataSet = Sorter.Randomize(dataSet, 1000);
            Essentials.Dump(dataSet);


            //Select Mode
            Console.WriteLine("Choose Sort Mode: [Integer]");

            Essentials.Dump(Data.SortModes, true);

            Console.ForegroundColor = ConsoleColor.Green;
            sortModeIndex = Convert.ToInt32(Console.ReadLine());
            Console.ResetColor();

            Console.WriteLine("You Chose {0}. Wise Choice.", Data.SortModes[sortModeIndex].Split(' ')[0] + " Sort");

            switch (sortModeIndex) {
                case 0: Sorter.ShakerSort(dataSet);
                    break;
                case 1: Sorter.SelectionSort(dataSet);
                    break;
                case 2: Sorter.BubbleSort(dataSet);
                    break;
                case 3: Sorter.QuickSort(dataSet);
                    break;
                case 4:Sorter.InsertionSort(dataSet);
                    break;
                case 5: Sorter.BogoSort(dataSet);
                    break;
                case 6: Sorter.MergeSort(dataSet.ToArray());
                    break;
                default:Essentials.Error();
                    break;
            }


            //Show Visualizer?
            if ((arraySize <= 256 && sortModeIndex != 5) || (arraySize <= 1500 && (sortModeIndex == 6 || sortModeIndex == 3)))
            {
                Console.WriteLine("Show Visualizer: [Y/N]");
                if (Console.ReadLine().ToLower().Equals("y"))
                {
                    Visualizer visualizer = new Visualizer(dataSet, sortModeIndex);
                    Application.Run(visualizer);
                }
            }


            //End
            Essentials.Finalize();

        }

       
    }
}
