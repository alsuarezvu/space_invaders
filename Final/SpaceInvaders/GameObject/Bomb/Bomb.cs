//-----------------------------------------------------------------------------
// Copyright 2024, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System.Diagnostics;

namespace SE456
{
    public class Bomb : BombCategory
    {
        public Bomb(GameObject.Name name, SpriteGame.Name spriteName, FallStrategy _pStrategy, float posX, float posY)
            : base(name, spriteName, posX, posY, BombCategory.Type.Bomb)
        {
            this.x = posX;
            this.y = posY;
            this.delta = BOMB_SPEED;

            Debug.Assert(_pStrategy != null);
            this.pStrategy = _pStrategy;

            this.pStrategy.Reset(this.y);

            this.poColObj.pColSprite.SetColor(0, 0, 0);
        }

        public void Resurrect(float posX, float posY, SpriteGame.Name spriteGameName, FallStrategy _fallStrategy)
        {
            this.x = posX;
            this.y = posY;
            this.delta = BOMB_SPEED;
            this.pStrategy = _fallStrategy;
            this.poColObj.pColSprite.SetColor(1, 1, 0);
            this.pStrategy.Reset(this.y);
            this.spriteName = spriteGameName;

            base.Resurrect();
        }

        public void Reset()
        {
            this.y = 700.0f;
            this.pStrategy.Reset(this.y);
        }
        public override void Remove()
        {
            // Since the Root object is being drawn
            // 1st set its size to zero
            this.poColObj.poColRect.Set(0, 0, 0, 0);
            base.Update();

            // Update the parent (missile root)
            GameObject pParent = (GameObject)this.pParent;
            pParent.Update();

            // Now remove it
            base.Remove();
        }
        public override void Update()
        {
            base.Update();
            this.y -= delta;

            // Strategy
            this.pStrategy.Fall(this);
        }
        public float GetBoundingBoxHeight()
        {
            return this.poColObj.poColRect.height;
        }
        ~Bomb()
        {
        }
        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an Alien
            // Call the appropriate collision reaction            
            other.VisitBomb(this);
        }

        public void SetPos(float xPos, float yPos)
        {
            this.x = xPos;
            this.y = yPos;
        }
        public void MultiplyScale(float sx, float sy)
        {
            Debug.Assert(this.pSpriteProxy != null);

            this.pSpriteProxy.sx *= sx;
            this.pSpriteProxy.sy *= sy;
        }

        public override void VisitMissile(Missile m)
        {
            // Bomb vs. Missile
            //Debug.WriteLine(" ---> Done");
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(this, m);
            pColPair.NotifyListeners();
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            // MissileGroup vs Bomb
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(m);
            ColPair.Collide(pGameObj, this);
        }

        public override void VisitWallRoot(WallRoot w)
        {
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(this, w);
            pColPair.NotifyListeners();
        }


        // Data
        public float delta;
        private FallStrategy pStrategy;

        private static readonly float BOMB_SPEED = 4.0f;
    }
}

// --- End of File ---
