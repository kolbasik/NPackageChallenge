using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using com.mobiquityinc.domain;
using com.mobiquityinc.packer;
using Xunit;

namespace com.mobiquityinc
{
    public sealed class PackerTests
    {
        public sealed class Pack : IDisposable
        {
            private readonly string tempFileName;

            public Pack()
            {
                tempFileName = Path.GetTempFileName();
            }

            public void Dispose()
            {
                if (File.Exists(tempFileName))
                {
                    File.Delete(tempFileName);
                }
            }

            [Fact]
            public void Should_read_test_cases_from_defined_file_and_return_the_result_as_string()
            {
                // prerequisite
                const string input = @"81 : (1,53.38,€45) (2,88.62,€98) (3,78.48,€3) (4,72.30,€76) (5,30.18,€9) (6,46.34,€48)
8 : (1,15.3,€34)
75 : (1,85.31,€29) (2,14.55,€74) (3,3.98,€16) (4,26.24,€55) (5,63.69,€52) (6,76.25,€75) (7,60.02,€74) (8,93.18,€35) (9,89.95,€78)
56 : (1,90.72,€13) (2,33.80,€40) (3,43.15,€10) (4,37.97,€16) (5,46.81,€36) (6,48.77,€79) (7,81.80,€45) (8,19.36,€79) (9,6.76,€64)";
                const string output = @"4
-
2,7
8,9";
                // arrange
                File.WriteAllText(tempFileName, input);

                // act
                var actual = Packer.Pack(tempFileName);

                // assert
                Assert.Equal(output, actual);
            }
        }

        public sealed class Parse
        {
            [Fact]
            public void Should_parse_test_case_from_a_string()
            {
                // arrange
                var expected = new TestCase(81, new[] {new Thing(1, 53.38m, 45), new Thing(2, 88.62m, 98), new Thing(3, 78.48m, 3)});

                // act
                var actual = Packer.Parse(@"81 : (1,53.38,€45) (2,88.62,€98) (3,78.48,€3)");

                // assert
                Assert.Equal(expected.ToString(), actual.ToString());
            }
        }

        public sealed class Validate
        {
            [Fact]
            public void Should_тще_throw_an_exception_if_test_case_is_valid()
            {
                // arrange
                var testCase = new TestCase(0m, new List<Thing>());

                // act & assert
                Packer.Validate(testCase);
            }

            [Fact]
            public void Should_throw_an_exception_if_max_package_weight_is_too_big()
            {
                // arrange
                var maxPackageWeight = decimal.MaxValue;
                var testCase = new TestCase(maxPackageWeight, new List<Thing>());

                // act & assert
                Assert.Throws<ArgumentException>(() => Packer.Validate(testCase));
            }

            [Fact]
            public void Should_throw_an_exception_if_things_are_too_many()
            {
                // arrange
                var things = 1000;
                var testCase = new TestCase(0m, Enumerable.Repeat(new Thing(0, 0m, 0m), things));

                // act & assert
                Assert.Throws<ArgumentException>(() => Packer.Validate(testCase));
            }

            [Fact]
            public void Should_throw_an_exception_if_thing_weight_is_too_big()
            {
                // arrange
                var weight = decimal.MaxValue;
                var testCase = new TestCase(0m, new [] { new Thing(1, weight, 0m) });

                // act & assert
                Assert.Throws<ArgumentException>(() => Packer.Validate(testCase));
            }

            [Fact]
            public void Should_throw_an_exception_if_the_price_is_to_much()
            {
                // arrange
                var price = decimal.MaxValue;
                var testCase = new TestCase(0m, new[] { new Thing(1, 0m, price) });

                // act & assert
                Assert.Throws<ArgumentException>(() => Packer.Validate(testCase));
            }
        }

        public sealed class Display
        {
            [Fact]
            public void Should_return_dash_if_none()
            {
                // arrange
                var things = new List<Thing>();

                // act
                var actual = Packer.Display(new Package(things));

                // assert
                Assert.Equal(@"-", actual);
            }

            [Fact]
            public void Should_return_a_set_of_index_numbers_separated_by_comma()
            {
                // arrange
                var things = new List<Thing>()
                {
                    new Thing(3, decimal.MinValue, decimal.MinValue),
                    new Thing(5, decimal.Zero, decimal.Zero),
                    new Thing(8, decimal.MaxValue, decimal.MaxValue),
                };

                // act
                var actual = Packer.Display(new Package(things));

                // assert
                Assert.Equal(@"3,5,8", actual);
            }
        }
    }
}
