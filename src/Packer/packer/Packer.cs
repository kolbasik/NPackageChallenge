using System;
using System.IO;
using com.mobiquityinc.exception;

namespace com.mobiquityinc.packer
{
    public static class Packer
    {
        public static string Pack(string filePath)
        {
            try
            {
                var handler = new TestCaseHandler();
                var results = handler.Handle(File.ReadAllLines(filePath));
                return string.Join(Environment.NewLine, results);
            }
            catch (Exception ex)
            {
                throw new APIException(@"An error occurred during packing.", ex);
            }
        }
    }
}