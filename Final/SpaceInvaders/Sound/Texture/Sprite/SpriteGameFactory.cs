using System;
using System.Diagnostics;

namespace SE456
{
    public class SpriteGameFactory
    {
        static Azul.Color green = new Azul.Color(0.0f, 1.0f, 0.0f, 1.0f);
        static Azul.Color red = new Azul.Color(1.0f, 0.0f, 0.0f, 1.0f);
        public static SpriteGame Create(SpriteGame.Name _name)
        {
            switch (_name)
            {

                case SpriteGame.Name.Squid:

                    //add the Sprites to the SpriteGameMan
                    //Add both alien images to the ImageMan
                    //attach A image first to Sprite
                    //animation will flip between A & B later
                    spriteGame = SpriteGameMan.Add(SpriteGame.Name.Squid, Image.Name.SquidA,
                        0, 0, SQUID_WIDTH, SQUID_HEIGHT);
                    break;

                case SpriteGame.Name.Crab:
                    spriteGame = SpriteGameMan.Add(SpriteGame.Name.Crab, Image.Name.CrabA,
                        0, 0, CRAB_WIDTH, CRAB_HEIGHT);
                    break;

  
                case SpriteGame.Name.Octopus:
                    spriteGame = SpriteGameMan.Add(SpriteGame.Name.Octopus, Image.Name.OctopusA,
                        0, 0, OCTOPUS_WIDTH, OCTOPUS_HEIGHT);
                    break;

                case SpriteGame.Name.Octopus_Green:
                    spriteGame = SpriteGameMan.Add(SpriteGame.Name.Octopus_Green, Image.Name.OctopusA,
                      0, 0, OCTOPUS_WIDTH, OCTOPUS_HEIGHT);
                    spriteGame.SwapColor(green);
                    break;

                case SpriteGame.Name.Alien_Splat:
                    spriteGame = SpriteGameMan.Add(SpriteGame.Name.Alien_Splat, Image.Name.Alien_Splat,
                       0, 0, OCTOPUS_WIDTH, OCTOPUS_HEIGHT);
                    break;

                case SpriteGame.Name.Ship:
                    spriteGame = SpriteGameMan.Add(SpriteGame.Name.Ship, Image.Name.Ship,
                        SHIP_START_X, SHIP_START_Y, SHIP_WIDTH, SHIP_HEIGHT);
                    spriteGame.SwapColor(green);
                    break;

                case SpriteGame.Name.Ship_Splat_1:
                    spriteGame = SpriteGameMan.Add(SpriteGame.Name.Ship_Splat_1, Image.Name.Ship_Splat_1,
                        SHIP_START_X, SHIP_START_Y, SHIP_WIDTH, SHIP_HEIGHT);
                    spriteGame.SwapColor(green);
                    break;

                case SpriteGame.Name.Ship_Splat_2:
                    spriteGame = SpriteGameMan.Add(SpriteGame.Name.Ship_Splat_2, Image.Name.Ship_Splat_2,
                        SHIP_START_X, SHIP_START_Y, SHIP_WIDTH, SHIP_HEIGHT);
                    spriteGame.SwapColor(green);
                    break;

                case SpriteGame.Name.Missile:
                    spriteGame = SpriteGameMan.Add(SpriteGame.Name.Missile, Image.Name.Missile,
                        0, 0, MISSILE_WIDTH, MISSILE_HEIGHT);
                    break;

                case SpriteGame.Name.Brick:
                    spriteGame = SpriteGameMan.Add(SpriteGame.Name.Brick, Image.Name.Brick,
                        0, 0, BRICK_WIDTH, BRICK_HEIGHT);
                    break;

                case SpriteGame.Name.Brick_LeftTop0:
                    spriteGame = SpriteGameMan.Add(SpriteGame.Name.Brick_LeftTop0, Image.Name.BrickLeft_Top0,
                        0, 0, BRICK_WIDTH, BRICK_HEIGHT);
                    break;

                case SpriteGame.Name.Brick_LeftBottom:
                    spriteGame = SpriteGameMan.Add(SpriteGame.Name.Brick_LeftBottom, Image.Name.BrickLeft_Bottom,
                        0, 0, BRICK_WIDTH, BRICK_HEIGHT);
                    break;

                case SpriteGame.Name.Brick_RightTop0:
                    spriteGame = SpriteGameMan.Add(SpriteGame.Name.Brick_RightTop0, Image.Name.BrickRight_Top0,
                        0, 0, BRICK_WIDTH, BRICK_HEIGHT);
                    break;

                case SpriteGame.Name.Brick_RightBottom:
                    spriteGame = SpriteGameMan.Add(SpriteGame.Name.Brick_RightBottom, Image.Name.BrickRight_Bottom,
                        0, 0, BRICK_WIDTH, BRICK_HEIGHT);
                    break;

                case SpriteGame.Name.SquigglyShotA:
                    spriteGame = SpriteGameMan.Add(SpriteGame.Name.SquigglyShotA, Image.Name.SquigglyShotA,
                        0, 0, BOMB_WIDTH, BOMB_HEIGHT);
                    break;

                case SpriteGame.Name.SquigglyShotB:
                    spriteGame = SpriteGameMan.Add(SpriteGame.Name.SquigglyShotB, Image.Name.SquigglyShotB,
                        0, 0, BOMB_WIDTH, BOMB_HEIGHT);
                    break;

                case SpriteGame.Name.SquigglyShotC:
                    SpriteGameMan.Add(SpriteGame.Name.SquigglyShotC, Image.Name.SquigglyShotC,
                        0, 0, BOMB_WIDTH, BOMB_HEIGHT);
                    break;

                case SpriteGame.Name.SquigglyShotD:
                    spriteGame = SpriteGameMan.Add(SpriteGame.Name.SquigglyShotD, Image.Name.SquigglyShotD,
                        0, 0, BOMB_WIDTH, BOMB_HEIGHT);
                    break;

                case SpriteGame.Name.PlungerShotA:
                    spriteGame = SpriteGameMan.Add(SpriteGame.Name.PlungerShotA, Image.Name.PlungerShotA,
                        0, 0, BOMB_WIDTH, BOMB_HEIGHT);
                    break;

                case SpriteGame.Name.PlungerShotB:
                    spriteGame = SpriteGameMan.Add(SpriteGame.Name.PlungerShotB, Image.Name.PlungerShotB,
                        0, 0, BOMB_WIDTH, BOMB_HEIGHT);
                    break;

                case SpriteGame.Name.PlungerShotC:
                    spriteGame = SpriteGameMan.Add(SpriteGame.Name.PlungerShotC, Image.Name.PlungerShotC,
                        0, 0, BOMB_WIDTH, BOMB_HEIGHT);
                    break;

                case SpriteGame.Name.PlungerShotD:
                    spriteGame = SpriteGameMan.Add(SpriteGame.Name.PlungerShotD, Image.Name.PlungerShotD,
                        0, 0, BOMB_WIDTH, BOMB_HEIGHT);
                    break;

                case SpriteGame.Name.RollingShotA:
                    spriteGame = SpriteGameMan.Add(SpriteGame.Name.RollingShotA, Image.Name.RollingShotA,
                        0, 0, BOMB_WIDTH, BOMB_HEIGHT);
                    break;

                case SpriteGame.Name.RollingShotB:
                    spriteGame = SpriteGameMan.Add(SpriteGame.Name.RollingShotB, Image.Name.RollingShotB,
                        0, 0, BOMB_WIDTH, BOMB_HEIGHT);
                    break;

                case SpriteGame.Name.RollingShotC:
                    spriteGame = SpriteGameMan.Add(SpriteGame.Name.RollingShotC, Image.Name.RollingShotC,
                        0, 0, BOMB_WIDTH, BOMB_HEIGHT);
                    break;

                case SpriteGame.Name.RollingShotD:
                    spriteGame = SpriteGameMan.Add(SpriteGame.Name.RollingShotD, Image.Name.RollingShotD,
                        0, 0, BOMB_WIDTH, BOMB_HEIGHT);
                    break;

                case SpriteGame.Name.SquigglyShotA_Red:
                    spriteGame = SpriteGameMan.Add(SpriteGame.Name.SquigglyShotA_Red, Image.Name.SquigglyShotA,
                        0, 0, BOMB_WIDTH, BOMB_HEIGHT);
                    spriteGame.SwapColor(red);
                    break;

                case SpriteGame.Name.SquigglyShotB_Red:
                    spriteGame = SpriteGameMan.Add(SpriteGame.Name.SquigglyShotB_Red, Image.Name.SquigglyShotB,
                        0, 0, BOMB_WIDTH, BOMB_HEIGHT);
                    spriteGame.SwapColor(red);
                    break;

                case SpriteGame.Name.SquigglyShotC_Red:
                    SpriteGameMan.Add(SpriteGame.Name.SquigglyShotC_Red, Image.Name.SquigglyShotC,
                        0, 0, BOMB_WIDTH, BOMB_HEIGHT);
                    spriteGame.SwapColor(red);
                    break;

                case SpriteGame.Name.SquigglyShotD_Red:
                    spriteGame = SpriteGameMan.Add(SpriteGame.Name.SquigglyShotD_Red, Image.Name.SquigglyShotD,
                        0, 0, BOMB_WIDTH, BOMB_HEIGHT);
                    spriteGame.SwapColor(red);
                    break;


                case SpriteGame.Name.PlungerShotA_Red:
                    spriteGame = SpriteGameMan.Add(SpriteGame.Name.PlungerShotA_Red, Image.Name.PlungerShotA,
                        0, 0, BOMB_WIDTH, BOMB_HEIGHT);
                    spriteGame.SwapColor(red);
                    break;

                case SpriteGame.Name.PlungerShotB_Red:
                    spriteGame = SpriteGameMan.Add(SpriteGame.Name.PlungerShotB_Red, Image.Name.PlungerShotB,
                        0, 0, BOMB_WIDTH, BOMB_HEIGHT);
                    spriteGame.SwapColor(red);
                    break;

                case SpriteGame.Name.PlungerShotC_Red:
                    spriteGame = SpriteGameMan.Add(SpriteGame.Name.PlungerShotC_Red, Image.Name.PlungerShotC,
                        0, 0, BOMB_WIDTH, BOMB_HEIGHT);
                    spriteGame.SwapColor(red);
                    break;

                case SpriteGame.Name.PlungerShotD_Red:
                    spriteGame = SpriteGameMan.Add(SpriteGame.Name.PlungerShotD_Red, Image.Name.PlungerShotD,
                        0, 0, BOMB_WIDTH, BOMB_HEIGHT);
                    spriteGame.SwapColor(red);
                    break;

                case SpriteGame.Name.RollingShotA_Red:
                    spriteGame = SpriteGameMan.Add(SpriteGame.Name.RollingShotA_Red, Image.Name.RollingShotA,
                        0, 0, BOMB_WIDTH, BOMB_HEIGHT);
                    spriteGame.SwapColor(red);
                    break;

                case SpriteGame.Name.RollingShotB_Red:
                    spriteGame = SpriteGameMan.Add(SpriteGame.Name.RollingShotB_Red, Image.Name.RollingShotB,
                        0, 0, BOMB_WIDTH, BOMB_HEIGHT);
                    spriteGame.SwapColor(red);
                    break;

                case SpriteGame.Name.RollingShotC_Red:
                    spriteGame = SpriteGameMan.Add(SpriteGame.Name.RollingShotC_Red, Image.Name.RollingShotC,
                        0, 0, BOMB_WIDTH, BOMB_HEIGHT);
                    spriteGame.SwapColor(red);
                    break;

                case SpriteGame.Name.RollingShotD_Red:
                    spriteGame = SpriteGameMan.Add(SpriteGame.Name.RollingShotD_Red, Image.Name.RollingShotD,
                        0, 0, BOMB_WIDTH, BOMB_HEIGHT);
                    spriteGame.SwapColor(red);
                    break;

                case SpriteGame.Name.Floor:
                    spriteGame = SpriteGameMan.Add(SpriteGame.Name.Floor, Image.Name.Floor,
                        0, 0, FLOOR_WIDTH, FLOOR_HEIGHT);
                    spriteGame.SwapColor(green);
                    break;

                case SpriteGame.Name.UFO:
                    spriteGame = SpriteGameMan.Add(SpriteGame.Name.UFO, Image.Name.UFO,
                        0, 0, UFO_WIDTH, UFO_HEIGHT);
                    spriteGame.SwapColor(red);
                    break;

                case SpriteGame.Name.UFO_Splat:
                    spriteGame = SpriteGameMan.Add(SpriteGame.Name.UFO_Splat, Image.Name.UFO_Splat,
                        0, 0, UFO_WIDTH, UFO_HEIGHT);
                    spriteGame.SwapColor(red);
                    break;

                case SpriteGame.Name.BombSplat:
                    SpriteGameMan.Add(SpriteGame.Name.BombSplat, Image.Name.BombSplat,
                        0, 0, BOMB_WIDTH, BOMB_HEIGHT);
                    break;

                case SpriteGame.Name.BombSplat_Green:
                    spriteGame = SpriteGameMan.Add(SpriteGame.Name.BombSplat_Green, Image.Name.BombSplat,
                        0, 0, BRICK_WIDTH, BRICK_HEIGHT);
                    spriteGame.SwapColor(green);
                    break;

            }
            return spriteGame;
        }

        private static SpriteGame spriteGame;

        private static readonly int SQUID_WIDTH = 24;
        private static readonly int SQUID_HEIGHT = 25;

        private static readonly int CRAB_WIDTH = 28;
        private static readonly int CRAB_HEIGHT = 25;

        private static readonly int OCTOPUS_WIDTH = 36;
        private static readonly int OCTOPUS_HEIGHT = 25;

        private static readonly int SHIP_WIDTH = 39;
        private static readonly int SHIP_HEIGHT = 24;
        private static readonly int SHIP_START_X = 336;
        private static readonly int SHIP_START_Y = 100;

        private static readonly int MISSILE_WIDTH = 3;
        private static readonly int MISSILE_HEIGHT = 12;

        private static readonly int BRICK_WIDTH = 11;
        private static readonly int BRICK_HEIGHT = 11;

        private static readonly int BOMB_WIDTH = 9;
        private static readonly int BOMB_HEIGHT = 21;

        private static readonly int FLOOR_WIDTH = 673;
        private static readonly int FLOOR_HEIGHT = 3;

        private static readonly int UFO_WIDTH = 48;
        private static readonly int UFO_HEIGHT = 24;

    }   
        
}
