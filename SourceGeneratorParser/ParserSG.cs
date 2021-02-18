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
        internal const string inNameSpaceIndent = "    ";
        internal const string defaltCultureInfo = "en_us";
        internal Dictionary<string, HashSet<string>> dirsWithFiles = new();

        public void Initialize(GeneratorInitializationContext context) { }

        public void Execute(GeneratorExecutionContext context)
        {
            StringBuilder dataBuilder = new StringBuilder();
           
            dataBuilder.Append(@"
using System;
namespace Faker {
    internal static partial class Data
    {
");
            StringBuilder enumBuilder = new StringBuilder();
            enumBuilder.Append(@"
using System;
namespace Faker {
");

            foreach (AdditionalText file in context.AdditionalFiles)
            {
                AddFileRecord(file);
                ParseFileToSourceCode(file, dataBuilder, enumBuilder, context);
               
            }

            
            //end of Faker namespace in GeneratedEnums.cs
            enumBuilder.Append(@"
}");

            //end of Data class and Faker namespace
            dataBuilder.Append(@"
    }
}");
            context.AddSource("GeneratedData.cs", SourceText.From(dataBuilder.ToString(), Encoding.UTF8));
            context.AddSource("GeneratedEnums.cs", SourceText.From(enumBuilder.ToString(), Encoding.UTF8));

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

        internal void ParseFileToSourceCode(AdditionalText file, StringBuilder dataBuilder, StringBuilder enumBuilder, GeneratorExecutionContext context)
        {
            string dirName = new DirectoryInfo(Path.GetDirectoryName(file.Path)).Name;
            string fileName = Path.GetFileNameWithoutExtension(file.Path);

            //per file

            //enum type for culture
            enumBuilder.Append($@"
    public enum {dirName}{fileName}Culture 
    {{
");
            Dictionary<string, List<string>> culInfosAndData = ParseFileToDictionary(file, context);
            foreach (var pair in culInfosAndData)
            {
                string culture = pair.Key;
                enumBuilder.AppendLine($"{inMethodIndent}{culture},");

                dataBuilder.Append($@"{inClassIndent}internal static string[] {dirName}{fileName}{culture} = {{
            ");

                int counter = 0;
                foreach (var word in pair.Value)  //for each word
                {
                    dataBuilder.Append($"\"{word}\",");
                    counter++;
                    if (counter == 10)
                    {
                        counter = 0;
                        dataBuilder.AppendLine();
                        dataBuilder.Append($"{inMethodIndent}");
                    }
                }
                dataBuilder.AppendLine(" };");
                dataBuilder.AppendLine();
            }
            enumBuilder.AppendLine($@"{inNameSpaceIndent}}}");
        }

        internal Dictionary<string, List<string>> ParseFileToDictionary(AdditionalText file, GeneratorExecutionContext context)
        {
            Dictionary<string, List<string>> culInfosAndData = new();

            var text = file.GetText(context.CancellationToken).ToString();
            var words = text.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            string curCulInfo = defaltCultureInfo; //Default
            List<string> curData = new();
            for (int i = 0; i < words.Length; i++)
            {
                string curWord = words[i].Trim();
                if (curWord.Length == 0)
                {
                    continue;
                }
                if ((curWord.StartsWith("[") && curWord.EndsWith("]")))
                {
                    AddDataToDictionaryIfNotNull(culInfosAndData, curCulInfo, curData);
                    curCulInfo = curWord.Trim(new char[] { '[', ']' });
                    curData = new List<string>();
                }
                else
                {
                    //ordinaryWord
                    curData.Add(curWord);
                }
            }
            if(curData is not null && curData.Count > 0)
            {
                AddDataToDictionaryIfNotNull(culInfosAndData, curCulInfo, curData);
            }

            return culInfosAndData;
        }

        internal void AddDataToDictionaryIfNotNull(Dictionary<string, List<string>> culInfosAndData, string curCulInfo, List<string> curData)
        {
            if (curCulInfo is not null && curData is not null)
            {
                if (culInfosAndData.ContainsKey(curCulInfo))
                {
                    culInfosAndData[curCulInfo].AddRange(curData);
                }
                else
                {
                    culInfosAndData.Add(curCulInfo, curData);
                }
            }
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
