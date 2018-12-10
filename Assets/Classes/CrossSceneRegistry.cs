using System.Collections.Generic;

namespace TypeRider.Assets.Classes
{
    public static class CrossSceneRegistry
    {
        public static float Difficulty
        {
            get
            {
                return difficulty;
            }
            set
            {
                difficulty = value;
            }
        }

        public static int PlayerScore 
        { 
            get
            {
                return playerScore;
            } 
            set
            {
                playerScore = value;
            } 
        }

        public static List<int> HighScores 
        { 
            get
            {
                return highScores;
            } 
            set
            {
                highScores = value;
            } 
        }

        private static float difficulty = 1f;

        private static int playerScore = 0;

        private static List<int> highScores = new List<int>();
    }
}