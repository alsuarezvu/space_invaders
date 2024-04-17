//-----------------------------------------------------------------------------
// Copyright 2024, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    class TextureMan : ManBase
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        private TextureMan(int reserveNum, int reserveGrow)
                : base(new SLinkMan(),new SLinkMan(), reserveNum, reserveGrow)   // <--- Kick the can (delegate)
        {
            // initialize derived data here
            TextureMan.psTextureCompare = new Texture();
            Debug.Assert(TextureMan.psTextureCompare != null);

        }

        //----------------------------------------------------------------------
        // Static Methods
        //----------------------------------------------------------------------
        public static void Create(int reserveNum = 3, int reserveGrow = 1)
        {
            // make sure values are ressonable 
            Debug.Assert(reserveNum >= 0);
            Debug.Assert(reserveGrow > 0);

            // initialize the singleton here
            Debug.Assert(psInstance == null);

            // Do the initialization
            if (psInstance == null)
            {
                psInstance = new TextureMan(reserveNum, reserveGrow);
            }

            // Preload these
            TextureMan.Add(Texture.Name.HotPink, "HotPink.t.azul");
            TextureMan.Add(Texture.Name.NullObject, "HotPink.t.azul");
        }
        public static void Destroy(bool bPrintEnable = false)
        {
            TextureMan pMan = TextureMan.privGetInstance();
            Debug.Assert(pMan != null);

            // Do something clever here
            // track peak number of active nodes
            // print stats on destroy
            // invalidate the singleton
            if (bPrintEnable)
            {
            TextureMan.DumpStats();
        }
        }
     
        public static Texture Add(Texture.Name name, string pTextureName)
        {
            TextureMan pMan = TextureMan.privGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pTextureName != null);

            Texture pTexture = (Texture)pMan.baseAdd();
            Debug.Assert(pTexture != null);

            // Initialize the data
            pTexture.Set(name, pTextureName);
            return pTexture;
        }

        public static Texture Find(Texture.Name name)
        {
            TextureMan pMan = TextureMan.privGetInstance();
            Debug.Assert(pMan != null);

            // Compare functions only compares two Textures

            // So:  Use the Compare Texture - as a reference
            //      use in the Compare() function
            TextureMan.psTextureCompare.name = name;

            Texture pData = (Texture)pMan.baseFind(TextureMan.psTextureCompare);
            return pData;
        }
        public static void Remove(Texture pTexture)
        {
            TextureMan pMan = TextureMan.privGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pTexture != null);
            pMan.baseRemove(pTexture);
        }
        public static void Dump()
        {
            Debug.WriteLine("\n   ------ Texture Man: ------");

            TextureMan pMan = TextureMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.baseDump();
        }

        public static void DumpStats()
        {
            Debug.WriteLine("\n   ------ Texture Man: ------");

            TextureMan pMan = TextureMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.baseDumpStats();

            Debug.WriteLine("   ------------\n");
        }


        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        override protected NodeBase derivedCreateNode()
        {
            NodeBase pNodeBase = new Texture();
            Debug.Assert(pNodeBase != null);

            return pNodeBase;
        }

        //------------------------------------
        // Private methods
        //------------------------------------
        private static TextureMan privGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(psInstance != null);

            return psInstance;
        }

        //------------------------------------
        // Data: unique data for this manager 
        //------------------------------------
        private static Texture psTextureCompare;
        private static TextureMan psInstance = null;
    }
}

// --- End of File ---
