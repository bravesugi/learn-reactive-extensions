using System;
using System.Reactive.Linq;

namespace Rx_Throw
{
    class Program
    {
        static void Main(string[] args)
        {
            // エラーを発行するだけのIObservable<int>を生成
            var source = Observable.Throw<int>(
                new Exception($"This is error message"));

            // 購読
            var subscription1 = source.Subscribe(
                i => Console.WriteLine($"OnNext({i})"),
                ex => Console.WriteLine($"OnError({ex.Message})"),
                () => Console.WriteLine($"Completed()"));
            var subscription2 = source.Subscribe(
                i => Console.WriteLine($"OnNext({i})"),
                ex => Console.WriteLine($"OnError({ex.Message})"),
                () => Console.WriteLine($"Completed()"));

            // 購読停止
            Console.WriteLine($"# Dispose method call.");
            subscription1.Dispose();
            subscription2.Dispose();
        }
    }
}
