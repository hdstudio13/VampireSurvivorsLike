using Entitas;

namespace Gameplay.Input.Systems
{
    public class InitializeInputSystem : IInitializeSystem
    {
        private readonly GameContext _context;

        public InitializeInputSystem(GameContext context)
        {
            _context = context;
        }
        
        public void Initialize()
        {
            _context.CreateEntity()
                .isInput = true;
        }
    }
}