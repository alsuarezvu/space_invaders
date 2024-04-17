//-----------------------------------------------------------------------------
// Copyright 2024, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    class BombObserver : ColObserver
    {
        public override void Notify()
        {
            //Debug.WriteLine("BombObserver: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);
            Bomb pBomb = (Bomb)this.pSubject.pObjA;
            pBomb.Reset();
        }

        // ------------------------------------
        // Data
        // ------------------------------------

        override public void Dump()
        {
            Debug.Assert(false);
        }
        override public System.Enum GetName()
        {
            return Name.BombObserver;
        }

    }
}

// --- End of File ---
