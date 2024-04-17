//-----------------------------------------------------------------------------
// Copyright 2024, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System.Diagnostics;

namespace SE456
{
    public class SpriteNode : DLink
    {
        //------------------------------------
        // Constructors
        //------------------------------------

        public SpriteNode()
        : base()
        {
            this.pSpriteBase = null;
            this.pBackSpriteNodeMan = null;
        }

        //------------------------------------
        // Methods
        //------------------------------------

        public void Set(SpriteBase pNode, SpriteNodeMan _pSpriteNodeMan)
        {
            Debug.Assert(pNode != null);
            this.pSpriteBase = pNode;

            // Set the back pointer
            // Allows easier deletion in the future
            Debug.Assert(pSpriteBase != null);
            this.pSpriteBase.SetSpriteNode(this);

            Debug.Assert(_pSpriteNodeMan != null);
            this.pBackSpriteNodeMan = _pSpriteNodeMan;
        }
        public SpriteBase GetSpriteBase()
        {
            return this.pSpriteBase;
        }
        public SpriteNodeMan GetSBNodeMan()
        {
            Debug.Assert(this.pBackSpriteNodeMan != null);
            return this.pBackSpriteNodeMan;
        }
        public SpriteBatch GetSpriteBatch()
        {
            Debug.Assert(this.pBackSpriteNodeMan != null);
            return this.pBackSpriteNodeMan.GetSpriteBatch();
        }
        private void privClear()
        {
            this.pSpriteBase = null;
        }

        //------------------------------------
        // Override
        //------------------------------------

        override public void Wash()
        {
            this.baseClear();
            this.privClear();
        }
        override public System.Enum GetName()
        {
            return null;
        }
        override public bool Compare(NodeBase pSpriteNodeBaseB)
        {
            // This is used in baseFind() 
            Debug.Assert(pSpriteNodeBaseB != null);

            SpriteNode pDataB = (SpriteNode)pSpriteNodeBaseB;

            bool status = false;
            if (this.pSpriteBase.GetName().GetHashCode() == pDataB.pSpriteBase.GetName().GetHashCode())
            {
                status = true;
            }

            return status;
        }
        override public void Dump()
        {
            // we are using HASH code as its unique identifier 
            Debug.WriteLine("   ({0}) node", this.GetHashCode());

            // Data:
            Debug.WriteLine("   pSprite: {0} ({1})", this.pSpriteBase.GetName(), this.pSpriteBase.GetHashCode());

            // Let the base print its contribution
            this.baseDump();
        }

        //------------------------------------
        // Data
        //------------------------------------
        public SpriteBase pSpriteBase;

        // Keenan(delete.C)
        private SpriteNodeMan pBackSpriteNodeMan;
    }
}

// --- End of File ---

