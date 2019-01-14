using System;
using McMaster.Extensions.CommandLineUtils;

namespace fjj
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var pager = new Pager())
            {
                for (var i = 1; i <= 1000; i++)
                {
                    pager.Writer.WriteLine($"This is sentence {i} of 1,000");
                }
            }
        }
    }
}
