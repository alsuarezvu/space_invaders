//-----------------------------------------------------------------------------
// Copyright 2024, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    class TimedCharacterFactory
    {

        private TimedCharacterFactory()
        {
            this.deltaTime = new Delta();
        }

        static public void Install(string pMessage, float deltaTimeToTrigger, float delayTime, float xPos, float yPos, float red, float green, float blue)
        {
            TimedCharacterFactory pInstance = TimedCharacterFactory.privInstance();
            // future use

            TimedCharacterCmd pCmd_old = null;

            for (int i = 0; i < pMessage.Length; i++)
            {
                string pCharacter = pMessage.Substring(0, i+1);
      
                TimedCharacterCmd pCmd = new TimedCharacterCmd(pCmd_old, pCharacter, xPos, yPos, red, green, blue);

                pInstance.deltaTime.setDelta(deltaTimeToTrigger + i * delayTime);
                TimerEventMan.AddBasedOnTriggerTime(TimerEvent.Name.TimedCharacter, pCmd, pInstance.deltaTime);

                pCmd_old = pCmd;
            }
        }


        private static TimedCharacterFactory privInstance()
        {
            if (pInstance == null)
            {
                TimedCharacterFactory.pInstance = new TimedCharacterFactory();
            }

            Debug.Assert(pInstance != null);

            return pInstance;
        }

        // -----------------------
        // Data
        // -----------------------

        // ready for instance data if needed

        private static TimedCharacterFactory pInstance = null;

        Delta deltaTime;

    }
}

// --- End of File ---
