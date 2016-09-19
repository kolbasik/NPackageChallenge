using System;
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
                var parser = new TestCaseParser();
                var validator = new TestCaseValidator();
                var packer = new TestCasePacker();

                var results = File.ReadAllLines(filePath)
                    .Map(parser.Parse)
                    .ForEach(validator.Validate)
                    .Map(packer.Pack)
                    .Map(Display);

                return string.Join(Environment.NewLine, results);
            }
            catch (Exception ex)
            {
                throw new APIException(@"An error occurred during packing.", ex);
            }
        }

        public static string Display(Package package)
        {
            return package?.Things.Count == 0 ? @"-" : string.Join(@",", package.Things.Select(thing => thing.Index));
        }
    }
}