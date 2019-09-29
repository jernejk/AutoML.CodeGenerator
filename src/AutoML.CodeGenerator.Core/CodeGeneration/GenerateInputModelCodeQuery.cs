using System.Collections.Generic;
using AutoML.CodeGenerator.Core.Data;

namespace AutoML.CodeGenerator.Core.CodeGeneration
{
    public class GenerateInputModelCodeQuery
    {
        public string Execute(string className, string classNamespace, List<ColumnDefinition> columnDefinitions)
        {
            int i = 0;
            bool hasNamespace = !string.IsNullOrWhiteSpace(classNamespace);
            string codeTab = "    ";
            string spacesForClass = hasNamespace ? codeTab : string.Empty;
            string spacesForProperties = spacesForClass + codeTab;

            string propertyCode = string.Empty;
            foreach (var columnDefinition in columnDefinitions)
            {
                if (!string.IsNullOrWhiteSpace(propertyCode))
                {
                    propertyCode += "\n\n";
                }

                string propertyName = columnDefinition.Name.Replace(" ", "_");
                string type = columnDefinition.DataType == ColumnDataType.Number ? "float" : "string";
                propertyCode += $"{spacesForProperties}[ColumnName(\"{columnDefinition.Name}\"), LoadColumn({i})]\n" +
                    $"{spacesForProperties}public {type} {propertyName} {{ get; set; }}";

                ++i;
            }

            string code = @$"{spacesForClass}public class {className}
{spacesForClass}{{
{propertyCode}
{spacesForClass}}}";

            if (hasNamespace)
            {
                code = $@"namespace {classNamespace}
{{
{code}
}}";
            }

            code = $@"using Microsoft.ML.Data;

{code}
";

            return code;
        }
    }
}
