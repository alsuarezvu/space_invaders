using System;
using System.Diagnostics;

namespace SE456
{
    public class RemoveActiveShipObserver : ColObserver
    {
        public RemoveActiveShipObserver(Font.Name name)
        {
            this.pShip = null;
            this.delta = new Delta(Delta.Name.AlienRemoval, .10f, .10f);
            this.delta.setReAdd(false);
            this.reserveCount = FontMan.Find(name);
        }
        public RemoveActiveShipObserver(RemoveActiveShipObserver b)
        {
            Debug.Assert(b != null);
            this.pShip = b.pShip;
        }

        public override void Notify()
        {
            // Delete missile
            //Debug.WriteLine("RemoveActiveShip: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);

            this.pShip = (Ship)this.pSubject.pObjB;
            Debug.Assert(this.pShip != null);

            this.pShip.pSpriteProxy.pSprite = SpriteGameMan.Find(SpriteGame.Name.Ship_Splat_1);

            delta.setDelta(.20f);
            DelayedShipSplat delayedShipSplat = new DelayedShipSplat((Ship)this.pShip);
            TimerEventMan.AddBasedOnTriggerTime(TimerEvent.Name.DelayedShipSplat, delayedShipSplat, delta);

            delta.setDelta(.50f);
            DelayedShipRemoval delayedShipRemovalCmd = new DelayedShipRemoval(this);
            TimerEventMan.AddBasedOnTriggerTime(TimerEvent.Name.DelayedShipRemoval, delayedShipRemovalCmd, delta);

            /*if (pShip.bMarkForDeath == false)
            {
                pShip.bMarkForDeath = true;
                //   Delay
                RemoveActiveShipObserver pObserver = new RemoveActiveShipObserver(this);
                DelayedObjectMan.Attach(pObserver);
            }*/
        }
        public override void Execute()
        {
            //  if this brick removed the last child in the column, then remove column
            // Debug.WriteLine(" brick {0}  parent {1}", this.pBrick, this.pBrick.pParent);
            GameObject pA = (GameObject)this.pShip;
            GameObject pB = (GameObject)IteratorForwardComposite.GetParent(pA);

            pA.Remove();

            //only reactive when it's officially removed
            ShipMan.ActivateReserveShip();
            this.reserveCount.UpdateMessage(ShipMan.getReserveShipCount() + "");
        }
       
        public GameObject getShip()
        {
            return (GameObject)this.pShip;
        }
        override public void Dump()
        {

        }
        override public System.Enum GetName()
        {
            return ColObserver.Name.RemoveActiveShipObserver;
        }



        // -------------------------------------------
        // data:
        // -------------------------------------------

        private GameObject pShip;
        private Delta delta;
        private Font reserveCount;

    }
}
