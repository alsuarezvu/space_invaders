using System;
using System.Diagnostics;
using static SE456.SceneContext;

namespace SE456
{
    public class ScenePlay : SceneState
    {
       
        IrrKlang.ISoundEngine sndEngine = null;
        IrrKlang.ISoundSource pSndShoot;
        IrrKlang.ISoundSource pUFOLowPitch;

        readonly Random pRandom = new Random();
        
        public SpriteBatchMan poSpriteBatchMan;

        
        SpriteAnimationCmd pSquidAnimation = new SpriteAnimationCmd(SpriteGame.Name.Squid, TimerEvent.Name.SquidAnimation);
        SpriteAnimationCmd pCrabAnimation = new SpriteAnimationCmd(SpriteGame.Name.Crab, TimerEvent.Name.CrabAnimation);
        SpriteAnimationCmd pOctopusAnimation = new SpriteAnimationCmd(SpriteGame.Name.Octopus, TimerEvent.Name.OctopusAnimation);

        SoundMarchCmd pAlienSoundMarchCmd;

        bool gameInitialized = false;
        bool gameOver = false;

        public int row_0 = 600;
        public int row_1 = 550;
        public int row_2 = 500;
        public int row_3 = 450;
        public int row_4 = 400;

        public int gameLevel = 0;
        public static readonly int Y_POS_LEVEL_ADVANCE = -20;




        public ScenePlay(SceneContext _sceneContext)
        {
            this.Initialize();
            this.sceneContext = _sceneContext;
        }
        public override void Handle()
        {
            cleanCurrentGame();
            //Debug.WriteLine("----------------------PRINT ALL NODES CLEAN---------------");
            //GameObjectNodeMan.PrintAllNodes();
            sceneContext.SetState(Scene.Over);
        }


