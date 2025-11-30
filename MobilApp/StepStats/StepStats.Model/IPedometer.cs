using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepStats.Model
{
    public class StepsEventArgs : EventArgs
    {
        public int Steps { get; private set; }
        public StepsEventArgs(int steps)
        {
            Steps = steps;
        }
    }
    public interface IPedometer : IDisposable
    {
        event EventHandler<StepsEventArgs>? StepsChanged;
        bool IsAvailable();
        Task StartTrackingAsync();
        void StopTracking();
    }
}
