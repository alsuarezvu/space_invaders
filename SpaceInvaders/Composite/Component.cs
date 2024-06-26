﻿//-----------------------------------------------------------------------------
// Copyright 2024, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    abstract public class Component : ColVisitor
    {
        public enum Container
        {
            LEAF,
            COMPOSITE,
            Unknown
        }

        public Component(Component.Container _type)
        {
            this.type = _type;
            this.pParent = null;
            this.pReverse = null;
        }

        public abstract void Print();
        public abstract void Add(Component c);
        public abstract void Remove(Component c);
        public abstract void DumpNode();

        // Data
        public Container type;
        public Component pParent ;
        public Component pReverse;
    }
}

// --- End of File ---
