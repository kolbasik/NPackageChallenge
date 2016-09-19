using System.Collections.Generic;
using System.Linq;
using com.mobiquityinc.domain;

namespace com.mobiquityinc.packer
{
    public sealed class TestCasePacker
    {
        /// <summary>
        ///     Your goal is to determine which things to put into the package so that the
        ///     total weight is less than or equal to the package limit and the total cost is
        ///     as large as possible. You would prefer to send a package which weights less
        ///     in case there is more than one package with the same price.
        /// </summary>
        /// <returns>The package with things.</returns>
        public Package Pack(TestCase testCase)
        {
            var packages = new List<Package>();
            foreach (var thing in testCase.Things)
            {
                var subset = new List<Package> { new Package { thing } };
                subset.AddRange(packages.Select(package => new Package { package.Things, thing }));
                subset.RemoveAll(package => package.Weight > testCase.MaxPackageWeight);
                packages.AddRange(subset);
            }

            var optimal = packages
                .OrderByDescending(package => package.Cost)
                .ThenBy(package => package.Weight)
                .FirstOrDefault() ?? new Package();

            return optimal;
        }
    }
}