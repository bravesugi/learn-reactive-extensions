using System;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Rx_Timer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // ３秒後から１秒間隔で値を発行するIObservable<int>を作成
            var source = Observable
                .Timer(
                    TimeSpan.FromSeconds(3),
                    TimeSpan.FromSeconds(3))
                .Timestamp();

            // 購読
            var subscription1 = source.Subscribe(
                i => Console.WriteLine($"[{i.Timestamp:HH:mm:ss:fff}] OnNext({i.Value.ToString()})"),
                ex => Console.WriteLine($"OnError({ex.Message})"),
                () => Console.WriteLine($"Completed()"));

            await Task.Delay(1500).ConfigureAwait(false);

            var subscription2 = source.Subscribe(
                i => Console.WriteLine($"[{i.Timestamp:HH:mm:ss:fff}] OnNext({i.Value.ToString()})"),
                ex => Console.WriteLine($"OnError({ex.Message})"),
                () => Console.WriteLine($"Completed()"));

            Console.WriteLine($"Please enter key to call Dispose method...");
            Console.ReadLine();

            // 購読停止
            Console.WriteLine($"# Dispose method call.");
            subscription1.Dispose();

            Console.WriteLine(
                $"Please enter key to stop {nameof(subscription2)}...");
            Console.ReadLine();
            subscription2.Dispose();

            Console.WriteLine($"Please enter key...");
            Console.ReadLine();
        }
    }
}
