//-----------------------------------------------------------------------------
// Copyright 2024, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    class ShipStateDead : MissileShipState
    {
        public override void Handle(Ship pShip)
        {
            //pShip.SetMissileState(ShipMan.State.Ready);
        }
        public override void ShootMissile(Ship pShip)
        {

        }
    }
}

// --- End of File ---
