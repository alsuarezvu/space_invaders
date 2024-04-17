//-----------------------------------------------------------------------------
// Copyright 2024, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    class RemoveBombMissileObserver : ColObserver
    {
        public RemoveBombMissileObserver()
        {
            this.pMissile = null;
        }
        public RemoveBombMissileObserver(RemoveBombMissileObserver m)
        {
            this.pMissile = m.pMissile;
        }
        public override void Notify()
        {
            // Delete missile
            //Debug.WriteLine("RemoveBombMissileObserver: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);
            
            this.pMissile = (Missile)this.pSubject.pObjB;
            Debug.Assert(this.pMissile != null);

            if (pMissile.bMarkForDeath == false)
            {
                pMissile.bMarkForDeath = true;
                //   Delay
                RemoveBombMissileObserver pObserver = new RemoveBombMissileObserver(this);
                DelayedObjectMan.Attach(pObserver);
            }
        }
        public override void Execute()
        {
            // Let the gameObject deal with this... 
            this.pMissile.Remove();
        }

        override public void Dump()
        {

        }
        override public System.Enum GetName()
        {
            return ColObserver.Name.RemoveBombMissileObserver;
        }

        // --------------------------------------
        // data:
        // --------------------------------------

        private GameObject pMissile;
    }
}

// --- End of File ---
