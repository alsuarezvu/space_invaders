//-----------------------------------------------------------------------------
// Copyright 2024, Ed Keenan, all rights reserved.
//-----------------------------------------------------------------------------
using System;
using System.Diagnostics;

namespace SE456
{
    public class RemoveBombObserver : ColObserver
    {
        public RemoveBombObserver()
        {
            this.pBomb = null;
            this.pBombRoot = (BombRoot)GameObjectNodeMan.Find(GameObject.Name.BombRoot);
            this.deltaRemoval = new Delta(Delta.Name.UFORemoval, .10f, .10f);
        }
        public RemoveBombObserver(RemoveBombObserver b)
        {
            this.pBomb = b.pBomb;
            this.pBombRoot = b.pBombRoot;
        }
        public override void Notify()
        {
            // Delete missile
            //Debug.WriteLine("RemoveBombObserver: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);

            this.pBomb = (Bomb)this.pSubject.pObjA;
            Debug.Assert(this.pBomb != null);

            if (pBomb.bMarkForDeath == false)
            {
                pBomb.bMarkForDeath = true;
                //   Delay
                RemoveBombObserver pObserver = new RemoveBombObserver(this);
                DelayedObjectMan.Attach(pObserver);
            }
        }
        public override void Execute()
        {
            // Let the gameObject deal with this... 
            this.pBomb.Remove();
        }

        public GameObject getBomb()
        {
            return this.pBomb;
        }
        override public void Dump()
        {

        }
        override public System.Enum GetName()
        {
            return ColObserver.Name.RemoveBombObserver;
        }

        // --------------------------------------
        // data:
        // --------------------------------------

        private GameObject pBomb;
        private BombRoot pBombRoot;

        private Delta deltaRemoval;
    }
}

// --- End of File ---
