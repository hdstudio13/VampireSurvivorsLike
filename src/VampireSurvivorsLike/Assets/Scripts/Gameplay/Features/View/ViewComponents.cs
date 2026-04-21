using Architecture.EntityViews.GameContext;
using Entitas;
using UnityEngine;

namespace Gameplay.Features.View
{
    [Game] public class ViewPath : IComponent { public string Value; }
    [Game] public class View : IComponent { public GameEntityView Value; }
}