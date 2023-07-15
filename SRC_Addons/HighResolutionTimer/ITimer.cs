﻿namespace PSMultiServer.SRC_Addons.HighResolutionTimer
{
    public interface ITimer : IDisposable
    {
        void SetPeriod(int periodMS);

        void WaitForTrigger();

        void Start();

        void Stop();
    }
}
