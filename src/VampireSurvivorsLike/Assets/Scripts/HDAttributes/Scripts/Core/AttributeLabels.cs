namespace HDAttributes.Scripts.Core
{
    public interface IGameAttributeLabel { }

// black, white and gray
    public interface IPositiveAttribute : IGameAttributeLabel { }
    public interface INegativeAttribute : IGameAttributeLabel { }
    public interface INeutralAttribute : IGameAttributeLabel { }
}