using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;

namespace Hamar.API.Utils
{
    public class CodeCompiler
    {
        private string[] filesToCompile;
        private string[] dllToInclude;
        public CodeCompiler(string[] files, params string[] param)
        {
            filesToCompile = files;
            dllToInclude = param;
        }

        public Assembly Compile()
        {
            using (var csc = new CSharpCodeProvider())
            {
                var parameters = new CompilerParameters()
                {
                    GenerateInMemory = true,
                    GenerateExecutable = false
                };

                parameters.ReferencedAssemblies.Add("System.Core.dll");
                parameters.ReferencedAssemblies.Add("mscorlib.dll");

                foreach (var d in dllToInclude)
                    parameters.ReferencedAssemblies.Add(d);

                var results = csc.CompileAssemblyFromFile(parameters, filesToCompile);

                return results.CompiledAssembly;
            }
        }
    }
}
