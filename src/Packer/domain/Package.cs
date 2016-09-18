using System.Collections.Generic;

namespace com.mobiquityinc.domain
{
    public sealed class Package
    {
        public Package(IEnumerable<Thing> things)
        {
            Things = new List<Thing>(things);
        }

        public List<Thing> Things { get; }
    }
}