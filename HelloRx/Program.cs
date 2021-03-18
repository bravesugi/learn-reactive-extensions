using System;
using System.Reactive.Subjects;

namespace HelloRx
{
    class Program
    {
        /// <summary>
        /// <see cref="Subject{T}"/> の動作
        /// OnCompleted OnErrorが呼び出されると既存のオブザーバーがすべてクリアされる。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var source = new NumberObservable();

            var subscriber1 = source.Subscribe(
                // OnNext
                value => Console.WriteLine($"#1: OnNext({value}) called."),
                // OnError
                ex => Console.WriteLine($"#1: OnError({ex.Message}) called."),
                // OnCompleted
                () => Console.WriteLine($"#1: OnCompleted() called."));

            var subscriber2 = source.Subscribe(
                // OnNext
                value => Console.WriteLine($"#2: OnNext({value}) called."),
                // OnError
                ex => Console.WriteLine($"#2: OnError({ex.Message}) called."),
                // OnCompleted
                () => Console.WriteLine($"#2: OnCompleted() called."));

            Console.WriteLine($"## Execute(1)");
            source.Execute(1);

            Console.WriteLine($"## Dispose");
            subscriber2.Dispose();

            Console.WriteLine($"## Execute(2)");
            source.Execute(2);

            Console.WriteLine($"## Execute(0)");
            source.Execute(0);

            Console.WriteLine($"## Execute(5)");
            source.Execute(5);

            var subscriber3 = source.Subscribe(
                // OnNext
                value => Console.WriteLine($"#3: OnNext({value}) called."),
                // OnError
                ex => Console.WriteLine($"#3: OnError({ex.Message}) called."),
                // OnCompleted
                () => Console.WriteLine($"#3: OnCompleted() called."));
            Console.WriteLine($"## Completed");
            source.Completed();

            var subscriber4 = source.Subscribe(
                // OnNext
                value => Console.WriteLine($"#4: OnNext({value}) called."),
                // OnError
                ex => Console.WriteLine($"#4: OnError({ex.Message}) called."),
                // OnCompleted
                () => Console.WriteLine($"#4: OnCompleted() called."));

            Console.WriteLine($"## Execute(99)");
            source.Execute(99);

            subscriber1?.Dispose();
            subscriber2?.Dispose();
            subscriber3?.Dispose();
            subscriber4?.Dispose();
        }
    }
}
