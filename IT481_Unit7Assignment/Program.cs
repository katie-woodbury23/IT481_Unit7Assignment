/*
Kathleen Woodbury
Purdue University Global
IT481 Advanced Software Development
Professor Kovacic
Unit 7 Assignment: Benchmarking
11/5/2023
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace IT481_Unit7Assignment
{
    class Program
    {
        enum SortAlg { BUBBLESORT, QUICKSORT }

        // Create the stopwatch object
        private static Stopwatch? stopWatch;

        // Create a debug for printing
        private static bool debug = false;

        static void Main(string[] args)
        {

            Console.WriteLine("***** BENCHMARKING EXERCISE *****");
            Console.WriteLine();

            //****************************************************// 
            // Arrays with duplicates 
            //****************************************************// 

            // Create a small integer array (10)
            int[] smallArray = getArray(10, 20);

            // Create a medium integer array (1000)
            int[] mediumArray = getArray(1000, 2000);

            // Create a large integer array (10,000)
            int[] largeArray = getArray(10000, 20000);

            //****************************************************// 
            // Arrays without duplicates 
            //****************************************************// 

            // Small array without duplicates
            int[] uniqueSmallArray = SetUniqueElements((int[])smallArray.Clone());

            // Medium array without duplicates
            int[] uniqueMediumArray = SetUniqueElements((int[])mediumArray.Clone());

            // Large array without duplicates
            int[] uniqueLargeArray = SetUniqueElements((int[])largeArray.Clone());

            //***************************************************// 
            // Bubble sort 
            //***************************************************// 

            // Run the small bubble sort
            String arrayDesc = "small";
            runSortArray((int[])smallArray.Clone(), arrayDesc, SortAlg.BUBBLESORT);

            // Run the small unique bubble sort
            arrayDesc = "small unique";
            runSortArray((int[])uniqueSmallArray.Clone(), arrayDesc, SortAlg.BUBBLESORT);

            // Run the medium bubble sort
            arrayDesc = "medium";
            runSortArray((int[])mediumArray.Clone(), arrayDesc, SortAlg.BUBBLESORT);

            // Run the medium unique bubble sort
            arrayDesc = "medium unique";
            runSortArray((int[])uniqueMediumArray.Clone(), arrayDesc, SortAlg.BUBBLESORT);

            // Run the large bubble sort
            arrayDesc = "large";
            runSortArray((int[])largeArray.Clone(), arrayDesc, SortAlg.BUBBLESORT);

            // Run the large unique bubble sort
            arrayDesc = "large unique";
            runSortArray((int[])uniqueLargeArray.Clone(), arrayDesc, SortAlg.BUBBLESORT);

            //*************************************************// 
            // Quicksort 
            //*************************************************// 

            // Run the small quick sort
            arrayDesc = "small";
            runSortArray((int[])smallArray.Clone(), arrayDesc, SortAlg.QUICKSORT);

            // Run the small unique quick sort
            arrayDesc = "small unique";
            runSortArray((int[])uniqueSmallArray.Clone(), arrayDesc, SortAlg.QUICKSORT);

            // Run the medium quick sort
            arrayDesc = "medium";
            runSortArray((int[])mediumArray.Clone(), arrayDesc, SortAlg.QUICKSORT);

            // Run the medium unique quick sort
            arrayDesc = "medium unique";
            runSortArray((int[])uniqueMediumArray.Clone(), arrayDesc, SortAlg.QUICKSORT);

            // Run the large quick sort
            arrayDesc = "large";
            runSortArray((int[])largeArray.Clone(), arrayDesc, SortAlg.QUICKSORT);

            // RUn the large unique quick sort
            arrayDesc = "large unique";
            runSortArray((int[])uniqueLargeArray.Clone(), arrayDesc, SortAlg.QUICKSORT);
            Console.Read();
        }

        // Get array of integers of sizes as determined by parameters passed
        private static int[] getArray(int size, int randomMaxSize)
        {
            // Create the array with size
            int[] myArray = new int[size];

            // Get the random values for the array
            for (int i = 0; i < myArray.Length; i++)
            {
                // Get the random number with randomMaxSize as the upper limit of 1 - randomMaxSize
                myArray[i] = GetRandomNumber(1, randomMaxSize);
            }

            // Return the filled array
            return myArray;
        }

        // Run the sort actions by printing and timing the arrays
        private static void runSortArray(int[] arr, String arrayDesc, SortAlg type)
        {
            long elapsedTime = 0;

            // Set the sort type as a string
            String sortAlg = String.Empty;

            if (type == SortAlg.BUBBLESORT)
            {
                sortAlg = "bubble";
            }
            else if (type == SortAlg.QUICKSORT)
            {
                sortAlg = "quick";
            }

            // Print array before sorting using bubble sort algorithm
            if (debug)
            {

                Console.WriteLine("Array before the " + sortAlg + " sort");
                for (int i = 0; i < arr.Length; i++)
                {
                    Console.Write(arr[i] + " ");
                }
                Console.WriteLine();
                Console.WriteLine();
            }

            // Start the timer
            stopWatch = Stopwatch.StartNew();

            // Sort an array using bubble sort algorithm
            if (type == SortAlg.BUBBLESORT)
            {
                BubbleSort(arr);
            }
            // Sort an array using quicksort algorithm
            else if (type == SortAlg.QUICKSORT)
            {
                // Set low and high values for a quick sort
                int low = 0;
                int high = arr.Length - 1;
                QuickSortAsc(arr, low, high);
            }

            // Print array after using sorting algorithm
            if (debug)
            {
                Console.WriteLine("Array after the " + sortAlg + " sort");
                for (int i = 0; i < arr.Length; i++)
                {
                    Console.Write(arr[i] + " ");
                }
                Console.WriteLine(); Console.WriteLine();
            }

            // Stop the wait timer
            stopWatch.Stop();

            // Get the time elapsed for waiting
            elapsedTime = stopWatch.ElapsedTicks;

            // Print out the time in nanoseconds
            Console.WriteLine("The run time for sorting the " + arrayDesc + " array via "
                + sortAlg + "sort in milliseconds is " + elapsedTime + ". The number of data items are " + arr.Length + ".");

            Console.WriteLine();

            // 1 second delay
            Thread.Sleep(1000);
        }

        // Perform the bubble sort
        private static void BubbleSort(int[] intArray)
        {
            /* 
             * In bubble sort, we basically traverse the array from first to array_length - 
             * 1 position and compare the element with the next one. Element is swapped with 
             * the next element if the next element is greater. 
             * 
             * Bubble sort steps are as follows. 
             * 
             * 1. Compare array[0] & array[1] 2. If array[0] > array [1] swap it. 3. Compare 
             * array[1] & array[2] 4. If array[1] > array[2] swap it. ... 5. Compare 
             * array[n-1] & array[n] 6. if [n-1] > array[n] then swap it. After this step we 
             * will have largest element at the last index. 
             * 
             * Repeat the same steps for array[1] to array[n-1] 
             * 
             */

            int temp = 0;
            for (int i = 0; i < intArray.Length; i++)
            {

                for (int j = 0; j < intArray.Length - 1; j++)
                {

                    if (intArray[j] > intArray[j + 1])
                    {
                        temp = intArray[j + 1];
                        intArray[j + 1] = intArray[j];
                        intArray[j] = temp;
                    }
                }
            }
        }

        // Remove duplicates in an array using a set
        private static int[] SetUniqueElements(int[] inputArray)
        {

            // Create the set
            HashSet<int> set = new HashSet<int>(inputArray);

            /* 
             // Create the set 
            HashSet<int> set = new HashSet<int>(inputArray); 
            
            //create the temporary array 
            int[] tmp = new int[inputArray.Length]; 
            int index = 0; 
            
            // Use the set to remove duplicates and add to new array. 
            foreach (int i in inputArray) 
                if (set.Add(i)) 
                    tmp[index++] = i; 
            */

            // Return the array
            return set.ToArray();
        }

        // Quicksort and compare numbers
        private static void QuickSortAsc(int[] x, int low, int high)
        {
            if (x == null || x.Length == 0)
                return;

            if (low >= high)
                return;

            // Pick the pivot
            int middle = low + (high - low) / 2;
            int pivot = x[middle];

            // Make left < pivot and right > pivot
            int i = low, j = high;
            while (i <= j)
            {

                while (x[i] < pivot)
                {
                    i++;
                }

                while (x[j] > pivot)
                {
                    j--;
                }

                if (i <= j)
                {
                    int temp = x[i];
                    x[i] = x[j];
                    x[j] = temp;
                    i++;
                    j--;
                }

            }

            // Recursively sort two sub parts
            if (low < j)
                QuickSortAsc(x, low, j);

            if (high > i)
                QuickSortAsc(x, i, high);
        }

        // Random number methods
        private static readonly Random getrandom = new Random();

        public static int GetRandomNumber(int min, int max)
        {

            lock (getrandom)
            {
                return getrandom.Next(min, max);
            }
        }
    }
}