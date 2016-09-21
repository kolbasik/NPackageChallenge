using System;
using System.IO;
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
    }
}
