using AutoML.CodeGenerator.Core;
using AutoML.CodeGenerator.Core.Data;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace AutoML.CodeGenerator.Tests
{
    public class DetermineColumnDefinitionsCommandTest
    {
        [Fact]
        public void ShouldCorrectlySetColumnDefinitions()
        {
            var metadata = new ImportMetadata
            {
                ColumnDefinitions = new List<ColumnDefinition>
                {
                    new ColumnDefinition
                    {
                        Name = "Date",
                        FirstValue = "17/05/2018"
                    },
                    new ColumnDefinition
                    {
                        Name = "Category",
                        FirstValue = "🛫 Transport"
                    },
                    new ColumnDefinition
                    {
                        Name = "Expense",
                        FirstValue = "12.23"
                    }
                }
            };

            var command = new DetermineColumnDefinitionsCommand();
            command.Execute(metadata);

            metadata.ColumnDefinitions[0].DataType.Should().Be(ColumnDataType.Text);
            metadata.ColumnDefinitions[0].MLType.Should().Be(ColumnMLType.Categorical);
            metadata.ColumnDefinitions[1].DataType.Should().Be(ColumnDataType.Text);
            metadata.ColumnDefinitions[1].MLType.Should().Be(ColumnMLType.Categorical);
            metadata.ColumnDefinitions[2].DataType.Should().Be(ColumnDataType.Number);
            metadata.ColumnDefinitions[2].MLType.Should().Be(ColumnMLType.Numeric);
        }
    }
}
