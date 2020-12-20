# aoc-2020
[Advent of Code 2020](https://adventofcode.com/2020) in F#

Modules and input files follow a naming convention to be invoked dynamically by command line argument. For example, to run "Day 12 Part 1", `dotnet run d12p1` will invoke the `run` function in the `D12P1` module with `d12p1.input` as the input argument. The result will be outputed to stdout with elapsed timings.

Unit tests are written with NUnit and Unquote and co-located with source code files. e.g. you can find `D12P1` module tests in the `d12p1.fs` file in the `Tests` submodule at the end of the file. Tests may be run with `dotnet test`.
