using System;
using System.Diagnostics;

namespace SE456
{
    public class AlienSquid : AlienBase
    {
        public AlienSquid(SpriteGame.Name spriteName, float posX, float posY)
            : base(GameObject.Name.Squid, spriteName, posX, posY)
        { 
        }

        public override void Accept(ColVisitor other)
        {
            other.VisitSquid(this);
        }

        public override System.Enum GetName()
        {
            return base.GetName(); 
        }
        public override void Update()
        {
            //Debug.WriteLine("AlienSquid Update");
            base.Update();

        }
    }
}
