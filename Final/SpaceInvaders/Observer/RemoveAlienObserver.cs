using System;
using System.Diagnostics;

namespace SE456
{
    public class RemoveAlienObserver : ColObserver
    {
        public RemoveAlienObserver()
        {
            this.pAlien = null;
            this.deltaRemoval = new Delta(Delta.Name.AlienRemoval, .10f, .10f);
            this.deltaRemoval.setReAdd(false);
        }
        public RemoveAlienObserver(RemoveAlienObserver b)
        {
            Debug.Assert(b != null);
            this.pAlien = b.pAlien;
        }

        public override void Notify()
        {
            // Delete missile
            //Debug.WriteLine("RemoveAlienObserver: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);

            this.pAlien = (AlienBase)this.pSubject.pObjB;
            Debug.Assert(this.pAlien != null);

            this.pAlien.pSpriteProxy.pSprite = SpriteGameMan.Find(SpriteGame.Name.Alien_Splat);

            DelayedAlienRemoval delayedAlienRemovalCmd = new DelayedAlienRemoval(this);
            TimerEventMan.AddBasedOnTriggerTime(TimerEvent.Name.AlienDelayRemoval, delayedAlienRemovalCmd, deltaRemoval);
        }

        public GameObject getAlien()
        {
            return pAlien;
        }
        public override void Execute()
        {
            //  if this brick removed the last child in the column, then remove column
            // Debug.WriteLine(" brick {0}  parent {1}", this.pBrick, this.pBrick.pParent);
            GameObject pA = (GameObject)this.pAlien;
            GameObject pB = (GameObject)IteratorForwardComposite.GetParent(pA);

            pA.Remove();
            AlienCounter.Subtract(1);

            // TODO: Need a better way... 
             if (privCheckParent(pB) == true)
             {
                GameObject pC = (GameObject)IteratorForwardComposite.GetParent(pB);
                pB.Remove();

                if (privCheckParent(pC) == true)
                {
                    pC.Remove();
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
            return ColObserver.Name.RemoveAlienObserver;
        }



        // -------------------------------------------
        // data:
        // -------------------------------------------

        private GameObject pAlien;
        private Delta deltaRemoval;
    }
}
