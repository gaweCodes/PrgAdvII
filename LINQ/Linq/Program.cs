using System;
using Linq.ExtensionMethods;

namespace Linq {
    public class Program {
        public static void Main(string[] args) {
            ExpressionTrees.Examples.TestIntro();
            LinqToXml.LinqToXml.Execute();

            ExpressionTrees.Examples.TestPrintExpressionTree();
            ExpressionTrees.Examples.TestBuildAnExpressionTree();

            new LambdaExpressions.TypeInference.cs.Examples().TestRefOut();

            new Linq.Initializers.Examples().TestCollectionInitializers();
            new Linq.Initializers.Examples().TestObjectInitializers();
            new Linq.Initializers.Examples().TestCombination();

            new Linq.LambdaExpressions.AnonymousTypes.Examples().Test();

            new Linq.LambdaExpressions.QueryExpressions.Examples().TestQuerySyntaxSelectMany();

            new Examples().TestDeferredEvaluation();
        }
    }
}