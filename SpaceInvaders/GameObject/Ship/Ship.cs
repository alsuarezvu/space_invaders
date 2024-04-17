//-----------------------------------------------------------------------------
// Copyright 2024, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    public class Ship : ShipCategory
    {

        public Ship(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY)
         : base(name, spriteName, posX, posY, ShipCategory.Type.Ship)
        {
            this.x = posX;
            this.y = posY;

            this.shipSpeed = 2.0f;
            this.missileState = null;
            this.moveState = null;

            this.poColObj.pColSprite.SetColor(0, 1, 0);
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an Bomb
            // Call the appropriate collision reaction
            other.VisitShip(this);
        }

        public void MoveRight()
        {
            this.moveState.MoveRight(this);
        }

        public void MoveLeft()
        {
            this.moveState.MoveLeft(this);
        }

        public void ShootMissile()
        {
            this.missileState.ShootMissile(this);
        }

        public void SetMissileState(ShipMan.State inState)
        {
            this.missileState = ShipMan.GetMissileState(inState);
        }

        public void SetMoveState(ShipMan.State inState)
        {
            this.moveState = ShipMan.GetMoveState(inState);
        }

        public void Handle()
        {
            this.missileState.Handle(this);
        }
        public MissileShipState GetState()
        {
            return this.missileState;
        }

        public void Resurrect(float posX, float posY)
        {
            this.x = posX;
            this.y = posY;
            this.shipSpeed = 2.0f;
            this.poColObj.pColSprite.SetColor(0, 1, 0);

            base.Resurrect();
        }

        public override void VisitBombRoot(BombRoot b)
        {
            // Bombroot vs Ship
            ColPair.Collide(b, (GameObject)IteratorForwardComposite.GetChild(this)); 
        }

        public override void VisitBomb(Bomb b)
        {
            // Bomb vs Ship
            //Debug.WriteLine(" ---> Done");
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(b, this);
            pColPair.NotifyListeners();
        }

        public override void Visit(AlienGrid a)
        {
            // GridRoot vs. ShipRoot
            GameObject pGameObjectAlienColumn = (GameObject)IteratorForwardComposite.GetChild(a);
            ColPair.Collide(pGameObjectAlienColumn, this);
        }

        public override void VisitColumn(AlienColumn a)
        {
            // GridRoot vs. ShipRoot
            GameObject pGameObjectAlien = (GameObject)IteratorForwardComposite.GetChild(a);
            ColPair.Collide(pGameObjectAlien, this);
        }

        public override void VisitSquid(AlienSquid a)
        {
            // Bomb vs Ship
            //Debug.WriteLine(" ---> Done");
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(a, this);
            pColPair.NotifyListeners();
        }

        public override void VisitCrab(AlienCrab a)
        {
            // Bomb vs Ship
            //Debug.WriteLine(" ---> Done");
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(a, this);
            pColPair.NotifyListeners();
        }

        public override void VisitOctopus(AlienOctopus a)
        {
            // Bomb vs Ship
            //Debug.WriteLine(" ---> Done");
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(a, this);
            pColPair.NotifyListeners();
        }

        // Data: --------------------
        public float shipSpeed;
        private MissileShipState missileState;
        private MoveShipState moveState;
    }
}

// --- End of File ---
