using System;
using System.Diagnostics;

namespace SE456
{
    public class ShipMoveRight : MoveShipState
    {
        public override void Handle(Ship pShip)
        {
            pShip.SetMoveState(ShipMan.State.MoveLeftRight);
        }

        public override void MoveRight(Ship pShip)
        {
            pShip.x += pShip.shipSpeed;
            this.Handle(pShip);
            
        }

        public override void MoveLeft(Ship pShip)
        {
            //no-op
        }
    }
}
