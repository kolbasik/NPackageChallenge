using System;
using System.Collections.Generic;
using System.Linq;
using com.mobiquityinc.domain;
using com.mobiquityinc.packer;
using Xunit;

namespace com.mobiquityinc
{
    public sealed class TestCaseValidatorTests
    {
        public sealed class Validate
        {
            private readonly TestCaseValidator validator;

            public Validate()
            {
                validator = new TestCaseValidator();
            }

            [Fact]
            public void Should_тще_throw_an_exception_if_test_case_is_valid()
            {
                // arrange
                var testCase = new TestCase(0m, new List<Thing>());

                // act & assert
                validator.Validate(testCase);
            }

            [Fact]
            public void Should_throw_an_exception_if_max_package_weight_is_too_big()
            {
                // arrange
                var maxPackageWeight = decimal.MaxValue;
                var testCase = new TestCase(maxPackageWeight, new List<Thing>());

                // act & assert
                Assert.Throws<ArgumentException>(() => validator.Validate(testCase));
            }

            [Fact]
            public void Should_throw_an_exception_if_things_are_too_many()
            {
                // arrange
                var things = 1000;
                var testCase = new TestCase(0m, Enumerable.Repeat(new Thing(0, 0m, 0m), things));

                // act & assert
                Assert.Throws<ArgumentException>(() => validator.Validate(testCase));
            }

            [Fact]
            public void Should_throw_an_exception_if_thing_weight_is_too_big()
            {
                // arrange
                var weight = decimal.MaxValue;
                var testCase = new TestCase(0m, new[] { new Thing(1, weight, 0m) });

                // act & assert
                Assert.Throws<ArgumentException>(() => validator.Validate(testCase));
            }

            [Fact]
            public void Should_throw_an_exception_if_the_price_is_to_much()
            {
                // arrange
                var price = decimal.MaxValue;
                var testCase = new TestCase(0m, new[] { new Thing(1, 0m, price) });

                // act & assert
                Assert.Throws<ArgumentException>(() => validator.Validate(testCase));
            }
        }
    }
}