using AutoML.CodeGenerator.Core.Data;

namespace AutoML.CodeGenerator.Core
{
    public class DetermineColumnDefinitionsCommand
    {
        public void Execute(ImportMetadata metadata)
        {
            foreach (var column in metadata.ColumnDefinitions)
            {
                bool isNumeric = !string.IsNullOrWhiteSpace(column.FirstValue) && double.TryParse(column.FirstValue, out _);
                if (isNumeric)
                {
                    column.DataType = ColumnDataType.Number;
                    column.MLType = ColumnMLType.Numeric;
                }
                else
                {
                    column.DataType = ColumnDataType.Text;
                    column.MLType = ColumnMLType.Categorical;
                }
            }
        }
    }
}
