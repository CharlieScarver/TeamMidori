namespace Midori.Interfaces
{
    public interface IPlayableCharacter : IAnimatableIdle, IAnimatableFalling, IAnimatableMovable, IAnimatableJumper
    {
        int ComboStageCounter { get; set; }
    }
}
