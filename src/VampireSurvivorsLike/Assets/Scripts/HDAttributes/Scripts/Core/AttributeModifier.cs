using System;

namespace HDAttributes.Scripts.Core
{
    public abstract class AttributeModifier
    {
        public abstract void InternalOnApply(IAttributeManager manager, GameAttribute attribute);
        public abstract void InternalOnRemove();
        public abstract void InternalOnUpdate(float deltaTime);

        public Action<AttributeModifier> OnKilled;

        protected void Kill()
        {
            OnKilled?.Invoke(this);
        }
    }

    public class AttributeModifier<TAttribute> : AttributeModifier where TAttribute : GameAttribute
    {
        protected IAttributeManager AttributeManager;
        protected TAttribute Attribute;

        public override void InternalOnApply(IAttributeManager manager, GameAttribute attribute)
        {
            if (attribute is TAttribute aux)
            {
                AttributeManager = manager;
                Attribute = aux;
                OnApply();
            }
            else
            {
                throw new System.InvalidCastException($"Attribute isn't a {typeof(TAttribute)}");
            }
        }

        public override void InternalOnRemove()
        {
            OnRemove();
        }

        public override void InternalOnUpdate(float deltaTime)
        {
            OnUpdate(deltaTime);
        }

        public virtual void OnApply()
        {

        }

        public virtual void OnRemove()
        {

        }


        public virtual void OnUpdate(float deltaTime)
        {

        }
    }
}
