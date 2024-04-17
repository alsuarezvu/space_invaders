using System;
using System.Diagnostics;

namespace SE456
{
    public class BombMan
    {
        private BombMan(Random _random, SpriteBatch _spriteBatch, SpriteBatch _boxBatch, GameObject _bombRoot)
        {
            this.pRandom = _random;
            this.spriteBatch = _spriteBatch;
            this.boxBatch = _boxBatch;
            this.bombRoot = _bombRoot;
        }

        public static void Create(Random _random, SpriteBatch _spriteBatch, SpriteBatch _boxBatch, GameObject _ufoRoot)
        {
            // make sure its the first time
            Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new BombMan(_random, _spriteBatch, _boxBatch, _ufoRoot);
            }

            Debug.Assert(pInstance != null);
        }

        public static void CreateBomb(float x, float y)
        {
            BombMan bombMan = PrivInstance();

            SpriteGame.Name spriteGameName = bombMan.selectRandomBombSprite();
            FallStrategy fallStrategy = bombMan.selectRandomFallStrategy();

            Bomb pBomb = null;
            GameObjectNode pGameObjNode = GhostMan.Find(GameObject.Name.Bomb);
            if (pGameObjNode == null)
            {
                pBomb = new Bomb(GameObject.Name.Bomb, spriteGameName, fallStrategy, x, y);
            }
            else 
            {
                pBomb = (Bomb)pGameObjNode.pGameObj;
                GhostMan.Remove(pGameObjNode);

                pBomb.Resurrect(x, y, spriteGameName, fallStrategy);
            }

            //Debug.WriteLine("Creating new bomb at x:" + x + " y:" + y);

            pBomb.ActivateCollisionSprite(bombMan.boxBatch);
            pBomb.ActivateSprite(bombMan.spriteBatch);

            // Attach the bomb to the Bomb root
            bombMan.bombRoot.Add(pBomb);
        }

        public static void CreateUFOBomb(float x, float y)
        {
            BombMan bombMan = PrivInstance();

            SpriteGame.Name spriteGameName = bombMan.selectRandomUFOBombSprite();
            FallStrategy fallStrategy = bombMan.selectRandomFallStrategy();

            SpriteGame spriteBomb = SpriteGameMan.Find(spriteGameName);
           
            Bomb pBomb = null;
            GameObjectNode pGameObjNode = GhostMan.Find(GameObject.Name.Bomb);
            if (pGameObjNode == null)
            {
                pBomb = new Bomb(GameObject.Name.Bomb, spriteGameName, fallStrategy, x, y);
            }
            else
            {
                pBomb = (Bomb)pGameObjNode.pGameObj;
                GhostMan.Remove(pGameObjNode);

                pBomb.Resurrect(x, y, spriteGameName, fallStrategy);
            }

            //Debug.WriteLine("Creating new bomb at x:" + x + " y:" + y);

            pBomb.ActivateCollisionSprite(bombMan.boxBatch);
            pBomb.ActivateSprite(bombMan.spriteBatch);

            // Attach the bomb to the Bomb root
            bombMan.bombRoot.Add(pBomb);
        }

        private FallStrategy selectRandomFallStrategy()
        {
            int random = pRandom.Next(0, 3);

            FallStrategy fallStrategy = null;

            switch(random)
            {
                case 0:
                    fallStrategy = new FallZigZag();
                    //Debug.WriteLine("FallZigZag");
                    break;

                case 1:
                    fallStrategy = new FallDagger();
                    //Debug.WriteLine("FallDagger");
                    break;

                case 2:
                    fallStrategy = new FallStraight();

                    //Debug.WriteLine("FallStraight");
                    break;

            }

            return fallStrategy;
        }
        private SpriteGame.Name selectRandomBombSprite()
        {
            int random = pRandom.Next(0, 12);

            SpriteGame.Name spriteGameName = SpriteGame.Name.Uninitialized;

            switch (random)
            {
                case 0:
                    spriteGameName = SpriteGame.Name.SquigglyShotA;
                    break;
                case 1:
                    spriteGameName = SpriteGame.Name.SquigglyShotB;
                    break;
                case 2:
                    spriteGameName = SpriteGame.Name.SquigglyShotC;
                    break;
                case 3:
                    spriteGameName = SpriteGame.Name.SquigglyShotD;
                    break;

                case 4:
                    spriteGameName = SpriteGame.Name.PlungerShotA;
                    break;

                case 5:
                    spriteGameName = SpriteGame.Name.PlungerShotB;
                    break;

                case 6:
                    spriteGameName = SpriteGame.Name.PlungerShotC;
                    break;

                case 7:
                    spriteGameName = SpriteGame.Name.PlungerShotD;
                    break;

                case 8:
                    spriteGameName = SpriteGame.Name.RollingShotA;
                    break;

                case 9:
                    spriteGameName = SpriteGame.Name.RollingShotB;
                    break;

                case 10:
                    spriteGameName = SpriteGame.Name.RollingShotC;
                    break;

                case 11:
                    spriteGameName = SpriteGame.Name.RollingShotD;
                    break;
            }

            return spriteGameName;
        }

        private SpriteGame.Name selectRandomUFOBombSprite()
        {
            int random = pRandom.Next(0, 12);

            SpriteGame.Name spriteGameName = SpriteGame.Name.Uninitialized;

            switch (random)
            {
                case 0:
                    spriteGameName = SpriteGame.Name.SquigglyShotA_Red;
                    break;
                case 1:
                    spriteGameName = SpriteGame.Name.SquigglyShotB_Red;
                    break;
                case 2:
                    spriteGameName = SpriteGame.Name.SquigglyShotC_Red;
                    break;
                case 3:
                    spriteGameName = SpriteGame.Name.SquigglyShotD_Red;
                    break;

                case 4:
                    spriteGameName = SpriteGame.Name.PlungerShotA_Red;
                    break;

                case 5:
                    spriteGameName = SpriteGame.Name.PlungerShotB_Red;
                    break;

                case 6:
                    spriteGameName = SpriteGame.Name.PlungerShotC_Red;
                    break;

                case 7:
                    spriteGameName = SpriteGame.Name.PlungerShotD_Red;
                    break;

                case 8:
                    spriteGameName = SpriteGame.Name.RollingShotA_Red;
                    break;

                case 9:
                    spriteGameName = SpriteGame.Name.RollingShotB_Red;
                    break;

                case 10:
                    spriteGameName = SpriteGame.Name.RollingShotC_Red;
                    break;

                case 11:
                    spriteGameName = SpriteGame.Name.RollingShotD_Red;
                    break;
            }

            return spriteGameName;
        }
        private static BombMan PrivInstance()
        {
            Debug.Assert(pInstance != null);

            return pInstance;
        }


        private static BombMan pInstance;
        private readonly Random pRandom;
        private readonly SpriteBatch spriteBatch;
        private readonly SpriteBatch boxBatch;
        private readonly GameObject bombRoot;

    }
}
