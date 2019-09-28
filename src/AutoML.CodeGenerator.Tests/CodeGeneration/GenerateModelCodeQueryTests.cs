using AutoML.CodeGenerator.Core;
using AutoML.CodeGenerator.Core.CodeGeneration;
using AutoML.CodeGenerator.Core.Data;
using AutoML.CodeGenerator.Csv;
using FluentAssertions;
using System;
using System.IO;
using Xunit;

namespace AutoML.CodeGenerator.Tests.CodeGeneration
{
    public class GenerateModelCodeQueryTests
    {
        [Fact]
        public void ShouldGenerateInputFile()
        {
            string csvContent = LoadFileContent("Data/from-bank-mini.csv");
            string csContent = LoadFileContent("Data/InputModel.txt");

            var metadata = GetMetadataFromCsv(csvContent);
            var generateModelCodeQuery = new GenerateModelCodeQuery();
            var code = generateModelCodeQuery.Execute("InputModel", "AutoML.CodeGenerator.Tests.Data", metadata.ColumnDefinitions);

            // This make sure it passes on all platforms.
            code.Replace("\r", string.Empty).Should().Be(csContent.Replace("\r", string.Empty));
        }

        private string LoadFileContent(string relativePath)
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, relativePath);
            return File.ReadAllText(filePath);
        }

        private ImportMetadata GetMetadataFromCsv(string csvContent)
        {
            var metadata = new CsvToMetadataQuery().Execute(csvContent);
            new DetermineColumnDefinitionsCommand().Execute(metadata);

            return metadata;
        }
    }
}
