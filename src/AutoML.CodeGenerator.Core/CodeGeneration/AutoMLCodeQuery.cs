using AutoML.CodeGenerator.Core.Data;
using System.Linq;

namespace AutoML.CodeGenerator.Core.CodeGeneration
{
    public class AutoMLCodeQuery
    {
        public string Execute(ImportMetadata importMetadata)
        {
            string label = importMetadata.ColumnDefinitions
                .Where(c => c.MLType == ColumnMLType.Label)
                .Select(c => c.Name)
                .Single();

            string columnDefinitionCode = $@"    var columnInfo = new ColumnInformation {{ LabelColumnName = ""{label}"" }};
";
            foreach (var column in importMetadata.ColumnDefinitions.Where(c => c.MLType != ColumnMLType.Label && c.MLType != ColumnMLType.Ignored && c.MLType != ColumnMLType.Undefined))
            {
                string mlType = column.MLType.ToString();
                columnDefinitionCode += @$"    columnInfo.{mlType}ColumnNames.Add(""{column.Name}"");
";
            }

            string template = $@"using Microsoft.ML;
using Microsoft.ML.AutoML;

public ITransformer AutoTrain(MLContext mlContext, IDataView trainingDataView, uint maxTimeInSec)
{{
{columnDefinitionCode}
    var experimentSettings = new MulticlassExperimentSettings()
    {{
        MaxExperimentTimeInSeconds = maxTimeInSec,
        OptimizingMetric = MulticlassClassificationMetric.MacroAccuracy
    }};

    var experiment = mlContext.Auto().CreateMulticlassClassificationExperiment(experimentSettings);
    var result = experiment.Execute(trainingDataView, columnInfo);

    // Final model that can be used and/or evaluated.
    return result.BestRun.Model;
}}";

            return template;
        }
    }
}
