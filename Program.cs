using System;
using System.IO;
using Jint;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: jint <script.js>");
            return;
        }

        string scriptPath = args[0];

        if (!File.Exists(scriptPath))
        {
            Console.WriteLine($"Error: File '{scriptPath}' not found.");
            return;
        }

        try
        {
            string scriptContent = File.ReadAllText(scriptPath);

            var engine = new Engine()
                .SetValue("console", new
                {
                    log = new Action<object>(obj => Console.WriteLine(obj)),
                    error = new Action<object>(obj => Console.Error.WriteLine(obj))
                });

            engine.Execute(scriptContent);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error executing script: {ex.Message}");
        }
    }
}