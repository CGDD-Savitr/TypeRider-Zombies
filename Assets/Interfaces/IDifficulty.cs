namespace TypeRider.Assets.Interfaces
{
    public interface IDifficulty
    {
        float Multiplier { get; }

        float InitialVelocity { get; }

        string Name { get; }

        string File { get; }        

        int ScoreThreshold { get; }
    }
}