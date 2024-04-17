using System;
using System.Diagnostics;

namespace SE456
{
    public class UFOMoveLeftStrategy : UFOMoveStrategy
    {
        public override void Move(UFO pUFO, float delta)
        { 
            pUFO.x -= delta;

            if(pUFO.x <= UFO_X_RIGHT)
            {
                pUFO.Remove();
                UFOMan.SetUFOActive(false);
            }
        }
        public readonly static int UFO_X_RIGHT = 0;
    }
}
