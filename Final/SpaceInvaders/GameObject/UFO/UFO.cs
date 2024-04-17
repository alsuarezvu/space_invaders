using System;
using System.Diagnostics;

namespace SE456
{
    public class UFO : Leaf
    {
        public UFO(GameObject.Name name, SpriteGame.Name spriteName, UFOMoveStrategy _pStrategy, float posX, float posY,
            int _bombSpawnCount, int _randomPoints)
            : base(name, spriteName, posX, posY)
        {
            this.x = posX;
            this.y = posY;
            this.delta = UFO_DELTA;

            Debug.Assert(_pStrategy != null);
            this.pStrategy = _pStrategy;

            this.poColObj.pColSprite.SetColor(1, 0, 0);

            this.bombSpawnCount = _bombSpawnCount;
            this.moveCount = 0;
            this.deltaBombSpawn = new Delta();

            this.deltaBombSpawn.setDelta(0);
            this.points = _randomPoints;
        }

        public void Resurrect(float posX, float posY, UFOMoveStrategy _moveStrategy, int _bombSpawnCount, int _randomPoints)
        {
            this.x = posX;
            this.y = posY;
            
            this.delta = UFO_DELTA;
            this.pStrategy = _moveStrategy;
            this.bombSpawnCount = _bombSpawnCount;
            this.moveCount = 0;

            this.points = _randomPoints;

            base.Resurrect();
            this.poColObj.pColSprite.SetColor(1, 0, 0);
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

            // Strategy
            this.pStrategy.Move(this, delta);

            this.moveCount++;
            if (this.moveCount == this.bombSpawnCount)
            {
                UFOBombSpawnEventCmd pBombEvent = new UFOBombSpawnEventCmd(this.x, this.y - UFO_HEIGHT / 2);
                TimerEventMan.AddBasedOnTriggerTime(TimerEvent.Name.BombSpawn, pBombEvent, this.deltaBombSpawn);
            }
        }
        public float GetBoundingBoxHeight()
        {
            return this.poColObj.poColRect.height;
        }

      

        ~UFO()
        {
        }
        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an Alien
            // Call the appropriate collision reaction            
            other.VisitUFO(this);
        }

        public int getPoints()
        {
            return points;
        }
        public override void VisitMissile(Missile m)
        {
            // Missle vs. UFO
            //Debug.WriteLine(" ---> Done");
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(m, this);
            pColPair.NotifyListeners();
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            // MissileGroup vs UFO
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(m);
            ColPair.Collide(this, pGameObj);
        }

        public override void VisitWallRoot(WallRoot w)
        {
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(this, w);
            pColPair.NotifyListeners();
        }

        // Data
        public float delta;
        private UFOMoveStrategy pStrategy;
        private static readonly int POINTS_DEFAULT = 100;
        private int points = POINTS_DEFAULT;
        private static readonly int UFO_HEIGHT = 24;
        private static readonly float UFO_DELTA = 2.0f;

        private int bombSpawnCount = 0;
        private int moveCount = 0;

        private readonly Delta deltaBombSpawn;

    }
}
