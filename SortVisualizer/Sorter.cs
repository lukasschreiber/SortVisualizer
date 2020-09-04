using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortVisualizer.Properties
{
    class Sorter
    {
        #region Randomize
        public static List<int> Randomize(List<int> data, int number) {
            int[] arr = data.ToArray();
            int n = arr.Length;

            for (int k = 0; k < number; k++)
            {
                for (int i = 0; i < n; i++)
                {
                    int temp = arr[i];
                    double r = new Random().NextDouble();
                    int newIndex = (int)Math.Floor(r * i);
                    arr[i] = arr[newIndex];
                    arr[newIndex] = temp;
                }
            }

            return arr.ToList();
        }
        #endregion

        #region Shaker Sort
        public static void ShakerSort(List<int> data) {
            int[] arr = data.ToArray();

            int access = 0;
            int compare = 0;

            for (int i = 0; i < arr.Length / 2; i++)
            {
                bool swapFlag = false;
                for (int j = i; j < arr.Length - i - 1; j++)
                {
                    compare++;
                    if (arr[j] > arr[j + 1])
                    {
                        access++;
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                        swapFlag = true;
                    }
                }

                for (int j = arr.Length - 2 - i; j > i; j--)
                {
                    compare++;
                    if (arr[j - 1] > arr[j])
                    {
                        access++;
                        int temp = arr[j - 1];
                        arr[j - 1] = arr[j];
                        arr[j] = temp;
                        swapFlag = true;
                    }
                }

                if (!swapFlag)
                {
                    break;
                }
            }

            Essentials.Dump(arr);
            Essentials.Result(access, compare);
        }
        #endregion

        #region Selection Sort
        public static void SelectionSort(List<int> data)
        {
            int[] arr = data.ToArray();

            int access = 0;
            int compare = 0;

            for (int i = 0; i < arr.Length - 1; i++) {
                int minIndex = i;
                for (int j = i + 1; j < arr.Length; j++) {
                    compare++;
                    if (arr[j] < arr[minIndex]) {
                        minIndex = j;
                    }
                }

                access++;
                int temp = arr[minIndex];
                arr[minIndex] = arr[i];
                arr[i] = temp;
            }

            Essentials.Dump(arr);
            Essentials.Result(access, compare);
        }

        #endregion

        #region Bubble Sort
        public static void BubbleSort(List<int> data)
        {
            int[] arr = data.ToArray();

            int access = 0;
            int compare = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr.Length - 1; j++)
                {
                    compare++;
                    if (arr[j] > arr[j + 1])
                    {
                        access++;
                        int temp = arr[j + 1];
                        arr[j + 1] = arr[j];
                        arr[j] = temp;
                    }
                }
            }

            Essentials.Dump(arr);
            Essentials.Result(access, compare);
        }

        #endregion

        #region Insertion Sort
        public static void InsertionSort(List<int> data)
        {
            int[] arr = data.ToArray();

            int access = 0;
            int compare = 0;

            for (int i = 0; i < arr.Length - 1; i++) {

                for (int j = i + 1; j > 0; j--) {
                    compare++;
                    if (arr[j] < arr[j - 1]) {
                        access++;
                        int temp = arr[j];
                        arr[j] = arr[j - 1];
                        arr[j - 1] = temp;
                    }
                }

            }

            Essentials.Dump(arr);
            Essentials.Result(access, compare);

        }

        #endregion

        #region Quick Sort
        public static void QuickSort(List<int> array) {
            int[] arr = array.ToArray();
            QuickSort(arr, 0, array.Count - 1);

            Essentials.Dump(arr);
            Essentials.Result(a, c);

        }

        static int a = 0;
        static int c = 0;

        public static void QuickSort(int[] array, int low, int high)
        {
            if (low < high)
            {
                int partitionIndex = Partition(array, low, high);

                QuickSort(array, low, partitionIndex - 1);
                QuickSort(array, partitionIndex + 1, high);
            }

        }

        static int Partition(int[] array, int low, int high)
        {
            int pivot = array[high];

            int lowIndex = (low - 1);

            for (int j = low; j < high; j++)
            {
                c++;
                if (array[j] <= pivot)
                {
                    lowIndex++;

                    a++;
                    int temp = array[lowIndex];
                    array[lowIndex] = array[j];
                    array[j] = temp;
                }
            }

            a++;
            int temp1 = array[lowIndex + 1];
            array[lowIndex + 1] = array[high];
            array[high] = temp1;

            return lowIndex + 1;
        }

        #endregion

        #region Bogo Sort
        public static void BogoSort(List<int> data) {

            Random r = new Random();

            int t = 1;

            while (!Essentials.isSorted(data))
            {
                t++;
                int a = r.Next(data.Count);
                int b = r.Next(data.Count);

                int temp = data[a];
                data[a] = data[b];
                data[b] = temp;
               

            }

            Essentials.Dump(data);
            Essentials.Result(t);
        }

        #endregion

        #region Merge Sort

        public static void MergeSort(int[] array) {
            int[] res = MergeSortHelper(array);

            Essentials.Dump(res);
            Essentials.Result(a, c);
        }
        public static int[] MergeSortHelper(int[] array)
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
                c++;
                left[i] = array[i];
            }

            int x = 0;
            for (int i = midPoint; i < array.Length; i++)
            {
                c++;
                right[x] = array[i];
                x++;
            }

            left = MergeSortHelper(left);
            right = MergeSortHelper(right);
            result = Merge(left, right);
            return result;
        }

        public static int[] Merge(int[] left, int[] right)
        {
            int resultLength = right.Length + left.Length;
            int[] result = new int[resultLength];

            int indexLeft = 0, indexRight = 0, indexResult = 0;
            while (indexLeft < left.Length || indexRight < right.Length)
            {
                c++;
                if (indexLeft < left.Length && indexRight < right.Length)
                {
                    if (left[indexLeft] <= right[indexRight])
                    {
                        a++;
                        result[indexResult] = left[indexLeft];
                        indexLeft++;
                        indexResult++;
                    }
                    else
                    {
                        a++;
                        result[indexResult] = right[indexRight];
                        indexRight++;
                        indexResult++;
                    }
                }
                else if (indexLeft < left.Length)
                {
                    a++;
                    result[indexResult] = left[indexLeft];
                    indexLeft++;
                    indexResult++;
                }
                else if (indexRight < right.Length)
                {
                    a++;
                    result[indexResult] = right[indexRight];
                    indexRight++;
                    indexResult++;
                }
            }
            return result;
        }
    }

    #endregion

}

