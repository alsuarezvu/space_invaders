//-----------------------------------------------------------------------------
// Copyright 2024, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    class ShipStateReady : MissileShipState
    {
        public override void Handle(Ship pShip)
        {
            pShip.SetMissileState(ShipMan.State.MissileFlying);
        }

        public override void ShootMissile(Ship pShip)
        {
            Missile pMissile = ShipMan.ActivateMissile();

            pMissile.SetPos(pShip.x, pShip.y + 20);
            //pMissile.SetActive(true);

            // switch states
            this.Handle(pShip);
        }

    }
}

// --- End of File ---
