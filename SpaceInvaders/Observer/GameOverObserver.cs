using System;
using System.Diagnostics;

namespace SE456
{
    public class GameOverObserver : ColObserver
    {

        public GameOverObserver(ScenePlay scene)
        {
            this.scenePlay = scene;
        }

        public override void Notify()
        {
            //Trigger the operations to transition to game over
            scenePlay.setGameOver(true);
        }

        public override void Dump()
        {
           //no-op
        }

        public override Enum GetName()
        {
            return ColObserver.Name.GameOverObserver;
        }

        private ScenePlay scenePlay;
    }
}
