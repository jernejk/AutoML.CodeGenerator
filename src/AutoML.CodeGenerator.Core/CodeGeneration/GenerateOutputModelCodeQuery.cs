using System.Collections.Generic;
using System.Linq;
using AutoML.CodeGenerator.Core.Data;

namespace AutoML.CodeGenerator.Core.CodeGeneration
{
    public class GenerateOutputModelCodeQuery
    {
        public string Execute(string className, string classNamespace, List<ColumnDefinition> columnDefinitions)
        {
            bool hasNamespace = !string.IsNullOrWhiteSpace(classNamespace);
            string codeTab = "    ";
            string spacesForClass = hasNamespace ? codeTab : string.Empty;
            string spacesForProperties = spacesForClass + codeTab;

            var label = columnDefinitions.First(c => c.MLType == ColumnMLType.Label);
            var labelDataType = label.DataType == ColumnDataType.Number ? "float" : "string";

            string code = @$"{spacesForClass}public class {className}
{spacesForClass}{{
{spacesForProperties}[ColumnName(""PredictedLabel"")]
{spacesForProperties}public {labelDataType} Prediction {{ get; set; }}
{spacesForProperties}public float[] Score {{ get; set; }}
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
