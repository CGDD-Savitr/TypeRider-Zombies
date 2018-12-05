using UnityEngine.UI;

namespace TypeRider.Assets.Classes
{
    public class Keyword
    {
        public string Key { get; set; }

        public Text Text { get; set; }

        public Direction Value { get; set; }

    }
    public enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }
}