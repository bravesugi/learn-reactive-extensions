using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Rx_Defer
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1,2,3と順番に値を発行して終了するIObservable<int>を作成する。
            var source = Observable.Defer<int>(() =>
            {
                Console.WriteLine($"# Defer method called");

                var s = new ReplaySubject<int>();
                //var s = new Subject<int>();
                s.OnNext(1);
                s.OnNext(2);
                s.OnNext(3);
                s.OnCompleted();

                return s.AsObservable();
            });

            // 購読
            var subscription1 = source.Subscribe(
                i => Console.WriteLine($"OnNext({i})"),
                ex => Console.WriteLine($"OnError({ex.Message}"),
                () => Console.WriteLine($"Completed"));
            var subscription2 = source.Subscribe(
                i => Console.WriteLine($"OnNext({i})"),
                ex => Console.WriteLine($"OnError({ex.Message}"),
                () => Console.WriteLine($"Completed"));

            // 購読停止
            subscription1.Dispose();
            subscription2.Dispose();
        }
    }
}
