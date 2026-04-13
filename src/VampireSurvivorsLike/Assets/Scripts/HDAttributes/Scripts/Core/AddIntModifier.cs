namespace HDAttributes.Scripts.Core
{
    public class AddIntModifier : AttributeModifier<GameAttribute<int>>
    {
        private int _value;

        public AddIntModifier(int value)
        {
            _value = value;
        }

        public override void OnApply()
        {
            Attribute.SetValue(Attribute.Value + _value);
            Kill();
        }
    }
}
