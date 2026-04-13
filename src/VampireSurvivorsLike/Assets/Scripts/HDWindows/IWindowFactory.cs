namespace WindowsSystem
{
    public interface IWindowFactory
    {
        T Create<T, Y>(Y model) where T : Window<Y> where Y : WindowModel, new();
        T Create<T>() where T : SimpleWindow;
    }
}