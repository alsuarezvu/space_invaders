//-----------------------------------------------------------------------------
// Copyright 2024, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    class MoveLeftObserver : InputObserver
    {
        public override void Notify()
        {
            if (ShipMan.GetShip() != null)
            {
                //Debug.WriteLine("Move Right");
                Ship pShip = ShipMan.GetShip();
                pShip.MoveLeft();
            }
        }


        override public void Dump()
        {
            Debug.Assert(false);
        }
        override public System.Enum GetName()
        {
            return Name.MoveLeftObserver;
        }

    }
}

// --- End of File ---
