using System;
using System.Diagnostics;

namespace SE456
{
    public class AlienGrid : Composite
    {
        public enum DIRECTION
        {
            RIGHT,
            LEFT
        }

        public AlienGrid()
            :base()
        {
            this.name = Name.AlienGrid;
            this.poColObj.pColSprite.SetColor(0, 0, 1);
            this.poColObj.pColSprite.Update();
            this.delta = DeltaMan.Find(Delta.Name.Move);
            this.delta_x = DELTA_X;
        }

        public void Resurrect()
        {
            this.name = Name.AlienGrid;
            this.poColObj.pColSprite.SetColor(0, 0, 1);
            this.poColObj.pColSprite.Update();
            this.delta = DeltaMan.Find(Delta.Name.Move);
            this.delta_x = DELTA_X;
            
            base.Resurrect();
            this.poColObj.pColSprite.SetColor(0, 0, 1);
        }
        public override void Accept(ColVisitor other)
        {
            other.Visit(this);
        }

        public override void Update()
        {
            base.BaseUpdateBoundingBox(this);
            base.Update();
        }

        public void MoveGrid()
        {

            IteratorForwardComposite pFor = new IteratorForwardComposite(this);

            Component pNode = pFor.First();

            //Debug.WriteLine("Moving Grid: X {0} Y {1}", this.delta_x, this.delta_y);
            while (!pFor.IsDone())
            {
                GameObject pGameObj = (GameObject)pNode;
                pGameObj.x += this.delta_x;
                pGameObj.y += this.delta_y;
                pNode = pFor.Next();
            }
            this.delta_y = 0.0f;
        }

        public int getAlienColumnCount()
        {
            IteratorForwardComposite pFor = new IteratorForwardComposite(this);
            int count = 0;
            Component pNode = pFor.First();
            while (!pFor.IsDone())
            {
               GameObject pGameObj = (GameObject)(pNode);
               if((GameObject.Name)pGameObj.GetName() == GameObject.Name.AlienColumn)
                {
                    count++;
                }

               pNode = pFor.Next();
                
            }
            return count;
        }

        public X_Y_pos getAlienCol_X_Y(int col)
        { 
            int count = 0;

            IteratorForwardComposite pFor = new IteratorForwardComposite(this);
            GameObject pGameObj = null;
            while (!pFor.IsDone() && count < col)
            {
                pGameObj = (GameObject)pFor.Curr();

                if (pGameObj.name == GameObject.Name.AlienColumn)
                {
                    count++;
                }
                pFor.Next();
            }

            Debug.Assert(pGameObj != null);

            //send back the lowest point of the column so the bomb can
            //be launched from it
            AlienColumn column = (AlienColumn)pGameObj;
            float bottomY = pGameObj.y - (column.poColObj.poColRect.height / 2);
            return new X_Y_pos(pGameObj.x, bottomY);
        }

        public class X_Y_pos
        {
            public float x;
            public float y;

            public X_Y_pos(float x, float y)
            {
                this.x = x;
                this.y = y;
            }
        }

        public void SetDelta_x(float inDelta)
        {
            this.delta_x = inDelta;
        }

        public void SetDelta_y(float inDelta)
        {
            this.delta_y = inDelta;
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            // MissileRoot vs WallRoot
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(m);
            ColPair.Collide(pGameObj, this);
        }

        public override void VisitMissile(Missile m)
        {
            // Missile vs WallRoot
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(this);
            ColPair.Collide(m, pGameObj);
        }

        // Data: ---------------
        private float delta_x;
        private float delta_y;
        public static readonly float DELTA_X = 4.0f;
        private Delta delta;
    }
}
