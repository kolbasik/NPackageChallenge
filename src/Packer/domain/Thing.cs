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

        public override string ToString()
        {
            return $"{{ Index: {Index}, Weight: {Weight}, Cost: {Cost} }}";
        }
    }
}