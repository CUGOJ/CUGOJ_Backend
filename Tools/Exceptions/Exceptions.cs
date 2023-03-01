using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CUGOJ.Tools.Exceptions
{
    public class CUGOJException : Exception
    {
        public override string Message => base.Message;
        public string ErrorMessage { get; set; } = "";
        public ExceptionTypeEnum ExceptionType { get; set; }
        public long ErrorCode { get; set; }

    }
    public enum ExceptionTypeEnum
    {
        SystemError,
        NotSet,
        Json,
        Net,
        NullRef,
        Parameter,
        OutRange,
        DataNotFound,
        PermissionDenied
    }
    public static class Exceptions
    {
        public static CUGOJException Todo(string errorMsg, ExceptionTypeEnum type)
        {
            return new CUGOJException();
        }
        public static CUGOJException Todo(string errorMsg)
        {
            return Todo(errorMsg, ExceptionTypeEnum.NotSet);
        }

        public static CUGOJException New(string errorMsg, ExceptionTypeEnum type, long errorCode)
        {
            return new CUGOJException()
            {
                ErrorMessage = errorMsg,
                ExceptionType = type,
                ErrorCode = errorCode,
            };
        }

        public static long nxtCode;


        private class TodoNode
        {
            public IEnumerable<SyntaxNode> Params = null!;

            public TodoNode(IEnumerable<SyntaxNode> @params)
            {
                Params = @params;
            }
        }

        private static string fullTodoName = "CUGOJ.Tools.Exceptions.Exceptions.Todo";
        private static string fullNewName = "CUGOJ.Tools.Exceptions.Exceptions.New";
        private static bool CheckTodoName(string str)
        {
            if (!fullTodoName.EndsWith(str))
                return false;
            if (fullTodoName!=str)
            {
                if (!fullTodoName.Substring(0, fullTodoName.Length - str.Length).EndsWith("."))
                    return false;
            }
            return true;
        }

        private static bool CheckNewName(string str)
        {
            if (!fullNewName.EndsWith(str))
                return false;
            if (fullNewName != str)
            {
                if (!fullNewName.Substring(0, fullNewName.Length - str.Length).EndsWith("."))
                    return false;
            }
            return true;
        }

        private static long TryGetNewNodeId(SyntaxNode node)
        {
            if (!node.IsKind(SyntaxKind.InvocationExpression))
                return -1;
            var nodes = node.ChildNodes();
            if (nodes.Count() != 2)
                return -1;
            var fir = nodes.First();
            var par = nodes.Skip(1).First();
            if (!fir.IsKind(SyntaxKind.SimpleMemberAccessExpression) || !CheckNewName(fir.ToFullString()))
                return -1;
            if (!par.IsKind(SyntaxKind.ArgumentList))
                return -1;
            var pars = par.ChildNodes();
            if (pars.Count() != 3)
                return -1;
            var codePar = pars.Skip(2).First();
            var code = codePar.ChildNodes().FirstOrDefault();
            if (code == null) 
                return -1;
            if (!code.IsKind(SyntaxKind.NumericLiteralExpression))
                return -1;
            return long.Parse(code.ToFullString());
            
        }

        private static bool TryGetTodoNode(SyntaxNode node)
        {
            if (!node.IsKind(SyntaxKind.InvocationExpression))
                return false;
            var nodes = node.ChildNodes();
            if (nodes.Count() != 2)
                return false;
            var fir = nodes.First();
            var par = nodes.Skip(1).First();
            if (!fir.IsKind(SyntaxKind.SimpleMemberAccessExpression) || !CheckTodoName(fir.ToFullString()))
                return false;
            if (!par.IsKind(SyntaxKind.ArgumentList))
                return false;
            var pars = par.ChildNodes();
            if (pars.Count() != 1 && pars.Count() != 2)
                return false;
            return true;
        }

        private static long nxtId = 0;

        public static void GenerateErrorCode(string root)
        {
            Console.WriteLine("生成错误码");
            DirectoryInfo dir = new(root);
            nxtId = 0;
            Dfs(dir, file =>
            {
                if (!file.Exists || file.Extension != ".cs") return;
                nxtId = long.Max(nxtId, LoadErrorCode(file));
            });
            Console.WriteLine($"下一错误码:{nxtId}");
            Dfs(dir, file =>
            {
                if (!file.Exists || file.Extension != ".cs") return;
                ReplaceErrorCode(file);
            });
        }


        public static long LoadErrorCode(FileInfo file)
        {
            var tree = CSharpSyntaxTree.ParseText(File.ReadAllText(file.FullName));
            var root = tree.GetCompilationUnitRoot();
            return LoadErrorCode(root);
        }

        public static void ReplaceErrorCode(FileInfo file)
        {
            var tree = CSharpSyntaxTree.ParseText(File.ReadAllText(file.FullName));
            var root = tree.GetCompilationUnitRoot();
            var newRoot = ReplaceErrorCode(root);
            if (!ReferenceEquals(newRoot,root))
            {
                File.WriteAllText(file.FullName, newRoot.ToFullString());
                Console.WriteLine($"已重写{file.FullName}");
            }
        }

        public static long PrintNodes(SyntaxNode node)
        {
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine($"Type:{node.Kind()}");
            Console.WriteLine($"FullString:{node.ToFullString()}");
            foreach (var nd in node.ChildNodes())
            {
                PrintNodes(nd);
            }
            return 0;
        }

        public static long LoadErrorCode(SyntaxNode node)
        {
            var code = TryGetNewNodeId(node);
            if (code!=-1)
            {
                return code + 1;
            }
            long ans = 0;
            foreach (var nd in node.ChildNodes())
            {
                ans = Math.Max(ans, LoadErrorCode(nd));
            }
            return ans;
        }

        public static SyntaxNode ReplaceErrorCode(SyntaxNode node)
        {
            var todo = TryGetTodoNode(node);
            if(todo)
            {
                var identify = node.ChildNodes().First();
                node = node.ReplaceNode(identify.ChildNodes().Last(), SyntaxFactory.IdentifierName("New"));
                var pars = node.ChildNodes().Skip(1).First();
                var newPar = pars.InsertNodesAfter(pars.ChildNodes().Last(),
                    new List<SyntaxNode>() { SyntaxFactory.Argument(SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(nxtCode))) });
                nxtCode++;
                node = node.ReplaceNode(pars, newPar);
                Console.WriteLine(node.ToFullString());
            }
            foreach (var nd in node.ChildNodes())
            {
                var newNode  =  ReplaceErrorCode(nd);
                if (!ReferenceEquals(newNode,nd))
                {
                    node = node.ReplaceNode(nd, newNode);
                }
            }
            return node;
        }

        public static void Dfs(DirectoryInfo root, Action<FileInfo> action)
        {
            var files = root.GetFiles();
            var dirs = root.GetDirectories();
            foreach (var dir in dirs)
            {
                Dfs(dir, action);
            }
            foreach (var file in files)
            {
                action(file);
            }
        }
    }
}
