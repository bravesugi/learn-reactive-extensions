using System;
using System.Reactive.Linq;
using System.Threading;

namespace Rx_ColdObservable
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1病後に1秒間隔で値を発行するIObservable<int>を生成
            var source = Observable
                .Timer(
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(1))
                .Timestamp();

            // 購読
            var subscription1 = source.Subscribe(
                i => Console.WriteLine($"#1: OnNext({i.Value}) on {i.Timestamp:yyyy/MM/dd HH:mm:ss.FFF}"),
                ex => Console.WriteLine($"#1: OnError({ex.Message})"),
                () => Console.WriteLine($"#1 Completed()"));

            Thread.Sleep(3000);

            // 購読
            var subscription2 = source.Subscribe(
                i => Console.WriteLine($"#2: OnNext({i.Value}) on {i.Timestamp:yyyy/MM/dd HH:mm:ss.FFF}"),
                ex => Console.WriteLine($"#2: OnError({ex.Message})"),
                () => Console.WriteLine($"#2 Completed()"));

            Console.WriteLine($"Please enter key to call Dispose method...");
            Console.ReadLine();

            // 購読停止
            Console.WriteLine($"Dispose method call.");
            subscription1.Dispose();
            subscription2.Dispose();

            Console.WriteLine($"Please enter key to end...");
            Console.ReadLine();
        }
    }
}
