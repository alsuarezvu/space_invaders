using System;

namespace SE456
{
    public class SceneContext
    {
        public enum Scene
        {
            Select,
            Play,
            Over
        }
        public SceneContext()
        {
            // reserve the states
            this.poSceneSelect = new SceneSelect(this);
            this.poScenePlay = new ScenePlay(this);
            this.poSceneOver = new SceneOver(this);

            // initialize to the select state
            this.pSceneState = this.poSceneSelect;
            this.pSceneState.Transition();
        }

        public SceneState GetState()
        {
            return this.pSceneState;
        }
        public void SetState(Scene eScene)
        {
            switch (eScene)
            {
                case Scene.Select:
                    this.pSceneState = this.poSceneSelect;
                    this.pSceneState.Transition();
                    break;

                case Scene.Play:
                    this.pSceneState = this.poScenePlay;
                    this.pSceneState.Transition();
                    break;

                case Scene.Over:
                    this.pSceneState = this.poSceneOver;
                    this.pSceneState.Transition();
                    break;

            }
        }

        public void HandleKeyPress(SceneState.KeyPress keyPress)
        {
            this.pSceneState.HandleKeyPress(keyPress);
        }

        // ----------------------------------------------------
        // Data: 
        // -------------------------------------------o---------
        SceneState pSceneState;
        SceneSelect poSceneSelect;
        SceneOver poSceneOver;
        ScenePlay poScenePlay;

    }
}