using com.mobiquityinc.domain;
using com.mobiquityinc.packer;
using Xunit;

namespace com.mobiquityinc
{
    public sealed class PackageFormatterTests
    {
        public sealed class Format
        {
            [Fact]
            public void Should_return_dash_if_none()
            {
                // arrange
                var formatter = new PackageFormatter();
                var package = new Package();

                // act
                var actual = formatter.Format(package);

                // assert
                Assert.Equal(@"-", actual);
            }

            [Fact]
            public void Should_return_a_set_of_index_numbers_separated_by_comma()
            {
                // arrange
                var formatter = new PackageFormatter();
                var package = new Package
                {
                    new Thing(3, decimal.MinValue, decimal.MinValue),
                    new Thing(5, decimal.Zero, decimal.Zero),
                    new Thing(8, decimal.MaxValue, decimal.MaxValue),
                };

                // act
                var actual = formatter.Format(package);

                // assert
                Assert.Equal(@"3,5,8", actual);
            }
        }
    }
}