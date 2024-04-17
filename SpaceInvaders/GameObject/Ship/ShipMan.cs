//-----------------------------------------------------------------------------
// Copyright 2024, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using IrrKlang;
using System;
using System.Diagnostics;

namespace SE456
{
    public class ShipMan
    {
        public enum State
        {
            Ready,
            MissileFlying,
            Dead,
            MoveLeftRight,
            MoveRight,
            MoveLeft,
            MoveEnd,
        }

        private ShipMan(IrrKlang.ISoundSource snd)
        {
            // Store the states
            this.pStateReady = new ShipStateReady();
            this.pStateMissileFlying = new ShipStateMissileFlying(snd);
            this.pStateDead = new ShipStateDead();
            this.pStateMoveLeftRight = new ShipMoveLeftRight();
            this.pStateMoveRight = new ShipMoveRight();
            this.pStateMoveLeft = new ShipMoveLeft();
            this.pStateMoveEnd = new ShipMoveEndState();

            // set active
            this.pShip = null;
            this.pMissile = null;
            this.countReserveShips = RESERVE_SHIP_COUNT;
        }

        public static void Create(IrrKlang.ISoundSource snd)
        {
            // make sure its the first time
            Debug.Assert(instance == null);

            // Do the initialization
            if (instance == null)
            {
                instance = new ShipMan(snd);
            }

            Debug.Assert(instance != null);
        }
        public static void CreateActiveShip(GameObject shipRoot)
        {
            // Stuff to initialize after the instance was created
            instance.pShip = ActivateShip(shipRoot);
            instance.pShip.SetMissileState(ShipMan.State.Ready);
            instance.pShip.SetMoveState(ShipMan.State.MoveLeftRight);
        }

        private static ShipMan PrivInstance()
        {
            Debug.Assert(instance != null);

            return instance;
        }

        public static Ship GetShip()
        {
            ShipMan pShipMan = ShipMan.PrivInstance();

            Debug.Assert(pShipMan != null);
            //Debug.Assert(pShipMan.pShip != null);

            return pShipMan.pShip;
        }

        public static MissileShipState GetMissileState(State state)
        {
            ShipMan pShipMan = ShipMan.PrivInstance();
            Debug.Assert(pShipMan != null);

            MissileShipState pShipState = null;

            switch (state)
            {
                case ShipMan.State.Ready:
                    pShipState = pShipMan.pStateReady;
                    break;

                case ShipMan.State.MissileFlying:
                    pShipState = pShipMan.pStateMissileFlying;
                    break;

                case ShipMan.State.Dead:
                    pShipState = pShipMan.pStateDead;
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }

            return pShipState;
        }

        public static MoveShipState GetMoveState(State state)
        {
            ShipMan pShipMan = ShipMan.PrivInstance();
            Debug.Assert(pShipMan != null);

            MoveShipState pShipState = null;

            switch (state)
            {
                case ShipMan.State.MoveLeftRight:
                    pShipState = pShipMan.pStateMoveLeftRight;
                    break;

                case ShipMan.State.MoveRight:
                    pShipState = pShipMan.pStateMoveRight;
                    break;

                case ShipMan.State.MoveLeft:
                    pShipState = pShipMan.pStateMoveLeft;
                    break;

                case ShipMan.State.MoveEnd:
                    pShipState = pShipMan.pStateMoveEnd;
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }

            return pShipState;
        }



        public static Missile GetMissile()
        {
            ShipMan pShipMan = ShipMan.PrivInstance();

            Debug.Assert(pShipMan != null);
            Debug.Assert(pShipMan.pMissile != null);

            return pShipMan.pMissile;
        }

        public static Missile ActivateMissile()
        {
            ShipMan pShipMan = ShipMan.PrivInstance();
            Debug.Assert(pShipMan != null);

            Missile pMissile = null;
            GameObjectNode pGameObjNode = GhostMan.Find(GameObject.Name.Missile);
            if (pGameObjNode == null)
            {
                pMissile = new Missile(SpriteGame.Name.Missile, 400, 100);
            }
            else
            {
                pMissile = (Missile)pGameObjNode.pGameObj;
                GhostMan.Remove(pGameObjNode);

                pMissile.Resurrect(400, 100);
            }

            pShipMan.pMissile = pMissile;

            // Attached to SpriteBatches
            SpriteBatch pSB_Missile = SpriteBatchMan.Find(SpriteBatch.Name.Missile);
            SpriteBatch pSB_Boxes = SpriteBatchMan.Find(SpriteBatch.Name.Box);

            pMissile.ActivateCollisionSprite(pSB_Boxes);
            pMissile.ActivateSprite(pSB_Missile);

            // Attach the missile to the missile root
            GameObject pMissileGroup = GameObjectNodeMan.Find(GameObject.Name.MissileGroup);
            Debug.Assert(pMissileGroup != null);

            pMissileGroup.ActivateCollisionSprite(pSB_Boxes);

            // Add to GameObject Tree - {update and collisions}
            pMissileGroup.Add(pShipMan.pMissile);

            return pShipMan.pMissile;
        }

