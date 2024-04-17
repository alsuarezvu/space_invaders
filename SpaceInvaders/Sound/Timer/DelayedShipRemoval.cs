using System;
using System.Diagnostics;

namespace SE456
{
    public class DelayedShipRemoval : Command
    {
        public DelayedShipRemoval(RemoveActiveShipObserver _observer)
        {
            observer = _observer;
        }
        public override void Execute(Delta deltaTime)
        {
            GameObject gameObject = observer.getShip();
            if (gameObject.bMarkForDeath == false)
            {
                gameObject.bMarkForDeath = true;
                DelayedObjectMan.Attach(observer);
            }
        }

        private RemoveActiveShipObserver observer;
    }
}
