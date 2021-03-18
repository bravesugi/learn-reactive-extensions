using System;
using System.Reactive.Linq;
using System.Threading;

namespace Rx_FromEvent
{
    class Program
    {
        static void Main(string[] args)
        {
            // イベントを発行するクラス
            var eventSource = new EventSource();
            var source = Observable
                .FromEvent<EventHandler, EventArgs>(
                    h => (s, e) => h(e),
                    // 普通は h => eventSource.Raised += h だけでよい
                    h =>
                    {
                        Console.WriteLine($"Add handler");
                        eventSource.Raised += h;
                    },
                    // 普通は h => eventSource.Raised -= h だけでよい
                    h =>
                    {
                        Console.WriteLine($"Remove handler");
                        eventSource.Raised -= h;
                    })
                .Timestamp();

            // 購読
            var subscription1 = source.Subscribe(
                i => Console.WriteLine($"#1: OnNext({i.Value}) on {i.Timestamp:yyyy/MM/dd HH:mm:ss.FFF}"),
                ex => Console.WriteLine($"#1: OnError({ex.Message})"),
                () => Console.WriteLine($"#1 Completed()"));
            // 購読
            var subscription2 = source.Subscribe(
                i => Console.WriteLine($"#2: OnNext({i.Value}) on {i.Timestamp:yyyy/MM/dd HH:mm:ss.FFF}"),
                ex => Console.WriteLine($"#2: OnError({ex.Message})"),
                () => Console.WriteLine($"#2 Completed()"));

            eventSource.OnRaised();
            eventSource.OnRaised();

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
