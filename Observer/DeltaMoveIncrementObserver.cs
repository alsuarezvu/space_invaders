using System;
using System.Diagnostics;

namespace SE456
{
    public class DeltaMoveIncrementObserver : ColObserver
    {
        public DeltaMoveIncrementObserver()
        {
            this.deltaMove = DeltaMan.Find(Delta.Name.Move);
        }

        public override void Notify()
        {
            this.deltaMove.incrementDelta();
            //Debug.WriteLine("New delta move: " + this.deltaMove.getDelta());
        }

        public override void Dump()
        {
            //no - op
        }

        public override Enum GetName()
        {
            return Name.DeltaMoveIncrementObserver;
        }

        Delta deltaMove;
    }
}
