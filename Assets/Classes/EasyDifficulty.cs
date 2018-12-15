using TypeRider.Assets.Interfaces;

namespace TypeRider.Assets.Classes
{
    public class EasyDifficulty : IDifficulty
    {
        public float Multiplier
        {
            get
            {
                return .25f;
            }
        }

        public string Name
        {
            get
            {
                return "EASY";
            }
        }

        public string File
        {
            get
            {
                return "small";
            }
        }

        public int ScoreThreshold
        {
            get
            {
                return 1250;
            }
        }

        public float InitialVelocity
        {
            get
            {
                return .75f;
            }
        }
    }
}