        public override void Initialize()
        {
            //------------------------------------------------------
            // Instantiate the Sound engines and pre-load the sounds 
            //------------------------------------------------------

            // start up the engine
            sndEngine = new IrrKlang.ISoundEngine();
            IrrKlang.ISoundSource pSndVader0 = sndEngine.AddSoundSourceFromFile("fastinvader1.wav");
            pSndVader0.DefaultVolume = 0.0f;
            sndEngine.Play2D(pSndVader0, false, false, false);

            IrrKlang.ISoundSource pSndVader1 = sndEngine.AddSoundSourceFromFile("fastinvader2.wav");
            pSndVader1.DefaultVolume = 0.0f;
            sndEngine.Play2D(pSndVader1, false, false, false);

            IrrKlang.ISoundSource pSndVader2 = sndEngine.AddSoundSourceFromFile("fastinvader3.wav");
            pSndVader2.DefaultVolume = 0.0f;
            sndEngine.Play2D(pSndVader2, false, false, false);

            IrrKlang.ISoundSource pSndVader3 = sndEngine.AddSoundSourceFromFile("fastinvader4.wav");
            pSndVader3.DefaultVolume = 0.0f;
            sndEngine.Play2D(pSndVader3, false, false, false);

            pSndShoot = sndEngine.AddSoundSourceFromFile("shoot.wav");
            pSndShoot.DefaultVolume = 0.0f;
            sndEngine.Play2D(pSndShoot, false, false, false);

            IrrKlang.ISoundSource pSndExplosion = sndEngine.AddSoundSourceFromFile("explosion.wav");
            pSndExplosion.DefaultVolume = 0.0f;
            sndEngine.Play2D(pSndExplosion, false, false, false);

            IrrKlang.ISoundSource pInvaderKilled = sndEngine.AddSoundSourceFromFile("invaderkilled.wav");
            pInvaderKilled.DefaultVolume = 0.0f;
            sndEngine.Play2D(pInvaderKilled, false, false, false);

            pUFOLowPitch = sndEngine.AddSoundSourceFromFile("ufo_lowpitch.wav");
            pUFOLowPitch.DefaultVolume = 0.0f;
            sndEngine.Play2D(pUFOLowPitch, false, false, false);

            pSndVader0.DefaultVolume = 1.0f;
            pSndVader1.DefaultVolume = 1.0f;
            pSndVader2.DefaultVolume = 1.0f;
            pSndVader3.DefaultVolume = 1.0f;
            pSndShoot.DefaultVolume = 1.0f;
            pSndExplosion.DefaultVolume = 1.0f;
            pInvaderKilled.DefaultVolume = 1.0f;
            pUFOLowPitch.DefaultVolume = 1.0f;

            //-------------------------------------------------------
            // Create SpriteBatches
            //-------------------------------------------------------

            this.poSpriteBatchMan = new SpriteBatchMan(3, 1);
            SpriteBatchMan.SetActive(this.poSpriteBatchMan);

            SpriteBatch pSB_Texts = SpriteBatchMan.Add(SpriteBatch.Name.Texts);
            SpriteBatch spriteBoxBatch = SpriteBatchMan.Add(SpriteBatch.Name.Box, 5);
            SpriteBatch spriteAlienBatch = SpriteBatchMan.Add(SpriteBatch.Name.Alien, 5);
            SpriteBatch spriteShipBatch = SpriteBatchMan.Add(SpriteBatch.Name.Ship, 5);
            SpriteBatch spriteBoxBottomWallBatch = SpriteBatchMan.Add(SpriteBatch.Name.BottomWall, 5);
            SpriteBatch spriteShieldBatch = SpriteBatchMan.Add(SpriteBatch.Name.Shields, 5);
            SpriteBatch spriteMissileBatch = SpriteBatchMan.Add(SpriteBatch.Name.Missile, 5);
            SpriteBatch spriteBombBatch = SpriteBatchMan.Add(SpriteBatch.Name.Bomb, 5);
            SpriteBatch spriteUFOBatch = SpriteBatchMan.Add(SpriteBatch.Name.UFO, 5);


            //-------------------------------------------------------------------
            // Initialize ShipMan
            //-------------------------------------------------------------------
            ShipMan.Create(pSndShoot);


            //-------------------------------------------------------------------
            // Create Missile Root
            //-------------------------------------------------------------------

            MissileGroup pMissileRoot = new MissileGroup();
            pMissileRoot.ActivateSprite(spriteMissileBatch);
            pMissileRoot.ActivateCollisionSprite(spriteBoxBatch);

            GameObjectNodeMan.Attach(pMissileRoot);

            //---------------------------------------------------------------------------------------------------------
            // Create Walls
            //---------------------------------------------------------------------------------------------------------

            WallGroup pWallGroup = new WallGroup(GameObject.Name.WallGroup, SpriteGame.Name.NullObject, 0.0f, 0.0f);
            //pWallGroup.ActivateSprite(spriteAlienBatch);
            pWallGroup.ActivateCollisionSprite(spriteBoxBatch);

            WallRight pWallRight = new WallRight(GameObject.Name.WallRight, SpriteGame.Name.NullObject, SCREEN_WIDTH - 12, 350, 24, 580);
            pWallRight.ActivateCollisionSprite(spriteBoxBatch);

            WallLeft pWallLeft = new WallLeft(GameObject.Name.WallLeft, SpriteGame.Name.NullObject, 12, 350, 24, 580);
            pWallLeft.ActivateCollisionSprite(spriteBoxBatch);

            WallTop pWallTop = new WallTop(GameObject.Name.WallTop, SpriteGame.Name.NullObject, 672 / 2, SCREEN_HEIGHT - (103 / 2), 672, 103);
            pWallTop.ActivateCollisionSprite(spriteBoxBatch);

            // Add to the composite the children
            pWallGroup.Add(pWallRight);
            pWallGroup.Add(pWallLeft);
            pWallGroup.Add(pWallTop);

            GameObjectNodeMan.Attach(pWallGroup);


            WallBottom pWallBottom = new WallBottom(GameObject.Name.WallBottom, SpriteGame.Name.Floor, 672 / 2, 50, 672, 3);
            pWallBottom.ActivateSprite(spriteBoxBottomWallBatch);
            pWallBottom.ActivateCollisionSprite(spriteBoxBottomWallBatch);

            WallRoot pWallRootBottom = new WallRoot(GameObject.Name.WallRoot, SpriteGame.Name.NullObject, 0.0f, 0.0f);
            pWallRootBottom.Add(pWallBottom);

            GameObjectNodeMan.Attach(pWallRootBottom);

            //---------------------------------------------------------------------------------------------------------
            // Create Bumpers
            //---------------------------------------------------------------------------------------------------------

            BumperLeft pBumperLeft = new BumperLeft(GameObject.Name.BumperLeft, SpriteGame.Name.NullObject, 25, 100, 50, 100);
            pBumperLeft.ActivateCollisionSprite(spriteBoxBatch);

            BumperLeftRoot pBumperLeftRoot = new BumperLeftRoot(GameObject.Name.BumperLeftRoot, SpriteGame.Name.NullObject, 0.0f, 0.0f);
            pBumperLeftRoot.Add(pBumperLeft);

            GameObjectNodeMan.Attach(pBumperLeftRoot);

            BumperRight pBumperRight = new BumperRight(GameObject.Name.BumperRight, SpriteGame.Name.NullObject, SCREEN_WIDTH - 25, 100, 50, 100);
            pBumperRight.ActivateCollisionSprite(spriteBoxBatch);

            BumperRightRoot pBumperRightRoot = new BumperRightRoot(GameObject.Name.BumperRightRoot, SpriteGame.Name.NullObject, 0.0f, 0.0f);
            pBumperRightRoot.Add(pBumperRight);

            GameObjectNodeMan.Attach(pBumperRightRoot);

            //---------------------------------------------------------------------------------------------------------
            // Create Alien Grid Root and attach to GameObjectNodeMan
            //---------------------------------------------------------------------------------------------------------
            GridRoot pAlienGridRoot = new GridRoot(GameObject.Name.AlienGridRoot, SpriteGame.Name.NullObject, 0.0f, 0.0f);
                    GameObjectNodeMan.Attach(pAlienGridRoot);

            //---------------------------------------------------------------------------------------------------------
            // Create Main Missile Ship and attach to GameObjectNodeMan
            //---------------------------------------------------------------------------------------------------------

            //Create Main Ship
            ShipRoot pShipRoot = new ShipRoot(GameObject.Name.ShipRoot, SpriteGame.Name.NullObject, 0.0f, 0.0f);
            pShipRoot.ActivateCollisionSprite(spriteBoxBatch);
            GameObjectNodeMan.Attach(pShipRoot);

            //---------------------------------------------------------------------------------------------------------
            // Create Reserve Ships
            //---------------------------------------------------------------------------------------------------------

            ShipRoot pShipRootReserve = new ShipRoot(GameObject.Name.ShipRootReserve, SpriteGame.Name.NullObject, 0.0f, 0.0f);
            GameObjectNodeMan.Attach(pShipRootReserve);

            //---------------------------------------------------------------------------------------------------------
            // Create Shield Root and attach to GameObjectNodeMan
            //---------------------------------------------------------------------------------------------------------

            Composite pShieldRoot = (Composite)new ShieldRoot(GameObject.Name.ShieldRoot, SpriteGame.Name.NullObject, 0.0f, 0.0f);
            GameObjectNodeMan.Attach(pShieldRoot);

            //---------------------------------------------------------------------------------------------------------
            // Create Bomb Root and attach to GameObjectNodeMan
            //---------------------------------------------------------------------------------------------------------
            BombRoot pBombRoot = new BombRoot(GameObject.Name.BombRoot, SpriteGame.Name.NullObject, 0.0f, 0.0f);
            pBombRoot.ActivateCollisionSprite(spriteBoxBatch);
            GameObjectNodeMan.Attach(pBombRoot);

            //-------------------------------------------------------------------
            // Initialize BombMan
            //-------------------------------------------------------------------
            BombMan.Create(pRandom, spriteBombBatch, spriteBoxBatch, pBombRoot);

            //---------------------------------------------------------------------------------------------------------
            // Create UFO Root and attach to GameObjectNodeMan
            //---------------------------------------------------------------------------------------------------------
            UFORoot pUFORoot = new UFORoot(GameObject.Name.UFORoot, SpriteGame.Name.NullObject, 0.0f, 0.0f);
            pBombRoot.ActivateCollisionSprite(spriteBoxBatch);
            GameObjectNodeMan.Attach(pUFORoot);

            //-------------------------------------------------------------------
            // Initialize UFOMan
            //-------------------------------------------------------------------
            UFOMan.Create(pRandom, spriteUFOBatch, spriteBoxBatch, pUFORoot);

            //-------------------------------------------------------------------
            // Create AlienCounter
            //-------------------------------------------------------------------
            AlienCounter.Create();

            //---------------------------------------------------------------------------------------------------------
            // Attach Move Right, Left, and Shoot Observers to InputMan
            // Set Simulation State to RealTime
            //---------------------------------------------------------------------------------------------------------

            InputSubject pInputSubject;
            pInputSubject = InputMan.GetArrowRightSubject();
            pInputSubject.Attach(new MoveRightObserver());

            pInputSubject = InputMan.GetArrowLeftSubject();
            pInputSubject.Attach(new MoveLeftObserver());

            pInputSubject = InputMan.GetSpaceSubject();
            pInputSubject.Attach(new ShootObserver(sndEngine, pSndShoot));

            //--------------------------------------------------------------------------------------------------------
            // Add Fonts
            //---------------------------------------------------------------------------------------------------------

            SpriteBatch spriteBatchTexts = SpriteBatchMan.Find(SpriteBatch.Name.Texts);
            //Top Line
            FontMan.Add(Font.Name.Score_1_Label, spriteBatchTexts, "S C O R E < 1 >", Glyph.Name.SpaceInvaders, 26, 740);
            FontMan.Add(Font.Name.Hi_Score_Label, spriteBatchTexts, "H I - S C O R E", Glyph.Name.SpaceInvaders, 265, 740);
            FontMan.Add(Font.Name.Score_2_Label, spriteBatchTexts, "S C O R E < 2 >", Glyph.Name.SpaceInvaders, 503, 740);

            //2nd Line
            FontMan.Add(Font.Name.Score_1, spriteBatchTexts, "0 0 0 0", Glyph.Name.SpaceInvaders, 52, 700);
            FontMan.Add(Font.Name.Hi_Score, spriteBatchTexts, "1 0 0 0", Glyph.Name.SpaceInvaders, 275, 700);
            FontMan.Add(Font.Name.Score_2, spriteBatchTexts, "0 0 0 0", Glyph.Name.SpaceInvaders, 557, 700);

            //Bottom line
            FontMan.Add(Font.Name.Reserve_Ships, spriteBatchTexts, "3", Glyph.Name.SpaceInvaders, 25, 30);
            FontMan.Add(Font.Name.Credits, spriteBatchTexts, "C R E D I T", Glyph.Name.SpaceInvaders, 485, 30);
            FontMan.Add(Font.Name.Credits_Num, spriteBatchTexts, "0 0", Glyph.Name.SpaceInvaders, 608, 30);

            //-------------------------------------------------------------------
            // Create ScoreMan
            //-------------------------------------------------------------------
            ScoreMan.Create(FontMan.Find(Font.Name.Score_1), FontMan.Find(Font.Name.Hi_Score));

            //--------------------------------------------------------------------------------------------------------
            // Initialize Sound Command and Attach marching sounds
            //---------------------------------------------------------------------------------------------------------

            //LTN - TimerEventMan
            pAlienSoundMarchCmd = new SoundMarchCmd(sndEngine);
            pAlienSoundMarchCmd.Attach(pSndVader3, SoundNode.Name.fastinvader4, "W A V - 4");
            pAlienSoundMarchCmd.Attach(pSndVader2, SoundNode.Name.fastinvader3, "W A V - 3");
            pAlienSoundMarchCmd.Attach(pSndVader1, SoundNode.Name.fastinvader2, "W A V - 2");
            pAlienSoundMarchCmd.Attach(pSndVader0, SoundNode.Name.fastinvader1, "W A V - 1");

            //--------------------------------------------------------------------------------------------------------
            // Attach Sprite Images to the Alien commands (Global Variable)
            //---------------------------------------------------------------------------------------------------------

            // Create an animation sprites for each type
            //LTN - TimerEventMan
            pSquidAnimation.Attach(Image.Name.SquidB);
            pSquidAnimation.Attach(Image.Name.SquidA);

            //LTN - TimerEventMan
            pCrabAnimation.Attach(Image.Name.CrabB);
            pCrabAnimation.Attach(Image.Name.CrabA);

            //LTN - TimerEventMan
            pOctopusAnimation.Attach(Image.Name.OctopusB);
            pOctopusAnimation.Attach(Image.Name.OctopusA);
            //--------------------------------------------------------------------------------------------------------
            // Create Delta Objects to share amongst commands and observers
            //---------------------------------------------------------------------------------------------------------

            DeltaMan.Add(Delta.Name.Timer, 0.70f, 0.05f);
            DeltaMan.Add(Delta.Name.Bomb, 0.70f, .70f); 
            DeltaMan.Add(Delta.Name.Move, 4.0f, 16.0f);
            DeltaMan.Add(Delta.Name.UFO, 30.0f, 30.0f);

            //------------------------------------------------------------------------------------------
            // Create Collision Pairs and Add to the Manager 
            //------------------------------------------------------------------------------------------

            // Alien Grid vs. Wall
            ColPair pAlien_Wall_ColPair = ColPairMan.Add(ColPair.Name.Alien_Wall, pAlienGridRoot, pWallGroup);
            Debug.Assert(pAlien_Wall_ColPair != null);
            pAlien_Wall_ColPair.Attach(new GridObserver());

            // Missile vs. Top Wall
            ColPair pMissile_pWallTop_ColPair = ColPairMan.Add(ColPair.Name.Missile_Wall, pMissileRoot, pWallTop);
            pMissile_pWallTop_ColPair.Attach(new ShipReadyObserver());
            pMissile_pWallTop_ColPair.Attach(new RemoveMissileObserver());
            

            // Ship vs. Left Bumper
            ColPair pShip_pLeftBumper_ColPair = ColPairMan.Add(ColPair.Name.Ship_LeftBumper, pShipRoot, pBumperLeftRoot);
            pShip_pLeftBumper_ColPair.Attach(new ShipMoveRightObserver());

            // Ship vs. Right Bumper
            ColPair pShip_pRightBumper_ColPair = ColPairMan.Add(ColPair.Name.Ship_RightBumper, pShipRoot, pBumperRightRoot);
            pShip_pRightBumper_ColPair.Attach(new ShipMoveLeftObserver());

            // Missile vs. Alien
            ColPair pMissile_pAlien_ColPair = ColPairMan.Add(ColPair.Name.Alien_Missile, pMissileRoot, pAlienGridRoot);
            pMissile_pAlien_ColPair.Attach(new RemoveAlienObserver());
            pMissile_pAlien_ColPair.Attach(new SndObserver(sndEngine, pInvaderKilled));
            pMissile_pAlien_ColPair.Attach(new RemoveMissileObserver());
            pMissile_pAlien_ColPair.Attach(new DeltaTimeDecrementObserver());
            pMissile_pAlien_ColPair.Attach(new DeltaMoveIncrementObserver());
            pMissile_pAlien_ColPair.Attach(new UpdateScoreObserver(Font.Name.Score_1));
            pMissile_pAlien_ColPair.Attach(new ShipReadyObserver());

            // Bomb vs. Shield
            ColPair pBomb_pShield_ColPair = ColPairMan.Add(ColPair.Name.Bomb_Shield, pBombRoot, pShieldRoot);
            pBomb_pShield_ColPair.Attach(new RemoveBombObserver());
            pBomb_pShield_ColPair.Attach(new RemoveBrickObserver());

            //Missile vs. Shield
            ColPair pMissile_pShield_ColPair = ColPairMan.Add(ColPair.Name.Misslie_Shield, pMissileRoot, pShieldRoot);
            pMissile_pShield_ColPair.Attach(new RemoveMissileObserver());
            pMissile_pShield_ColPair.Attach(new RemoveBrickObserver());
            pMissile_pShield_ColPair.Attach(new ShipReadyObserver());

            //Bomb vs. Missile
            ColPair pBomb_pMissile_ColPair = ColPairMan.Add(ColPair.Name.Bomb_Missile, pMissileRoot, pBombRoot);
            pBomb_pMissile_ColPair.Attach(new RemoveBombWithSplatObserver());
            pBomb_pMissile_ColPair.Attach(new RemoveBombMissileObserver());
            pBomb_pMissile_ColPair.Attach(new SndObserver(sndEngine, pSndExplosion));
            pBomb_pMissile_ColPair.Attach(new ShipReadyObserver());

            //Bomb vs. Wall
            ColPair pBomb_pWall_ColPair = ColPairMan.Add(ColPair.Name.Bomb_Wall, pWallRootBottom, pBombRoot);
            pBomb_pWall_ColPair.Attach(new RemoveBombObserver());
           

            //Alien vs. Shield
            ColPair pAlien_pShield_ColPair = ColPairMan.Add(ColPair.Name.Alien_Shield, pAlienGridRoot, pShieldRoot);
            pAlien_pShield_ColPair.Attach(new RemoveBrickObserver());

            //Alien vs. Bottom Wall
            ColPair pAlien_pWall_ColPair = ColPairMan.Add(ColPair.Name.Alien_Wall, pAlienGridRoot, pWallRootBottom);
            pAlien_pWall_ColPair.Attach(new GameOverObserver(this));

            //Alien vs. Ship
            ColPair pAlien_pShip_ColPair = ColPairMan.Add(ColPair.Name.Alien_Ship, pAlienGridRoot, pShipRoot);
            pAlien_pShip_ColPair.Attach(new GameOverObserver(this));
            pAlien_pShip_ColPair.Attach(new RemoveActiveShipObserver(Font.Name.Reserve_Ships));
            pAlien_pShip_ColPair.Attach(new SndObserver(sndEngine, pSndExplosion));
            //pAlien_pShip_ColPair.Attach(new ShipDeadStateObserver());

            //Bomb vs. Ship
            ColPair pBomb_pShip_ColPair = ColPairMan.Add(ColPair.Name.Bomb_Ship, pBombRoot, pShipRoot);
            pBomb_pShip_ColPair.Attach(new ShipReadyObserver());
            pBomb_pShip_ColPair.Attach(new RemoveActiveShipObserver(Font.Name.Reserve_Ships));
            pBomb_pShip_ColPair.Attach(new RemoveBombObserver());
            pBomb_pShip_ColPair.Attach(new SndObserver(sndEngine, pSndExplosion));
           

            ColPair pUFO_pMissile_ColPair = ColPairMan.Add(ColPair.Name.Missile_UFO, pMissileRoot, pUFORoot);
            pUFO_pMissile_ColPair.Attach(new RemoveUFOObserver());
            pUFO_pMissile_ColPair.Attach(new RemoveMissileObserver());
            pUFO_pMissile_ColPair.Attach(new SndObserver(sndEngine, pSndExplosion));
            pUFO_pMissile_ColPair.Attach(new UpdateUFOScoreObserver(Font.Name.Score_1));
            pUFO_pMissile_ColPair.Attach(new ShipReadyObserver());
        }

