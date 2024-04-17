﻿using System;
using System.Diagnostics;

namespace SE456
{
    public class DelayedBombRemoval : Command
    {
        public DelayedBombRemoval(RemoveBombWithSplatObserver _observer)
        {
            observer = _observer;
        }
        public override void Execute(Delta deltaTime)
        {
            GameObject gameObject = observer.getBomb();
            if (gameObject.bMarkForDeath == false)
            {
                gameObject.bMarkForDeath = true;
                DelayedObjectMan.Attach(observer);
            }
        }

        private RemoveBombWithSplatObserver observer;
    }
}
