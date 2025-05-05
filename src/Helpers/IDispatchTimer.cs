using System;

namespace Src.Services
{
    public interface IDispatchTimer
    {
        event EventHandler Tick;
        void Start();
        void Stop();
    }
}
