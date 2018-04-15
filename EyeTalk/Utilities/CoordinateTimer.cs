
namespace EyeTalk.Utilities
{
    class CoordinateTimer
    {
        public System.Timers.Timer coordinateTimer;

        public CoordinateTimer()
        {
            coordinateTimer = new System.Timers.Timer();
            BeginTimer();
        }

        public void BeginTimer()
        {
            coordinateTimer = new System.Timers.Timer();
            coordinateTimer.Interval = 200;
            coordinateTimer.AutoReset = true;
            coordinateTimer.Enabled = true;
        }
    }
}
