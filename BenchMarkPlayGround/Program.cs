using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

namespace MyBenchmarks
{
    /// <summary>
    /// Runs benchmarks for different methods of ordering lists.
    /// </summary>
    [SimpleJob(RuntimeMoniker.Net70)]
    [SimpleJob(RuntimeMoniker.Net80)]
    public class SortComparing
    {
        /// <summary>
        /// Size of the list with the test data
        /// </summary>
        private const int listSize = 10000;

        /// <summary>
        /// test data used for the benchmark tests
        /// </summary>
        private readonly List<string> testData = new ();

        /// <summary>
        /// Initialize the test for the benchmarks
        /// </summary>
        public SortComparing()
        {
            Random rand = new();
            for (int i = 0; i < listSize; i++)
            {
                testData.Add(GetRandomString(rand, rand.Next(0, 35)));
            }
        }

        /// <summary>
        /// Creates a random string of the given length
        /// </summary>
        /// <param name="rand">Random instance</param>
        /// <param name="length">Length of the string to be generated</param>
        /// <returns>A random string of the given length</returns>
        private static string GetRandomString(Random rand, int length)
        { 
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                stringBuilder.Append(GetRandomChar(rand));
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Returns a random character
        /// </summary>
        /// <param name="rand">Random instance</param>
        /// <returns>A random character</returns>
        private static char GetRandomChar(Random rand)
        {
            int randValue = rand.Next(0, 26);
            return Convert.ToChar(randValue + 65);
        }

        /// <summary>
        /// Orders the test data using list.OrderBy
        /// </summary>
        [Benchmark]
        public void OrderBy()
        {
            List<string> testDataCopy = new (testData);
            List<string> ordered = testDataCopy.OrderBy(s => s).ToList();
        }

        /// <summary>
        /// Orders the test data using list.Sort
        /// </summary>
        [Benchmark]
        public void Sort()
        {
            List<string> testDataCopy = new (testData);
            testDataCopy.Sort();
        }

        /// <summary>
        /// Orders the test data using Array.Sort
        /// </summary>
        [Benchmark]
        public void SortAsArray()
        {
            List<string> testDataCopy = new (testData);
            string[] arrayData = testDataCopy.ToArray();
            Array.Sort(arrayData);
        }
    }

    public class Program
    {
        /// <summary>
        /// Run the benchmark tests
        /// </summary>
        public static void Main()
        {
            BenchmarkRunner.Run<SortComparing>();
        }
    }
}
