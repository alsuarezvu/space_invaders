using System.Diagnostics;

namespace SE456
{
    class AlienMoveCmd : Command
    {
  
        public void Attach(AlienGrid gameObj)
        {
            this.poAlienGrid = gameObj;
        }


        public override void Execute(Delta deltaTime)
        {
            this.poAlienGrid.MoveGrid();

            TimerEventMan.AddBasedOnTriggerTime(TimerEvent.Name.AlienGridMove, this, deltaTime);
        }

        // Data: ---------------
        private AlienGrid poAlienGrid;
    }
}
