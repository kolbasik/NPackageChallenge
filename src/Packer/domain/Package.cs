using System.Collections.Generic;

namespace com.mobiquityinc.domain
{
    public sealed class Package
    {
        public Package(List<Thing> things)
        {
            Things = things;
        }

        public List<Thing> Things { get; }
    }
}