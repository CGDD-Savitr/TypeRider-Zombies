using TypeRider.Assets.Interfaces;

namespace TypeRider.Assets.Classes
{
    public class HardDifficulty : IDifficulty
    {
        public float Multiplier
        {
            get
            {
                return 1.5f;
            }
        }

        public string Name
        {
            get
            {
                return "HARD";
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
                return 40000;
            }
        }
        
        public float InitialVelocity
        {
            get
            {
                return 1.5f;
            }
        }
    }
}