using Logic.Generators.Extensions;
using Logic.Generators.Generated;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Linq;
using System.Text;

namespace Logic.Generators.Files
{
    public class ClientInterfaceFile
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
            stringBuilder.AppendLine($"namespace Logic.Schema.Types.Clients");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine($"    public interface I{fileName}Client");
            stringBuilder.AppendLine("    {");
            foreach (MethodType method in endpoint.Client.Methods)
            {
                stringBuilder.AppendLine($"        Task {method.CompileSignature()};");
            }
            stringBuilder.AppendLine("    }");
            stringBuilder.AppendLine("}");
            context.AddSource($"I{fileName}Client.cs", SourceText.From(stringBuilder.ToString(), Encoding.UTF8));
        }
    }
}
