//-----------------------------------------------------------------------------
// Copyright 2024, Ed Keenan, all rights reserved.
//-----------------------------------------------------------------------------
using System;
using System.Diagnostics;

namespace SE456
{
    class BombSpawnEventCmd : Command
    {
        public BombSpawnEventCmd(Random pRandom, SpriteBatch _spriteBatchBomb, SpriteBatch _spriteBatchBox)
        {
            this.pRandom = pRandom;
        }

        override public void Execute(Delta deltaTime)
        {
             
                GridRoot gridRoot = (GridRoot)GameObjectNodeMan.Find(GameObject.Name.AlienGridRoot);

                AlienGrid alienGrid = (AlienGrid)gridRoot.GetHead();

                if (alienGrid != null)
                {
                    //find the # of columns
                    int count = alienGrid.getAlienColumnCount();

                    //select a random number out of the columns
                    int random = pRandom.Next(1, count+1);

                    //Debug.WriteLine("selected number: " + random);

                    //get the Y value of the randomly selected column
                    float y = alienGrid.getAlienCol_X_Y(random).y;

                    //get the X value of the radomly selected column
                    float x = alienGrid.getAlienCol_X_Y(random).x;

                    if (x > 0.0f && y > 0.0f) 
                    {
                        BombMan.CreateBomb(x, y);

                        //pick the next random delta to decide when the next bomb will go
                        deltaTime.setDelta(pRandom.Next(1, MAX_DELTA));

                        TimerEventMan.AddBasedOnTriggerTime(TimerEvent.Name.BombSpawn, this, deltaTime);
                    } 
            }
            

        }

        readonly Random pRandom;

        private static readonly int MAX_DELTA = 7;
    }
}

// --- End of File ---
