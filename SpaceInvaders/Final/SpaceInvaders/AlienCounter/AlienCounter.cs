using System;
using System.Diagnostics;

namespace SE456
{
    public class AlienCounter
    {
        private AlienCounter()
        {

        }

        public static void Create()
        {
            // make sure its the first time
            Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new AlienCounter();
            }

            Debug.Assert(pInstance != null);
        }

        public static void Subtract(int count)
        {
            AlienCounter alienCounter = PrivInstance();

            alienCounter.alienCount = alienCounter.alienCount - count;
        }

        public static int GetCount()
        {
            AlienCounter alienCounter = PrivInstance();

            return alienCounter.alienCount;
        }

        public static void Reset(int count)
        {
            AlienCounter alienCounter = PrivInstance();

            alienCounter.alienCount = count;
        }

        public static void Add(int count)
        {
            AlienCounter alienCounter = PrivInstance();

            alienCounter.alienCount += count;
        }

        private static AlienCounter PrivInstance()
        {
            Debug.Assert(pInstance != null);

            return pInstance;
        }


        private static AlienCounter pInstance;
        private int alienCount;
    }
}
