namespace HDAttributes.Scripts.Core
{
    public class AddFloatModifier : AttributeModifier<GameAttribute<float>>
    {
        private float _value;
        private bool _isPersistent;

        public AddFloatModifier(float value, bool isPersistent)
        {
            _value = value;
            _isPersistent = isPersistent;
        }

        public override void OnApply()
        {
            Attribute.SetValue(Attribute.Value + _value);

            if (_isPersistent)
                Kill();
        }

        public override void OnRemove()
        {
            if (!_isPersistent)
                Attribute.SetValue(Attribute.Value - _value);

            base.OnRemove();
        }
    }
}
