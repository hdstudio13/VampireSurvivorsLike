namespace Gameplay.Features.Effects.Factory
{
    public interface IEffectsFactory
    {
        GameEntity CreateDamage(float damage, uint targetId, uint ownerId);
    }
}