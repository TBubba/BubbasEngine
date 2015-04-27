using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BubbasEngine.Engine.Timing
{
    internal class TimeKeeper
    {
        // Private
        private int _stepsPerSec = 0; // Amount of desiered steps per second
        private double _timeStep = 0d; // Amount of time (MS) needed for one step
        private double _accumulator = 0d; // Current amount of time (MS)
        private int _stepCount; // Current amount of steps (not yet "stepped")

        //
        internal double TimeStep
        { get { return _timeStep; } }
        internal double Accumulator
        { get { return _accumulator; } }
        internal int StepsPerSec
        { get { return _stepsPerSec; } }

        // Constructor(s)
        internal TimeKeeper(int updatesPerSec)
        {
            // Set interval
            _stepsPerSec = updatesPerSec;
            _timeStep = (double)(1d / updatesPerSec * 1000d);
        }

        // Step
        internal void Step(double delta)
        {
            // Add current delta time (MS)
            _accumulator += delta;

            // If there should be one ore more fixed steps
            if (_accumulator >= _timeStep)
            {
                // Count number of fixed steps
                _stepCount = (int)(_accumulator / _timeStep);

                // Remove fixed steps time from the accumulator
                _accumulator %= _timeStep;
            }
        }
        internal int GetStepCount()
        {
            // Gets and resets the count
            int count = _stepCount;
            _stepCount = 0;

            // Returns the count
            return count;
        }
    }
}
