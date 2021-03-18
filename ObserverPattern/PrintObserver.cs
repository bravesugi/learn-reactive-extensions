using System;

namespace ObserverPattern
{
    public sealed class PrintObserver : IObserver<int>
    {
        public void OnCompleted()
        {
            Console.WriteLine($"{nameof(OnCompleted)} called.");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine($"{nameof(OnError)} {error.Message} called.");
        }

        public void OnNext(int value)
        {
            Console.WriteLine($"{nameof(OnNext)} {value} called.");
        }
    }
}
