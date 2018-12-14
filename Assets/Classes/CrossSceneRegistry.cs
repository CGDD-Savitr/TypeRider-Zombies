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

		public static int[] PowerDurations
		{
			get
			{
				return powerDurations;
			}
			set
			{
				powerDurations = value;
			}
		}

        public static int WhichFloorType
        {
            get
            {
                return whichFloorType;
            }
            set
            {
                whichFloorType = value;
            }
        }

		private static IDifficulty difficulty = new NormalDifficulty();

        private static int playerScore = 0;
        private static int whichFloorType = 0;

        private static List<int> highScores = new List<int>();

        private static bool[] canUsePower = new bool[] { false, false, false };

        private static bool[] activatedPower = new bool[3];

		private static int[] powerDurations = new int[] { 5, 5, 10 };
	}
}