        //------------------------------------------------------------------------------------------
        // This method contains everything needed to re-create a new game after a previous one has been played.
        //------------------------------------------------------------------------------------------
        private void InitializeNewGame(int _gameLevel)
        {
            //added to avoid instaneces where transition was called 2x
            //perhaps due to how the Input Man processes the key strokes
            if (!gameInitialized)
            {
                //--------------------------------------------------------------------------------------------------------
                // Reset Game Level
                //---------------------------------------------------------------------------------------------------------
                gameLevel = _gameLevel;

                //--------------------------------------------------------------------------------------------------------
                // Reset Delta Objects to share amongst commands and observers
                //---------------------------------------------------------------------------------------------------------
                Delta deltaAlienMarch = DeltaMan.Find(Delta.Name.Timer);
                deltaAlienMarch.Set(Delta.Name.Timer, 0.70f, 0.05f);

                Delta deltaBombSpawn = DeltaMan.Find(Delta.Name.Bomb);
                deltaBombSpawn.Set(Delta.Name.Bomb, 0.70f, .05f);

                Delta deltaAlienMove = DeltaMan.Find(Delta.Name.Move);
                deltaAlienMove.Set(Delta.Name.Move, 4.0f, 16.0f);

                //--------------------------------------------------------------------------------------------------------
                // Reset Alien Counter
                //---------------------------------------------------------------------------------------------------------
                AlienCounter.Reset(0);

                //--------------------------------------------------------------------------------------------------------
                // Create Alien Grid
                //---------------------------------------------------------------------------------------------------------
                GameObject pAlienGridRoot = GameObjectNodeMan.Find(GameObject.Name.AlienGridRoot);
                AlienGrid pAlienGrid = createAlienGrid();
                pAlienGridRoot.Add(pAlienGrid);

                //--------------------------------------------------------------------------------------------------------
                //Create Active Ships
                //---------------------------------------------------------------------------------------------------------
                //Create Active Ship and attach to GameRoot
                GameObject pShipRoot = GameObjectNodeMan.Find(GameObject.Name.ShipRoot);
                ShipMan.CreateActiveShip(pShipRoot);

                //--------------------------------------------------------------------------------------------------------
                // Create Reserve Ships
                //---------------------------------------------------------------------------------------------------------
                ShipMan.CreateReserveShip(88, 33);
                ShipMan.CreateReserveShip(133, 33);
                ShipMan.CreateReserveShip(178, 33);

                //--------------------------------------------------------------------------------------------------------
                // Create Shields
                //---------------------------------------------------------------------------------------------------------
                ShieldRoot pShieldRoot = (ShieldRoot)GameObjectNodeMan.Find(GameObject.Name.ShieldRoot);
                createShields(pShieldRoot);

                //--------------------------------------------------------------------------------------------------------
                // Reset Game Texts
                //---------------------------------------------------------------------------------------------------------
                resetGameScoresAndTexts();


                //--------------------------------------------------------------------------------------------------------
                // Reset UFOMan
                //---------------------------------------------------------------------------------------------------------
                UFOMan.SetUFOActive(false);

                //--------------------------------------------------------------------------------------------------------
                // Re-initialize commands for game play
                //---------------------------------------------------------------------------------------------------------
                //LTN - TimerEventMan
                AlienMoveCmd pAlienMoveCmd = new AlienMoveCmd();
                pAlienMoveCmd.Attach(pAlienGrid);

                //LTN - TimerEventMan
                BombSpawnEventCmd pBombEvent = new BombSpawnEventCmd(pRandom, SpriteBatchMan.Find(SpriteBatch.Name.Bomb),
                    SpriteBatchMan.Find(SpriteBatch.Name.Box));

                UFOEventCmd pUFOEvent = new UFOEventCmd(pRandom, SpriteBatchMan.Find(SpriteBatch.Name.UFO),
                    SpriteBatchMan.Find(SpriteBatch.Name.Box), sndEngine, pUFOLowPitch);


                //--------------------------------------------------------------------------------------------------------
                // Re-Create Timers
                //---------------------------------------------------------------------------------------------------------

                TimerEventMan.AddBasedOnTriggerTime(TimerEvent.Name.AlienGridMove, pAlienMoveCmd, deltaAlienMarch);

                TimerEventMan.AddBasedOnTriggerTime(TimerEvent.Name.SquidAnimation, pSquidAnimation, deltaAlienMarch);
                TimerEventMan.AddBasedOnTriggerTime(TimerEvent.Name.CrabAnimation, pCrabAnimation, deltaAlienMarch);
                TimerEventMan.AddBasedOnTriggerTime(TimerEvent.Name.OctopusAnimation, pOctopusAnimation, deltaAlienMarch);

                TimerEventMan.AddBasedOnTriggerTime(TimerEvent.Name.AlienMarchSound, pAlienSoundMarchCmd, deltaAlienMarch);

                TimerEventMan.AddBasedOnTriggerTime(TimerEvent.Name.BombSpawn, pBombEvent, deltaBombSpawn);

                TimerEventMan.AddBasedOnTriggerTime(TimerEvent.Name.UFO, pUFOEvent, deltaBombSpawn);

                Simulation.SetState(Simulation.State.Realtime);

                gameInitialized = true;

            }
            else
            {
                //Debug.WriteLine("Attempted to initialize another game when one was already initialized!");
            }
        }


