//-----------------------------------------------------------------------------
// Copyright 2024, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    abstract public class MissileShipState
    {
        // state()
        public abstract void Handle(Ship pShip);

        // strategy()
        public abstract void ShootMissile(Ship pShip);

    }
}

// --- End of File ---
