using System.IO;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Skills.Core;
using Microsoft.SemanticKernel.Planning;
using Microsoft.SemanticKernel.Connectors.AI.HuggingFace;
using Microsoft.SemanticKernel.Orchestration;
using Microsoft.Extensions.Logging;
using Globant.Plugins;

var builder = new KernelBuilder();

builder.WithOpenAIChatCompletionService(
    "gpt-3.5-turbo", // OpenAI Model name
    "sk...Ed5"       // OpenAI API Key
);

using ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
{
    builder
        .SetMinimumLevel(0)
        .AddDebug()
        .AddConsole();
});

builder.WithLoggerFactory(loggerFactory);

var kernel = builder.Build();

var filePlugin = kernel.ImportSkill(new FileIOSkill(), "file");

// Import the semantic functions
var pluginsDirectory = Directory.GetCurrentDirectory();
var orquestationPlugin = kernel.ImportSemanticSkillFromDirectory(pluginsDirectory, "Plugins");
// Import the native functions
var codePlugin = kernel.ImportSkill(new Statistics(), "codeStatisticsPlugin");

// TEST

var sourceCodeFilePath = "/Users/adolfo/Documents/Proyectos/Labs/SemanticKernel/Program.cs";
var result = await kernel.RunAsync(sourceCodeFilePath, codePlugin["GetStatistics"]);

Console.WriteLine(result);

