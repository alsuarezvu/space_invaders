using System;
using System.Diagnostics;

namespace SE456
{
    public class ShipMoveLeftRight : MoveShipState
    {
        public override void Handle(Ship pShip)
        {
            //no - op
        }

        public override void MoveRight(Ship pShip)
        {
            pShip.x += pShip.shipSpeed;
        }

        public override void MoveLeft(Ship pShip)
        {
            pShip.x -= pShip.shipSpeed;
        }

    }
}
