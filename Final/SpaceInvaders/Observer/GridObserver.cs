//-----------------------------------------------------------------------------
// Copyright 2024, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    public class GridObserver : ColObserver
    {
        public GridObserver()
        {
            this.delta = DeltaMan.Find(Delta.Name.Move);
        }
        override public void Notify()
        {
            //Debug.WriteLine("Grid_Observer: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);

            // OK do some magic
            AlienGrid pGrid = (AlienGrid)this.pSubject.pObjA;

            WallCategory pWall = (WallCategory)this.pSubject.pObjB;
            if (pWall.GetCategoryType() == WallCategory.Type.Right)
            {
                pGrid.SetDelta_x(delta.getDelta() * -1.0f);
                //pGrid.SetDirection(AlienGrid.DIRECTION.LEFT);
                pGrid.SetDelta_y(DELTA_Y);
            }
            else if (pWall.GetCategoryType() == WallCategory.Type.Left)
            {
                //pGrid.SetDirection(AlienGrid.DIRECTION.RIGHT);
                pGrid.SetDelta_x(delta.getDelta());
                pGrid.SetDelta_y(DELTA_Y);
            }
            else
            {
                Debug.Assert(false);
            }

        }

        override public void Dump()
        {
            Debug.Assert(false);
        }
        override public System.Enum GetName()
        {
            return Name.GridObserver;
        }

        //data
        private readonly Delta delta;
        private static readonly float DELTA_Y = -20.0f;



    }
}

// --- End of File ---
