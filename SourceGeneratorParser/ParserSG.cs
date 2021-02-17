using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;

namespace SourceGeneratorParser
{
    [Generator]
    public class ParserSG : ISourceGenerator
    {
        internal const string inMethodIndent = "            ";
        internal const string inClassIndent = "        ";
        internal Dictionary<string, HashSet<string>> dirsWithFiles = new();

        public void Initialize(GeneratorInitializationContext context) { }

        public void Execute(GeneratorExecutionContext context)
        {
            StringBuilder sb = new StringBuilder();
           
            sb.Append(@"
using System;
namespace Faker {
    public static partial class Data
    {
        public static partial string GetFileText()
        {
");
            sb.AppendLine($"{inMethodIndent}Console.WriteLine(\"ABRAKA\");");
            sb.AppendLine($"{inMethodIndent}Console.WriteLine(\"DABRA\");");
            foreach (AdditionalText file in context.AdditionalFiles)
            {
                AddFileRecord(file);
                //Path.GetExtension(file.Path);
                string dirName = new DirectoryInfo(Path.GetDirectoryName(file.Path)).Name;
                string fileName = Path.GetFileNameWithoutExtension(file.Path);
                sb.AppendLine($@"{inMethodIndent}Console.WriteLine(""{dirName}"");");
                sb.AppendLine($@"{inMethodIndent}Console.WriteLine(""{fileName}"");");

                sb.AppendLine($@"{inMethodIndent}Console.WriteLine(""{file.Path.Replace('\\', '/')}"");");
                var text = file.GetText(context.CancellationToken).ToString();
                //char[] delims = { ' ', '\t', '\n' };
                var words = text.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in words)
                {
                    sb.AppendLine($@"{inMethodIndent}Console.WriteLine(""{item}"");");
                }
                
               
            }
            sb.AppendLine($"{inMethodIndent}return \"ABRAKA\";");
            sb.Append(@"
        }
    }
}");
            context.AddSource("Generated.cs", SourceText.From(sb.ToString(), Encoding.UTF8));

            string initMethodSource;
            string partialClassesSource;
            (initMethodSource, partialClassesSource) = this.CreateSourceForRGPartialClasses();
            context.AddSource("GeneratedInitMethod.cs", SourceText.From(initMethodSource, Encoding.UTF8));
            context.AddSource("GeneratedPartialClasses.cs", SourceText.From(partialClassesSource, Encoding.UTF8));

        }

        internal bool AddFileRecord(AdditionalText file)
        {
            string dirName = new DirectoryInfo(Path.GetDirectoryName(file.Path)).Name;
            string fileName = Path.GetFileNameWithoutExtension(file.Path);
            if (!this.dirsWithFiles.ContainsKey(dirName))
            {
                this.dirsWithFiles.Add(dirName, new HashSet<string>());
            }
            if (this.dirsWithFiles[dirName].Contains(fileName))
            {
                return false;
            }
            this.dirsWithFiles[dirName].Add(fileName);
            return true;
        }

        internal void ParseFileToSourceCode(AdditionalText file, StringBuilder sb, GeneratorExecutionContext context)
        {
            var text = file.GetText(context.CancellationToken).ToString();
            var words = text.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
            //TODO:parse file to dictionary
        }

        internal (string, string) CreateSourceForRGPartialClasses()
        {
            StringBuilder initBuilder = new();
            initBuilder.Append(@"
using System;
namespace Faker
{
    public partial class RandomGenerator
    {
         internal partial void RandomGeneratorInitializationGenerated()
        {
");
            StringBuilder partialClassesBuilder = new();
            partialClassesBuilder.Append(@"
using System;
namespace Faker
{
    public partial class RandomGenerator
    {
");

            foreach (var pair in this.dirsWithFiles)
            {
                string dirName = pair.Key;
                initBuilder.AppendLine($@"{inMethodIndent}this.{dirName} = new Random{dirName}(this);");
                partialClassesBuilder.AppendLine($"{inClassIndent}public Random{dirName} {dirName} {{ get; private set; }}");
                partialClassesBuilder.AppendLine($"{inClassIndent}public class Random{dirName}");
                partialClassesBuilder.AppendLine($"{inClassIndent}{{");  //curly bracket opening partial class definition
                partialClassesBuilder.Append(
$@"            /// <summary>
            /// reference to instance of Random generator that has reference to this instance of Random{dirName}
            /// </summary>
            internal RandomGenerator RG {{ get; }}
            public Random{dirName}(RandomGenerator rg)
            {{
                this.RG = rg;
            }}
");


                foreach (var fileName in pair.Value)
                {
                    //TODO:generate method per file in the dir
                }

                partialClassesBuilder.AppendLine($"{inClassIndent}}}");  //curly bracket closing partial class definition
                partialClassesBuilder.AppendLine();
            }

            initBuilder.Append(
@"        }
    }
}");
            partialClassesBuilder.Append(
@"   }
}");
            return (initBuilder.ToString(), partialClassesBuilder.ToString()) ;
        }

    }
}
