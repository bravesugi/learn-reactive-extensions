using System;
using System.Reactive.Linq;
using System.Threading;
using System.Timers;

namespace Rx_HotObservable
{
    class Program
    {
        static void Main(string[] args)
        {
            // １秒間隔で値を発行するTimer
            var timer = new System.Timers.Timer(1000);
            var source = Observable
                .FromEvent<ElapsedEventHandler, ElapsedEventArgs>(
                    h => (s, e) => h(e),
                    h => timer.Elapsed += h,
                    h => timer.Elapsed -= h)
                .Timestamp();
            timer.Start();

            // 購読
            var subscription1 = source.Subscribe(
                i => Console.WriteLine($"#1: OnNext({i.Value.SignalTime:yyyy/MM/dd HH:mm:ss.FFF}) on {i.Timestamp:yyyy/MM/dd HH:mm:ss.FFF}"),
                ex => Console.WriteLine($"#1: OnError({ex.Message})"),
                () => Console.WriteLine($"#1 Completed()"));

            Thread.Sleep(3000);

            // 購読
            var subscription2 = source.Subscribe(
                i => Console.WriteLine($"#2: OnNext({i.Value.SignalTime:yyyy/MM/dd HH:mm:ss.FFF}) on {i.Timestamp:yyyy/MM/dd HH:mm:ss.FFF}"),
                ex => Console.WriteLine($"#2: OnError({ex.Message})"),
                () => Console.WriteLine($"#2 Completed()"));

            Console.WriteLine($"Please enter key to call Dispose method...");
            Console.ReadLine();

            // 購読停止
            Console.WriteLine($"Dispose method call.");
            subscription1.Dispose();
            subscription2.Dispose();
            timer.Stop();

            Console.WriteLine($"Please enter key to end...");
            Console.ReadLine();
        }
    }
}
