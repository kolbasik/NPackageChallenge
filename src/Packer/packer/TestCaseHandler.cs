using System.Collections.Generic;
using com.mobiquityinc.extensions;

namespace com.mobiquityinc.packer
{
    public sealed class TestCaseHandler
    {
        private readonly PackageFormatter packageFormatter;
        private readonly TestCasePacker testCasePacker;
        private readonly TestCaseParser testCaseParser;
        private readonly TestCaseValidator testCaseValidator;

        public TestCaseHandler()
        {
            packageFormatter = new PackageFormatter();
            testCasePacker = new TestCasePacker();
            testCaseParser = new TestCaseParser();
            testCaseValidator = new TestCaseValidator();
        }

        public IEnumerable<string> Handle(IEnumerable<string> source)
        {
            return source.Map(testCaseParser.Parse)
                .ForEach(testCaseValidator.Validate)
                .Map(testCasePacker.Pack)
                .Map(packageFormatter.Format);
        }
    }
}