        //------------------------------------------------------------------------------------------
        // This method contains everything needed to delete objects from a current game
        //------------------------------------------------------------------------------------------
        private void cleanCurrentGame()
        {
            //Remove AlienGridRoot children
            removeChildren(GameObjectNodeMan.Find(GameObject.Name.AlienGridRoot));  

            //Remove ShipRoot children
            removeChildren(GameObjectNodeMan.Find(GameObject.Name.ShipRoot));

            //Remove ShipRootReserve children
            removeChildren(GameObjectNodeMan.Find(GameObject.Name.ShipRootReserve));

            //remove active ship for ShipMan
            ShipMan.resetForNewGame();

            //Remove ShieldRoot children
            removeChildren(GameObjectNodeMan.Find(GameObject.Name.ShieldRoot));

            //Remove BombRoot children
            removeChildren(GameObjectNodeMan.Find(GameObject.Name.BombRoot));

            //Remove UFORoot children
            removeChildren(GameObjectNodeMan.Find(GameObject.Name.UFORoot));

            //Remove TimerEvents
            TimerEventMan.RemoveAll();

            gameInitialized = false;
            gameOver = false;
        }

        private void removeChildren(GameObject gameObjectRoot)
        {
            IteratorReverseComposite pRev = new IteratorReverseComposite(gameObjectRoot);
            for (pRev.First(); !pRev.IsDone(); pRev.Next())
            {
                GameObject pTmp = (GameObject)pRev.Curr();
                if (!(pTmp.name == gameObjectRoot.name))
                {
                    //Debug.WriteLine("Removing " + pTmp.name);
                    pTmp.Remove();
                }
            }

          
        }

