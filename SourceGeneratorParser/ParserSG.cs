using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.IO;
using System.Text;

namespace SourceGeneratorParser
{
    [Generator]
    public class ParserSG : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context) { }

        public void Execute(GeneratorExecutionContext context)
        {
            StringBuilder sb = new StringBuilder();
            string indent = "            ";
            sb.Append(@"
using System;
namespace Faker {
    public static partial class Data
    {
        public static partial string GetFileText()
        {
 ");
            sb.AppendLine($"{indent}Console.WriteLine(\"ABRAKA\");");
            foreach (AdditionalText file in context.AdditionalFiles)
            {
                //Path.GetExtension(file.Path);
                sb.AppendLine($@"{indent}Console.WriteLine(""{file.Path.Replace('\\', '/')}"");");
                var text = file.GetText(context.CancellationToken).ToString();
                //char[] delims = { ' ', '\t', '\n' };
                var words = text.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in words)
                {
                    sb.AppendLine($@"{indent}Console.WriteLine(""{item}"");");
                }
               
            }
            sb.AppendLine($"{indent}return \"ABRAKA\";");
            sb.Append(@"
        }
    }
}");
            
            context.AddSource("Generated.cs", SourceText.From(sb.ToString(), Encoding.UTF8));
        }
    }
}
