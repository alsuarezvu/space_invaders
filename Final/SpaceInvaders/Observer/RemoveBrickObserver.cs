//-----------------------------------------------------------------------------
// Copyright 2024, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    class RemoveBrickObserver : ColObserver
    {
        public RemoveBrickObserver()
        {
            this.pBrick = null;
        }
        public RemoveBrickObserver(RemoveBrickObserver b)
        {
            Debug.Assert(b != null);
            this.pBrick = b.pBrick;
        }

        public override void Notify()
        {
            // Delete missile
            //Debug.WriteLine("RemoveBrickObserver: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);

            this.pBrick = (ShieldBrick)this.pSubject.pObjB;
            Debug.Assert(this.pBrick != null);

            if (this.pBrick.pSpriteProxy.pSprite.name != SpriteGame.Name.BombSplat_Green)
            {
                this.pBrick.pSpriteProxy.pSprite = SpriteGameMan.Find(SpriteGame.Name.BombSplat_Green);
            }
            else
            {
                if (pBrick.bMarkForDeath == false)
                {
                    pBrick.bMarkForDeath = true;
                    //   Delay
                    RemoveBrickObserver pObserver = new RemoveBrickObserver(this);
                    DelayedObjectMan.Attach(pObserver);
                }
            }
          
        }
        public override void Execute()
        {
            //  if this brick removed the last child in the column, then remove column
            // Debug.WriteLine(" brick {0}  parent {1}", this.pBrick, this.pBrick.pParent);
            GameObject pA = (GameObject)this.pBrick;
            GameObject pB = (GameObject)IteratorForwardComposite.GetParent(pA);

            pA.Remove();

            // TODO: Need a better way... 
            if (privCheckParent(pB) == true)
            {
                GameObject pC = (GameObject)IteratorForwardComposite.GetParent(pB);
                pB.Remove();

                if (privCheckParent(pC) == true)
                {
                    //        pC.Remove();
                }

            }
        }
        private bool privCheckParent(GameObject pObj)
        {
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(pObj);
            if (pGameObj == null)
            {
                return true;
            }

            return false;
        }
        override public void Dump()
        {

        }
        override public System.Enum GetName()
        {
            return ColObserver.Name.RemoveBrickObserver;
        }



        // -------------------------------------------
        // data:
        // -------------------------------------------

        private GameObject pBrick;
    }
}

// --- End of File ---
