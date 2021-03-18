using System;
using System.Reactive.Linq;
using System.Threading;

namespace Rx_Start
{
    class Program
    {
        static void Main(string[] args)
        {
            var source = Observable.Start(() =>
            {
                Console.WriteLine($"Background task has started.");
                Thread.Sleep(2000);
                Console.WriteLine($"Background task has ended.");

                return 1;
            }).Timestamp();

            // 購読
            Console.WriteLine($"Subscribe1");
            var subscription1 = source.Subscribe(
                i => Console.WriteLine($"#1: OnNext({i.Value}) on {i.Timestamp:yyyy/MM/dd HH:mm:ss.FFF}"),
                ex => Console.WriteLine($"#1: OnError({ex.Message})"),
                () => Console.WriteLine($"#1 Completed()"));

            // 処理が確実に終わるように５秒待つ
            Console.WriteLine($"Sleep 5seconds.");
            Thread.Sleep(5000);

            // 購読停止
            Console.WriteLine($"Dispose method call.");
            subscription1.Dispose();

            // 購読
            Console.WriteLine($"Subscribe2");
            var subscription2 = source.Subscribe(
                i => Console.WriteLine($"#2: OnNext({i.Value}) on {i.Timestamp:yyyy/MM/dd HH:mm:ss.FFF}"),
                ex => Console.WriteLine($"#2: OnError({ex.Message})"),
                () => Console.WriteLine($"#2 Completed()"));
            
            // 購読停止
            Console.WriteLine($"Dispose method call.");
            subscription1.Dispose();

            Console.WriteLine($"Please enter key to end...");
            Console.ReadLine();
        }
    }
}
