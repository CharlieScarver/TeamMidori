namespace Midori.Interfaces
{
    public interface IPlayableCharacter : IUnit, IAnimatableIdle, IAnimatableFalling, IAnimatableMovable, IAnimatableJumper
    {
        int ComboStageCounter { get; set; }
    }
}
