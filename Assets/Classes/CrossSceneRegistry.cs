using System.Collections.Generic;
using TypeRider.Assets.Interfaces;

namespace TypeRider.Assets.Classes
{
    public static class CrossSceneRegistry
    {
        public static IDifficulty Difficulty
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

        public static bool[] CanUsePower
        {
            get
            {
                return canUsePower;
            }
            set
            {
                canUsePower = value;
            }
        }

        public static bool[] ActivatedPower
        {
            get
            {
                return activatedPower;
            }
            set
            {
                activatedPower = value;
            }
        }

        private static IDifficulty difficulty = new NormalDifficulty();

        private static int playerScore = 0;

        private static List<int> highScores = new List<int>();

        private static bool[] canUsePower = new bool[] { false, false, false };

        private static bool[] activatedPower = new bool[3];
    }
}