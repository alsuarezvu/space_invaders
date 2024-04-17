//-----------------------------------------------------------------------------
// Copyright 2024, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    public class ShipRoot : Composite
    {
        public ShipRoot(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.poColObj.pColSprite.SetColor(0, 1, 0);
        }

        ~ShipRoot()
        {
        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an Alien
            // Call the appropriate collision reaction            
            other.VisitShipRoot(this);
        }
        public override void Update()
        {
            base.BaseUpdateBoundingBox(this);
            base.Update();
        }

        public override void VisitBombRoot(BombRoot b)
        {
            // BombRoot vs Ship
            GameObject pGameObjShip = (GameObject)IteratorForwardComposite.GetChild(this);
            GameObject pGameObjBomb = (GameObject)IteratorForwardComposite.GetChild(b);
            ColPair.Collide(pGameObjBomb, pGameObjShip);
        }

        public override void VisitGridRoot(GridRoot g)
        {
            // GridRoot vs. ShipRoot
            GameObject pGameObjAlienGrid = (GameObject)IteratorForwardComposite.GetChild(g);
            GameObject pGameObjShip = (GameObject)IteratorForwardComposite.GetChild(this);
           
            ColPair.Collide(pGameObjAlienGrid, pGameObjShip);
        }
    }
}

// --- End of File ---

