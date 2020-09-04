using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SortVisualizer
{
    public class VisualSorter
    {
        static int compare = 0;
        static int access = 0;
        static bool finish = false;
        static bool swapFlag = false;

        static int iPosition = 0;
        static int jPosition = 0;
        static int minIndex = 0;
        static Direction direction = Direction.FORWARDS;
        enum Direction { FORWARDS, BACKWARDS };


        #region BubbleSort
        public static AlgorithmData BubbleSort(int[] data, int index) {

            List<Bar> output = new List<Bar>();

            int jPosition = index % data.Length;
            int iPosition = (index - jPosition) / data.Length;

            //Sort
            if (!finish)
            {

                if (iPosition <= data.Length && jPosition < data.Length - 1)
                {
                    int j = jPosition;
                    compare++;
                    Visualizer.setCompare(data[j], data[j + 1]);
                    if (data[j] > data[j + 1])
                    {
                        access++;
                        int temp = data[j + 1];
                        data[j + 1] = data[j];
                        data[j] = temp;
                    }

                }

            }

            //verify and stop
            if (iPosition >= data.Length - 1 && jPosition >= data.Length - 1)
            {
                finish = true;

            }

            if (!finish)
            {
                //Color Bars
                for (int i = 0; i < data.Length; i++)
                {

                    if (i == jPosition || i == jPosition + 1)
                    {
                        output.Add(new Bar(data[i], Color.Red));
                    }
                    else
                    {
                        output.Add(new Bar(data[i], Color.White));
                    }

                }
            }
            else {
                for (int i = 0; i < data.Length; i++)
                {
                    output.Add(new Bar(data[i], Color.White));
                }

            }

            return new AlgorithmData(compare, access, output, "BubbleSort");
        }
        #endregion

        #region SelectionSort
        public static AlgorithmData SelectionSort(int[] data, int index) {

            List<Bar> output = new List<Bar>();

            if (jPosition < data.Length)
            {
                jPosition++;
            }
            else if(iPosition < data.Length){
                iPosition++;
                minIndex = iPosition;
                jPosition = iPosition + 1;
            }

            //sorter
            if (iPosition < data.Length && jPosition < data.Length) {
                compare++;
                Visualizer.setCompare(data[jPosition], data[minIndex]);
                if (data[jPosition] < data[minIndex] && jPosition > iPosition)
                {
                    minIndex = jPosition;
                }


                if (iPosition < data.Length && jPosition == data.Length-1)
                {
                    access++;
                    int temp = data[minIndex];
                    data[minIndex] = data[iPosition];
                    data[iPosition] = temp;
                }

            }


            for (int m = 0; m < data.Length; m++) {
                if (m == minIndex)
                {
                    output.Add(new Bar(data[m], Color.LightGreen));
                }
                else if (m == jPosition)
                {
                    output.Add(new Bar(data[m], Color.Red));
                }
                else if (m == iPosition)
                {
                    output.Add(new Bar(data[m], Color.White));
                }
                else {
                    output.Add(new Bar(data[m], Color.White));
                }
            }

            return new AlgorithmData(compare, access, output, "SelectionSort");
        }
        #endregion

        #region ShakerSort
        public static AlgorithmData ShakerSort(int[] data, int index) {

            List<Bar> output = new List<Bar>();


            if (jPosition < data.Length - iPosition + 1 && jPosition > iPosition - 1 && !(jPosition == iPosition && direction == Direction.BACKWARDS))
            {
                if (direction == Direction.FORWARDS)
                    jPosition++;

                if (direction == Direction.BACKWARDS)
                    jPosition--;

                if (jPosition == data.Length - iPosition - 1)
                {
                    direction = Direction.BACKWARDS;
                }

            }
            else if (iPosition < data.Length / 2) {
                direction = Direction.FORWARDS;
                iPosition++;
                jPosition = iPosition;
            }

            //sorter
            if (Essentials.COMP >= compare && iPosition < data.Length / 2 && ((jPosition < data.Length - iPosition - 1 && direction == Direction.FORWARDS) || (jPosition > iPosition && direction == Direction.BACKWARDS))) {

                compare++;
                if (direction == Direction.FORWARDS)
                {
                    Visualizer.setCompare(data[jPosition], data[jPosition + 1]);

                    if (data[jPosition] > data[jPosition + 1])
                    {
                        access++;
                        int temp = data[jPosition];
                        data[jPosition] = data[jPosition + 1];
                        data[jPosition + 1] = temp;
                        swapFlag = true;
                    }
                }

                if (direction == Direction.BACKWARDS) {
                    Visualizer.setCompare(data[jPosition - 1], data[jPosition]);

                    if (data[jPosition - 1] > data[jPosition])
                    {
                        access++;
                        int temp = data[jPosition - 1];
                        data[jPosition - 1] = data[jPosition];
                        data[jPosition] = temp;
                        swapFlag = true;
                    }

                }

                if (!swapFlag) {
                    finish = true;
                }

            }


            for (int i = 0; i < data.Length; i++) {
                if (i == jPosition && Essentials.COMP >= compare)
                {
                    output.Add(new Bar(data[i], Color.Red));

                }
                else {
                    output.Add(new Bar(data[i], Color.White));
                }
            }

            return new AlgorithmData(compare, access, output, "Shaker Sort");
        }
        #endregion

        #region InsertionSort

        public static AlgorithmData InsertionSort(int[] data, int index) {

            List<Bar> output = new List<Bar>();

            if (jPosition > 0)
            {
                jPosition--;


            }
            else if (iPosition < data.Length)
            {
                iPosition++;
                jPosition = iPosition + 1;
            }


            //sorter
            if (iPosition < data.Length - 1 && jPosition > 0)
            {
                compare++;
                Visualizer.setCompare(data[jPosition], data[jPosition - 1]);
                if (data[jPosition] < data[jPosition - 1])
                {
                    access++;
                    int temp = data[jPosition];
                    data[jPosition] = data[jPosition - 1];
                    data[jPosition - 1] = temp;
                }
            }


            for (int i = 0; i < data.Length; i++) {
                if (i == jPosition)
                {
                    output.Add(new Bar(data[i], Color.Red));
                }
                else
                {
                    output.Add(new Bar(data[i], Color.White));
                }
            }

            return new AlgorithmData(compare, access, output, "Insertion Sort");
        }

        #endregion

        #region getter setter
        public static void setJPosition(int j) {
            jPosition = j;
        }
        #endregion
    }

    #region AlgorithmData
    public class AlgorithmData {
        public int Compare = 0;
        public int Access = 0;
        public string Name = ""; 
        public List<Bar> List = new List<Bar>();

        public AlgorithmData(int c, int a, List<Bar> l, string n) {
            Compare = c;
            Access = a;
            List = l;
            Name = n;
        }
    }

    public class Bar{
        private int val;
        private Color col;
        public Bar(int Value, Color ColorKey) {
            this.Value = Value;
            this.ColorKey = ColorKey;
        }

        public int Value { 
            get { return this.val; } 
            set { this.val = value; } 
        }

        public Color ColorKey
        {
            get { return this.col; }
            set { this.col = value; }
        }
    }
    #endregion
}