        private AlienGrid createAlienGrid()
        {
            AlienFactory AF = new AlienFactory(SpriteBatchMan.Find(SpriteBatch.Name.Alien), SpriteBatchMan.Find(SpriteBatch.Name.Box));
            AlienGrid pAlienGrid = (AlienGrid)AF.Create(GameObject.Name.AlienGrid, 0.0f, 0.0f);

            int x_pos = COL_0_POS;

            for (int i = 0; i < 11; i++)
            {
                GameObject pCol = AF.Create(GameObject.Name.AlienColumn, 0.0f, 0.0f);

                pAlienGrid.Add(pCol);

                pCol.Add(AF.Create(GameObject.Name.Octopus, x_pos, row_4 + (gameLevel * Y_POS_LEVEL_ADVANCE)));
                pCol.Add(AF.Create(GameObject.Name.Octopus, x_pos, row_3 + (gameLevel * Y_POS_LEVEL_ADVANCE)));
                pCol.Add(AF.Create(GameObject.Name.Crab, x_pos, row_2 + (gameLevel * Y_POS_LEVEL_ADVANCE)));
                pCol.Add(AF.Create(GameObject.Name.Crab, x_pos, row_1 + (gameLevel * Y_POS_LEVEL_ADVANCE)));
                pCol.Add(AF.Create(GameObject.Name.Squid, x_pos, row_0 + (gameLevel * Y_POS_LEVEL_ADVANCE)));

                x_pos += HORIZONTAL_SHIFT;
            }
            return pAlienGrid; 
        }

