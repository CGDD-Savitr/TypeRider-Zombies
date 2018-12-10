using TypeRider.Assets.Interfaces;

namespace TypeRider.Assets.Classes
{
    public class NormalDifficulty : IDifficulty
    {
        public float Multiplier
        {
            get
            {
                return 1.0f;
            }
        }

        public string Name
        {
            get
            {
                return "NORMAL";
            }
        }

        public string File
        {
            get
            {
                return "med";
            }
        }

        public int ScoreThreshold
        {
            get
            {
                return 10000;
            }
        }

        public float InitialVelocity
        {
            get
            {
                return 1.0f;
            }
        }
    }
}