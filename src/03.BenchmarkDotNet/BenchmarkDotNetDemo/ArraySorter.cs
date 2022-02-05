using System;
using System.Linq;

using BenchmarkDotNet.Attributes;

namespace BenchmarkDotNetDemo
{
    public class ArraySorter
    {
        private readonly string[] words = new[]
        {
            "dev.bg", "useful", ".net", "libraries", "stoyan", "filip"
        };

        [Benchmark]
        public void Sort()
        {
            string[] generatedWords = Enumerable.Range(1, 5000)
                .Select(x => $"{x}{words[x % 6]}{x}")
                .ToArray();

            Array.Sort(generatedWords);
        }
    }
}
