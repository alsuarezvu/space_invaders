using System;
using System.Diagnostics;

namespace SE456
{
    public class ShipMoveLeftObserver : ColObserver
    {
        public override void Notify()
        {
            Ship pShip = ShipMan.GetShip();
            if (pShip != null) 
            {
                pShip.SetMoveState(ShipMan.State.MoveLeft);
            }  
 
        }

        public override void Dump()
        {
            //no-op
        }

        public override Enum GetName()
        {
            return Name.ShipMoveRight;
        }
    }
}
