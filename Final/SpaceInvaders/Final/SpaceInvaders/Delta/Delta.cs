using System;
using System.Diagnostics;

namespace SE456
{
    public class Delta : DLink
    {

        //-----------------------------------------------------------
        // Enum
        //-----------------------------------------------------------
        public enum Name
        {
            Move,
            Timer,
            Bomb,
            UFO,
            AlienRemoval,
            UFORemoval,
            BombRemoval,

            Uninitialized
        }
        public Delta()
        {
            new Delta(Delta.Name.Uninitialized, 0.0f, 0.0f); ;
        }

        public Delta(Name _name, float _initialDelta, float _targetDelta)
        {
            this.name = _name;
            this.targetDelta = _targetDelta;
            this.initialDelta = _initialDelta;

            this.delta = _initialDelta;
            if (this.delta > this.targetDelta)
            {
                this.increment = (this.delta - this.targetDelta) / NUM_DECREMENT;
            }
            else
            {
                this.increment = (this.targetDelta - this.delta) / NUM_DECREMENT;
            }

            this.reAdd = true;
            //Debug.WriteLine("Calculated Increment: " + this.increment);
        }

        public void setReAdd(bool _reAdd)
        {
            this.reAdd |= _reAdd;
        }

        public bool getReAdd()
        {
            return this.reAdd;
        }
        public void Set(Name _name, float _initialDelta, float _targetDelta)
        {
            this.name = _name;
            this.targetDelta = _targetDelta;

            this.delta = _initialDelta;
            if (this.delta > this.targetDelta)
            {
                this.increment = (this.delta - this.targetDelta) / NUM_DECREMENT;
            }
            else
            {
                this.increment = (this.targetDelta - this.delta) / NUM_DECREMENT;
            }
            //Debug.WriteLine("Calculated Increment: " + this.increment);

        }

        public void decrementDelta()
        {
            this.delta -= this.increment;
        }

        public void incrementDelta()
        {
              this.delta += this.increment;
        }

        public float getDelta(int num)
        {
            if (this.initialDelta > this.targetDelta)
            {
                return num * ((this.initialDelta - this.targetDelta) / NUM_DECREMENT);
            }
            else
            {
                return num * ((this.targetDelta - this.initialDelta) / NUM_DECREMENT);
            }
        }
        public float getDelta()
        {
            return this.delta;
        }

        public void setIncrement(float _increment)
        {
            this.increment = _increment;
        }

        public void setDelta(float _delta)
        {
            this.delta = _delta;
        }

        public override void Wash()
        {
            name = Name.Uninitialized;
            increment = 0.0f ;
            targetDelta = 0.0f;
            delta = 0.0f;
    }

        public override void Dump()
        {
            // Dump - Print contents to the debug output window
            //        Using HASH code as its unique identifier 
            Debug.WriteLine("   Name: {0} ({1})", this.name, this.GetHashCode());

            // Data:
            Debug.WriteLine("      Delta: {0}", this.delta);
            Debug.WriteLine("   Increment: {0}", this.increment);
            Debug.WriteLine(" Target Delta Time: {0}", this.targetDelta);

            // Let the base print its contribution
            this.baseDump();
        }

        public override Enum GetName()
        {
            return name;
        }

        //Data
        float initialDelta;
        float increment;
        float targetDelta;
        float delta;
        bool reAdd;
        public Name name;

        private static readonly int NUM_DECREMENT = 55;
    }
}
