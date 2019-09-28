using AutoML.CodeGenerator.Core.CodeGeneration;
using AutoML.CodeGenerator.Core.Data;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace AutoML.CodeGenerator.Tests.CodeGeneration
{
    public class AutoMLCodeQueryTests
    {
        [Fact]
        public void ShouldGenerateAutoMLCode()
        {
            string csContent = LoadFileContent("Data/AutoMLCode.txt");

            var metadata = new ImportMetadata
            {
                ColumnDefinitions = new List<ColumnDefinition>
                {
                    new ColumnDefinition
                    {
                        Name = "Category",
                        DataType = ColumnDataType.Text,
                        MLType = ColumnMLType.Label
                    },
                    new ColumnDefinition
                    {
                        Name = "Description",
                        DataType = ColumnDataType.Text,
                        MLType = ColumnMLType.Text
                    },
                    new ColumnDefinition
                    {
                        Name = "Account",
                        DataType = ColumnDataType.Text,
                        MLType = ColumnMLType.Categorical
                    },
                    new ColumnDefinition
                    {
                        Name = "Date",
                        DataType = ColumnDataType.Text,
                        MLType = ColumnMLType.Ignored
                    },
                    new ColumnDefinition
                    {
                        Name = "Expense",
                        DataType = ColumnDataType.Text,
                        MLType = ColumnMLType.Numeric
                    }
                }
            };

            var generateModelCodeQuery = new AutoMLCodeQuery();
            var code = generateModelCodeQuery.Execute(metadata);

            // This make sure it passes on all platforms.
            code.Replace("\r", string.Empty).Should().Be(csContent.Replace("\r", string.Empty));
        }

        private string LoadFileContent(string relativePath)
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, relativePath);
            return File.ReadAllText(filePath);
        }
    }
}
