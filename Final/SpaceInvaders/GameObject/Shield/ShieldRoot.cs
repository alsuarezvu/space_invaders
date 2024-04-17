//-----------------------------------------------------------------------------
// Copyright 2024, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    public class ShieldRoot : Composite
    {
        public ShieldRoot(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

        }
        ~ShieldRoot()
        {
        }
        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an Alien
            // Call the appropriate collision reaction            
            other.VisitShieldRoot(this);
        }
        public override void VisitMissileGroup(MissileGroup m)
        {
            // MissileRoot vs ShieldRoot
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(m);
            ColPair.Collide(pGameObj, this);
        }
        public override void VisitMissile(Missile m)
        {
            // Missile vs ShieldRoot
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(this);
            ColPair.Collide(m, pGameObj);
        }
        public override void VisitBombRoot(BombRoot b)
        {
            // BombRoot vs ShieldRoot
            ColPair.Collide((GameObject)IteratorForwardComposite.GetChild(b), this);
        }
        public override void VisitBomb(Bomb b)
        {
            // Missile vs ShieldRoot
            ColPair.Collide(b, (GameObject)IteratorForwardComposite.GetChild(this));
        }
        public override void Update()
        {
            // Go to first child
            base.BaseUpdateBoundingBox(this);
            base.Update();
        }

        public override void VisitGridRoot(GridRoot b)
        {
            // AlienGridRoot vs ShieldRoot
            GameObject alienGridRootChild = (GameObject) IteratorForwardComposite.GetChild(b);
            GameObject shieldRootChild = (GameObject)IteratorForwardComposite.GetChild(this);
            ColPair.Collide(alienGridRootChild, shieldRootChild);
        }

        public override void Visit(AlienGrid b)
        {
            // AlienGridRoot vs ShieldRoot
            GameObject alienGridChild = (GameObject)IteratorForwardComposite.GetChild(b);
            GameObject shieldRootChild = (GameObject)IteratorForwardComposite.GetChild(this);
            ColPair.Collide(alienGridChild, shieldRootChild);
        }


    }
}

// --- End of File ---