        private static Ship ActivateShip(GameObject shipRoot)
        {
            ShipMan pShipMan = ShipMan.PrivInstance();
            Debug.Assert(pShipMan != null);

            // copy over safe copy
            // LTN - owned by ShipMan.. but needs some cleanup
            Ship pShip = null;
            GameObjectNode pGameObjNode = GhostMan.Find(GameObject.Name.Ship);
            if (pGameObjNode == null)
            {
                pShip = new Ship(GameObject.Name.Ship, SpriteGame.Name.Ship, 200, 100);
            }
            else
            {
                pShip = (Ship)pGameObjNode.pGameObj;
                GhostMan.Remove(pGameObjNode);

                pShip.Resurrect(200, 100);
            }

            pShipMan.pShip = pShip;

            // Attach the sprite to the correct sprite batch
            SpriteBatch pSB_Ship = SpriteBatchMan.Find(SpriteBatch.Name.Ship);
            pSB_Ship.Attach(pShip.pSpriteProxy);

            //Activate ship collision boxes
            SpriteBatch pSB_Boxes = SpriteBatchMan.Find(SpriteBatch.Name.Box);
            pSB_Boxes.Attach(pShip.poColObj.pColSprite);

            // Add to GameObject Tree - {update and collisions}
            shipRoot.Add(pShipMan.pShip);
            return pShip;
        }

        public static Ship CreateReserveShip(float x, float y)
        {
            ShipMan pShipMan = ShipMan.PrivInstance();
            Debug.Assert(pShipMan != null);

            // copy over safe copy
            // LTN - owned by ShipMan.. but needs some cleanup

            Ship pShip = null;
            GameObjectNode pGameObjNode = GhostMan.Find(GameObject.Name.ShipReserve);
            if (pGameObjNode == null)
            {
                pShip = new Ship(GameObject.Name.ShipReserve, SpriteGame.Name.Ship, x, y);
            }
            else
            {
                pShip = (Ship)pGameObjNode.pGameObj;
                GhostMan.Remove(pGameObjNode);

                pShip.Resurrect(x, y);
            }

            // Attach the sprite to the correct sprite batch
            SpriteBatch pSB_Ships = SpriteBatchMan.Find(SpriteBatch.Name.Ship);
            pSB_Ships.Attach(pShip.pSpriteProxy);

            //Activate ship collision boxes
            SpriteBatch pSB_Boxes = SpriteBatchMan.Find(SpriteBatch.Name.Box);
            pSB_Boxes.Attach(pShip.poColObj.pColSprite);

            //attach to reserve root
            GameObject pShipRootReserve = GameObjectNodeMan.Find(GameObject.Name.ShipRootReserve);
            Debug.Assert(pShipRootReserve != null);

            // Add to GameObject Tree - {update and collisions}
            pShipRootReserve.Add(pShip);

            return pShip;
        }

        public static Ship ActivateReserveShip()
        {
            ShipMan pShipMan = ShipMan.PrivInstance();
            Debug.Assert(pShipMan != null);

            if (pShipMan.countReserveShips > 0)
            {
                // copy over safe copy
                // LTN - owned by ShipMan.. but needs some cleanup
                GameObject pShipRootReserve = GameObjectNodeMan.Find(GameObject.Name.ShipRootReserve);
                Debug.Assert(pShipRootReserve != null);

                Composite composite = (Composite)pShipRootReserve;

                Ship pShip = (Ship)composite.GetHead();
                composite.Remove(pShip);

                pShip.x = 200;
                pShip.y = 100;

                //Activate ship collision boxes
                /*SpriteBatch pSB_Boxes = SpriteBatchMan.Find(SpriteBatch.Name.Box);
                pSB_Boxes.Attach(pShip.poColObj.pColSprite);*/

                //update the link
                pShipMan.pShip = pShip;

                instance.pShip.SetMissileState(ShipMan.State.Ready);
                instance.pShip.SetMoveState(ShipMan.State.MoveLeftRight);

                //Add it to the active ship root
                GameObject pShipRoot = GameObjectNodeMan.Find(GameObject.Name.ShipRoot);
                Debug.Assert(pShipRoot != null);

                // Add to GameObject Tree - {update and collisions}
                pShipRoot.Add(pShipMan.pShip);

                pShipMan.countReserveShips--;
                return pShip;
            }

            return null;
           
        }

        public static int getReserveShipCount()
        {
            ShipMan pShipMan = ShipMan.PrivInstance();
            Debug.Assert(pShipMan != null);

            return pShipMan.countReserveShips;
        }

        public static void resetForNewGame()
        {
            ShipMan pShipMan = ShipMan.PrivInstance();
            Debug.Assert(pShipMan != null);

            pShipMan.pShip = null;
            pShipMan.pMissile = null;
            pShipMan.countReserveShips = RESERVE_SHIP_COUNT;
        }

        // Data: ----------------------------------------------
        private static ShipMan instance = null;

        // Active
        private Ship pShip;
        private Missile pMissile;

        //to keep track of resere ships
        private int countReserveShips = RESERVE_SHIP_COUNT;

        // Reference
        private ShipStateReady pStateReady;
        private ShipStateMissileFlying pStateMissileFlying;
        private readonly ShipStateDead pStateDead;
        private ShipMoveLeftRight pStateMoveLeftRight;
        private ShipMoveRight pStateMoveRight;
        private ShipMoveLeft pStateMoveLeft;
        private ShipMoveEndState pStateMoveEnd;

        private static readonly int RESERVE_SHIP_COUNT = 3;
       
    }
}

// --- End of File ---
