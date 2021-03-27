using System;
using System.Reactive.Linq;
using System.Threading;

namespace Rx_ToAsync
{
    class Program
    {
        static void Main(string[] args)
        {
            // 戻り値はFunc<IObservable<T>>
            var source = Observable.ToAsync(() =>
            {
                Console.WriteLine($"backgroudnd task start.");
                Thread.Sleep(2000);
                Console.WriteLine($"backgroudnd task end.");

                return 1;
            });

            // ToAsyncはデリゲートを返すのでInvoke() or () をしないと処理が開始されない
            Console.WriteLine($"souce() call.");
            var invokedSource = source.Invoke().Timestamp();
            var subscription1 = invokedSource.Subscribe(
                i => Console.WriteLine($"#1: OnNext({i.Value}) on {i.Timestamp:yyyy-MM-dd HH:mm:ss.FFF}"),
                ex => Console.WriteLine($"#1: OnError({ex.Message})"),
                () => Console.WriteLine($"#1 Completed()"));

            // 処理が確実に終わるように5秒待つ
            Console.WriteLine($"Sleep 5sec.");
            Thread.Sleep(5000);

            // Observableが発行する値の購読を停止
            Console.WriteLine($"dispose method ccall.");
            subscription1.Dispose();

            // 購読
            Console.WriteLine($"subscribe");
            var subscription2 = invokedSource.Subscribe(
                i => Console.WriteLine($"#2: OnNext({i.Value}) on {i.Timestamp:yyyy-MM-dd HH:mm:ss.FFF}"),
                ex => Console.WriteLine($"#2: OnError({ex.Message})"),
                () => Console.WriteLine($"#2 Completed()"));
            subscription2.Dispose();
        }
    }
}
