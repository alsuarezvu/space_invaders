using System;
using System.Diagnostics;

namespace SE456
{
    public abstract class MoveShipState
    {
        // state()
        public abstract void Handle(Ship pShip);

        // strategy()
        public abstract void MoveRight(Ship pShip);
        public abstract void MoveLeft(Ship pShip);
    }
}
