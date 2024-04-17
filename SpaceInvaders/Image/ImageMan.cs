//----------------------------------------------------------------------ImageMan-------
// Copyright 2024, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    public class ImageMan : ManBase
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        private ImageMan(int reserveNum, int reserveGrow)
                : base(new SLinkMan(), new SLinkMan(), reserveNum, reserveGrow)   // <--- Kick the can (delegate)
        {
            // initialize derived data here
            psImageCompare = new Image();
        }

        //----------------------------------------------------------------------
        // Static Methods
        //----------------------------------------------------------------------
        public static void Create(int reserveNum = 9, int reserveGrow = 1)
        {
            // make sure values are ressonable 
            Debug.Assert(reserveNum >= 0);
            Debug.Assert(reserveGrow > 0);

            // initialize the singleton here
            Debug.Assert(psInstance == null);

            // Do the initialization
            if (psInstance == null)
            {
                psInstance = new ImageMan(reserveNum, reserveGrow);
            }

            // Have the manager pre-load these
            ImageMan.Add(Image.Name.HotPink, Texture.Name.HotPink, 0, 0, 128, 128);
            ImageMan.Add(Image.Name.NullObject, Texture.Name.HotPink, 0, 0, 0, 0);
        }
        public static void Destroy(bool bPrintEnable = false)
        {
            ImageMan pMan = ImageMan.privGetInstance();
            Debug.Assert(pMan != null);

            // Do something clever here
            // track peak number of active nodes
            // print stats on destroy
            // invalidate the singleton
            if (bPrintEnable)
            {
            ImageMan.DumpStats();
        }
        }
        public static Image Add(Image.Name name, Texture.Name _TextName, float x, float y, float w, float h)
        {
            ImageMan pMan = ImageMan.privGetInstance();
            Debug.Assert(pMan != null);

            Texture pTexture = TextureMan.Find(_TextName);
            Debug.Assert(pTexture != null);

            Image pImage = (Image)pMan.baseAdd();
            Debug.Assert(pImage != null);

            // Initialize the data
            pImage.Set(name, pTexture, x,y,w,h);
            return pImage;
        }
        public static Image Find(Image.Name name)
        {
            ImageMan pMan = ImageMan.privGetInstance();
            Debug.Assert(pMan != null);

            // Compare functions only compares two Images

            // So:  Use the Compare Image - as a reference
            //      use in the Compare() function
            ImageMan.psImageCompare.name = name;

            Image pData = (Image)pMan.baseFind(ImageMan.psImageCompare);
            return pData;
        }
        public static void Remove(Image pImage)
        {
            ImageMan pMan = ImageMan.privGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pImage != null);
            pMan.baseRemove(pImage);
        }
        public static void Dump()
        {
            Debug.WriteLine("\n   ------ Image Man: ------");

            ImageMan pMan = ImageMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.baseDump();

        }
        public static void DumpStats()
        {
            Debug.WriteLine("\n   ------ Image Man: ------");

            ImageMan pMan = ImageMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.baseDumpStats();

            Debug.WriteLine("   ------------\n");
        }


        //------------------------------------
        // Override Abstract methods
        //------------------------------------
        override protected NodeBase derivedCreateNode()
        {
            NodeBase pNodeBase = new Image();
            Debug.Assert(pNodeBase != null);

            return pNodeBase;
        }

        //------------------------------------
        // Private methods
        //------------------------------------
        private static ImageMan privGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(psInstance != null);

            return psInstance;
        }

        //------------------------------------
        // Data: unique data for this manager 
        //------------------------------------
        private static Image psImageCompare;
        private static ImageMan psInstance = null;
    }
}

// --- End of File ---
