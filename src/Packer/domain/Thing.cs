namespace com.mobiquityinc.domain
{
    public sealed class Thing
    {
        public Thing(uint index, decimal weight, decimal cost)
        {
            Index = index;
            Weight = weight;
            Cost = cost;
        }

        public uint Index { get; }
        public decimal Weight { get; }
        public decimal Cost { get; }
    }
}