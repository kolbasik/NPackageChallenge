using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using com.mobiquityinc.packer;

namespace com.mobiquityinc.pack
{
    class Program
    {
        static void Main(string[] args)
        {
            Trace.Listeners.Add(new ConsoleTraceListener());
            try
            {
                var filePath = args.FirstOrDefault() ?? @"input.txt";
                Trace.Write(Packer.Pack(filePath));
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                do {
                    sb.AppendLine($@"{ex.GetType().FullName}: {ex.Message}");
                } while ((ex = ex.InnerException) != null);
                Trace.TraceError(sb.ToString());
            }
        }
    }
}
