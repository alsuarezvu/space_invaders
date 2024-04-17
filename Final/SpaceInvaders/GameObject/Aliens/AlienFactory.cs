using System;
using System.Diagnostics;

namespace SE456
{
    class AlienFactory
    {
        public AlienFactory(SpriteBatch _alienBatch, SpriteBatch _boxBatch)
        {
            this.pAlienBatch = _alienBatch;
            this.pBoxBatch = _boxBatch;
        }

        public GameObject Create(GameObject.Name type, float posX, float posY)
        {
            //Debug.WriteLine("Creating " + type + " at x: " + posX + " y: " + posY);
            GameObjectNode pGameObjNode = null;
            GameObject pGameObj = null;

            switch (type)
            {
                case GameObject.Name.Squid:
                    //LTN - Composite & GameObjectNodeMan
                    pGameObjNode = GhostMan.Find(GameObject.Name.Squid);
                    AlienSquid alienSquid;
                    if (pGameObjNode == null)
                    {
                        alienSquid = new AlienSquid(SpriteGame.Name.Squid, posX, posY);
                    }
                    else
                    {
                        alienSquid = (AlienSquid)pGameObjNode.pGameObj;
                        GhostMan.Remove(pGameObjNode);
                        alienSquid.Resurrect(SpriteGame.Name.Squid, posX, posY);
                    }
                    pGameObj = alienSquid;
                    AlienCounter.Add(1);

                    break;
                case GameObject.Name.Crab:
                    //LTN - Composite & GameObjectNodeMan
                    pGameObjNode = GhostMan.Find(GameObject.Name.Crab);
                    AlienCrab alienCrab;
                    if (pGameObjNode == null)
                    {
                        alienCrab = new AlienCrab(SpriteGame.Name.Crab, posX, posY);
                    }
                    else
                    {
                        alienCrab = (AlienCrab)pGameObjNode.pGameObj;
                        GhostMan.Remove(pGameObjNode);
                        alienCrab.Resurrect(SpriteGame.Name.Crab, posX, posY);
                    }
                    pGameObj = alienCrab;
                    AlienCounter.Add(1);

                    break;
                case GameObject.Name.Octopus:
                    //LTN - Composite & GameObjectNodeMan
                    pGameObjNode = GhostMan.Find(GameObject.Name.Octopus);
                    AlienOctopus alienOctopus;
                    if (pGameObjNode == null)
                    {
                        alienOctopus = new AlienOctopus(SpriteGame.Name.Octopus, posX, posY);
                    }
                    else
                    {
                        alienOctopus = (AlienOctopus)pGameObjNode.pGameObj;
                        GhostMan.Remove(pGameObjNode);
                        alienOctopus.Resurrect(SpriteGame.Name.Octopus, posX, posY);
                    }
                    pGameObj = alienOctopus;
                    AlienCounter.Add(1);

                    break;
                case GameObject.Name.AlienColumn:
                    pGameObjNode = GhostMan.Find(GameObject.Name.AlienColumn);
                    AlienColumn alienColumn;
                    if (pGameObjNode == null)
                    {
                        alienColumn = new AlienColumn();
                    }
                    else
                    {
                        alienColumn = (AlienColumn)pGameObjNode.pGameObj;
                        GhostMan.Remove(pGameObjNode);

                        alienColumn.Resurrect();
                    }
                    pGameObj = alienColumn;

                    break;
                case GameObject.Name.AlienGrid:
                    pGameObjNode = GhostMan.Find(GameObject.Name.AlienGrid);
                    AlienGrid alienGrid;
                    if (pGameObjNode ==  null)
                    {
                        alienGrid = new AlienGrid();
                    }
                    else
                    {
                        alienGrid = (AlienGrid)pGameObjNode.pGameObj;
                        GhostMan.Remove(pGameObjNode);

                        alienGrid.Resurrect();
                    }
                    pGameObj = alienGrid;
                   
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }

            pGameObj.ActivateSprite(this.pAlienBatch);
            pGameObj.ActivateCollisionSprite(this.pBoxBatch);
            

            return pGameObj;
        }

        //Date: --------------------
        SpriteBatch pAlienBatch;
        SpriteBatch pBoxBatch;
    }
}
