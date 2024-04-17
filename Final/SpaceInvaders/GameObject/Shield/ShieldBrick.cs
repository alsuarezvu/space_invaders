//-----------------------------------------------------------------------------
// Copyright 2024, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    public class ShieldBrick : ShieldCategory
    {
        public ShieldBrick(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY)
            : base(name, spriteName, posX, posY, ShieldCategory.Type.Brick)
        {
            this.x = posX;
            this.y = posY;

            this.SetCollisionColor(1.0f, 1.0f, 1.0f);
        }

        public void Resurrect(SpriteGame.Name name, float posX, float posY)
        {
            this.spriteName = name;
            this.pSpriteProxy.pSprite = SpriteGameMan.Find(name);

            this.x = posX;
            this.y = posY;

            base.Resurrect();
            this.poColObj.pColSprite.SetColor(1.0f, 1.0f, 1.0f);
        }
        ~ShieldBrick()
        {

        }
        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an Alien
            // Call the appropriate collision reaction            
            other.VisitShieldBrick(this);
        }
        public override void VisitMissile(Missile m)
        {
            // Missile vs ShieldBrick
            //Debug.WriteLine(" ---> Done");
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(m, this);
            pColPair.NotifyListeners();
        }
        public override void VisitBomb(Bomb b)
        {
            // Bomb vs ShieldBrick
            //Debug.WriteLine(" ---> Done");
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(b, this);
            pColPair.NotifyListeners();
        }

        public override void VisitGridRoot(GridRoot b)
        {
            // Bomb vs ShieldBrick
            //Debug.WriteLine(" ---> Done");
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(b, this);
            pColPair.NotifyListeners();
        }

        public override void Visit(AlienGrid b)
        {
            // AlienGridRoot vs ShieldRoot
            GameObject alienGridChild = (GameObject)IteratorForwardComposite.GetChild(b);
            GameObject shieldColumnChild = (GameObject)IteratorForwardComposite.GetChild(this);
            ColPair.Collide(alienGridChild, shieldColumnChild);
        }

        public override void VisitColumn(AlienColumn b)
        {
            // AlienGridRoot vs ShieldRoot
            GameObject alien = (GameObject)IteratorForwardComposite.GetChild(b);
            ColPair.Collide(alien, this);
        }

        public override void VisitAlien(AlienBase alien)
        {
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(alien, this);
            pColPair.NotifyListeners();
        }

        public override void VisitSquid(AlienSquid alien)
        {
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(alien, this);
            pColPair.NotifyListeners();
        }

        public override void VisitCrab(AlienCrab alien)
        {
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(alien, this);
            pColPair.NotifyListeners();
        }

        public override void VisitOctopus(AlienOctopus alien)
        {
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(alien, this);
            pColPair.NotifyListeners();
        }

        public override void Update()
        {
            base.Update();
        }

        // ---------------------------------
        // Data: 
        // ---------------------------------


    }
}

// --- End of File ---
