//-----------------------------------------------------------------------------
// Copyright 2024, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    public class SpriteBatchMan : ManBase
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        public SpriteBatchMan(int reserveNum = 1, int reserveGrow = 1)
                : base(new DLinkMan(), new DLinkMan(), reserveNum, reserveGrow)   // <--- Kick the can (delegate)
        {
            SpriteBatchMan.psActiveSBMan = null;
        }

        //----------------------------------------------------------------------
        // Static Methods
        //----------------------------------------------------------------------
        public static void Create()
        {
            // initialize the singleton here
            Debug.Assert(psInstance == null);

            // Do the initialization
            if (psInstance == null)
            {
                psInstance = new SpriteBatchMan();
            }

            SpriteBatchMan.SetActive(psInstance);
        }
        public static void Destroy(bool bPrintEnable = false)
        {
            SpriteBatchMan pMan = SpriteBatchMan.psActiveSBMan;
            Debug.Assert(pMan != null);

            // Do something clever here
            // track peak number of active nodes
            // print stats on destroy
            // invalidate the singleton
            if (bPrintEnable)
            {
                SpriteBatchMan.DumpStats();
            }
        }

        public static SpriteBatch Add(SpriteBatch.Name name, int reserveNum = 3, int reserveGrow = 1)
        {
            SpriteBatchMan pMan = SpriteBatchMan.psActiveSBMan;
            Debug.Assert(pMan != null);

            SpriteBatch pSpriteBatch = (SpriteBatch)pMan.baseAdd();
            Debug.Assert(pSpriteBatch != null);

            // Initialize the data
            pSpriteBatch.Set(name, reserveNum, reserveGrow);
            return pSpriteBatch;
        }
        public static void SetActive(SpriteBatchMan pSBMan)
        {
            SpriteBatchMan pMan = SpriteBatchMan.privGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pSBMan != null);
            SpriteBatchMan.psActiveSBMan = pSBMan;
        }
        public static void Draw()
        {
            SpriteBatchMan pMan = SpriteBatchMan.psActiveSBMan;
            Debug.Assert(pMan != null);

            // walk through the list and render
            Iterator pIt = pMan.baseGetIterator();
            Debug.Assert(pIt != null);

            // iterate through the nodes
            for (pIt.First(); !pIt.IsDone(); pIt.Next())
            {
                // Downcast (its OK - homogeneous list)
                // Assumes someone before here called update() on each sprite
                SpriteBatch pSpriteBatch = (SpriteBatch)pIt.Current();
                if (pSpriteBatch.getActivateState())
                {
                    pSpriteBatch.GetSpriteNodeMan().Draw();
                }
            }
        }
        public static SpriteBatch Find(SpriteBatch.Name name)
        {
            SpriteBatchMan pMan = SpriteBatchMan.psActiveSBMan;
            Debug.Assert(pMan != null);

            // Compare functions only compares two SpriteBatchs

            // So:  Use the Compare SpriteBatch - as a reference
            //      use in the Compare() function
            SpriteBatchMan.psSpriteBatchCompare.name = name;

            SpriteBatch pData = (SpriteBatch)pMan.baseFind(SpriteBatchMan.psSpriteBatchCompare);
            return pData;
        }

        public static void Remove(SpriteBatch pSpriteBatch)
        {
            SpriteBatchMan pMan = SpriteBatchMan.psActiveSBMan;
            Debug.Assert(pMan != null);

            Debug.Assert(pSpriteBatch != null);
            pMan.baseRemove(pSpriteBatch);
        }

        public static void Remove(SpriteNode pSpriteBatchNode)
        {
            Debug.Assert(pSpriteBatchNode != null);
            SpriteNodeMan pSpriteNodeMan = pSpriteBatchNode.GetSBNodeMan();

            Debug.Assert(pSpriteNodeMan != null);
            pSpriteNodeMan.Remove(pSpriteBatchNode);
        }

        public static void Dump()
        {
            Debug.WriteLine("\n   ------ SpriteBatch Man: ------");

            SpriteBatchMan pMan = SpriteBatchMan.psActiveSBMan;
            Debug.Assert(pMan != null);

            pMan.baseDump();

        }
        public static void DumpStats()
        {
            Debug.WriteLine("\n   ------ SpriteBatch Man: ------");

            SpriteBatchMan pMan = SpriteBatchMan.psActiveSBMan;
            Debug.Assert(pMan != null);

            pMan.baseDumpStats();

            Debug.WriteLine("   ------------\n");
        }


        //------------------------------------
        // Override Abstract methods
        //------------------------------------
        override protected NodeBase derivedCreateNode()
        {
            NodeBase pNodeBase = new SpriteBatch();
            Debug.Assert(pNodeBase != null);

            return pNodeBase;
        }

        //------------------------------------
        // Private methods
        //------------------------------------
        private static SpriteBatchMan privGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(psInstance != null);

            return psInstance;
        }

        //------------------------------------
        // Data: unique data for this manager 
        //------------------------------------
        private static SpriteBatchMan psInstance = null;
        private static SpriteBatchMan psActiveSBMan = null;
        private static SpriteBatch psSpriteBatchCompare = new SpriteBatch();

    }
}

// --- End of File ---
