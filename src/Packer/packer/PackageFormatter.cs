using System.Linq;
using com.mobiquityinc.domain;

namespace com.mobiquityinc.packer
{
    public sealed class PackageFormatter
    {
        public string Format(Package package)
        {
            return package?.Things.Count == 0 ? @"-" : string.Join(@",", package.Things.Select(thing => thing.Index));
        }
    }
}