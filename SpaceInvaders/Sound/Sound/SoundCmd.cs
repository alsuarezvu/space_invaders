using IrrKlang;
using System;
using System.Diagnostics;
namespace SE456
{
    public class SoundCmd : Command
    {
        public SoundCmd(IrrKlang.ISoundEngine sndEngine, IrrKlang.ISoundSource snd)
        {
            this.soundEngine = sndEngine;
            Debug.Assert(this.soundEngine != null);

            this.soundSource = snd;
        }

        public override void Execute(Delta deltaTime)
        {
            // play the sound
            soundEngine.Play2D(soundSource, false, false, false);
        }


        private IrrKlang.ISoundEngine soundEngine;
        private ISoundSource soundSource;
    }
}
