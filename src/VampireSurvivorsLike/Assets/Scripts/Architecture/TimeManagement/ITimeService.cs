namespace Architecture.TimeManagement
{
    public interface ITimeService
    {
        public float UnscaledDeltaTime { get; }
        public float DeltaTime { get; }
    }
}