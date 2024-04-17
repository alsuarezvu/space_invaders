using System;
using System.Diagnostics;

namespace SE456
{
    public class RemoveUFOObserver : ColObserver
    {
        public RemoveUFOObserver()
        {
            this.pUFO = null;
            this.deltaRemoval = new Delta(Delta.Name.UFORemoval, .10f, .10f);
            this.deltaRemoval.setReAdd(false);
        }
        public RemoveUFOObserver(RemoveUFOObserver m)
        {
            this.pUFO = m.pUFO;
        }
        public override void Notify()
        {
            // Delete missile
            //Debug.WriteLine("RemoveUFOObserver: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);

            this.pUFO = (UFO)this.pSubject.pObjB;
            Debug.Assert(this.pUFO != null);

            this.pUFO.pSpriteProxy.pSprite = SpriteGameMan.Find(SpriteGame.Name.UFO_Splat);

            DelayedUFORemoval delayedUFORemovalCmd = new DelayedUFORemoval(this);

            TimerEventMan.AddBasedOnTriggerTime(TimerEvent.Name.AlienDelayRemoval, delayedUFORemovalCmd, deltaRemoval);
        }

        public override void Execute()
        {
            // Let the gameObject deal with this... 
            this.pUFO.Remove();
        }

        public GameObject getUFO()
        {
            return this.pUFO;
        }
        override public void Dump()
        {

        }
        override public System.Enum GetName()
        {
            return ColObserver.Name.RemoveMissileObserver;
        }

        // --------------------------------------
        // data:
        // --------------------------------------

        private GameObject pUFO;
        private Delta deltaRemoval;
    }
}
