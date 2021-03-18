using System;

namespace ObserverPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var source = new NumberObservable();

            var subscriber1 = source.Subscribe(new PrintObserver());
            var subscriber2 = source.Subscribe(new PrintObserver());

            Console.WriteLine($"## Execute(1)");
            source.Execute(1);

            Console.WriteLine($"## Dispose");
            subscriber2.Dispose();

            Console.WriteLine($"## Execute(2)");
            source.Execute(2);

            Console.WriteLine($"## Execute(0)");
            source.Execute(0);

            var subscriber3 = source.Subscribe(new PrintObserver());
            Console.WriteLine($"## Completed");
            source.Completed();
        }
    }
}
