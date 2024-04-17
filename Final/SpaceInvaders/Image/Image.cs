//-----------------------------------------------------------------------------
// Copyright 2024, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System.Diagnostics;

namespace SE456
{
    public class Image : SLink
    {
        //------------------------------------
        // Enum
        //------------------------------------
        public enum Name
        {
            RedGhost,
            PinkGhost,
            BlueGhost,
            OrangeGhost,
            MsPacMan,
            PowerUpGhost,
            Prezel,

            SquidA,
            SquidB,
            CrabA,
            CrabB,
            OctopusA,
            OctopusB,
            Alien_Splat,

            Ship,
            Ship_Splat_1,
            Ship_Splat_2,

            Missile,
            Floor,

            UFO,
            UFO_Splat,

            SquigglyShotA,
            SquigglyShotB,
            SquigglyShotC,
            SquigglyShotD,
            
            PlungerShotA,
            PlungerShotB,
            PlungerShotC,
            PlungerShotD,

            RollingShotA,
            RollingShotB,
            RollingShotC,
            RollingShotD,

            BombSplat,
            BombSplat_Green,

            Brick,
            BrickLeft_Top0,
            BrickLeft_Bottom,
            BrickRight_Top0,
            BrickRight_Bottom,

            NullObject,
            HotPink,
            Uninitialized
        }

        //------------------------------------
        // Constructors
        //------------------------------------
        public Image()
            : base()
        {
            this.name = Name.Uninitialized;
            this.pTexture = null;
            this.poRect = new Azul.Rect();
        }

        //------------------------------------
        // Methods
        //------------------------------------
        public void Set(Name name, Texture pSrcTexture, float x, float y, float w, float h)
        {
            Debug.Assert(pSrcTexture != null);
            this.pTexture = pSrcTexture;

            // Remember the allocation was already made in constructor
            // so don't remove... replace the data
            this.poRect.Set(x,y,w,h);

            this.name = name;
        }
        private void privClear()
        {
            Debug.Assert(this.poRect != null);
            this.name = Name.Uninitialized;
            this.pTexture = null;
            this.poRect.Clear();
        }

        public Azul.Texture GetAzulTexture()
        {
            return this.pTexture.GetAzulTexture();
        }

        public Azul.Rect GetAzulRect()
        {
            return this.poRect;
        }

        //------------------------------------
        // Override
        //------------------------------------
        public override System.Enum GetName()
        {
            return this.name;
        }
        override public void Wash()
        {
            this.baseClear();
            this.privClear();
        }

        override public void Dump()
        {
            // we are using HASH code as its unique identifier 
            Debug.WriteLine("   {0} ({1})", this.name, this.GetHashCode());

            // Data:
            Debug.WriteLine("   Name: {0} ({1})", this.name, this.GetHashCode());
            if (this.pTexture == null)
            {
                Debug.WriteLine("      Texture: null");
            }
            else
            {
                Debug.WriteLine("      Texture: {0}", this.pTexture.name);
            }
            Debug.WriteLine("      Rect: [{0} {1} {2} {3}] ", this.poRect.x, this.poRect.y, this.poRect.width, this.poRect.height);

            // Let the base print its contribution
            this.baseDump();
        }

        //------------------------------------
        // Data
        //------------------------------------
        public Name name;
        public Azul.Rect poRect;
        public Texture pTexture;
    }
}

// --- End of File ---
