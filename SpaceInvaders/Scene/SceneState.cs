
using System.Diagnostics;

namespace SE456
{
    public abstract class SceneState
    {
        public enum KeyPress
        {
            Activate_Boxes,
            Deactivate_Boxes,
            Activate_Sprites,
            Deactive_Sprites,
        }

        public abstract void Handle();
        public abstract void Initialize();
        public abstract void Update(float systemTime);
        public abstract void Draw();
        public abstract void Transition();

        public abstract void HandleKeyPress(SceneState.KeyPress keyPress);

    }
}