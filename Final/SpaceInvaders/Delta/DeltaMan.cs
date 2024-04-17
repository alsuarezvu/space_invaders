using System;
using System.Diagnostics;

namespace SE456
{
    public class DeltaMan : ManBase
    {
        private DeltaMan(int reserveNum, int reserveGrow)
            : base(new DLinkMan(), new DLinkMan(), reserveNum, reserveGrow)
        {
            this.poNodeCompare = new Delta();

        }

        //----------------------------------------------------------------------
        // Static Methods
        //----------------------------------------------------------------------
        public static void Create(int reserveNum = 3, int reserveGrow = 1)
        {
            // make sure values are ressonable 
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);

            // initialize the singleton here
            Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new DeltaMan(reserveNum, reserveGrow);
            }

        }

        public static void Destroy(bool bPrintEnable = false)
        {
            DeltaMan pMan = DeltaMan.privGetInstance();
            Debug.Assert(pMan != null);

            // Do something clever here
            // track peak number of active nodes
            // print stats on destroy
            // invalidate the singleton
            if (bPrintEnable)
            {
                DeltaMan.DumpStats();
            }
        }

        public static Delta Add(Delta.Name name, float initialDelta, float targetDelta)
        {
            DeltaMan pMan = DeltaMan.privGetInstance();
            Debug.Assert(pMan != null);

            Delta pNode = (Delta)pMan.baseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(name, initialDelta, targetDelta);
            return pNode;
        }

        public static Delta Find(Delta.Name name)
        {
            DeltaMan pMan = DeltaMan.privGetInstance();
            Debug.Assert(pMan != null);

            // Compare functions only compares two Nodes

            // So:  Use the Compare Node - as a reference
            //      use in the Compare() function
            pMan.poNodeCompare.name = name;

            Delta pData = (Delta)pMan.baseFind(pMan.poNodeCompare);
            return pData;
        }

        public static void Dump()
        {
            Debug.WriteLine("\n   ------ Delta Man: ------");

            DeltaMan pMan = DeltaMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.baseDump();

        }
        public static void DumpStats()
        {
            Debug.WriteLine("\n   ------ Delta Man: ------");

            DeltaMan pMan = DeltaMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.baseDumpStats();

            Debug.WriteLine("   ------------\n");
        }

        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static DeltaMan privGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);

            return pInstance;
        }

        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------

        protected override NodeBase derivedCreateNode()
        {
            NodeBase pNodeBase = new Delta();
            Debug.Assert(pNodeBase != null);

            return pNodeBase;
        }

        //----------------------------------------------------------------------
        // Data: unique data for this manager 
        //----------------------------------------------------------------------
        private readonly Delta poNodeCompare;
        private static DeltaMan pInstance = null;
    }
}
