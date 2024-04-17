using System;
using System.Diagnostics;

namespace SE456
{
    public abstract class AlienBase : Leaf
    {
        protected AlienBase(GameObject.Name gameName, SpriteGame.Name spriteName, float x, float y)
            : base(gameName, spriteName, x, y)
        {
            this.poColObj.pColSprite.SetColor(1, 0, 0);
        }
        public void Resurrect(SpriteGame.Name name, float posX, float posY)
        {
            this.x = posX;
            this.y = posY;

            this.spriteName = name;
            this.pSpriteProxy.pSprite = SpriteGameMan.Find(name);

            base.Resurrect();
            this.poColObj.pColSprite.SetColor(1, 0, 0);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            // MissileRoot vs WallRoot
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(m);
            ColPair.Collide(pGameObj, this);
        }

        public override void VisitMissile(Missile m)
        {
            // Missile vs Alien
            //Debug.WriteLine(" ---> Done");
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(m, this);
            pColPair.NotifyListeners();
        }

        public override void Accept(ColVisitor other)
        {
            other.VisitAlien(this);
        }

    }

}
