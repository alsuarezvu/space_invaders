//-----------------------------------------------------------------------------
// Copyright 2024, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    public class TimerEventMan : ManBase
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        private TimerEventMan(int reserveNum, int reserveGrow)
                : base(new DLinkMan(), new DLinkMan(), reserveNum, reserveGrow)   
        {
            // initialize derived data here
            this.poNodeCompare = new TimerEvent();
            this.mCurrTime = 0.0f;
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
                pInstance = new TimerEventMan(reserveNum, reserveGrow);
            }

        }
        public static void Destroy(bool bPrintEnable = false)
        {
            TimerEventMan pMan = TimerEventMan.privGetInstance();
            Debug.Assert(pMan != null);

            // Do something clever here
            // track peak number of active nodes
            // print stats on destroy
            // invalidate the singleton
            if (bPrintEnable)
            {
                TimerEventMan.DumpStats();
            }
        }

        /*public static TimerEvent Add(TimerEvent.Name timeName, Command pCommand, float deltaTimeToTrigger)
        {
            TimerEventMan pMan = TimerEventMan.privGetInstance();
            Debug.Assert(pMan != null);

            TimerEvent pNode = (TimerEvent)pMan.baseAdd();
            Debug.Assert(pNode != null);

            Debug.Assert(pCommand != null);
            Debug.Assert(deltaTimeToTrigger >= 0.0f);

            pNode.Set(timeName, pCommand, deltaTimeToTrigger);
            return pNode;
        }*/

        public static TimerEvent AddBasedOnTriggerTime(TimerEvent.Name timeName, Command pCommand, Delta deltaTimeToTrigger)
        {
            TimerEventMan pMan = TimerEventMan.privGetInstance();
            Debug.Assert(pMan != null);

            TimerEvent pNode = (TimerEvent)pMan.baseGetFromReserve();
            Debug.Assert(pNode != null);

            Debug.Assert(pCommand != null);
            Debug.Assert(deltaTimeToTrigger.getDelta() >= 0.0f);

            pNode.Set(timeName, pCommand, deltaTimeToTrigger);

            AddBasedOnPriority(pNode);
            return pNode;
        }

        public static void AddBasedOnPriority(TimerEvent pTimerEvent)
        {
            TimerEventMan pMan = TimerEventMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.addBasedOnPriority(pTimerEvent);

            //Figure out where to add this spriteBatch
            //bool added = false;
            //Iterator it = pMan.baseGetIterator();

            //DLink current = (DLink)it.First();
            //DLink prevCurrent = null;
            //if (current == null)
            //{
            //    pMan.addToFront(pTimerEvent);
            //    added = true;
            //}
            //else
            //{
            //    while (current != null)
            //    {
            //        float triggerTime = ((TimerEvent)current).triggerTime;
            //        if (pTimerEvent.triggerTime <= triggerTime)
            //        {
            //            if (current.pPrev == null)
            //            {
            //                pMan.addToFront(pTimerEvent);
            //            }

            //            pTimerEvent.pNext = current;
            //            current.pPrev = pTimerEvent;

            //            pTimerEvent.pPrev = prevCurrent;

            //            if (prevCurrent != null)
            //            {
            //                prevCurrent.pNext = pTimerEvent;
            //            }

            //            added = true;
            //            break;
            //        }
            //        prevCurrent = current;
            //        current = (DLink)it.Next();

            //    }

            //    if (!added)
            //    {
            //        //add this spritebatch to the end
            //        prevCurrent.pNext = pTimerEvent;
            //        pTimerEvent.pPrev = prevCurrent;
            //    }
            //}
        }

        public static TimerEvent Find(TimerEvent.Name name)
        {
            TimerEventMan pMan = TimerEventMan.privGetInstance();
            Debug.Assert(pMan != null);

            // Compare functions only compares two Nodes

            // So:  Use the Compare Node - as a reference
            //      use in the Compare() function
            pMan.poNodeCompare.name = name;

            TimerEvent pData = (TimerEvent)pMan.baseFind(pMan.poNodeCompare);
            return pData;
        }

        public static void Remove(TimerEvent timeEvent)
        {
            Debug.Assert(timeEvent != null);

            TimerEventMan pMan = TimerEventMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.baseRemove(timeEvent);
        }

        public static void RemoveAll()
        {
            TimerEventMan pMan = TimerEventMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.baseRemoveAll();

        }
        public static void Dump()
        {
            Debug.WriteLine("\n   ------ TimerEvent Man: ------");

            TimerEventMan pMan = TimerEventMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.baseDump();

        }
        public static void DumpStats()
        {
            Debug.WriteLine("\n   ------ TimerEvent Man: ------");

            TimerEventMan pMan = TimerEventMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.baseDumpStats();

            Debug.WriteLine("   ------------\n");
        }

        public static void Update(float totalTime)
        {
           // Debug.WriteLine("Time: {0}", totalTime);
            // Get the instance
            TimerEventMan pMan = TimerEventMan.privGetInstance();
            Debug.Assert(pMan != null);

            // squirrel away
            pMan.mCurrTime = totalTime;

            // walk through the list and execute
            Iterator pIt = pMan.baseGetIterator();
            Debug.Assert(pIt != null);

            TimerEvent pNode = null;

            // Walk the list until there is no more list OR currTime is greater than timeEvent 
            // ToDo Fix: List needs to be sorted then its an early out
            for (pIt.First(); !pIt.IsDone(); pIt.Next())
            {
                pNode = (TimerEvent)pIt.Current();
                if (pMan.mCurrTime >= pNode.triggerTime)
                {
                    // call it
                    pNode.Process();

                    // remove from list
                    pIt.Erase(pMan);
                }
                else
                {
                    break;
                }
            }

        }

        public static float GetCurrTime()
        {
            // Get the instance
            TimerEventMan pMan = TimerEventMan.privGetInstance();
            Debug.Assert(pMan != null);

            // return time
            return pMan.mCurrTime;
        }

        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static TimerEventMan privGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);

            return pInstance;
        }

        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        override protected NodeBase derivedCreateNode()
        {
            NodeBase pNodeBase = new TimerEvent();
            Debug.Assert(pNodeBase != null);

            return pNodeBase;
        }

        //----------------------------------------------------------------------
        // Data: unique data for this manager 
        //----------------------------------------------------------------------
        private readonly TimerEvent poNodeCompare;
        private static TimerEventMan pInstance = null;
        protected float mCurrTime;
    }
}

// --- End of File ---

