using AutoML.CodeGenerator.Core.Data;

namespace AutoML.CodeGenerator.Core.CodeGeneration
{
    public class GenerateAutoMLTrainingSampleQuery
    {
        private readonly AutoMLCodeQuery _autoMLCodeQuery;

        public GenerateAutoMLTrainingSampleQuery(AutoMLCodeQuery autoMLCodeQuery)
        {
            _autoMLCodeQuery = autoMLCodeQuery;
        }

        public string Execute(ImportMetadata metadata, string sampleNamespace, string modelsNamespace)
        {
            string autoMlCode = _autoMLCodeQuery.Execute(metadata);
            autoMlCode = autoMlCode
                .TrimEnd('\r', '\n')
                .Replace("\n", "\n        ");
            return $@"using Microsoft.ML;
using Microsoft.ML.AutoML;
using Microsoft.ML.Data;
using {modelsNamespace};

namespace {sampleNamespace}
{{
    public static class Program
    {{
        public static void Main(string[] args)
        {{
            string pathToCsv = ""[path-to-csv]"";
            var mlContext = new MLContext();
            IDataView trainingDataView = mlContext.Data.LoadFromTextFile<ModelInput>(
                path: pathToCsv,
                hasHeader: true,
                separatorChar: ',',
                allowQuoting: true,
                allowSparse: false);

            var model = AutoTrain(mlContext, trainingDataView, 5);

            // Save training model to disk.
            mlContext.Model.Save(model, trainingDataView.Schema, ""MLModel.zip"");
        }}

        {autoMlCode}
    }}
}}
";
        }
    }
}
