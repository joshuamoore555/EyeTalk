
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
            //Starts a timer which loops every 200 milliseconds. Used to call the check position method
            coordinateTimer = new System.Timers.Timer();
            coordinateTimer.Interval = 200;
            coordinateTimer.AutoReset = true;
            coordinateTimer.Enabled = true;
        }
    }
}
