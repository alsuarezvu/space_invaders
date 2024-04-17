//-----------------------------------------------------------------------------
// Copyright 2024, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    public class MissileGroup : Composite
    {
        public MissileGroup()
            : base()
        {
            this.name = Name.MissileGroup;

            this.poColObj.pColSprite.SetColor(0, 0, 0);
        }

        ~MissileGroup()
        {

        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an MissileGroup
            // Call the appropriate collision reaction            
            other.VisitMissileGroup(this);
        }

        public override void Update()
        {
            // Go to first child
            base.BaseUpdateBoundingBox(this);
            base.Update();
        }

        public override void VisitBomb(Bomb b)
        {
            // Bomb vs. Missile
            ColPair.Collide(b, (GameObject)IteratorForwardComposite.GetChild(this));
        }

        public override void VisitBombRoot(BombRoot b)
        {
            // Missile vs Bombroot
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(this);
            ColPair.Collide(b, pGameObj);
        }

        public override void VisitUFORoot(UFORoot u)
        {
            // Missile vs Bombroot
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(this);
            ColPair.Collide(pGameObj, u);
        }
    
    }
}

// --- End of File ---

