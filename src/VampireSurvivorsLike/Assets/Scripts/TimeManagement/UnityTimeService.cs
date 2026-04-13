namespace TimeManagement
{
    public class UnityTimeService : ITimeService
    {
        public float UnscaledDeltaTime => UnityEngine.Time.unscaledDeltaTime;
        public float DeltaTime => UnityEngine.Time.deltaTime;
    }
}