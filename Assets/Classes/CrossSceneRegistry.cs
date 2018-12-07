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

        private static float difficulty = 1f;

        private static int playerScore = 0;
    }
}