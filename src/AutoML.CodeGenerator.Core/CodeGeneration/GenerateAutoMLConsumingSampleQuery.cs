using AutoML.CodeGenerator.Core.Data;

namespace AutoML.CodeGenerator.Core.CodeGeneration
{
    public class GenerateAutoMLConsumingSampleQuery
    {
        private readonly AutoMLCodeQuery _autoMLCodeQuery;

        public GenerateAutoMLConsumingSampleQuery(AutoMLCodeQuery autoMLCodeQuery)
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
using System;
using {modelsNamespace}

namespace {sampleNamespace}
{{
    public static class Program
    {{
        public static void Main(string[] args)
        {{
            var mlContext = new MLContext();

            // Load model & create prediction engine
            string modelPath = AppDomain.CurrentDomain.BaseDirectory + ""MLModel.zip"";
            ITransformer mlModel = mlContext.Model.Load(modelPath, out var modelInputSchema);
            var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);

            // TODO: Generate an input
            ModelInput input = new ModelInput();

            // Use model to make prediction on input data
            ModelOutput result = predEngine.Predict(input);
            Console.WriteLine(""Predicted value: "" + result.Prediction);
        }}
    }}
}}
";
        }
    }
}
