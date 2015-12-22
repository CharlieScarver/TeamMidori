namespace Midori.Interfaces
{
    public interface IRangedAttacker
    {
        int DamageRanged { get; }

        bool IsAttackingRanged { get; }        
    }
}
