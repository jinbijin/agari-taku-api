using Logic.Generators.Extensions;
using Logic.Generators.Generated;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Linq;
using System.Text;

namespace Logic.Generators.Files
{
    public class ServerInterfaceFile
    {
        public void Generate(GeneratorExecutionContext context, HubEndpoint endpoint, string fileName)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("using System.Threading.Tasks;");
            foreach (ReferenceType reference in endpoint.References)
            {
                stringBuilder.AppendLine($"using {reference.Namespace};");
            }
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"namespace Logic.Schema.Types.Servers");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine($"    public interface I{fileName}Server");
            stringBuilder.AppendLine("    {");
            foreach (MethodType method in endpoint.Server.Methods)
            {
                stringBuilder.AppendLine($"        Task {method.CompileSignature()};");
            }
            stringBuilder.AppendLine("    }");
            stringBuilder.AppendLine("}");
            context.AddSource($"I{fileName}Server.cs", SourceText.From(stringBuilder.ToString(), Encoding.UTF8));
        }
    }
}
