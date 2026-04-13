namespace WindowsSystem
{
    public abstract class SimpleWindow : Window<WindowModel>
    {
        protected virtual void Awake()
        {
            SetUp(new WindowModel());
        }
    }
}