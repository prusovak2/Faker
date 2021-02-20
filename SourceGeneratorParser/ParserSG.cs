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
        internal const string inNestedClassIndent = inMethodIndent;
        internal const string inNestedClassMethodIndent = "                ";
        internal const string inClassIndent = "        ";
        internal const string inNameSpaceIndent = "    ";
        internal const string defaltCultureInfo = "en_us";
        internal Dictionary<string, HashSet<string>> dirsWithFiles = new();

        public void Initialize(GeneratorInitializationContext context) { }

        public void Execute(GeneratorExecutionContext context)
        {
            StringBuilder dataCtorBuilder = new StringBuilder();
            dataCtorBuilder.Append(@"
using System;
using System.Collections.Generic;
namespace Faker {
    internal static partial class Data
    {
        static Data()
        { 
");

            StringBuilder dataDictsBuilder = new StringBuilder();
            dataDictsBuilder.Append(@"
using System;
using System.Collections.Generic;
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
                ParseFileToSourceCode(file, dataCtorBuilder,dataDictsBuilder, enumBuilder, context);
            }

            
            //end of Faker namespace in GeneratedEnums.cs
            enumBuilder.Append(@"
}");

            //end of Data class and Faker namespace
            dataDictsBuilder.Append(@"
    }
}");
            //end of Data static ctor
            dataCtorBuilder.Append(@"
        }
    }
}");

            context.AddSource("GeneratedDataCtor.cs", SourceText.From(dataCtorBuilder.ToString(), Encoding.UTF8));
            context.AddSource("GeneratedDataDicts", SourceText.From(dataDictsBuilder.ToString(), Encoding.UTF8));
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

        internal void ParseFileToSourceCode(AdditionalText file, StringBuilder dataCtorBuilder, StringBuilder dataDictsBuilder, StringBuilder enumBuilder, GeneratorExecutionContext context)
        {
            //per file
            string dirName = new DirectoryInfo(Path.GetDirectoryName(file.Path)).Name;
            string fileName = Path.GetFileNameWithoutExtension(file.Path);

            //enum type for cultures from this file
            enumBuilder.Append($@"
    public enum {dirName}{fileName}Culture 
    {{
");
            //read file and parse it to dictionary, where keys are culture strings and values are list containing data from file
            Dictionary<string, List<string>> culInfosAndData = ParseFileToDictionary(file, context);

            // add declaration of per file dictionary to Data class
            dataDictsBuilder.AppendLine($@"{inClassIndent}internal static Dictionary<{dirName}{fileName}Culture, string[]> {dirName}{fileName} = new();");

            foreach (var pair in culInfosAndData)
            {
                string culture = pair.Key;
                //add new enum constant representing this culture
                enumBuilder.AppendLine($"{inMethodIndent}{culture},");

                //create array containing data belonging to this culture
                dataCtorBuilder.Append($@"{inMethodIndent}string[] {dirName}{fileName}{culture} = {{
            ");

                int counter = 0; 
                foreach (var word in pair.Value)  //for each word
                {
                    dataCtorBuilder.Append($"\"{word}\",");  //add word to array
                    counter++;
                    if (counter == 10) // ten records per line in resulting source code to keep it readable
                    {
                        counter = 0;
                        dataCtorBuilder.AppendLine();
                        dataCtorBuilder.Append($"{inNestedClassMethodIndent}"); //indent on the new line
                    }
                }
                dataCtorBuilder.AppendLine(" };"); //end of an array
                dataCtorBuilder.AppendLine();
                dataCtorBuilder.AppendLine($"{inMethodIndent}Data.{dirName}{fileName}.Add({dirName}{fileName}Culture.{culture}, {dirName}{fileName}{culture});"); //add array to dictionary
                dataCtorBuilder.AppendLine();
            }
            enumBuilder.AppendLine($@"{inNameSpaceIndent}}}"); //end of enum type definition
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
                    AddDataToDictionaryIfNotNullAndNonEmpty(culInfosAndData, curCulInfo, curData);
                    curCulInfo = curWord.Trim(new char[] { '[', ']' });
                    curData = new List<string>();
                }
                else
                {
                    //ordinaryWord
                    curData.Add(curWord);
                }
            }
            
            AddDataToDictionaryIfNotNullAndNonEmpty(culInfosAndData, curCulInfo, curData);
            

            return culInfosAndData;
        }

        internal void AddDataToDictionaryIfNotNullAndNonEmpty(Dictionary<string, List<string>> culInfosAndData, string curCulInfo, List<string> curData)
        {
            if (curCulInfo is not null && curData is not null && curData.Count > 0)
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
                partialClassesBuilder.AppendLine();
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

                    partialClassesBuilder.AppendLine($@"
            public string {fileName}({dirName}{fileName}Culture? culInfo = null)
            {{
                {dirName}{fileName}Culture culToUse;
                if (culInfo.HasValue)
                {{
                    culToUse = culInfo.Value;
                }}
                else if(!Enum.TryParse(RG.Culture.SGFriendlyName(), true, out culToUse))
                {{
                    Enum.TryParse(RG.Culture.TwoLetterISOLanguageName, true, out culToUse);
                }}
                string[] toPickFrom = Faker.Data.{dirName}{fileName}[culToUse];
                return RG.Pick(toPickFrom);
            }}");

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
