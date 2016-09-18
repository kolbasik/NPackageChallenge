using System;
using com.mobiquityinc.exception;

namespace com.mobiquityinc.packer
{
    public class Packer
    {
        public static string Pack(string filePath)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                throw new APIException(@"An error occurred during packing.", ex);
            }
        }
    }
}
