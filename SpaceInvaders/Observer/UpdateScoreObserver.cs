using System;
using System.Diagnostics;

namespace SE456
{
    
        public class UpdateScoreObserver : ColObserver
        {
            public UpdateScoreObserver(Font.Name name)
            {
                this.score = FontMan.Find(name);
                Debug.Assert(this.score != null);

                this.pAlien = null;
            }
      

            public override void Notify()
            {
                //Debug.WriteLine("UpdateScoreObserver: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);

                this.pAlien = (AlienBase)this.pSubject.pObjB;
                Debug.Assert(this.pAlien != null);

               
                switch (pAlien.name)
                {
                    case GameObject.Name.Octopus:
                        ScoreMan.AddScore(10);
                        break;

                    case GameObject.Name.Crab:
                        ScoreMan.AddScore(20);
                        break;

                    case GameObject.Name.Squid:
                        ScoreMan.AddScore(30);
                        break;

                }
            }
            public override void Execute()
            {
               //
            }
          
            override public System.Enum GetName()
            {
            return ColObserver.Name.UpdateScoreObserver;
            }

        public override void Dump()
        {
            //no - op
        }



        // -------------------------------------------
        // data:
        // -------------------------------------------

        private readonly Font score;
        private AlienBase pAlien;
        }

}
