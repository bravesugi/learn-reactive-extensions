using System;
using System.Reactive.Subjects;

namespace HelloRx
{
    public sealed class NumberObservable : IObservable<int>
    {
        private Subject<int> _source = new Subject<int>();

        public void Execute(int value)
        {
            if (value == 0)
            {
                _source.OnError(new Exception("value is 0"));

                _source = new Subject<int>();
                return;
            }

            _source.OnNext(value);
        }

        public void Completed()
        {
            _source.OnCompleted();
        }

        public IDisposable Subscribe(IObserver<int> observer)
        {
            return _source.Subscribe(observer);
        }
    }
}
