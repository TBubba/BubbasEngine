using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BubbasEngine.Engine.Timing
{
    public class TimeManagerArgs
    {
        // Updates
        public int StepsPerSecond;
        public bool Accumulate;

        // Constructor(s)
        public TimeManagerArgs()
        {
            StepsPerSecond = 60;
            Accumulate = true;
        }
        public TimeManagerArgs(TimeManagerArgs args)
        {
            StepsPerSecond = args.StepsPerSecond;
            Accumulate = args.Accumulate;
        }
    }
}
