﻿using System;
using System.Diagnostics;

namespace SE456
{
    public class BumperRightRoot : Composite
    {
        public BumperRightRoot(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.poColObj.pColSprite.SetColor(0, 0, 0);
        }

        ~BumperRightRoot()
        {
        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an Alien
            // Call the appropriate collision reaction            
            other.VisitBumperRightRoot(this);
        }
        public override void Update()
        {
            base.BaseUpdateBoundingBox(this);
            base.Update();
        }

        public override void VisitShipRoot(ShipRoot s)
        {
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(this);
            ColPair.Collide(s, pGameObj);
        }
    }
}
