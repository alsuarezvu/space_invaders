using System;
using System.Diagnostics;

namespace SE456
{
    public class DeltaTime
    {
        public DeltaTime()
        {
            this.initialDelta = 0.0f;
            this.targetDeltaTime = 0.0f;

            this.delta = this.initialDelta;
            this.increment = this.initialDelta - this.targetDeltaTime;
        }

        public DeltaTime(float _initialDelta, float _targetEndDelta)
        {
            this.initialDelta = _initialDelta;
            this.targetDeltaTime = _targetEndDelta;

            this.delta = this.initialDelta;
            this.increment = (this.delta - this.targetDeltaTime) / NUM_DECREMENT;
            //Debug.WriteLine("Calculated Increment: " + this.increment);
        }

        public void decrementDelta()
        {
            this.delta -= this.increment;
        }

        public float getDelta()
        {
            return this.delta;
        }

        public void setIncrement(float _increment)
        {
            this.increment = _increment;
        }

        ///Data
        float initialDelta;
        float increment;
        float targetDeltaTime;
        float delta;

        private static readonly int NUM_DECREMENT = 55;
    }
}
