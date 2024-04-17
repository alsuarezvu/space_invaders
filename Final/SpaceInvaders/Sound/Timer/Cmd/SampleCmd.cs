//-----------------------------------------------------------------------------
// Copyright 2024, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    class SampleCmd : Command
    {
        public SampleCmd(String txt)
        {
            // string only for testing
            this.pString = txt;
        }

        public override void Execute(Delta deltaTime)
        {
            Debug.WriteLine(" {0} time:{1} ", this.pString, TimerEventMan.GetCurrTime());
        }

        private String pString;
    }
}

// --- End of File ---
