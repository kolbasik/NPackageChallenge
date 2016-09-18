using System.Collections.Generic;

namespace com.mobiquityinc.domain
{
    public sealed class TestCase
    {
        public TestCase(decimal maxPackageWeight, IEnumerable<Thing> things)
        {
            MaxPackageWeight = maxPackageWeight;
            Things = new List<Thing>(things);
        }

        public decimal MaxPackageWeight { get; }
        public List<Thing> Things { get; }
    }
}