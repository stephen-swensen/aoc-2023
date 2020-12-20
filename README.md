# aoc-2020
Advent of Code 2020 in F#

Modules and input files follow a naming convention to be invoked dynamically by command line argument. For example, to run "Day 12 Part 1", `dotnet run d12p1` will invoke the `run` function in the `D12P1` module with `d12p1.input` as the input argument. The result will be outputed to stdout with elapsed timings.
