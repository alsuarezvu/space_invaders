//-----------------------------------------------------------------------------
// Copyright 2024, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    public class ShieldColumn : Composite
    {
        public ShieldColumn(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;
        }

        public void Resurrect(float posX, float posY)
        {
            this.x = posX;
            this.y = posY;
        }
        ~ShieldColumn()
        {
        }
        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an Alien
            // Call the appropriate collision reaction            
            other.VisitShieldColumn(this);
        }
        public override void VisitMissile(Missile m)
        {
            // Missile vs ShieldColumn
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(this);
            ColPair.Collide(m, pGameObj);
        }
        public override void VisitBomb(Bomb b)
        {
            // Bomb vs ShieldColumn
            ColPair.Collide(b, (GameObject)IteratorForwardComposite.GetChild(this));
        }

        public override void Visit(AlienGrid b)
        {
            // AlienGridRoot vs ShieldRoot
            GameObject alienColumn = (GameObject)IteratorForwardComposite.GetChild(b);
            GameObject shieldColumnChild = (GameObject)IteratorForwardComposite.GetChild(this);
            ColPair.Collide(alienColumn, shieldColumnChild);
        }

        public override void VisitColumn(AlienColumn b)
        {
            // AlienColumn vs ShieldRoot
            GameObject alien = (GameObject)IteratorForwardComposite.GetChild(b);
            GameObject shieldBrick = (GameObject)IteratorForwardComposite.GetChild(this);
            ColPair.Collide(alien, shieldBrick);
        }
        public override void Update()
        {
            base.BaseUpdateBoundingBox(this);
            base.Update();
        }

        // ---------------------------------------------
        // Data: 
        // ---------------------------------------------


    }
}

// --- End of File ---