        private void createShields(ShieldRoot pShieldRoot)
        {
            ShieldFactory shieldFactory = new ShieldFactory(SpriteBatchMan.Find(SpriteBatch.Name.Shields),
               SpriteBatchMan.Find(SpriteBatch.Name.Box), pShieldRoot);

            shieldFactory.SetParent(pShieldRoot);

            ShieldGrid shieldGrid_0 = (ShieldGrid)shieldFactory.Create(ShieldCategory.Type.Grid, GameObject.Name.NullObject, 0.0f, 0.0f);
            ShieldGrid shieldGrid_1 = (ShieldGrid)shieldFactory.Create(ShieldCategory.Type.Grid, GameObject.Name.NullObject, 0.0f, 0.0f);
            ShieldGrid shieldGrid_2 = (ShieldGrid)shieldFactory.Create(ShieldCategory.Type.Grid, GameObject.Name.NullObject, 0.0f, 0.0f);
            ShieldGrid shieldGrid_3 = (ShieldGrid)shieldFactory.Create(ShieldCategory.Type.Grid, GameObject.Name.NullObject, 0.0f, 0.0f);

            shieldFactory.createShield(shieldGrid_0, 91.0f, 150.0f);
            shieldFactory.createShield(shieldGrid_1, 232.0f, 150.0f);
            shieldFactory.createShield(shieldGrid_2, 373.0f, 150.0f);
            shieldFactory.createShield(shieldGrid_3, 514.0f, 150.0f);
        }

