using System;
using System.Diagnostics;

namespace SE456
{

    public class UpdateReserveShipCountObserver : ColObserver
    {
        public UpdateReserveShipCountObserver(Font.Name name)
        {
            this.reserveCount = FontMan.Find(name);
        }
       

        public override void Notify()
        {
            this.reserveCount.UpdateMessage(ShipMan.getReserveShipCount()+"");
        }
        public override void Execute()
        {
           //
        }
      
        override public void Dump()
        {

        }
        override public System.Enum GetName()
        {
            return ColObserver.Name.UpdateReserveShipCountObserver;
        }



        // -------------------------------------------
        // data:
        // -------------------------------------------

        private Font reserveCount;

    }

}
