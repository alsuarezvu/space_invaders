using System;
using System.Diagnostics;

namespace SE456
{
    public class ShipMoveLeft : MoveShipState
    {
        public override void Handle(Ship pShip)
        {
            pShip.SetMoveState(ShipMan.State.MoveLeftRight);
        }

        public override void MoveRight(Ship pShip)
        {
            //no-op
        }

        public override void MoveLeft(Ship pShip)
        {
            pShip.x -= pShip.shipSpeed;
            this.Handle(pShip);
        }
    }
}
