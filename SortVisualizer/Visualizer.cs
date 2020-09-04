using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SortVisualizer
{
    public partial class Visualizer : Form
    {
        int SortMode;
        int[] StartData;
        bool stop = false;
        float realtime = 0.0f;
        static string compString = "";
        public Visualizer(List<int> data, int sortMode)
        {
            InitializeComponent();
            this.StartData = data.ToArray();
            this.SortMode = sortMode;
            this.Width = 1500;
            this.background.Width = this.Width;

            if (SortMode != 3 && SortMode != 6)
            {
                InitTimer();
            }
            else if (SortMode == 3)
            {
                int[] a = data.ToArray();
                QuickSort(a, 0, a.Length - 1);
            }
            else if (SortMode == 6) {
                int[] a = data.ToArray();
                MergeSort(a);
            }
            StopWatchStart();


        }

        #region time measurement

        static Stopwatch sw = new Stopwatch();

        public static void StopWatchStart()
        {
            sw.Start();
        }

        public static void StopWatchStop()
        {
            sw.Stop();
        }

        public static double TimeElapsedMillies() {
            return sw.Elapsed.TotalMilliseconds;
        }

        public static double RealTimeElapsedMillies()
        {
            return Math.Round(TimeElapsedMillies() / timeout,5);
        }
        #endregion

        #region QuickSort

        private int compare = 0;
        private int access = 0;
        
        async private Task QuickSort(int[] array, int low, int high)
        {
            if (low < high)
            {
                int partitionIndex = await Partition(array, low, high);

                await QuickSort(array, low, partitionIndex - 1);
                await QuickSort(array, partitionIndex + 1, high);
            }

        }

        async Task<int[]> swap(int[] arr, int a, int b) {
            int temp = arr[a];
            arr[a] = arr[b];
            arr[b] = temp;
            return arr;
        }

        async Task<int> Partition(int[] array, int low, int high)
        {
            int pivot = array[high];

            int lowIndex = (low - 1);

            for (int j = low; j < high; j++)
            {
                compare++;
                setCompare(array[j], pivot);
                if (array[j] <= pivot)
                {
                    lowIndex++;

                    await swap(array, lowIndex, j);
                    access++;

                    await Task.Delay(timeout);
                    List<Bar> list = new List<Bar>();
                    for (int i = 0; i < array.Length; i++) {
                        if (array[i] == pivot)
                        {
                            list.Add(new Bar(array[i], Color.Red));

                        }
                        else if (array[i] == j){
                            list.Add(new Bar(array[i], Color.Red));
                        }
                        else
                        {
                            list.Add(new Bar(array[i], Color.White));

                        }
                    }
                    algorithmData = new AlgorithmData(compare, access, list, "Quicksort");
                    background.Invalidate();
                }
            }

            await swap(array, lowIndex + 1, high);
            access++;

            return lowIndex + 1;
        }

        #endregion

        #region MergeSort

        async void MergeSort(int[] array)
        {
            int[] res = await MergeSortHelper(array);

        }

        async Task<int[]> MergeSortHelper(int[] array)
        {
            int[] left;
            int[] right;
            int[] result = new int[array.Length];

            if (array.Length <= 1)
            {
                return array;
            }

            int midPoint = array.Length / 2;
            left = new int[midPoint];

            if (array.Length % 2 == 0)
            {
                right = new int[midPoint];
            }
            else
            {
                right = new int[midPoint + 1];
            }


            for (int i = 0; i < midPoint; i++)
            {
                left[i] = array[i];
            }

            int x = 0;
            for (int i = midPoint; i < array.Length; i++)
            {
                right[x] = array[i];
                x++;
            }

            left = await MergeSortHelper(left);
            right = await MergeSortHelper(right);
            result = await Merge(left, right);
            return result;
        }

        async Task<int[]> Merge(int[] left, int[] right)
        {
            int resultLength = right.Length + left.Length;
            int[] result = new int[resultLength];

            int indexLeft = 0, indexRight = 0, indexResult = 0;
            while (indexLeft < left.Length || indexRight < right.Length)
            {
                if (indexLeft < left.Length && indexRight < right.Length)
                {
                    if (left[indexLeft] <= right[indexRight])
                    {
                        result[indexResult] = left[indexLeft];
                        indexLeft++;
                        indexResult++;
                    }
                    else
                    {
                        result[indexResult] = right[indexRight];
                        indexRight++;
                        indexResult++;
                    }
                }
                else if (indexLeft < left.Length)
                {
                    result[indexResult] = left[indexLeft];
                    indexLeft++;
                    indexResult++;
                }
                else if (indexRight < right.Length)
                {
                    result[indexResult] = right[indexRight];
                    indexRight++;
                    indexResult++;
                }

                await Task.Delay(timeout);

                List<Bar> list = new List<Bar>();
                for (int i = 0; i < result.Length; i++)
                {
                    list.Add(new Bar(result[i], Color.White));
                }
                algorithmData = new AlgorithmData(5, 5, list, "MergeSort");
                background.Invalidate();
            }
            return result;
        }


        #endregion

        #region engine
        //engine
        static int timeout = 4;
        public void InitTimer()
        {

            frame = 0;

            Task.Factory.StartNew(() =>
            {
                while (!stop)
                {
                    background.Invalidate();
                    Thread.Sleep(timeout);
                }
            });
        }

        private void Visualizer_FormClosing(object sender, FormClosingEventArgs e)
        {
            stop = true;
        }

        //paint
        int frame = 0;

        #endregion

        #region render

        static AlgorithmData algorithmData = null;
        private void background_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            frame++;

            //draw Background, reset pane
            Brush bg = Brushes.Black;
            g.FillRectangle(bg, 0, 0, this.FindForm().Width, background.Height);



            //retrieve info
            switch (Program.sortModeIndex)
            {
                case 0:
                    algorithmData = VisualSorter.ShakerSort(StartData, frame);
                    break;
                case 1:
                    algorithmData = VisualSorter.SelectionSort(StartData, frame);
                    break;
                case 2:
                    algorithmData = VisualSorter.BubbleSort(StartData, frame);
                    break;
                case 3:
                    //algorithmData = VisualSorter.QuickSort(StartData, 0, StartData.Length - 1);
                    break;
                case 4:
                    algorithmData = VisualSorter.InsertionSort(StartData, frame);
                    break;
                case 5:
                    break;
                case 6:
                    break;
                default:
                    Essentials.Error("Renderer: Index not found");
                    break;
            }

            //Render Text

            string s = "Method: " + algorithmData.Name + "   " + "Array Accesses: " + algorithmData.Access + "   " + "Comparisons: " + algorithmData.Compare + "   " + "Wait Time: " + timeout + "ms" + "   " + "Real Time: " + RealTimeElapsedMillies() + "ms";

            g.DrawString(s, new Font(FontFamily.GenericMonospace,9), new SolidBrush(Color.White), 5,5);

            g.DrawString(compString, new Font(FontFamily.GenericMonospace, 9), new SolidBrush(compString.Contains('>')?Color.Red:Color.LightGreen), 5, 25);


            //draw Strokes
            List<Bar> SortList = algorithmData.List;

            float Distance, Width;
            Distance = 0;
            Width = 0;
            if (SortList.Count <= 256)
            {
                Distance = (int)Math.Ceiling(this.FindForm().Width / SortList.Count / 12.0);
                Width = (this.FindForm().Width - Distance * SortList.Count) / SortList.Count;

            }
            else {
                Distance = 0;
                Width = this.FindForm().Width / SortList.Count;
                if (this.FindForm().Width - Width * SortList.Count > 25) {
                    this.FindForm().Width = (int)Width * SortList.Count;
                } 
            }
            for (int i = 0; i < SortList.Count; i++) {
                float BarHeight = SortList[i].Value*(400f/SortList.Count);
                g.FillRectangle(new SolidBrush(SortList[i].ColorKey), i * Width + i * Distance, background.Bottom - BarHeight, Width, BarHeight);
            }

         
        }

        #endregion

        #region compare

        public static void setCompare(int a, int b) {
            if (a > b)
            {
                compString = a + " > " + b;
            }
            else if (a < b) {
                compString = a + " < " + b;
            }
        }

        #endregion

    }
}
