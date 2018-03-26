﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeTalk.Utilities
{
    class CoordinateTimer
    {
        public System.Timers.Timer coordinateTimer;

        public CoordinateTimer(){
            coordinateTimer = new System.Timers.Timer();
        }

        public void BeginTimer()
        {
            coordinateTimer = new System.Timers.Timer();
            coordinateTimer.Interval = 125;
            //timer.Elapsed += Check;
            coordinateTimer.AutoReset = true;
            coordinateTimer.Enabled = true;
        }
    }