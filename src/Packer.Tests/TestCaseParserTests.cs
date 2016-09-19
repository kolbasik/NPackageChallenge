using com.mobiquityinc.domain;
using com.mobiquityinc.packer;
using Xunit;

namespace com.mobiquityinc
{
    public sealed class TestCaseParserTests
    {
        public sealed class Parse
        {
            [Fact]
            public void Should_parse_test_case_from_a_string()
            {
                // arrange
                var parser = new TestCaseParser();
                var expected = new TestCase(81, new[] { new Thing(1, 53.38m, 45), new Thing(2, 88.62m, 98), new Thing(3, 78.48m, 3) });

                // act
                var actual = parser.Parse(@"81 : (1,53.38,€45) (2,88.62,€98) (3,78.48,€3)");

                // assert
                Assert.Equal(expected.ToString(), actual.ToString());
            }
        }
    }
}