using System;
using com.mobiquityinc.domain;

namespace com.mobiquityinc.packer
{
    public sealed class TestCaseValidator
    {
        const decimal PACKAGE_MAX_WEIGHT = 100;
        const decimal THING_MAX_WEIGHT = 100;
        const decimal THING_MAX_PRICE = 100;
        const int THINGS_MAX_COUNT = 15;

        public void Validate(TestCase testCase)
        {
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
    }
}