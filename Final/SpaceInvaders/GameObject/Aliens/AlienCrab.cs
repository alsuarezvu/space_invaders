using System;
using System.Diagnostics;

namespace SE456
{
    public class AlienCrab : AlienBase
    {
        public AlienCrab(SpriteGame.Name spriteName, float posX, float posY)
            : base(GameObject.Name.Crab, spriteName, posX, posY)
        {
        }

        public override void Accept(ColVisitor other)
        {
            other.VisitCrab(this);  
        }

        public override System.Enum GetName()
        {
            return base.GetName();
        }
       

        public override void Update()
        {
            //Debug.WriteLine("AlienCrab Update");
            base.Update();

        }
    }
}
