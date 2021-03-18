using System;
using System.Reactive.Linq;

namespace Rx_Range
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1空始まる値を10個発行するIObservable<int>を作成する。
            var source = Observable.Range(1, 10);

            // 購読
            var subscription = source.Subscribe(
                i => Console.WriteLine($"OnNext({i})"),
                ex => Console.WriteLine($"OnError({ex.Message}"),
                () => Console.WriteLine($"Completed"));

            // 購読停止
            subscription.Dispose();
        }
    }
}
