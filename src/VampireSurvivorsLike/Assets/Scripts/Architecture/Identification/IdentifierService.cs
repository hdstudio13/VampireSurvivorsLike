namespace Architecture.Identification
{
    public class IdentifierService : IIdentifierService
    {
        private uint _counter = 0;
        
        public uint Next() => _counter++;
    }
}