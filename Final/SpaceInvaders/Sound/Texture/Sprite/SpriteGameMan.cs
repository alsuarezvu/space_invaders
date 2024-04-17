//-----------------------------------------------------------------------------
// Copyright 2024, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    class SpriteGameMan : ManBase
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        private SpriteGameMan(int reserveNum, int reserveGrow)
                : base(new DLinkMan(), new DLinkMan(), reserveNum, reserveGrow)   // <--- Kick the can (delegate)
        {
            // initialize derived data here
            SpriteGameMan.psSpriteCompare = new SpriteGame();
        }

        //----------------------------------------------------------------------
        // Static Methods
        //----------------------------------------------------------------------
        public static void Create(int reserveNum = 8, int reserveGrow = 1)
        {
            // make sure values are ressonable 
            Debug.Assert(reserveNum >= 0);
            Debug.Assert(reserveGrow > 0);

            // initialize the singleton here
            Debug.Assert(psInstance == null);

            // Do the initialization
            if (psInstance == null)
            {
                psInstance = new SpriteGameMan(reserveNum, reserveGrow);
            }

            // Null sprite added to manager
            SpriteGameMan.Add(SpriteGame.Name.NullObject, Image.Name.NullObject, 0.0f, 0.0f, 0.0f, 0.0f);
        }
        public static void Destroy( bool bPrintEnable = false )
        {
            SpriteGameMan pMan = SpriteGameMan.privGetInstance();
            Debug.Assert(pMan != null);

            // Do something clever here
            // track peak number of active nodes
            // print stats on destroy
            // invalidate the singleton
            if (bPrintEnable)
            {
                SpriteGameMan.DumpStats();
            }
        }
        public static SpriteGame Add(SpriteGame.Name _SpriteName, 
                                        Image.Name _ImageName, 
                                        float x, 
                                        float y, 
                                        float w, 
                                        float h,
                                        Azul.Color pColor = null)
        {
            SpriteGameMan pMan = SpriteGameMan.privGetInstance();
            Debug.Assert(pMan != null);

            Image pImage = ImageMan.Find(_ImageName);
            Debug.Assert(pImage != null);
   
            SpriteGame pSprite = (SpriteGame)pMan.baseAdd();
            Debug.Assert(pSprite != null);

            // Initialize the data
            pSprite.Set(_SpriteName, pImage, x,y,w,h, pColor);
            return pSprite;
        }

        public static SpriteGame Find(SpriteGame.Name name)
        {
            SpriteGameMan pMan = SpriteGameMan.privGetInstance();
            Debug.Assert(pMan != null);

            // Compare functions only compares two Sprites

            // So:  Use the Compare Sprite - as a reference
            //      use in the Compare() function
            SpriteGameMan.psSpriteCompare.name = name;

            SpriteGame pData = (SpriteGame)pMan.baseFind(SpriteGameMan.psSpriteCompare);
            return pData;
        }
        public static void Remove(SpriteGame pSprite)
        {
            SpriteGameMan pMan = SpriteGameMan.privGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pSprite != null);
            pMan.baseRemove(pSprite);
        }
        public static void Dump()
        {
            Debug.WriteLine("\n   ------ SpriteGame Man: ------");

            SpriteGameMan pMan = SpriteGameMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.baseDump();
        }
        public static void DumpStats()
        {
            Debug.WriteLine("\n   ------ SpriteGame Man: ------");

            SpriteGameMan pMan = SpriteGameMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.baseDumpStats();

            Debug.WriteLine("   ------------\n");
        }

        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        override protected NodeBase derivedCreateNode()
        {
            NodeBase pNodeBase = new SpriteGame();
            Debug.Assert(pNodeBase != null);

            return pNodeBase;
        }

        //------------------------------------
        // Private methods
        //------------------------------------
        private static SpriteGameMan privGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(psInstance != null);

            return psInstance;
        }

        //------------------------------------
        // Data: unique data for this manager 
        //------------------------------------
        private static SpriteGame psSpriteCompare;
        private static SpriteGameMan psInstance = null;

    }
}

// --- End of File ---