        private void resetGameScoresAndTexts()
        {
            SpriteBatch spriteBatchTexts = SpriteBatchMan.Find(SpriteBatch.Name.Texts);

            ScoreMan.resetScore();
            ScoreMan.PrintScore(FontMan.Find(Font.Name.Score_1));
            ScoreMan.PrintHighestScore(FontMan.Find(Font.Name.Hi_Score));
            FontMan.Find(Font.Name.Score_2).UpdateMessage("0 0 0 0"); //TODO: Not implemented

            //Update Reserve Ship Count
            FontMan.Find(Font.Name.Reserve_Ships).UpdateMessage("3");
        }
        public override void Update(float systemTime)
        {
            // Single Step, Free running...
            Simulation.Update(systemTime);

            // Snd update - keeps everything moving and updating smoothly
            sndEngine.Update();

            // Input
            InputMan.Update();

            //// Run based on simulation stepping
            if (Simulation.GetTimeStep() > 0.0f)
            {
                // Fire off the timer events
                TimerEventMan.Update(Simulation.GetTotalTime());

                // walk through all objects and push to proxy
                GameObjectNodeMan.Update();

                // Do the collision checks
                ColPairMan.Process();

                // Delete any objects here...
                DelayedObjectMan.Process();

                if (ShipMan.getReserveShipCount() == 0 || this.gameOver)
                {
                    ScoreMan.UpdateHighestScore();
                    Simulation.SetState(Simulation.State.Pause);
                    this.Handle();
                }

                if (AlienCounter.GetCount() == 0)
                {
                    ScoreMan.UpdateHighestScore();
                    Simulation.SetState(Simulation.State.Pause);

                    cleanCurrentGame();

                    gameLevel = gameLevel + 1;
                    //player has won level start over
                    InitializeNewGame(gameLevel);
                }
            }
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
            InitializeNewGame(0);
            //Debug.WriteLine("----------------------GHOST MAN TRANSITION---------------");
            //GhostMan.Dump();

            //Debug.WriteLine("----------------------PRINT ALL NODES TRANSITION---------------");
            //GameObjectNodeMan.PrintAllNodes();
        }

