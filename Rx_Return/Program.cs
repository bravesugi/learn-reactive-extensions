using System;
using System.Reactive.Linq;

namespace Rx_Return
{
    class Program
    {
        static void Main(string[] args)
        {
            // 10を発行するIObservable<int>を作成する。
            var source = Observable.Return(10);

            // 購読
            var subscription1 = source.Subscribe(
                i => Console.WriteLine($"OnNext({i})"),
                ex => Console.WriteLine($"OnError({ex})"),
                () => Console.WriteLine($"OnCompleted()"));

            // 購読の停止
            subscription1.Dispose();

            // 購読
            var subscription2 = source.Subscribe(
                i => Console.WriteLine($"OnNext({i})"),
                ex => Console.WriteLine($"OnError({ex})"),
                () => Console.WriteLine($"OnCompleted()"));

            // 購読の停止
            subscription2.Dispose();
        }
    }
}
