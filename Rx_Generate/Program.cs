using System;
using System.Reactive.Linq;

namespace Rx_Generate
{
    class Program
    {
        static void Main(string[] args)
        {
            // IObservable<int>を作成する。
            // for (var i = 0; i < 10; i++)
            // {
            //     yield return i;
            // }
            var source = Observable
                .Generate(
                    0,
                    i => i < 10,
                    i => ++i,
                    i => i * i,
                    i => TimeSpan.FromMilliseconds(i * i * 100))
                .Timestamp();

            // 購読
            var subscription = source.Subscribe(
                i => Console.WriteLine($"{i.Timestamp.LocalDateTime} OnNext({i.Value.ToString()})"),
                ex => Console.WriteLine($"OnError({ex.Message})"),
                () => Console.WriteLine($"Completed()"));

            Console.WriteLine($"Please enter key...");
            Console.ReadLine();

            // 購読停止
            Console.WriteLine($"Dispose method call.");
            subscription.Dispose();

            Console.WriteLine($"Please enter key to end...");
            Console.ReadLine();
        }
    }
}
