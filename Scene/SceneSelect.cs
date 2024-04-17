
using System.Diagnostics;

namespace SE456
{
    public class SceneSelect : SceneState
    {
        public SceneSelect(SceneContext _sceneContext)
        {
            this.Initialize();
            this.sceneContext = _sceneContext;
        }
        public override void Handle()
        {
            //
        }
        public override void Initialize()
        {
            this.poSpriteBatchMan = new SpriteBatchMan(3, 1);
            SpriteBatchMan.SetActive(this.poSpriteBatchMan);

            this.poFontMan = new FontMan(3, 1);
            FontMan.SetActive(this.poFontMan);

            SpriteBatch pSB_Texts = SpriteBatchMan.Add(SpriteBatch.Name.Texts);
            SpriteBatch pAliens = SpriteBatchMan.Add(SpriteBatch.Name.Alien);
            SpriteBatch pBox = SpriteBatchMan.Add(SpriteBatch.Name.Box);
         
            FontMan.Add(Font.Name.Play, pSB_Texts, "P  L  A  Y", Glyph.Name.SpaceInvaders, 275,  600);
            FontMan.Add(Font.Name.Space_Invaders, pSB_Texts, "S  P  A  C  E       I  N  V  A  D  E  R  S", Glyph.Name.SpaceInvaders, 200, 550);
            FontMan.Add(Font.Name.Score_Table, pSB_Texts, "*  S  C  O  R  E     A  D  V  A  N  C  E    T  A  B  L  E  *", Glyph.Name.SpaceInvaders, 150, 450);

            SpriteGameProxy ufo = SpriteGameProxyMan.Add(SpriteGame.Name.UFO);
            ufo.x =250;
            ufo.y = 400;

            SpriteGameProxy squid = SpriteGameProxyMan.Add(SpriteGame.Name.Squid);
            squid.x = 250;
            squid.y = 350;

            SpriteGameProxy crab = SpriteGameProxyMan.Add(SpriteGame.Name.Crab);
            crab.x = 250;
            crab.y = 300;

            SpriteGameProxy octopus = SpriteGameProxyMan.Add(SpriteGame.Name.Octopus_Green);
            octopus.x = 250;
            octopus.y = 250;

            pAliens.Attach(ufo);
            pAliens.Attach(squid);
            pAliens.Attach(crab);
            pAliens.Attach(octopus);

            FontMan.Add(Font.Name.UFO_Score, pSB_Texts, "=  ?  M  Y  S  T  E  R  Y", Glyph.Name.SpaceInvaders, 300, 400);
            FontMan.Add(Font.Name.Squid_Score, pSB_Texts, "=  3  0", Glyph.Name.SpaceInvaders, 300, 350);
            FontMan.Add(Font.Name.Crab_Score, pSB_Texts, "=  2  0", Glyph.Name.SpaceInvaders, 300, 300);
            FontMan.Add(Font.Name.Octopus_Score, pSB_Texts, "=  1  0", Glyph.Name.SpaceInvaders, 300, 250);

            FontMan.Add(Font.Name.PressEnter, pSB_Texts, "P  R  E  S  S   1    T  O    P  L  A  Y", Glyph.Name.SpaceInvaders, 200, 100);
        }
        public override void Update(float systemTime)
        {
            // Single Step, Free running...
            Simulation.Update(systemTime);

            // Input
            InputMan.Update();

            //Debug.WriteLine("Simulation.GetTimeStep" + Simulation.GetTimeStep());
            // Run based on simulation stepping
            //if (Simulation.GetTimeStep() > 0.0f)
            //{
                // Fire off the timer events
                TimerEventMan.Update(Simulation.GetTotalTime());

                // Do the collision checks
                ColPairMan.Process();

                // walk through all objects and push to flyweight
                GameObjectNodeMan.Update();

                // Delete any objects here...
                DelayedObjectMan.Process();
            //}
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
        public FontMan poFontMan;
    }
}