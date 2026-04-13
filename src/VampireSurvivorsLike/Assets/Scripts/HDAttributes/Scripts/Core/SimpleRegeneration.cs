namespace HDAttributes.Scripts.Core
{
    public class SimpleRegeneration : AttributeModifier<GameAttribute<float>>
    {
        private float _speed;

        public SimpleRegeneration(float speed)
        {
            _speed = speed;
        }

        public override void OnUpdate(float deltaTime)
        {
            Attribute.SetValue(Attribute.Value + _speed * deltaTime);

            if (Attribute.MaxValue == Attribute.Value)
            {
                Kill();
            }
            else if (Attribute.MinValue == Attribute.Value)
            {
                Kill();
            }
        }
    }
}
