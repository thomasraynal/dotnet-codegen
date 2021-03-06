﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace Dotnet.CodeGen.Tests
{
    public class ManualTests
    {
        private readonly ITestOutputHelper _output;
        private readonly string _solutionFolder;
        private readonly string _tmpOutput;

        public ManualTests(ITestOutputHelper output)
        {
            _output = output;
            _solutionFolder = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "../../../.."));
            _tmpOutput = Path.Combine(_solutionFolder, "_testsnugetpakage." + Path.GetRandomFileName());
        }

        //[Fact]
        [Fact(Skip = "Just to get the tooling installed on my computer without having to deploy on nuget.")]
        public void ManualInstall()
        {
            PackAndUseTests.PackTool(_output, _solutionFolder, _tmpOutput);
            PackAndUseTests.InstallTool(_output, _solutionFolder, _tmpOutput);
        }

        [Fact]
        public void ManualUninstall()
        {
            PackAndUseTests.UninstallTool(_output, _solutionFolder, _tmpOutput);
        }
    }
}
