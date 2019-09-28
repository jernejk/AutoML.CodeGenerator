namespace AutoML.CodeGenerator.Core.Data
{
    public class ColumnDefinition
    {
        public string? Name { get; set; }
        public ColumnDataType DataType { get; set; }
        public ColumnMLType MLType { get; set; }
        public string? FirstValue { get; set; }
    }
}
