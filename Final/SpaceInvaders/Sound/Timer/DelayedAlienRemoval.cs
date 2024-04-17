using System;
using System.Diagnostics;

namespace SE456
{
    public class DelayedAlienRemoval : Command
    {
        public DelayedAlienRemoval(RemoveAlienObserver _observer)
        {
            observer = _observer;
        }
        public override void Execute(Delta deltaTime)
        {
            GameObject gameObject = observer.getAlien();
            if (gameObject.bMarkForDeath == false)
            {
                gameObject.bMarkForDeath = true;
                DelayedObjectMan.Attach(observer);
            }
        }

        private RemoveAlienObserver observer;    
    }
}
