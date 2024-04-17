//-----------------------------------------------------------------------------
// Copyright 2024, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    public class ShipReadyObserver : ColObserver
    {
        public override void Notify()
        {
            Ship pShip = ShipMan.GetShip();

            if (pShip != null )
            {
                // Correction... only method that changes state is Handle
                // So correct this....
                // pShip.SetState(ShipMan.State.Ready);
                pShip.Handle();
            }
           

        }

        override public void Dump()
        {
            Debug.Assert(false);
        }
        override public System.Enum GetName()
        {
            return Name.ShipReadyObserver;
        }
    }

    // data
}

// --- End of File ---
