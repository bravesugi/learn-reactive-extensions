using System;
using System.Reactive.Linq;

namespace Rx_Repeat
{
    class Program
    {
        static void Main(string[] args)
        {
            // 2を5回発行するIObservable<int>を作成する
            var source = Observable.Repeat(2, 5);

            // それを３回繰り返すIObservable<int>を作成する。
            source = source.Repeat(3);

            // 購読
            var subscription1 = source.Subscribe(
                i => Console.WriteLine($"OnNext({i})"),
                ex => Console.WriteLine($"OnError({ex})"),
                () => Console.WriteLine($"OnCompleted()"));

            // 購読の停止
            subscription1.Dispose();
        }
    }
}
