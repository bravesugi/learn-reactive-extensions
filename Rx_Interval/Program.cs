using System;
using System.Reactive.Linq;

namespace Rx_Interval
{
    class Program
    {
        static void Main(string[] args)
        {
            var source = Observable
                .Interval(TimeSpan.FromSeconds(1))
                .Timestamp();

            // 購読
            var subscription1 = source.Subscribe(
                i => Console.WriteLine($"[{i.Timestamp:yyyy-MM-dd HH:mm:ss:fff}] OnNext({i.Value.ToString()})"),
                ex => Console.WriteLine($"OnError({ex.Message})"),
                () => Console.WriteLine($"Completed()"));
            var subscription2 = source.Subscribe(
                i => Console.WriteLine($"[{i.Timestamp:yyyy-MM-dd HH:mm:ss:fff}] OnNext({i.Value.ToString()})"),
                ex => Console.WriteLine($"OnError({ex.Message})"),
                () => Console.WriteLine($"Completed()"));

            Console.WriteLine($"Please enter key to call Dispose method...");
            Console.ReadLine();

            // 購読停止
            Console.WriteLine($"# Dispose method call.");
            subscription1.Dispose();
            subscription2.Dispose();

            Console.WriteLine($"Please enter key...");
            Console.ReadLine();
        }
    }
}
