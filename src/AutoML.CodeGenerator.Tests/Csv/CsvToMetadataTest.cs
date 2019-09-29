using AutoML.CodeGenerator.Core.Data;
using AutoML.CodeGenerator.Csv;
using FluentAssertions;
using System;
using System.IO;
using Xunit;

namespace AutoML.CodeGenerator.Tests.Csv
{
    public class CsvToMetadataTest
    {
        [Fact]
        public void SimpleCsvTest()
        {
            string simpleCsv = "TextHeader,NumericHeader\n" +
                "Some text,1.2343";

            CsvToMetadataQuery query = new CsvToMetadataQuery();
            var metadata = query.Execute(simpleCsv);

            metadata.Should().NotBeNull();
            metadata.ColumnDefinitions.Should().HaveCount(2, "first one is text and second one is numeric");

            ColumnShouldBe(metadata.ColumnDefinitions[0], "TextHeader", "Some text");
            ColumnShouldBe(metadata.ColumnDefinitions[1], "NumericHeader", "1.2343");
        }

        [Fact]
        public void SimpleCsvWithOnlyHeadersTest()
        {
            string simpleCsv = "TextHeader,TextHeader2";

            CsvToMetadataQuery query = new CsvToMetadataQuery();
            var metadata = query.Execute(simpleCsv);

            metadata.Should().NotBeNull();
            metadata.ColumnDefinitions.Should().HaveCount(2);

            ColumnShouldBe(metadata.ColumnDefinitions[0], "TextHeader", null);
            ColumnShouldBe(metadata.ColumnDefinitions[1], "TextHeader2", null);
        }

        [Fact]
        public void DemoCsvTest()
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, "Data/from-bank-mini.csv");
            string csvContent = File.ReadAllText(filePath);

            CsvToMetadataQuery query = new CsvToMetadataQuery();
            var metadata = query.Execute(csvContent);

            metadata.Should().NotBeNull();
            metadata.ColumnDefinitions.Should().HaveCount(10, "demo CSV has 10 columns");

            ColumnShouldBe(metadata.ColumnDefinitions[0], "Date", "17/05/2018");
            ColumnShouldBe(metadata.ColumnDefinitions[1], "Account", "ANZ Rewards");
            ColumnShouldBe(metadata.ColumnDefinitions[2], "Category", "🛫 Transport");
            ColumnShouldBe(metadata.ColumnDefinitions[3], "Tags", "🚕 taxi");
            ColumnShouldBe(metadata.ColumnDefinitions[4], "Expense amount", "23.25");
            ColumnShouldBe(metadata.ColumnDefinitions[5], "Income amount", "0");
            ColumnShouldBe(metadata.ColumnDefinitions[6], "Currency", "AUD");
            ColumnShouldBe(metadata.ColumnDefinitions[7], "In main currency", "23.25");
            ColumnShouldBe(metadata.ColumnDefinitions[8], "Main currency", "AUD");
            ColumnShouldBe(metadata.ColumnDefinitions[9], "Description", "PAYPAL *UBER AU 4029357733");
        }

        private static void ColumnShouldBe(ColumnDefinition columnDefinition, string columnName, string columnFirstValue)
        {
            columnDefinition.Name.Should().Be(columnName);
            columnDefinition.FirstValue.Should().Be(columnFirstValue);
        }
    }
}
