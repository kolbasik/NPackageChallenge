using System.Collections;
using System.Collections.Generic;

namespace com.mobiquityinc.domain
{
    public sealed class Package : IEnumerable<Thing>
    {
        private readonly List<Thing> things;

        public Package()
        {
            things = new List<Thing>();
        }

        public decimal Weight { get; private set; }
        public decimal Cost { get; private set; }
        public IReadOnlyCollection<Thing> Things => things.AsReadOnly();

        public void Add(IEnumerable<Thing> things)
        {
            foreach (var thing in things)
            {
                Add(thing);
            }
        }

        public void Add(Thing thing)
        {
            things.Add(thing);
            Weight += thing.Weight;
            Cost += thing.Cost;
        }

        IEnumerator<Thing> IEnumerable<Thing>.GetEnumerator() => things.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => things.GetEnumerator();
    }
}