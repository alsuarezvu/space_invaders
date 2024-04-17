using System;
using System.Diagnostics;

namespace SE456
{
    public class ActivateReserveShipObserver : ColObserver
    {
        public override void Notify()
        {
            //ShipMan.ActivateReserveShip();
        }
        public override void Execute()
        {
           //
        }
       
        override public void Dump()
        {
            //
        }
        override public System.Enum GetName()
        {
            return ColObserver.Name.ActivateReserveShipObserver;
        }
    }
}
