//-----------------------------------------------------------------------------
// Copyright 2024, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using IrrKlang;
using System;
using System.Diagnostics;

namespace SE456
{
    class ShipStateMissileFlying : MissileShipState
    {
        public ShipStateMissileFlying(IrrKlang.ISoundSource snd) {
            this.sndSource = snd;
        }
        public override void Handle(Ship pShip)
        {
            pShip.SetMissileState(ShipMan.State.Ready);
        }
        public override void ShootMissile(Ship pShip)
        {
            //
        }

        private IrrKlang.ISoundSource sndSource;
    }
}

// --- End of File ---
