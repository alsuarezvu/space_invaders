using System;
using System.Diagnostics;

namespace SE456
{
    public class SoundMarchCmd : Command
    {
        public SoundMarchCmd(IrrKlang.ISoundEngine sndEngine) {
            this.poSLinkMan = new SLinkMan();
            Debug.Assert(this.poSLinkMan != null);

            // need to keep iterator for state
            this.pIt = this.poSLinkMan.GetIterator();
            Debug.Assert(this.pIt != null);

            this.soundEngine = sndEngine;
            Debug.Assert(this.soundEngine != null);
        }

        public void Attach(IrrKlang.ISoundSource snd, SoundNode.Name name, String wavName)
        {
            // Create a new holder
            SoundNode pSoundHolder = new SoundNode(snd, name, wavName);
            Debug.Assert(pSoundHolder != null);

            // Attach it to the list ( Push to front )
            this.poSLinkMan.AddToFront(pSoundHolder);

            // update the iterator
            this.pIt = this.poSLinkMan.GetIterator();
            Debug.Assert(this.pIt != null);
        }
        public override void Execute(Delta deltaTime)
        {
            // Wrap if at end of iteration list
            if (this.pIt.IsDone())
            {
                this.pIt.First();
            }

            // Debug.WriteLine("<--- Sound");
            // Get the image
            SoundNode pSoundNode = (SoundNode)this.pIt.Current();
            Debug.Assert(pSoundNode != null);

            // advance for next iteration
            this.pIt.Next();

            // play the sound
            soundEngine.Play2D(pSoundNode.pSound, false, false, false);

            TimerEventMan.AddBasedOnTriggerTime(TimerEvent.Name.AlienMarchSound, this, deltaTime);

        }

        // Data: ---------------
        private SLinkMan poSLinkMan;
        private Iterator pIt;
        private IrrKlang.ISoundEngine soundEngine;
    }
}
