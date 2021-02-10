using System;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace FakerGenerator
{
    public class FakerSyntaxReceiver : ISyntaxReceiver
    {
        public List<RuleForPack> RuleForInvocations { get; } = new();
        public List<SimpleNameSyntax> AccessedMembers { get; } = new();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is InvocationExpressionSyntax
                {
                    ArgumentList:
                    {
                        Arguments:
                        {
                            Count: 2
                        } arguments
                    },
                    Expression:
                    {

                    }
                } invocation
               )
            {
                if (arguments[0].Expression is LambdaExpressionSyntax memberLambda && arguments[1].Expression is LambdaExpressionSyntax rgLambda)
                {
                    if (memberLambda.Body is MemberAccessExpressionSyntax memberAceessSyntax &&
                        rgLambda.Body is InvocationExpressionSyntax RGinvocation &&
                        RGinvocation.Expression is MemberAccessExpressionSyntax randFundAccesSyntax)
                    {
                        SimpleNameSyntax memberName = memberAceessSyntax.Name;
                        SimpleNameSyntax randFuncName = randFundAccesSyntax.Name;
                        RuleForInvocations.Add(new RuleForPack(invocation, memberName, randFuncName));
                    }

                }
            }
        }
    }

    public class RuleForPack
    {
        public InvocationExpressionSyntax Invocation { get; set; }
        public SimpleNameSyntax MemberName { get; set; }
        public SimpleNameSyntax RandomFunctionName { get; set; }

        public RuleForPack(InvocationExpressionSyntax invocation, SimpleNameSyntax memberName, SimpleNameSyntax ranName)
        {
            this.Invocation = invocation;
            this.MemberName = memberName;
            this.RandomFunctionName = ranName;
        }
    }
}
