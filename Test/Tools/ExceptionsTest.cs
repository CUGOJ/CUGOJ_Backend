using CUGOJ.Tools.Exceptions;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Test.Tools
{
    [TestClass]
    public class ExceptionsTest
    {
        [TestMethod]
        public void TestSyntax()
        {
            Exceptions.LoadErrorCode(new FileInfo("../../../../Grains/Common/User/UserGrainBase.cs"));
        }

        [TestMethod]
        public void GenerateErrorCode()
        {
            Exceptions.GenerateErrorCode("D:\\Code\\CUGOJ\\CUGOJ_Backend\\CUGOJ_Backend");
        }

        [TestMethod]
        public void TestCreateNode()
        {
            var tree = CSharpSyntaxTree.ParseText("");
            var root = tree.GetCompilationUnitRoot();
            SyntaxFactory.IdentifierName("New");
            SyntaxFactory.Argument(SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(12)));
            Console.WriteLine(root.ToString());
        }
    }
}
