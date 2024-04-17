//-----------------------------------------------------------------------------
// Copyright 2024, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;

namespace SE456
{
    public abstract class ListBase
    {
        abstract public void AddToFront(NodeBase pNode);
        abstract public void Remove(NodeBase pNode);
        abstract public NodeBase RemoveFromFront();

        abstract public void AddBasedOnPriority(NodeBase pNode);
        abstract public Iterator GetIterator();
    }
}

// --- End of File ---
