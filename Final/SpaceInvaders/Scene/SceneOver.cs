
using System.Diagnostics;

namespace SE456
{
    public class SceneOver : SceneState
    {
        public SceneOver(SceneContext _sceneContext)
        {
            this.Initialize();
            this.sceneContext = _sceneContext;
        }
        public override void Handle()
        {
            
        }
        public override void Initialize()
        {
            this.poSpriteBatchMan = new SpriteBatchMan(3, 1);
            SpriteBatchMan.SetActive(this.poSpriteBatchMan);

            SpriteBatch pSB_Texts = SpriteBatchMan.Add(SpriteBatch.Name.Texts);
            SpriteBatch pAliens = SpriteBatchMan.Add(SpriteBatch.Name.Alien);
            SpriteBatch pBox = SpriteBatchMan.Add(SpriteBatch.Name.Box);

            FontMan.Add(Font.Name.Game_Over, pSB_Texts, "G  A  M  E    O  V  E  R", Glyph.Name.SpaceInvaders, SCREEN_WIDTH / 2 - 100, SCREEN_HEIGHT - 100);
            FontMan.Add(Font.Name.Hi_Score_Label, pSB_Texts, "H  I    -    S  C  O  R  E", Glyph.Name.SpaceInvaders, SCREEN_WIDTH / 2 - 100, SCREEN_HEIGHT - 150);
            FontMan.Add(Font.Name.Hi_Score_Game_Over, pSB_Texts, "0   0   0   0", Glyph.Name.SpaceInvaders, SCREEN_WIDTH / 2 - 50, SCREEN_HEIGHT - 200);
            FontMan.Add(Font.Name.Play_Again, pSB_Texts, "P  R  E  S  S    0    T  O    P  L  A  Y    A  G  A  I  N", Glyph.Name.SpaceInvaders, SCREEN_WIDTH / 2 - 200, SCREEN_HEIGHT - 250);
        }
        public override void Update(float systemTime)
        {
            // Single Step, Free running...
            Simulation.Update(systemTime);

            // Input
            InputMan.Update();
        }

        public override void Draw()
        {
            // draw all objects
            SpriteBatchMan.Draw();
        }

        public override void Transition()
        {
            // update SpriteBatchMan()
            SpriteBatchMan.SetActive(this.poSpriteBatchMan);
            ScoreMan.PrintHighestScore(FontMan.Find(Font.Name.Hi_Score_Game_Over));
        }

        public override void HandleKeyPress(KeyPress keyPress)
        {
            //no -op
        }

        // ---------------------------------------------------
        // Data
        // ---------------------------------------------------
        public SpriteBatchMan poSpriteBatchMan;
        public static readonly int SCREEN_WIDTH = 672;
        public static readonly int SCREEN_HEIGHT = 768;

        private SceneContext sceneContext;
    }
}