namespace Gameplay.Features.EntityDestroy
{
    public static class GameEntityDestroyExtensions
    {
        public static GameEntity SetAlive(this GameEntity gameEntity)
        {
            gameEntity.isDestroyed = false;
            gameEntity.isDestroying = false;
            gameEntity.isAlive = true;
            return gameEntity;
        }

        public static GameEntity SetDead(this GameEntity gameEntity)
        {
            if (!gameEntity.isDestroyed)
                gameEntity.isDestroying = true;
            gameEntity.isAlive = false;
            return gameEntity;
        }
    }
}