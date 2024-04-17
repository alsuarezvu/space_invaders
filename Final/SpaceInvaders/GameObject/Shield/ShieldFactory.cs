//-----------------------------------------------------------------------------
// Copyright 2024, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    class ShieldFactory
    {
        public ShieldFactory(SpriteBatch _spriteBatch, SpriteBatch _collisionSpriteBatch, Composite pTree)
        {
            this.pSpriteBatch = _spriteBatch;
            Debug.Assert(this.pSpriteBatch != null);

            this.pCollisionSpriteBatch = _collisionSpriteBatch;
            Debug.Assert(this.pCollisionSpriteBatch != null);

            Debug.Assert(pTree != null);
            this.pTree = pTree;
        }
        public void SetParent(GameObject pParentNode)
        {
            // OK being null
            Debug.Assert(pParentNode != null);
            this.pTree = (Composite)pParentNode;
        }
        ~ShieldFactory()
        {
            this.pSpriteBatch = null;
        }
        public GameObject Create(ShieldCategory.Type type, GameObject.Name gameName, float posX = 0.0f, float posY = 0.0f)
        {
            GameObject pShield = null;
            GameObjectNode pGameObjNode = null;

            switch (type)
            {
                case ShieldCategory.Type.Brick:
                    ShieldBrick shieldBrick = null;
                    pGameObjNode = GhostMan.Find(gameName);
                    if (pGameObjNode == null)
                    {
                        shieldBrick = new ShieldBrick(gameName, SpriteGame.Name.Brick, posX, posY);
                    }
                    else
                    {
                        shieldBrick = (ShieldBrick)pGameObjNode.pGameObj;
                        GhostMan.Remove(pGameObjNode);

                        shieldBrick.Resurrect(SpriteGame.Name.Brick, posX, posY);
                    }
                    pShield = shieldBrick;
                    pShield.ActivateSprite(this.pSpriteBatch);
                    break;

                case ShieldCategory.Type.LeftTop0:
                    ShieldBrick shieldBrick_leftTop0 = null;
                    pGameObjNode = GhostMan.Find(gameName);
                    if (pGameObjNode == null)
                    {
                        shieldBrick_leftTop0 = new ShieldBrick(gameName, SpriteGame.Name.Brick_LeftTop0, posX, posY);
                    }
                    else
                    {
                        shieldBrick_leftTop0 = (ShieldBrick)pGameObjNode.pGameObj;
                        GhostMan.Remove(pGameObjNode);

                        shieldBrick_leftTop0.Resurrect(SpriteGame.Name.Brick_LeftTop0, posX, posY);
                    }
                    pShield = shieldBrick_leftTop0;
                    pShield.ActivateSprite(this.pSpriteBatch);
                    break;

                case ShieldCategory.Type.LeftBottom:
                    ShieldBrick shieldBrick_leftBottom = null;
                    pGameObjNode = GhostMan.Find(gameName);
                    if (pGameObjNode == null)
                    {
                        shieldBrick_leftBottom = new ShieldBrick(gameName, SpriteGame.Name.Brick_LeftBottom, posX, posY);
                    }
                    else
                    {
                        shieldBrick_leftBottom = (ShieldBrick)pGameObjNode.pGameObj;
                        GhostMan.Remove(pGameObjNode);

                        shieldBrick_leftBottom.Resurrect(SpriteGame.Name.Brick_LeftBottom, posX, posY);
                    }
                    pShield = shieldBrick_leftBottom;
                    pShield.ActivateSprite(this.pSpriteBatch);
                    break;

                case ShieldCategory.Type.RightTop0:
                    ShieldBrick shieldBrick_rightTop0 = null;
                    pGameObjNode = GhostMan.Find(gameName);
                    if (pGameObjNode == null)
                    {
                        shieldBrick_rightTop0 = new ShieldBrick(gameName, SpriteGame.Name.Brick_RightTop0, posX, posY);
                    }
                    else
                    {
                        shieldBrick_rightTop0 = (ShieldBrick)pGameObjNode.pGameObj;
                        GhostMan.Remove(pGameObjNode);

                        shieldBrick_rightTop0.Resurrect(SpriteGame.Name.Brick_RightTop0, posX, posY);
                    }
                    pShield = shieldBrick_rightTop0;
                    pShield.ActivateSprite(this.pSpriteBatch);
                    break;

                case ShieldCategory.Type.RightBottom:
                    ShieldBrick shieldBrick_rightBottom = null;
                    pGameObjNode = GhostMan.Find(gameName);
                    if (pGameObjNode == null)
                    {
                        shieldBrick_rightBottom = new ShieldBrick(gameName, SpriteGame.Name.Brick_RightBottom, posX, posY);
                    }
                    else
                    {
                        shieldBrick_rightBottom = (ShieldBrick)pGameObjNode.pGameObj;
                        GhostMan.Remove(pGameObjNode);

                        shieldBrick_rightBottom.Resurrect(SpriteGame.Name.Brick_RightBottom, posX, posY);
                    }
                    pShield = shieldBrick_rightBottom;
                    pShield.ActivateSprite(this.pSpriteBatch);
                  
                    break;

                case ShieldCategory.Type.Root:
                    pShield = new ShieldRoot(gameName, SpriteGame.Name.NullObject, posX, posY);
                    pShield.SetCollisionColor(0.0f, 0.0f, 1.0f);
                    break;

                case ShieldCategory.Type.Grid:
                    ShieldGrid shieldGrid = null;
                    pGameObjNode = GhostMan.Find(gameName);
                    if (pGameObjNode == null)
                    {
                        shieldGrid = new ShieldGrid(gameName, SpriteGame.Name.NullObject, posX, posY);
                    }
                    else
                    {
                        shieldGrid = (ShieldGrid)pGameObjNode.pGameObj;
                        GhostMan.Remove(pGameObjNode);

                        shieldGrid.Resurrect(posX, posY);
                    }

                    shieldGrid.SetCollisionColor(0.0f, 0.0f, 1.0f);
                    pShield = shieldGrid;
                    break;

                case ShieldCategory.Type.Column:
                    ShieldColumn shieldColumn = null;
                    pGameObjNode = GhostMan.Find(gameName);
                    if (pGameObjNode == null)
                    {
                        shieldColumn = new ShieldColumn(gameName, SpriteGame.Name.NullObject, posX, posY);
                    }
                    else
                    {
                        shieldColumn = (ShieldColumn)pGameObjNode.pGameObj;
                        GhostMan.Remove(pGameObjNode);

                        shieldColumn.Resurrect(posX, posY);
                    }

                    shieldColumn.SetCollisionColor(0.0f, 0.0f, 1.0f);
                    pShield = shieldColumn;
                    break;

                default:
                    // something is wrong
                    Debug.Assert(false);
                    break;
            }

            // add to the tree
            this.pTree.Add(pShield);

            // Attached to Group
            
            pShield.ActivateCollisionSprite(this.pCollisionSpriteBatch);

            return pShield;
        }
        
        public void createShield(GameObject pParentNode, float leftBottom_posX, float rightBottom_posY)
        {
            
            float COL_0 = leftBottom_posX;
            float COL_1 = COL_0 + BRICK_WIDTH;
            float COL_2 = COL_1 + BRICK_WIDTH;
            float COL_3 = COL_2 + BRICK_WIDTH;
            float COL_4 = COL_3 + BRICK_WIDTH;
            float COL_5 = COL_4 + BRICK_WIDTH;
            float COL_6 = COL_5 + BRICK_WIDTH;

            float ROW_0 = rightBottom_posY;
            float ROW_1 = ROW_0 + BRICK_HEIGHT;
            float ROW_2 = ROW_1 + BRICK_HEIGHT;
            float ROW_3 = ROW_2 + BRICK_HEIGHT;
            float ROW_4 = ROW_3 + BRICK_HEIGHT;

            this.SetParent(pParentNode);

            //column 0 start from bottom
            this.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, COL_0, ROW_0);
            this.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, COL_0, ROW_1);
            this.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, COL_0, ROW_2);
            this.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, COL_0, ROW_3);
            this.Create(ShieldCategory.Type.LeftTop0, GameObject.Name.ShieldBrick, COL_0, ROW_4);

            //column 1 start from bottom
            this.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, COL_1, ROW_0);
            this.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, COL_1, ROW_1);
            this.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, COL_1, ROW_2);
            this.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, COL_1, ROW_3);
            this.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, COL_1, ROW_4);

            //column 3 start from bottom
            this.Create(ShieldCategory.Type.LeftBottom, GameObject.Name.ShieldBrick, COL_2, ROW_1);
            this.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, COL_2, ROW_2);
            this.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, COL_2, ROW_3);
            this.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, COL_2, ROW_4);

            //column 4 start from bottom
            this.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, COL_3, ROW_2);
            this.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, COL_3, ROW_3);
            this.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, COL_3, ROW_4);

            //column 5 start from bottom
            this.Create(ShieldCategory.Type.RightBottom, GameObject.Name.ShieldBrick, COL_4, ROW_1);
            this.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, COL_4, ROW_2);
            this.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, COL_4, ROW_3);
            this.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, COL_4, ROW_4);

            //column 6 start from bottom
            this.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, COL_5, ROW_0);
            this.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, COL_5, ROW_1);
            this.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, COL_5, ROW_2);
            this.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, COL_5, ROW_3);
            this.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, COL_5, ROW_4);

            //column 7 start from bottom
            this.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, COL_6, ROW_0);
            this.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, COL_6, ROW_1);
            this.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, COL_6, ROW_2);
            this.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, COL_6, ROW_3);
            this.Create(ShieldCategory.Type.RightTop0, GameObject.Name.ShieldBrick, COL_6, ROW_4);
        }

        // Data: ---------------------
        private SpriteBatch pSpriteBatch;
        private readonly SpriteBatch pCollisionSpriteBatch;
        private Composite pTree;

        private readonly static float BRICK_WIDTH = 11.0f;
        private readonly static float BRICK_HEIGHT = 11.0f;



    }
}

// --- End of File ---
