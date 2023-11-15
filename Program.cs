using Microsoft.SemanticKernel;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.Plugins.Core;
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

var filePlugin = kernel.ImportFunctions(new FileIOPlugin(), "file");

// Import the semantic functions
var pluginsDirectory = Directory.GetCurrentDirectory();
var orquestationPlugin = kernel.ImportSemanticFunctionsFromDirectory(pluginsDirectory, "Plugins");
// Import the native functions
var codePlugin = kernel.ImportFunctions(new Statistics(), "codeStatisticsPlugin");

// TEST

var sourceCodeFilePath = "/Users/adolfo/Documents/Proyectos/Labs/SemanticKernel/Program.cs";
var result = await kernel.RunAsync(sourceCodeFilePath, codePlugin["GetStatistics"]);

Console.WriteLine(result);

