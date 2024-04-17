using System;
using System.Diagnostics;

namespace SE456
{
    public class ScoreMan
    {
        private ScoreMan(Font _scoreFont, Font _highestScoreFont)
        {
            this.score = 0;
            this.highestScore = 0;
            this.scoreFont = _scoreFont;
            this.highestScoreFont = _highestScoreFont;
        }

        public static void Create(Font _scoreFont, Font _highestScoreFont)
        {
            if(poInstance == null)
            {
                poInstance = new ScoreMan(_scoreFont, _highestScoreFont);
            }
        }

        public static void AddScore(int _score)
        {
            ScoreMan scoreMan = privGetInstance();
            scoreMan.score = scoreMan.score + _score;

            //figure out how many zeros to put in front of the score
            string scoreString = scoreMan.score.ToString();
            int length = scoreString.Length;

            string zeros = "";
            switch (length)
            {
                case 1:
                    zeros = "0 0 0 ";
                    break;
                case 2:
                    zeros = "0 0 ";
                    break;
                case 3:
                    zeros = "0 ";
                    break;

                case 4:
                    zeros = "";
                    break;
            }

            //Using string class to add a " " in between all the characters in the score
            //for aesthetic reasons
            scoreMan.scoreFont.UpdateMessage(zeros + String.Join(" ", scoreString));
        }

        
        public static void UpdateHighestScore()
        {
            ScoreMan scoreMan = privGetInstance();

            if (scoreMan.score > scoreMan.highestScore)
            {
                scoreMan.highestScore = scoreMan.score;
            }

          
        }

        public static void resetScore()
        {
            ScoreMan scoreMan = privGetInstance();
            scoreMan.score = 0;
        }

        public static int GetScore()
        {
            ScoreMan scoreMan = privGetInstance();
            return scoreMan.score;
        }


        public static void PrintHighestScore(Font font)
        {
            ScoreMan scoreMan = privGetInstance();

            //figure out how many zeros to put in front of the score
            string scoreString = scoreMan.highestScore.ToString();
            int length = scoreString.Length;

            string zeros = "";
            switch (length)
            {
                case 1:
                    zeros = "0 0 0 ";
                    break;
                case 2:
                    zeros = "0 0 ";
                    break;
                case 3:
                    zeros = "0 ";
                    break;

                case 4:
                    zeros = "";
                    break;
            }

            font.UpdateMessage(zeros + String.Join(" ", scoreString));
        }

        public static void PrintScore(Font font)
        {
            ScoreMan scoreMan = privGetInstance();

            //figure out how many zeros to put in front of the score
            string scoreString = scoreMan.score.ToString();
            int length = scoreString.Length;

            string zeros = "";
            switch (length)
            {
                case 1:
                    zeros = "0 0 0 ";
                    break;
                case 2:
                    zeros = "0 0 ";
                    break;
                case 3:
                    zeros = "0 ";
                    break;

                case 4:
                    zeros = "";
                    break;
            }

            font.UpdateMessage(zeros + String.Join(" ", scoreString));
        }

        private void prettyPrintScore(Font font)
        {
            ScoreMan scoreMan = privGetInstance();

            //figure out how many zeros to put in front of the score
            string scoreString = scoreMan.score.ToString();
            int length = scoreString.Length;

            string zeros = "";
            switch (length)
            {
                case 1:
                    zeros = "0 0 0 ";
                    break;
                case 2:
                    zeros = "0 0";
                    break;
                case 3:
                    zeros = "0";
                    break;

                case 4:
                    zeros = "";
                    break;
            }

            //Using string class to add a " " in between all the characters in the score
            //for aesthetic reasons
            scoreMan.scoreFont.UpdateMessage(zeros + String.Join(" ", scoreString));
        }
        //------------------------------------
        // Private methods
        //------------------------------------
        private static ScoreMan privGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(poInstance != null);

            return poInstance;
        }

        private static ScoreMan poInstance;
        private int score;
        private int highestScore;

        private Font scoreFont;
        private Font highestScoreFont;
    }
}
