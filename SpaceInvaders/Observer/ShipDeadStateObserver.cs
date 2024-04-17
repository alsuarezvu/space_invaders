using System;
using System.Diagnostics;

namespace SE456
{
    public class ShipDeadStateObserver : ColObserver
    {
        public ShipDeadStateObserver() { }

        public override void Dump()
        {
            throw new NotImplementedException();
        }

        public override Enum GetName()
        {
            throw new NotImplementedException();
        }

        public override void Notify()
        {
            Ship pShip = ShipMan.GetShip();

            if (pShip != null)
            {
                pShip.SetMoveState(ShipMan.State.MoveEnd);
                pShip.SetMissileState(ShipMan.State.Dead);
            }
        }
    }
}
