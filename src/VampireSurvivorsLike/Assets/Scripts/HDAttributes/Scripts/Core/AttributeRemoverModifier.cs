namespace HDAttributes.Scripts.Core
{
    public class AttributeRemoverModifier : AttributeModifier<GameAttribute>
    {
        private float _delay;
        private float _passedTime;

        public AttributeRemoverModifier(float delay)
        {
            _delay = delay;
        }

        public override void OnUpdate(float deltaTime)
        {
            if (_passedTime >= _delay)
            {
                RemoveAttribute();
            }
            else
            {
                _passedTime += deltaTime;
            }
        }

        private void RemoveAttribute()
        {
            AttributeManager.Remove(Attribute.ID);
            Kill();
        }
    }
}
