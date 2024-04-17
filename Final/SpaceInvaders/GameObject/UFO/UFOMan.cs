using System;
using System.Diagnostics;

namespace SE456
{
    public class UFOMan
    {
        private UFOMan(Random _random, SpriteBatch _spriteBatch, SpriteBatch _boxBatch, GameObject _ufoRoot)
        {
           this.pRandom = _random;
           this.spriteBatch = _spriteBatch;
           this.boxBatch = _boxBatch;
           this.ufoRoot = _ufoRoot;
           this.isUFOActive = false;
        }

        public static void Create(Random _random, SpriteBatch _spriteBatch, SpriteBatch _boxBatch, GameObject _ufoRoot)
        {
            // make sure its the first time
            Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new UFOMan(_random, _spriteBatch, _boxBatch, _ufoRoot);
            }

            Debug.Assert(pInstance != null);
        }

        public static void CreateUFO()
        {
            UFOMan ufoMan = PrivInstance();

           
                int random = ufoMan.pRandom.Next(0, 2);

                UFOMoveStrategy moveStrategy;
                int x_Pos = -1;
                if (random == 0)
                {
                    moveStrategy = new UFOMoveRightStrategy();
                    x_Pos = UFO_X_RIGHT + UFO_WIDTH;
                }
                else
                {
                    moveStrategy = new UFOMoveLeftStrategy();
                    x_Pos = UFO_X_LEFT - UFO_WIDTH;
                }

                UFO pUFO = null;
                GameObjectNode pGameObjNode = GhostMan.Find(GameObject.Name.UFO);

                //start at 50 and end at MAX move - 50 to account for bumpers
                
                int randomSpawnCount = ufoMan.pRandom.Next(50, MAX_X_MOVE_COUNT - 50);
                int randomPoints = ufoMan.pRandom.Next(100, 501);
                if (pGameObjNode == null)
                {
                    pUFO = new UFO(GameObject.Name.UFO, SpriteGame.Name.UFO, moveStrategy, x_Pos, UFO_Y, randomSpawnCount, randomPoints);
                }
                else
                {
                    pUFO = (UFO)pGameObjNode.pGameObj;
                    GhostMan.Remove(pGameObjNode);

                    pUFO.Resurrect(x_Pos, UFO_Y, moveStrategy, randomSpawnCount, randomPoints);
                }

                pUFO.ActivateSprite(ufoMan.spriteBatch);
                pUFO.ActivateCollisionSprite(ufoMan.boxBatch);

                ufoMan.ufoRoot.Add(pUFO);
                ufoMan.isUFOActive = true;
            
        }

        private static UFOMan PrivInstance()
        {
            Debug.Assert(pInstance != null);

            return pInstance;
        }

        public static void SetUFOActive(bool isActive)
        {
            UFOMan ufoMan = PrivInstance();
            ufoMan.isUFOActive = isActive;
        }

        public static bool IsUFOActive()
        {
            UFOMan ufoMan = PrivInstance();
            return ufoMan.isUFOActive;
        }

        private static UFOMan pInstance;
        private readonly Random pRandom;
        private readonly SpriteBatch spriteBatch;
        private readonly SpriteBatch boxBatch;
        private readonly GameObject ufoRoot;
        private bool isUFOActive;

        public static int UFO_Y = 650;
        public static int UFO_X_LEFT = 672;
        public static int UFO_X_RIGHT = 0;

        private static readonly int UFO_WIDTH = 48;

        private static readonly int MAX_X_MOVE_COUNT = 336;
    }
}
