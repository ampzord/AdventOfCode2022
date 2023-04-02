using AdventOfCode2022.Puzzles;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode2022;

[MemoryDiagnoser]
public class BenchmarksClass
{
    // int n = 10_000;

    [Benchmark]
    public void SolutionBenchmark()
    {
        Day8.SolutionPart1();
    }
}