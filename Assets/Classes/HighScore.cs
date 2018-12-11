using System;

namespace TypeRider.Assets.Classes
{
    [Serializable]
    public class HighScore
    {
        public int Score { get; set; }
        public DateTime Timestamp { get; set; }

        public override string ToString()
        {
            return Timestamp + ": " + Score;
        }
    }
}