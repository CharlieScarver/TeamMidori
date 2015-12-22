using Microsoft.Xna.Framework;

namespace Midori.Interfaces
{
    public interface IUnit : IGameObject, IDestroyable, IMoveable, IMultiJumper, INeedToKnowWhereImFacing, IAnimatable, IUpdatable
    {
        bool IsMovingRight { get; }

        bool IsMovingLeft { get; }

        bool IsJumping { get; }

        bool IsFalling { get; }

        bool HasFreePathing { get; }

        Rectangle FuturePosition { get; }
    }
}
