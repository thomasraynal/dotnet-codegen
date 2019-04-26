﻿using Dotnet.CodeGen.CustomHandlebars;
using HandlebarsDotNet;
using Newtonsoft.Json.Linq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Dotnet.CodeGen.Tests
{
    public class HandlebarsHelperSpecificationsTests
    {
        [Theory]
        [MemberData(nameof(StandardHelpersTests_Data))]
        public void StandardHelpersTests(string helperName, IStandardHelper helper, string json, string template, string expectedOutput)
        {
            var handleBar = Handlebars.Create(new HandlebarsConfiguration
            {
                Helpers = { { helperName, helper.Helper } }
            });

            var output = handleBar.Compile(template).Invoke(JObject.Parse(json));
            output.ShouldBe(expectedOutput);
        }

        public static IEnumerable<object[]> StandardHelpersTests_Data()
        {
            foreach (var helper in HandlebarsConfigurationHelper.StandardHelpers)
                foreach (var att in helper.GetType().GetCustomAttributes(typeof(HandlebarsHelperSpecificationAttribute), false).Cast<HandlebarsHelperSpecificationAttribute>())
                {
                    yield return new object[] { helper.Name, helper, att.Json, att.Template, att.ExpectedOutput };
                }
        }

        [Theory]
        [MemberData(nameof(BlockHelpersTests_Data))]
        public void BlockHelpersTests(string helperName, IBlockHelper helper, string json, string template, string expectedOutput)
        {
            var handleBar = Handlebars.Create(new HandlebarsConfiguration
            {
                BlockHelpers = { { helperName, helper.Helper } }
            });

            var output = handleBar.Compile(template).Invoke(JObject.Parse(json));
            output.ShouldBe(expectedOutput);
        }

        public static IEnumerable<object[]> BlockHelpersTests_Data()
        {
            foreach (var helper in HandlebarsConfigurationHelper.BlockHelpers)
                foreach (var att in helper.GetType().GetCustomAttributes(typeof(HandlebarsHelperSpecificationAttribute), false).Cast<HandlebarsHelperSpecificationAttribute>())
                {
                    yield return new object[] { helper.Name, helper, att.Json, att.Template, att.ExpectedOutput };
                }
        }
    }
}
