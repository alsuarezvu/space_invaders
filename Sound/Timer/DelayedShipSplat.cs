using System;
using System.Diagnostics;

namespace SE456
{
    public class DelayedShipSplat : Command
    {
        public DelayedShipSplat(GameObject _ship) { 
            this.ship = _ship;
        }
        public override void Execute(Delta deltaTime)
        {
            this.ship.pSpriteProxy.pSprite = SpriteGameMan.Find(SpriteGame.Name.Ship_Splat_2);
        }

        private GameObject ship;
    }
}
