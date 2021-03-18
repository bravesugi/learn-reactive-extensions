using System;

namespace Rx_FromEvent
{
    /// <summary>
    /// イベント発行クラス
    /// </summary>
    internal sealed class EventSource
    {
        public event EventHandler Raised;

        public void OnRaised()
        {
            var h = Raised;
            h?.Invoke(this, EventArgs.Empty);
        }
    }
}