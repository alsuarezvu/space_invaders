using System;
using System.Diagnostics;

namespace SE456
{
    public class DelayedUFORemoval : Command
    {
        public DelayedUFORemoval(RemoveUFOObserver _observer)
        {
            observer = _observer;
        }
        public override void Execute(Delta deltaTime)
        {
            GameObject gameObject = observer.getUFO();
            if (gameObject.bMarkForDeath == false)
            {
                gameObject.bMarkForDeath = true;
                DelayedObjectMan.Attach(observer);
            }
        }

        private RemoveUFOObserver observer;
    }
}