        public override void HandleKeyPress(KeyPress keyPress)
        {
            SpriteBatch boxSpriteBatch = SpriteBatchMan.Find(SpriteBatch.Name.Box);
            SpriteBatch alienSpriteBatch = SpriteBatchMan.Find(SpriteBatch.Name.Alien);
            SpriteBatch shieldSpriteBatch = SpriteBatchMan.Find(SpriteBatch.Name.Shields);
            SpriteBatch bombSpriteBatch = SpriteBatchMan.Find(SpriteBatch.Name.Bomb);
            SpriteBatch shipSpriteBatch = SpriteBatchMan.Find(SpriteBatch.Name.Ship);
            SpriteBatch ufoSpriteBatch = SpriteBatchMan.Find(SpriteBatch.Name.UFO);

            if (KeyPress.Activate_Boxes == keyPress)
            {
                boxSpriteBatch.setActivateState(true);
            }

            if (KeyPress.Deactivate_Boxes == keyPress)
            {
                boxSpriteBatch.setActivateState(false);
            }

            if (KeyPress.Activate_Sprites == keyPress)
            {
                alienSpriteBatch.setActivateState(true);
                shieldSpriteBatch.setActivateState(true);
                bombSpriteBatch.setActivateState(true);
                shipSpriteBatch.setActivateState(true);
                ufoSpriteBatch.setActivateState(true);
            }

            if (KeyPress.Deactive_Sprites == keyPress)
            {
                alienSpriteBatch.setActivateState(false);
                shieldSpriteBatch.setActivateState(false);
                bombSpriteBatch.setActivateState(false);
                shipSpriteBatch.setActivateState(false);
                ufoSpriteBatch.setActivateState(false);
            }


        }

        public void setGameOver(bool _gameOver)
        {
            this.gameOver = _gameOver;
        }

        public static readonly int SCREEN_WIDTH = 672;
        public static readonly int SCREEN_HEIGHT = 768;

        public static int COL_0_POS = 86;

        public static int HORIZONTAL_SHIFT = 50;

        public static int NUM_ALIENS = 55;

        private SceneContext sceneContext;

    }
}