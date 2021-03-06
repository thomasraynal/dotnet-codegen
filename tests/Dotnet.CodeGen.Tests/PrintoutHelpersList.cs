﻿using Dotnet.CodeGen.CustomHandlebars;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

#if DEBUG

namespace Dotnet.CodeGen.Tests
{
    public class PrintoutHelpersList
    {
        private readonly ITestOutputHelper _output;

        public PrintoutHelpersList(ITestOutputHelper output)
        {
            _output = output;
        }

        /// <summary>
        /// This test is just a way to generate a documentation for existing custom helpers
        /// </summary>
        [Fact]
        public void Test()
        {
            _output.WriteLine("| Helper | Input document | Handlebars template | Result |");
            _output.WriteLine("|--------|----------------|---------------------|--------|");

            string escapeStr(string str)
            {
                return str.Replace("\r", "\\r").Replace("\n", "\\n");
            };

            foreach (var helper in HandlebarsConfigurationHelper.Helpers.OrderBy(h => h.ToString()))
                foreach (var att in helper.GetType().GetCustomAttributes(typeof(HandlebarsHelperSpecificationAttribute), false).Cast<HandlebarsHelperSpecificationAttribute>())
                {
                    const int jsonLimit = 100;
                    var json = att.Json.Length >= jsonLimit
                        ? att.Json.Substring(0, jsonLimit) + "..."
                        : att.Json
                        ;

                    _output.WriteLine($"| {helper} | `\"{escapeStr(json)}\"` | `\"{escapeStr(att.Template)}\"` | `\"{escapeStr(att.ExpectedOutput)}\"` |");
                }
        }
    }
}

#endif