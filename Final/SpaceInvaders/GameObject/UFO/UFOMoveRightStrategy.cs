using System;
using System.Diagnostics;

namespace SE456
{
    public class UFOMoveRightStrategy : UFOMoveStrategy
    {
        public override void Move(UFO pUFO, float delta)
        {
            pUFO.x += delta;

            if(pUFO.x >= UFO_X_LEFT)
            {
                pUFO.Remove();
                UFOMan.SetUFOActive(false);
            }
        }

        public static readonly int UFO_X_LEFT = 672;
    }
}
