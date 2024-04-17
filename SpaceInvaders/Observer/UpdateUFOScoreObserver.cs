using System;
using System.Diagnostics;

namespace SE456
{
    public class UpdateUFOScoreObserver : ColObserver
    {
        public UpdateUFOScoreObserver(Font.Name name)
        {
            this.score = FontMan.Find(name);
            Debug.Assert(this.score != null);
        }
       
        public override void Notify()
        {
            // Delete missile
            //Debug.WriteLine("UpdateUFOScoreObserver: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);

            this.pUFO = (UFO)this.pSubject.pObjB;
            Debug.Assert(this.pUFO != null);

            //Update the score in scoreMan
            ScoreMan.AddScore(this.pUFO.getPoints());
        }
       

        override public void Dump()
        {

        }
        override public System.Enum GetName()
        {
            return ColObserver.Name.UpdateUFOScoreObserver;
        }

        // --------------------------------------
        // data:
        // --------------------------------------

        private UFO pUFO;
        private Font score;
    }
}
