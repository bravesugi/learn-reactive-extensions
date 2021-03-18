using System;
using System.Collections.Generic;

namespace ObserverPattern
{
    public sealed class NumberObservable : IObservable<int>
    {
        private readonly List<IObserver<int>> _observers = new List<IObserver<int>>();

        public void Execute(int value)
        {
            if (value == 0)
            {
                foreach (var obs in _observers)
                {
                    obs.OnError(new Exception("value is 0"));
                }

                _observers.Clear();
                return;
            }

            foreach (var obs in _observers)
            {
                obs.OnNext(value);
            }
        }

        public void Completed()
        {
            foreach (var obs in _observers)
            {
                obs.OnCompleted();
            }

            _observers.Clear();
        }

        public IDisposable Subscribe(IObserver<int> observer)
        {
            _observers.Add(observer);
            return new RemoveListDisposable(_observers, observer);
        }

        private class RemoveListDisposable : IDisposable
        {
            private List<IObserver<int>> _observers = new List<IObserver<int>>();
            private IObserver<int> _observer;

            public RemoveListDisposable(List<IObserver<int>> observers, IObserver<int> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if (_observers is null)
                {
                    return;
                }

                if (_observers.IndexOf(_observer) != -1)
                {
                    _observers.Remove(_observer);
                }

                _observers = null;
                _observer = null;
            }
        }
    }
}
