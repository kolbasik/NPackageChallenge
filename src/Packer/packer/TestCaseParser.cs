using System;
using System.Globalization;
using System.Linq;
using com.mobiquityinc.domain;

namespace com.mobiquityinc.packer
{
    public sealed class TestCaseParser
    {
        public TestCase Parse(string text)
        {
            try
            {
                var parts = text.Split(':').Select(x => x.Trim()).ToArray();
                var maxPackageWeight = parts[0];
                var things = parts[1].Split(' ').Select(x => x.Trim(' ', '(', ')').Split(','));
                return new TestCase(decimal.Parse(maxPackageWeight),
                    things.Select(x => new Thing(
                        uint.Parse(x[0], CultureInfo.InvariantCulture),
                        decimal.Parse(x[1], CultureInfo.InvariantCulture),
                        decimal.Parse(x[2].TrimStart('€'), CultureInfo.InvariantCulture))));
            }
            catch (Exception ex)
            {
                throw new FormatException($@"The test case has incorrect format: {text}", ex);
            }
        }
    }
}