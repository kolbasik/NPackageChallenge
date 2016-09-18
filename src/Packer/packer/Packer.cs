using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using com.mobiquityinc.domain;
using com.mobiquityinc.exception;
using com.mobiquityinc.extensions;

namespace com.mobiquityinc.packer
{
    public static class Packer
    {
        public static string Pack(string filePath)
        {
            try
            {
                return Pack(File.ReadAllLines(filePath));
            }
            catch (Exception ex)
            {
                throw new APIException(@"An error occurred during packing.", ex);
            }
        }

        public static string Pack(IEnumerable<string> testCases)
        {
            var results = testCases
                .Map(Parse)
                .ForEach(Validate)
                .Map(Pack)
                .Map(ToString);
            return string.Join(Environment.NewLine, results);
        }

        public static TestCase Parse(string text)
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

        public static void Validate(TestCase testCase)
        {
            const decimal PACKAGE_MAX_WEIGHT = 100;
            const decimal THING_MAX_WEIGHT = 100;
            const decimal THING_MAX_PRICE = 100;
            const int THINGS_MAX_COUNT = 15;

            if (testCase.MaxPackageWeight > PACKAGE_MAX_WEIGHT)
            {
                throw new ArgumentException($@"Package limit should be less then {PACKAGE_MAX_WEIGHT}.");
            }
            if (testCase.Things.Count > THINGS_MAX_COUNT)
            {
                throw new ArgumentException($@"Things count in one test case should be less then {THINGS_MAX_COUNT}.");
            }
            foreach (var thing in testCase.Things)
            {
                if (thing.Weight > THING_MAX_WEIGHT)
                {
                    throw new ArgumentException($@"Thing weight should be less then {THING_MAX_WEIGHT}.");
                }
                if (thing.Cost > THING_MAX_PRICE)
                {
                    throw new ArgumentException($@"Thing price should be less then {THING_MAX_PRICE}.");
                }
            }
        }

        /// <summary>
        ///     Your goal is to determine which things to put into the package so that the
        ///     total weight is less than or equal to the package limit and the total cost is
        ///     as large as possible. You would prefer to send a package which weights less
        ///     in case there is more than one package with the same price.
        /// </summary>
        /// <returns>The package with things.</returns>
        private static Package Pack(TestCase testCase)
        {
            var combinations = new List<List<Thing>>();
            foreach (var thing in testCase.Things)
            {
                var subset = new List<List<Thing>> {new List<Thing> {thing}};
                subset.AddRange(combinations.Select(comb => new List<Thing>(comb) {thing}));
                combinations.AddRange(subset);
            }

            combinations.RemoveAll(comb => comb.Sum(thing => thing.Weight) > testCase.MaxPackageWeight);
            var optimal = combinations
                .OrderByDescending(comb => comb.Sum(thing => thing.Cost))
                .ThenBy(comb => comb.Sum(thing => thing.Weight))
                .FirstOrDefault() ?? Enumerable.Empty<Thing>();

            return new Package(optimal);
        }

        public static string ToString(Package package)
        {
            return package.Things.Count == 0 ? @"-" : string.Join(@",", package.Things.Select(thing => thing.Index));
        }
    }
}