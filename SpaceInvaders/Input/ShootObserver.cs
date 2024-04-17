//-----------------------------------------------------------------------------
// Copyright 2024, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    class ShootObserver : InputObserver
    {
        public ShootObserver(IrrKlang.ISoundEngine sndEngine, IrrKlang.ISoundSource sndSource) {
            this.soundEngine = sndEngine;
            this.soundSource = sndSource;
        }
        public override void Notify()
        {
            //Debug.WriteLine("Shoot Observer");
            Ship pShip = ShipMan.GetShip();
            if (pShip != null)
            {
                pShip.ShootMissile();
                soundEngine.Play2D(soundSource, false, false, false);
            }
        }
        override public void Dump()
        {
            Debug.Assert(false);
        }
        override public System.Enum GetName()
        {
            return Name.ShootObserver;
        }

        IrrKlang.ISoundEngine soundEngine;
        IrrKlang.ISoundSource soundSource;

    }
}

// --- End of File ---
