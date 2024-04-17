using System;
using System.Diagnostics;

namespace SE456
{
    public class DeltaTimeDecrementObserver : ColObserver
    {
        public DeltaTimeDecrementObserver()
        {
            this.deltaTime = DeltaMan.Find(Delta.Name.Timer);
        }

        public override void Notify()
        {
            this.deltaTime.decrementDelta();
            //Debug.WriteLine("New delta time: " + this.deltaTime.getDelta());
        }

        public override void Dump()
        {
            //no - op
        }

        public override Enum GetName()
        {
            return Name.DeltaTimeDecrementObserver;
        }

        Delta deltaTime;
    }
}
