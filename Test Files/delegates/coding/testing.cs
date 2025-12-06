using System;


Console.WriteLine("Hello World!");

// Example: simple Action
Action<int> print = x => Console.WriteLine("Number: " + x);
print(42);