# aoc-2020
[Advent of Code 2020](https://adventofcode.com/2020) in F#

Requires [.NET 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0)

Modules and input files follow a naming convention to be invoked dynamically by command line argument. For example, for "Day 12 Part 1", executing `dotnet run d12p1` will invoke the `run` function in the `D12P1` module with `d12p1.input` as the input argument. If no "Part 2" input file is provided, then it will fallback on the corresponding "Part 1" input file. For example, executing `dotnet run d02p2` will use `d02p1.input` as its input file if `d02p2.input` does not exist. Results are outputed to stdout with elapsed timings.

Unit tests are written with NUnit and Unquote and co-located with source code files. e.g. you can find `D12P1` module tests in the `d12p1.fs` file in the `Tests` submodule at the end of the file. Tests may be run with `dotnet test`.
