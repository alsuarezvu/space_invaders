using IrrKlang;
using System;
using System.Diagnostics;

namespace SE456
{
    public class UFOEventCmd : Command
    {
        public UFOEventCmd(Random pRandom, SpriteBatch _spriteBatchUFO, SpriteBatch _spriteBatchBox,
            ISoundEngine _sndEngine, ISoundSource _sndSource)
        {
            this.pUFORoot = (UFORoot)GameObjectNodeMan.Find(GameObject.Name.UFORoot);
            Debug.Assert(this.pUFORoot != null);

            this.pSB_UFO = _spriteBatchUFO;
            Debug.Assert(this.pSB_UFO != null);

            this.pSB_Boxes = _spriteBatchBox;
            Debug.Assert(this.pSB_Boxes != null);

            this.pRandom = pRandom;

            this.pSoundEngine = _sndEngine;
            this.pSoundSource = _sndSource;
        }

        override public void Execute(Delta deltaTime)
        {
            if (!UFOMan.IsUFOActive())
            {
                UFOMan.CreateUFO();
                this.pSoundEngine.Play2D(pSoundSource, false, false, false);
            }
            //pick the next random delta to decide when the next UFO will go
            deltaTime.setDelta(pRandom.Next(1, MAX_DELTA));

            TimerEventMan.AddBasedOnTriggerTime(TimerEvent.Name.UFO, this, deltaTime);
        }

        readonly UFORoot pUFORoot;
        readonly SpriteBatch pSB_UFO;
        readonly SpriteBatch pSB_Boxes;
        readonly Random pRandom;
        readonly ISoundEngine pSoundEngine;
        readonly ISoundSource pSoundSource;

        //private static readonly int MIN_DELTA = 10;
        private static readonly int MAX_DELTA = 15;
  
    }
}
