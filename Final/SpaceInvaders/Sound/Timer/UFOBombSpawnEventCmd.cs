using System;
using System.Diagnostics;


namespace SE456
{ 
    public class UFOBombSpawnEventCmd : Command
    {
        public UFOBombSpawnEventCmd(float _x, float _y)
        { 
            this.x = _x;
            this.y = _y;
        }

        override public void Execute(Delta deltaTime)
        {
            BombMan.CreateUFOBomb(x, y);

        }

        private float x;
        private float y;
    }
}
