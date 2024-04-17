﻿using System;
using System.Diagnostics;

namespace SE456
{
    public class BumperLeft : WallCategory
    {
        public BumperLeft(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY, float width, float height)
            : base(name, spriteName, posX, posY, Type.Left)
        {
            this.poColObj.poColRect.Set(posX, posY, width, height);

            this.x = posX;
            this.y = posY;

            this.poColObj.pColSprite.SetColor(1, 1, 0);
        }

        ~BumperLeft()
        {

        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an Alien
            // Call the appropriate collision reaction            
            //other.VisitWallLeft(this);
        }
        public override void Update()
        {
            // Go to first child
            base.Update();
        }



        //public override void VisitGroup(BirdGrid a)
        //{
        //    // BirdGroup vs WallRight
        //    //Debug.WriteLine("\ncollide: {0} with {1}", this, a);
        //    //Debug.WriteLine("               --->DONE<----");

        //    ColPair pColPair = ColPairMan.GetActiveColPair();
        //    Debug.Assert(pColPair != null);

        //    pColPair.SetCollision(a, this);
        //    pColPair.NotifyListeners();
        //}

        public override void Visit(AlienGrid a)
        {
            // BirdGroup vs WallRight
            //Debug.WriteLine("\ncollide: {0} with {1}", this, a);
            //Debug.WriteLine("               --->DONE<----");

            ColPair pColPair = ColPairMan.GetActiveColPair();
            Debug.Assert(pColPair != null);

            pColPair.SetCollision(a, this);
            pColPair.NotifyListeners();
        }

        public override void VisitShipRoot(ShipRoot s)
        {
            ColPair pColPair = ColPairMan.GetActiveColPair();
            Debug.Assert(pColPair != null);

            pColPair.SetCollision(s, this);
            pColPair.NotifyListeners();
        }
    }
}

// --- End of File ---
