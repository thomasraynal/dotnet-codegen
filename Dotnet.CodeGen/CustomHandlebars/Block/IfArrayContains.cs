﻿using HandlebarsDotNet;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Dotnet.CodeGen.CustomHandlebars.Block
{
    public static partial class SPECS
    {
        public const string IfArrayContains_TEST_DOCUMENT = @"
        {
            'type' : 'object',
            'required' : [ 'errorMeSSage', 'test' ],
            'properties' : {
                'errorMessage' : {
                    'type' : 'string'
                },
                'non_required_prop' : {
                    'type' : 'int'
                }
            }
        }";
    }

    /// <summary>
    /// Write the template if the second argument is found in the array passed as first argument
    /// (values are compared with string insensitive comparison)
    /// </summary>
    [HandlebarsHelperSpecification(SPECS.IfArrayContains_TEST_DOCUMENT, "{{#if_array_contains required 'errorMessage'}}OK{{else}}NOK{{/if_array_contains}}", "OK")]
    [HandlebarsHelperSpecification(SPECS.IfArrayContains_TEST_DOCUMENT, "{{#if_array_contains required 'test'}}OK{{else}}NOK{{/if_array_contains}}", "OK")]
    [HandlebarsHelperSpecification(SPECS.IfArrayContains_TEST_DOCUMENT, "{{#if_array_contains required 'notFound'}}OK{{else}}NOK{{/if_array_contains}}", "NOK")]
    [HandlebarsHelperSpecification(SPECS.IfArrayContains_TEST_DOCUMENT, "{{#each properties}}{{#if_array_contains ../required @key}}{{type}}{{else}}{{/if_array_contains}}{{/each}}", "string")]
    public class IfArrayContains : BlockHelperBase
    {
        public IfArrayContains() : base("if_array_contains") { }

        public override HandlebarsBlockHelper Helper =>
            (TextWriter output, HelperOptions options, object context, object[] arguments) =>
                {
                    EnsureArgumentsCount(arguments, 2);

                    var array = GetArgumentAsArray(arguments, 0);

                    var search = GetArgumentStringValue(arguments, 1);

                    if (array
                        .Select(token => GetStringValue(token))
                        .Any(s => string.Compare(s, search, StringComparison.InvariantCultureIgnoreCase) == 0)
                        )
                    {
                        options.Template(output, context);
                    }
                    else
                    {
                        options.Inverse(output, context);
                    }
                };
    }
}