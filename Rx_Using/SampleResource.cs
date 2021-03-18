using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Rx_Using
{
    public sealed class SampleResource : IDisposable
    {
        public IObservable<string> GetData()
        {
            return Observable.Create<string>(observer =>
            {
                observer.OnNext("one");
                observer.OnNext("two");
                observer.OnNext("three");
                observer.OnCompleted();

                return Disposable.Empty;
            });
        }

        public void Dispose()
        {
            Console.WriteLine($"Resource.Dispose called.");
        }
    }
}
