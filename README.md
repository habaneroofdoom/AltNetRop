# Railway-Oriented Programming in C#

This is a simple project intended to show a simple implementation of Railway-Oriented programming using C#

It's based on the amazing work done by Scott Wlaschin at [F# For Fun and Profit](https://fsharpforfunandprofit.com/rop/)
I can't recommend this site highly enough!

## How to run

You need to have [.NET Core](https://www.microsoft.com/net/download/core) installed.
This was done using the .NET Core 2.0 Preview 2 with Visual Studio Code to edit, but there's no reason why it shouldn't work on other versions.
Given it's just plain C# with no tricks, it should work fine in visual studio as well.
Although it'd be pretty easy to remove I've used a few C# 6 features (nameof) so you'll need VS 2015 or later to get it to build.

There are four versions of the code:
 1. The optimistic "Happy Path"
 2. The unfortunate "Sad Path"
 3. The safer but clunky "Safe Path"
 4. The railway-oriented "Awesome Path"

 If you look at `Program.cs` you can just execute using different IOC contexts to run the different versions.
 eg. `Handle<V4.V4Registry>(evt);` will run the V4 ROP version.

To build:
 * Change to the Demo directory
 * Run command `dotnet build`

To execute:
 * Change to the Demo directory
 * Run command `dotnet run`

To run the tests:
 * Change to the Tests directory
 * Run command `dotnet test`

Note that these are all set up as build tasks, so in VS Code you can just use *Run Task...* or do F5 to run the code