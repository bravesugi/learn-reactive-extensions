using System;
using System.Reactive.Linq;

namespace Rx_Create
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1,2,3と順番に値を発行して終了するIObservable<int>を作成する。
            var source = Observable.Create<int>(observer =>
            {
                Console.WriteLine($"# {nameof(Observable.Create)} method called");

                observer.OnNext(1);
                observer.OnNext(2);
                observer.OnNext(3);

                // 下をコメントアウトすればDisposeのタイミングで呼び出せる
                observer.OnCompleted();

                return () =>
                {
                    // すでにCompletedの後なのですぐに呼ばれる
                    Console.WriteLine($"Disposable action");
                };
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
            Console.WriteLine($"# Dispose method call.");
            subscription1.Dispose();
            subscription2.Dispose();
        }
    }
}
