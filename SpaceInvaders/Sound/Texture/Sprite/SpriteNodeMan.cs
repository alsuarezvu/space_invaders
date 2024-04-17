//-----------------------------------------------------------------------------
// Copyright 2024, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using Azul;
using System;
using System.Diagnostics;

namespace SE456
{
    public class SpriteNodeMan : ManBase
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        public SpriteNodeMan(int reserveNum = 3, int reserveGrow = 1)
                : base(new DLinkMan(), new DLinkMan(), reserveNum, reserveGrow)   // <--- Kick the can (delegate)
        {
            // initialize derived data here
            psSpriteNodeCompare = new SpriteNode();

            this.pBackSpriteBatch = null;
        }

        //----------------------------------------------------------------------
        // Methods
        //----------------------------------------------------------------------
        public void Set(SpriteBatch.Name name, int reserveNum, int reserveGrow)
        {
            this.name = name;

            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);

            this.baseSetReserve(reserveNum, reserveGrow);
        }
        public SpriteNode Attach(SpriteBase pNode)
        {
            SpriteNode pSpriteNode = (SpriteNode)this.baseAdd();
            Debug.Assert(pSpriteNode != null);

            // Initialize the data
            pSpriteNode.Set(pNode, this);
            return pSpriteNode;
        }
        public void Draw()
        {
            // walk through the list and render
            Iterator pIt = this.baseGetIterator();
            Debug.Assert(pIt != null);

            // iterate through the nodes
            for (pIt.First(); !pIt.IsDone(); pIt.Next())
            {
                // Downcast (its OK - homogeneous list)
                // Assumes someone before here called update() on each sprite

                SpriteNode pNode = (SpriteNode)pIt.Current();

                //the if statement is a band-aid fix for now
                //seems like some sprite nodes have null sprite bases. 
                if (pNode.pSpriteBase != null)
                {
                    pNode.pSpriteBase.Render();
                }
               
            }
        }
        public void Remove(SpriteNode pSpriteNode)
        {
            Debug.Assert(pSpriteNode != null);
            this.baseRemove(pSpriteNode);
        }

        public void SetSpriteBatch(SpriteBatch _pSpriteBatch)
        {
            this.pBackSpriteBatch = _pSpriteBatch;
        }
        public SpriteBatch GetSpriteBatch()
        {
            return this.pBackSpriteBatch;
        }

        public void Dump()
        {
            Debug.WriteLine("\n   ------ SpriteNode Man: ------");

            this.baseDump();
        }
        public void DumpStats()
        {
            Debug.WriteLine("\n   ------ SpriteNode Man: ------");

            this.baseDumpStats();

            Debug.WriteLine("   ------------\n");
        }


        //------------------------------------
        // Override Abstract methods
        //------------------------------------
        override protected NodeBase derivedCreateNode()
        {
            NodeBase pNodeBase = new SpriteNode();
            Debug.Assert(pNodeBase != null);

            return pNodeBase;
        }

        //------------------------------------
        // Private methods
        //------------------------------------


        //------------------------------------
        // Data: unique data for this manager 
        //------------------------------------
        private static SpriteNode psSpriteNodeCompare;
        private SpriteBatch.Name name;
        private SpriteBatch pBackSpriteBatch;
    }
}

// --- End of File ---
