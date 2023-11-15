using System.ComponentModel;
using Microsoft.SemanticKernel;

namespace Globant.Plugins;

public sealed class Statistics 
{
    [SKFunction, Description("Genenate statistics for a source code file in a given path")]
    
    //[SKParameter("path", "The path to the source file to genenate statistics")]
    public string GetStatistics([Description("Path to the source code file to read")] string path) 
    {
        var lineCount = File.ReadLines(path).Count();
        return $"Statistics: {lineCount} lines";
    }
}