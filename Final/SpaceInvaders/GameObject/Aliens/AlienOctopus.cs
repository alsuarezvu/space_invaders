using System;
using System.Diagnostics;

namespace SE456
{
    public class AlienOctopus : AlienBase
    {
        public AlienOctopus(SpriteGame.Name spriteName, float posX, float posY)
            : base(GameObject.Name.Octopus, spriteName, posX, posY)
        {
        }

        public override void Accept(ColVisitor other)
        {
            other.VisitOctopus(this);
        }

        public override System.Enum GetName()
        {
            return base.GetName();
        }
        public override void Update()
        {
            //Debug.WriteLine("AlienOctopus Update");
            base.Update();

        }
    }
}
