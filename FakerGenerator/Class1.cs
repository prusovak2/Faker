using System;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace FakerGenerator
{
    [Generator]
    public class FakerGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new FakerSyntaxReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            if (context.SyntaxReceiver is not FakerSyntaxReceiver fakerSyntaxReceiver)
            {
                throw new InvalidOperationException("Wrong syntax receiver provided for FakerGenerator");
            }
            List<RuleForPack> RuleForInvocations = fakerSyntaxReceiver.RuleForInvocations;

            const string indent = "         ";
            StringBuilder sourceBuilder = new StringBuilder(@"
using System;
namespace TestApp
{
    public partial class MyPartialClass
    {
        public partial void Method() 
        {
            Console.WriteLine(""Hello from generated code!"");
");
            int i = 0;
            foreach (var ruleForPack in RuleForInvocations)
            {
                SemanticModel semanticModel = context.Compilation.GetSemanticModel(ruleForPack.Invocation.SyntaxTree);
                SymbolInfo symbolInfo = semanticModel.GetSymbolInfo(ruleForPack.Invocation);
                IMethodSymbol invokedSymbol = symbolInfo.Symbol as IMethodSymbol;
                string methodName = invokedSymbol.Name;

                if (methodName != "RuleFor")
                {
                    continue;
                }
                //IParameterSymbol firstParam = invokedSymbol.Parameters[0];
                //ITypeSymbol typeSymbol = firstParam.Type;

                IInvocationOperation op = (IInvocationOperation)semanticModel.GetOperation(ruleForPack.Invocation);
                var type = op.Instance.Type;
                sourceBuilder.Append($"{indent}Console.WriteLine(\"{type}\");\n");
                sourceBuilder.Append($"{indent}Console.WriteLine(\"{methodName}\");\n");
                sourceBuilder.Append($"{indent}Console.WriteLine(\"{ruleForPack.MemberName}\");\n");
                sourceBuilder.Append($"{indent}Console.WriteLine(\"{ruleForPack.RandomFunctionName}\");\n");
                i++;
            }

            sourceBuilder.Append(@"
        }
    }
}");
            context.AddSource("Generated.cs", SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
        }
    }
}