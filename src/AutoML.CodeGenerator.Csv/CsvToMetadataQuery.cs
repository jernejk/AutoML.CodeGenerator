using AutoML.CodeGenerator.Core.Data;
using CsvHelper;
using System.IO;

namespace AutoML.CodeGenerator.Csv
{
    public class CsvToMetadataQuery
    {
        public ImportMetadata Execute(string content)
        {
            // TODO: Use TextFieldParser when client-side Blazor supports .NET Core 3.
            // NOTE: Use Visual Studio 2019 16.3+
            using TextReader fileReader = new StringReader(content);

            var csv = new CsvParser(fileReader);
            var headers = csv.Read();
            var firstRow = csv.Read();

            var metadata = new ImportMetadata();
            for (int i = 0; i < headers.Length; ++i)
            {
                var column = new ColumnDefinition
                {
                    Name = headers[i],
                    FirstValue = firstRow?[i]
                };

                metadata.ColumnDefinitions.Add(column);
            }

            return metadata;
        }
    }
}
