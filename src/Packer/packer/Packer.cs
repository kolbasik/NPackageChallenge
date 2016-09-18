using System;
using System.Collections.Generic;
using System.IO;
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

        private static TestCase Parse(string text)
        {
            throw new NotImplementedException();
        }

        private static void Validate(TestCase testCase)
        {
            throw new NotImplementedException();
        }

        private static Package Pack(TestCase testCase)
        {
            throw new NotImplementedException();
        }

        private static string ToString(Package package)
        {
            throw new NotImplementedException();
        }
    }
}