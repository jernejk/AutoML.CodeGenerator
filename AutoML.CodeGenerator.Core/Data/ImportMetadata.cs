using System.Collections.Generic;

namespace AutoML.CodeGenerator.Core.Data
{
    public class ImportMetadata
    {
        public ImportMetadata()
        {
            ColumnDefinitions = new List<ColumnDefinition>();
        }

        public List<ColumnDefinition> ColumnDefinitions { get; set; }
    }
}
