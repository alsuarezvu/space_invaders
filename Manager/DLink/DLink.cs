//-----------------------------------------------------------------------------
// Copyright 2024, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    abstract public class DLink : NodeBase
    {
        protected DLink()
            : base()
        {
            this.baseClear();
        }

        public void Clear()
        {
            this.baseClear();
        }

        protected void baseClear()
        {
            this.pNext = null;
            this.pPrev = null;
            this.priority = 0.0f;
        }

        protected void baseDump()
        {
            if (this.pPrev == null)
            {
                Debug.WriteLine("      prev: null");
            }
            else
            {
                NodeBase pTmp = (NodeBase)this.pPrev;
                Debug.WriteLine("      prev: {0} ({1})", pTmp.GetName(), pTmp.GetHashCode());
            }

            if (this.pNext == null)
            {
                Debug.WriteLine("      next: null");
            }
            else
            {
                NodeBase pTmp = (NodeBase)this.pNext;
                Debug.WriteLine("      next: {0} ({1})", pTmp.GetName(), pTmp.GetHashCode());
            }
        }

        // Data: -----------------------------
        public DLink pNext;
        public DLink pPrev;
    }
}

// --- End of File ---
