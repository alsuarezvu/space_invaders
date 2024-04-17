//-----------------------------------------------------------------------------
// Copyright 2024, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;
using static SE456.AlienGrid;

namespace SE456
{
    public class AlienColumn : Composite
    {
        public AlienColumn()
            : base()
        {
            this.name = GameObject.Name.AlienColumn;
            this.poColObj.pColSprite.SetColor(0, 1, 0);
            this.poColObj.pColSprite.Update();
        }

        public void Resurrect()
        {
            this.name = GameObject.Name.AlienColumn;
            this.poColObj.pColSprite.SetColor(0, 1, 0);
            this.poColObj.pColSprite.Update();
            base.Resurrect();
            this.poColObj.pColSprite.SetColor(0, 1, 0);
        }

        public override void Update()
        {
            ////Debug.WriteLine(this.name+" Update");

            //Iterator pIt = this.poDLinkMan.GetIterator();

            //Debug.Assert(pIt != null);

            //GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(this);

            //// create a local pointer
            //ColRect pColTotal = this.poColObj.poColRect;

            //// Set it to the first child;
            //pColTotal.Set(pGameObj.poColObj.poColRect);

            ////Debug.WriteLine("\n");
            //for (pIt.First(); !pIt.IsDone(); pIt.Next())
            //{
            //    pGameObj = (GameObject)pIt.Current();
            //    Debug.Assert(pGameObj != null);

            //    //Debug.WriteLine("obj:{0} {1} {2}", pGameObj.GetName(), pGameObj.x, pGameObj.y);

            //    // Inside union (x,y,width,height are updated)
            //    pColTotal.Union(pGameObj.poColObj.poColRect);
            //}

            //// Transfer to the game object its center
            //this.x = this.poColObj.poColRect.x;
            //this.y = this.poColObj.poColRect.y;

            ////Debug.WriteLine("ALIEN COLUMN name:{0} x:{1} y:{2} w:{3} h:{4}",
            //    //this.name,
            //    //this.poColObj.poColRect.x,
            //    //this.poColObj.poColRect.y,
            //    //this.poColObj.poColRect.width,
            //    //this.poColObj.poColRect.height);

            ////Debug.WriteLine("x:{0} y:{1} w:{2} h:{3}", pColTotal.x, pColTotal.y, pColTotal.width, pColTotal.height);
            base.BaseUpdateBoundingBox(this);
            base.Update();
        }

        public override void Print()
        {
            Debug.WriteLine("");
            Debug.WriteLine("Column:");

            // walk through the list and render
            Iterator pIt = this.poDLinkMan.GetIterator();
            Debug.Assert(pIt != null);

            for (pIt.First(); !pIt.IsDone(); pIt.Next())
            {
                GameObject pNode = (GameObject)pIt.Current();
                Debug.Assert(pNode != null);

                pNode.Dump();
            }
        }

        public override void Accept(ColVisitor other)
        {
            other.VisitColumn(this);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            // MissileRoot vs WallRoot
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(m);
            ColPair.Collide(pGameObj, this);
        }

        public override void VisitMissile(Missile m)
        {
            // Missile vs WallRoot
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(this);
            ColPair.Collide(m, pGameObj);
        }

        public override void VisitGridRoot(GridRoot b)
        {
            // GridRoot vs ShieldRoot
            ColPair.Collide(b, (GameObject)IteratorForwardComposite.GetChild(this));
        }

    }
}

// --- End of File ---
