using System;
using System.Diagnostics;

namespace SE456
{
    public class SoundNode : SLink
    {
        public enum Name {
            fastinvader1,
            fastinvader2,
            fastinvader3,
            fastinvader4,
        }
        //------------------------------------
        // Constructors
        //------------------------------------
        public SoundNode(IrrKlang.ISoundSource sound, Name soundName, String wavName)
            : base()
        {
            Debug.Assert(sound != null);
            this.pSound = sound;
            this.name = soundName;
            this.wav = wavName;

            Debug.Assert(this.pSound != null);
            Debug.Assert(this.wav != null);
        }

       
        public override void Dump()
        {
            // we are using HASH code as its unique identifier 
            Debug.WriteLine("   ({0}) node", this.GetHashCode());

            // Data:
            Debug.WriteLine("   sound name: {0}", this.GetName());

            // Let the base print its contribution
            this.baseDump();
        }

        public override Enum GetName()
        {
            return this.name;
        }

        public override void Wash()
        {
            this.privClean();
        }

        private void privClean()
        {
            this.pSound = null;
        }

        //------------------------------------
        // Data
        //------------------------------------
        public Name name;
        public IrrKlang.ISoundSource pSound;
        public String wav;
        
    }
}
