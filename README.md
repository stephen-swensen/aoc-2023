# aoc-fsharp

Contains Advent of Code in F# for multiple years. Navigate to directory such as `2020` or `2023` as working dir for running commands.

[Advent of Code 2020](https://adventofcode.com/2020)

[Advent of Code 2023](https://adventofcode.com/2023)

Requires [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

Usage: `dotnet run <cmd1> <cmd2> ...` - if no commands are given, all discoverable puzzel commands are run (by searching for .fs files named according to command conventions).

Commands follow a naming convention. For example, `d02p2` means "Day 2 Part 2".

Input files follow a naming convention. For example, `d02p2.input` is the input file for the `d02p2` command. If `d02p2.input` does not exist, then `d02p1.input` is will tried as a fallback (Part 2 input puzzels are commonly the same as Part 1 input puzzels).

Modules follow a naming convention. For example, "D02P2" is the code module for the `d02p2` command. It must implement a function `run: () -> InputReader`

Unit tests are written with NUnit and Unquote and co-located with source code files. e.g. you can find `D12P1` module tests in the `d12p1.fs` file at the end of the file. Tests may be run with `dotnet test`.

