//-----------------------------------------------------------------------------
// Copyright 2024, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;
using System.Windows.Controls;

namespace SE456
{
    public class SpaceInvaders : Azul.Game
    {

        //IrrKlang.ISoundEngine sndEngine = null;
        //readonly Random pRandom = new Random();

        public SceneContext pSceneContext = null;

        public SpriteBatchMan poSpriteBatchMan;

        //-----------------------------------------------------------------------------
        // Game::Initialize()
        //		Allows the engine to perform any initialization it needs to before 
        //      starting to run.  This is where it can query for any required services 
        //      and load any non-graphic related content. 
        //-----------------------------------------------------------------------------
        public override void Initialize()
        {
            // Game Window Device setup
            this.SetWindowName("Space Invaders Final");
            this.SetWidthHeight(SCREEN_WIDTH, SCREEN_HEIGHT);
            this.SetClearColor(0.0f, 0.0f, 0.0f, 0.0f);
        }

        //-----------------------------------------------------------------------------
        // Game::LoadContent()
        //		Allows you to load all content needed for your engine,
        //	    such as objects, graphics, etc.
        //-----------------------------------------------------------------------------
        public override void LoadContent()
        {
            //-------------------------------------------------------
            // Load Managers
            //-------------------------------------------------------

            TextureMan.Create();
            ImageMan.Create();
            SpriteGameMan.Create();
            SpriteBoxMan.Create();
            SpriteBatchMan.Create();
            TimerEventMan.Create();
            SpriteGameProxyMan.Create();
            GameObjectNodeMan.Create(); 
            ColPairMan.Create();
            GlyphMan.Create();
            FontMan.Create();
            DeltaMan.Create();
            Simulation.Create();
            GhostMan.Create();


            //-------------------------------------------------------
            // Load the Textures
            //-------------------------------------------------------

            TextureMan.Add(Texture.Name.SpaceInvaders, "SpaceInvaders_ROM.t.azul");
            TextureMan.Add(Texture.Name.PacMan, "PacMan.t.azul");
            TextureMan.Add(Texture.Name.Birds, "Birds_N_Shield.t.azul");

            //-------------------------------------------------------
            // Load the Fonts
            //-------------------------------------------------------
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 65, Texture.Name.SpaceInvaders, 3, 36, 5, 8);   // .A 
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 66, Texture.Name.SpaceInvaders, 11, 36, 5, 8);   // .B
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 67, Texture.Name.SpaceInvaders, 19, 36, 5, 8);   // .C 
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 68, Texture.Name.SpaceInvaders, 27, 36, 5, 8);   // .D 
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 69, Texture.Name.SpaceInvaders, 35, 36, 5, 8);   // .E 
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 70, Texture.Name.SpaceInvaders, 43, 36, 5, 8);   // .F 
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 71, Texture.Name.SpaceInvaders, 51, 36, 5, 8);   // .G 
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 72, Texture.Name.SpaceInvaders, 59, 36, 5, 8);   // .H 
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 73, Texture.Name.SpaceInvaders, 67, 36, 5, 8);   // .I 
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 74, Texture.Name.SpaceInvaders, 75, 36, 5, 8);   // .J 
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 75, Texture.Name.SpaceInvaders, 83, 36, 5, 8);   // .K 
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 76, Texture.Name.SpaceInvaders, 91, 36, 5, 8);   // .L 
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 77, Texture.Name.SpaceInvaders, 99, 36, 5, 8);   // .M 
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 78, Texture.Name.SpaceInvaders, 3,  46, 5, 8);   // .N 
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 79, Texture.Name.SpaceInvaders, 11, 46, 5, 8);   // .O 
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 80, Texture.Name.SpaceInvaders, 19, 46, 5, 8);   // .P 
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 81, Texture.Name.SpaceInvaders, 27, 46, 5, 8);   // .Q 
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 82, Texture.Name.SpaceInvaders, 35, 46, 5, 8);   // .R 
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 83, Texture.Name.SpaceInvaders, 43, 46, 5, 8);   // .S 
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 84, Texture.Name.SpaceInvaders, 51, 46, 5, 8);   // .T 
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 85, Texture.Name.SpaceInvaders, 59, 46, 5, 8);   // .U 
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 86, Texture.Name.SpaceInvaders, 67, 46, 5, 8);   // .V 
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 87, Texture.Name.SpaceInvaders, 75, 46, 5, 8);   // .W 
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 88, Texture.Name.SpaceInvaders, 83, 46, 5, 8);   // .X 
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 89, Texture.Name.SpaceInvaders, 91, 46, 5, 8);   // .Y 
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 90, Texture.Name.SpaceInvaders, 99, 46, 5, 8);   // .Z 
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 48, Texture.Name.SpaceInvaders, 3,  56, 5, 8);   // 0 
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 49, Texture.Name.SpaceInvaders, 11, 56, 5, 8);   // 1 
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 50, Texture.Name.SpaceInvaders, 19, 56, 5, 8);   // 2 
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 51, Texture.Name.SpaceInvaders, 27, 56, 5, 8);   // 3 
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 52, Texture.Name.SpaceInvaders, 35, 56, 5, 8);   // 4 
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 53, Texture.Name.SpaceInvaders, 43, 56, 5, 8);   // 5 
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 54, Texture.Name.SpaceInvaders, 51, 56, 5, 8);   // 6 
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 55, Texture.Name.SpaceInvaders, 59, 56, 5, 8);   // 7 
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 56, Texture.Name.SpaceInvaders, 67, 56, 5, 8);   // 8 
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 57, Texture.Name.SpaceInvaders, 75, 56, 5, 8);   // 9 
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 60, Texture.Name.SpaceInvaders, 83, 56, 5, 8);  // < 
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 62, Texture.Name.SpaceInvaders, 91, 56, 5, 8);  // > 
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 32, Texture.Name.SpaceInvaders, 99, 56, 5, 8);  // Space 
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 61, Texture.Name.SpaceInvaders, 107, 56, 5, 8);  // = 
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 42, Texture.Name.SpaceInvaders, 115, 56, 5, 8);  // * 
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 63, Texture.Name.SpaceInvaders, 123, 56, 5, 8);  // ? 
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 45, Texture.Name.SpaceInvaders, 131, 56, 5, 8);  // - 
            
            
            //-------------------------------------------------------
            // Create Images
            //-------------------------------------------------------

            // -- Space Invaders ---
            ImageMan.Add(Image.Name.SquidA, Texture.Name.SpaceInvaders, 61, 3, 8, 8);
            ImageMan.Add(Image.Name.SquidB, Texture.Name.SpaceInvaders, 72, 3, 8, 8);
            ImageMan.Add(Image.Name.CrabA, Texture.Name.SpaceInvaders, 33, 3, 11, 8);
            ImageMan.Add(Image.Name.CrabB, Texture.Name.SpaceInvaders, 47, 3, 11, 8);
            ImageMan.Add(Image.Name.OctopusA, Texture.Name.SpaceInvaders, 3, 3, 12, 8);
            ImageMan.Add(Image.Name.OctopusB, Texture.Name.SpaceInvaders, 18, 3, 12, 8);

            ImageMan.Add(Image.Name.Ship, Texture.Name.SpaceInvaders, 3, 14, 13, 8);
            ImageMan.Add(Image.Name.Ship_Splat_1, Texture.Name.SpaceInvaders, 20, 14, 15, 8);
            ImageMan.Add(Image.Name.Ship_Splat_2, Texture.Name.SpaceInvaders, 38, 14, 16, 8);
            ImageMan.Add(Image.Name.Floor, Texture.Name.SpaceInvaders, 131, 59, 5, 1);
            ImageMan.Add(Image.Name.UFO, Texture.Name.SpaceInvaders, 99, 3, 16, 8);
            ImageMan.Add(Image.Name.UFO_Splat, Texture.Name.SpaceInvaders, 118, 3, 21, 8);
            ImageMan.Add(Image.Name.Missile, Texture.Name.SpaceInvaders, 3, 29, 1, 4);

            ImageMan.Add(Image.Name.SquigglyShotA, Texture.Name.SpaceInvaders, 18, 26, 3, 7);
            ImageMan.Add(Image.Name.SquigglyShotB, Texture.Name.SpaceInvaders, 24, 26, 3, 7);
            ImageMan.Add(Image.Name.SquigglyShotC, Texture.Name.SpaceInvaders, 30, 26, 3, 7);
            ImageMan.Add(Image.Name.SquigglyShotD, Texture.Name.SpaceInvaders, 36, 26, 3, 7);
            ImageMan.Add(Image.Name.PlungerShotA, Texture.Name.SpaceInvaders, 42, 27, 3, 6);
            ImageMan.Add(Image.Name.PlungerShotB, Texture.Name.SpaceInvaders, 48, 27, 3, 6);
            ImageMan.Add(Image.Name.PlungerShotC, Texture.Name.SpaceInvaders, 54, 27, 3, 6);
            ImageMan.Add(Image.Name.PlungerShotD, Texture.Name.SpaceInvaders, 60, 27, 3, 6);
            ImageMan.Add(Image.Name.RollingShotA, Texture.Name.SpaceInvaders, 65, 26, 3, 7);
            ImageMan.Add(Image.Name.RollingShotB, Texture.Name.SpaceInvaders, 70, 26, 3, 7);
            ImageMan.Add(Image.Name.RollingShotC, Texture.Name.SpaceInvaders, 75, 26, 3, 7);
            ImageMan.Add(Image.Name.RollingShotD, Texture.Name.SpaceInvaders, 80, 26, 3, 7);

            ImageMan.Add(Image.Name.Brick, Texture.Name.Birds, 20, 210, 10, 5);
            ImageMan.Add(Image.Name.BrickLeft_Top0, Texture.Name.Birds, 15, 180, 10, 10);
            ImageMan.Add(Image.Name.BrickLeft_Bottom, Texture.Name.Birds, 36, 215, 10, 5);
            ImageMan.Add(Image.Name.BrickRight_Top0, Texture.Name.Birds, 75, 180, 10, 10);
            ImageMan.Add(Image.Name.BrickRight_Bottom, Texture.Name.Birds, 55, 215, 10, 5);
            ImageMan.Add(Image.Name.Alien_Splat, Texture.Name.SpaceInvaders, 83, 3, 13, 8);
            ImageMan.Add(Image.Name.BombSplat, Texture.Name.SpaceInvaders, 86, 25, 6, 8);

            //--------------------------------------------------------------------------------------------------------
            // Create Game Sprites
            //---------------------------------------------------------------------------------------------------------
            SpriteGameFactory.Create(SpriteGame.Name.Squid);
            SpriteGameFactory.Create(SpriteGame.Name.Crab);
            SpriteGameFactory.Create(SpriteGame.Name.Octopus);
            SpriteGameFactory.Create(SpriteGame.Name.Octopus_Green);
            SpriteGameFactory.Create(SpriteGame.Name.Alien_Splat);
  
            SpriteGameFactory.Create(SpriteGame.Name.Ship);
            SpriteGameFactory.Create(SpriteGame.Name.Ship_Splat_1);
            SpriteGameFactory.Create(SpriteGame.Name.Ship_Splat_2);
            SpriteGameFactory.Create(SpriteGame.Name.Missile);

            SpriteGameFactory.Create(SpriteGame.Name.Brick);
            SpriteGameFactory.Create(SpriteGame.Name.Brick_LeftTop0);
            SpriteGameFactory.Create(SpriteGame.Name.Brick_LeftBottom);
            SpriteGameFactory.Create(SpriteGame.Name.Brick_RightTop0);
            SpriteGameFactory.Create(SpriteGame.Name.Brick_RightBottom);

            SpriteGameFactory.Create(SpriteGame.Name.SquigglyShotA);
            SpriteGameFactory.Create(SpriteGame.Name.SquigglyShotB);
            SpriteGameFactory.Create(SpriteGame.Name.SquigglyShotC);
            SpriteGameFactory.Create(SpriteGame.Name.SquigglyShotD);

            SpriteGameFactory.Create(SpriteGame.Name.RollingShotA);
            SpriteGameFactory.Create(SpriteGame.Name.RollingShotB);
            SpriteGameFactory.Create(SpriteGame.Name.RollingShotC);
            SpriteGameFactory.Create(SpriteGame.Name.RollingShotD);

            SpriteGameFactory.Create(SpriteGame.Name.PlungerShotA);
            SpriteGameFactory.Create(SpriteGame.Name.PlungerShotB);
            SpriteGameFactory.Create(SpriteGame.Name.PlungerShotC);
            SpriteGameFactory.Create(SpriteGame.Name.PlungerShotD);

            SpriteGameFactory.Create(SpriteGame.Name.SquigglyShotA_Red);
            SpriteGameFactory.Create(SpriteGame.Name.SquigglyShotB_Red);
            SpriteGameFactory.Create(SpriteGame.Name.SquigglyShotC_Red);
            SpriteGameFactory.Create(SpriteGame.Name.SquigglyShotD_Red);

            SpriteGameFactory.Create(SpriteGame.Name.RollingShotA_Red);
            SpriteGameFactory.Create(SpriteGame.Name.RollingShotB_Red);
            SpriteGameFactory.Create(SpriteGame.Name.RollingShotC_Red);
            SpriteGameFactory.Create(SpriteGame.Name.RollingShotD_Red);

            SpriteGameFactory.Create(SpriteGame.Name.PlungerShotA_Red);
            SpriteGameFactory.Create(SpriteGame.Name.PlungerShotB_Red);
            SpriteGameFactory.Create(SpriteGame.Name.PlungerShotC_Red);
            SpriteGameFactory.Create(SpriteGame.Name.PlungerShotD_Red);

            SpriteGameFactory.Create(SpriteGame.Name.BombSplat);
            SpriteGameFactory.Create(SpriteGame.Name.BombSplat_Green);

            SpriteGameFactory.Create(SpriteGame.Name.Floor);
            SpriteGameFactory.Create(SpriteGame.Name.UFO);
            SpriteGameFactory.Create(SpriteGame.Name.UFO_Splat);



            //------------------------------------------------------
            // Instantiate the scene context
            //------------------------------------------------------
            pSceneContext = new SceneContext();


        }

        //-----------------------------------------------------------------------------
        // Game::Update()
        //      Called once per frame, update data, tranformations, etc
        //      Use this function to control process order
        //      Input, AI, Physics, Animation, and Graphics
        //-----------------------------------------------------------------------------
        public override void Update()
        {
            GlobalTimer.Update(this.GetTime());

            // Hack to proof of concept... 
            if (Azul.Keyboard.KeyPressed(Azul.AZUL_KEY.KEY_0) == true)
            {
                pSceneContext.SetState(SceneContext.Scene.Select);
            }

            if (Azul.Keyboard.KeyPressed(Azul.AZUL_KEY.KEY_1) == true)
            {
                pSceneContext.SetState(SceneContext.Scene.Play);
            }

            if (Azul.Keyboard.KeyPressed(Azul.AZUL_KEY.KEY_2) == true)
            {
                pSceneContext.SetState(SceneContext.Scene.Over);
            }

            if (Azul.Keyboard.KeyPressed(Azul.AZUL_KEY.KEY_4) == true)
            {
                pSceneContext.HandleKeyPress(ScenePlay.KeyPress.Activate_Boxes);
            }

            if (Azul.Keyboard.KeyPressed(Azul.AZUL_KEY.KEY_5) == true)
            {
                pSceneContext.HandleKeyPress(ScenePlay.KeyPress.Deactivate_Boxes);
            }

            if (Azul.Keyboard.KeyPressed(Azul.AZUL_KEY.KEY_6) == true)
            {
                pSceneContext.HandleKeyPress(ScenePlay.KeyPress.Activate_Sprites);
            }

            if (Azul.Keyboard.KeyPressed(Azul.AZUL_KEY.KEY_7) == true)
            {
                pSceneContext.HandleKeyPress(ScenePlay.KeyPress.Deactive_Sprites);
            }

            // Update the scene
            pSceneContext.GetState().Update(this.GetTime());
        }

        //-----------------------------------------------------------------------------
        // Game::Draw()
        //		This function is called once per frame
        //	    Use this for draw graphics to the screen.
        //      Only do rendering here
        //-----------------------------------------------------------------------------

        public override void Draw()
        {
            SpriteBatchMan.Draw();

        }

        //-----------------------------------------------------------------------------
        // Game::UnLoadContent()
        //       unload content (resources loaded above)
        //       unload all content that was loaded before the Engine Loop started
        //-----------------------------------------------------------------------------
        public override void UnLoadContent()
        {
            FontMan.Destroy();
            GlyphMan.Destroy();
            ColPairMan.Destroy();
            GameObjectNodeMan.Destroy();
            SpriteGameProxyMan.Destroy();
            TimerEventMan.Destroy();
            SpriteBoxMan.Destroy();
            SpriteBatchMan.Destroy();
            SpriteGameMan.Destroy();
            ImageMan.Destroy();
            TextureMan.Destroy();
            DeltaMan.Destroy();
            GhostMan.Destroy();
            //Simulation.Destroy();
        }


        public override void DisplayHeader()
        {
            Console.Write(this.Header());
        }

        public override void DisplayFooter()
        {
            Console.Write(this.Footer());
        }

        public static readonly int SCREEN_WIDTH = 672;
        public static readonly int SCREEN_HEIGHT = 768;

        public static int ROW_0 = 600;
        public static int ROW_1 = 550;
        public static int ROW_2 = 500;
        public static int ROW_3 = 450;
        public static int ROW_4 = 400;

        public static int COL_0_POS = 86;

        public static int HORIZONTAL_SHIFT = 50;
    }
}

// --- End of File ---
