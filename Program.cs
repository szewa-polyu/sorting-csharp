using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace QuickSort
{
    class Program
    {
        static IEnumerable<int> numbers = new int[] { 10, 80, 30, 90, 40, 50, 70 };        

        static void Main(string[] args)
        {            
            Console.WriteLine("Original Array:");
            Console.WriteLine(ToString(numbers));

            Func<IEnumerable<int>, IEnumerable<int>> SortFunc;
            //SortFunc = QuickSort;
            SortFunc = MergeSort;

            Console.WriteLine("Sorted Array:");
            Console.WriteLine(ToString(SortFunc(numbers)));
        }

        static IEnumerable<T> QuickSort<T>(IEnumerable<T> list)
            where T : IComparable
        {     
            //Debug.WriteLine(ToString(list));

            if (!list.Any())
            {
                return new T[] {};
            }

            if (list.Count() == 1)
            {
                return list;
            }

            var first = list.First();
            var trimmedList = list.Skip(1);
            var biggerThanFirst = trimmedList.Where(x => x.CompareTo(first) > 0);
            var smallerThanOrEqualToFirst = trimmedList.Except(biggerThanFirst);

            var sorted = QuickSort(smallerThanOrEqualToFirst).Concat(new T[] { first })
                .Concat(QuickSort(biggerThanFirst));

            return sorted;
        }

        static IEnumerable<T> MergeSort<T>(IEnumerable<T> list)
            where T : IComparable
        {
            if (!list.Any())
            {
                return new T[] {};
            }

            int listLength = list.Count();
            if (listLength == 1)
            {
                return list;
            }

            var halfLengthOfList = listLength / 2;
            var sortedFirstHalf = MergeSort(list.Take(halfLengthOfList)).ToArray();            
            var sortedSecondHalf = MergeSort(list.Skip(halfLengthOfList)).ToArray();            

            var sorted = MergeByOrder(sortedFirstHalf, sortedSecondHalf);

            return sorted;
        }

        static IEnumerable<T> MergeByOrder<T>(IEnumerable<T> list1, IEnumerable<T> list2)
            where T : IComparable
        {
            var array1 = list1.ToArray();
            var array2 = list2.ToArray();

            var array1Idx = 0;
            var array2Idx = 0;
            
            var array1Length = array1.Length;
            var array2Length = array2.Length;

            var resultantArray = new List<T>(array1Length + array2Length);

            while (array1Idx < array1Length && array2Idx < array2Length)
            {
                T nextToEnterResultant;

                if (array1[array1Idx].CompareTo(array2[array2Idx]) < 0)
                {
                    nextToEnterResultant = array1[array1Idx++];
                }
                else
                {
                    nextToEnterResultant = array2[array2Idx++];                    
                }

                resultantArray.Add(nextToEnterResultant);
            }

            IEnumerable<T> leftOverSublist = new T[] {};
            if (array1Idx < array1Length)
            {
                leftOverSublist = array1.Skip(array1Idx);
            }
            else if (array2Idx < array2Length)
            {
                leftOverSublist = array2.Skip(array2Idx);
            }
            resultantArray.AddRange(leftOverSublist);
            
            return resultantArray;
        }

        static string ToString<T>(IEnumerable<T> list, string delimiter = ",")
        {
            if (!list.Any())
            {
                return "";
            }

            string result = "";
            foreach (var item in list)
            {
                result += item.ToString() + delimiter;
            }
            return result.Substring(0, result.Length - delimiter.Length);
        }
    }
}
