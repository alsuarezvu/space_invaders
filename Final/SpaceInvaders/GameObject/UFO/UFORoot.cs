using System;
using System.Diagnostics;

namespace SE456
{
    public class UFORoot : Composite
    {
        public UFORoot(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY)
           : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.poColObj.pColSprite.SetColor(1, 0, 0);
        }

        ~UFORoot()
        {
        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an Alien
            // Call the appropriate collision reaction            
            other.VisitUFORoot(this);
        }

        public override void Update()
        {
            // Go to first child
            base.BaseUpdateBoundingBox(this);
            base.Update();
            //Debug.Write("updating width and height to: " + this.poColObj.poColRect.width + " " + this.poColObj.poColRect.height);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            // Bomb vs. Missile
            ColPair.Collide(m, (GameObject)IteratorForwardComposite.GetChild(this));
        }

        public override void VisitMissile(Missile m)
        {
            // Bomb vs. Missile
            ColPair.Collide(m, (GameObject)IteratorForwardComposite.GetChild(this));
        }

        public override void VisitWallRoot(WallRoot w)
        {
            // Bomb vs. WallGroup
            ColPair.Collide(w, (GameObject)IteratorForwardComposite.GetChild(this));
        }
    }
}
