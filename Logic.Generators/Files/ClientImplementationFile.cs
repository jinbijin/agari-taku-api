using Logic.Generators.Extensions;
using Logic.Generators.Generated;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Linq;
using System.Text;

namespace Logic.Generators.Files
{
    public class ClientImplementationFile
    {
        public void Generate(GeneratorExecutionContext context, HubEndpoint endpoint, string fileName)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("using Microsoft.AspNetCore.SignalR.Client;");
            stringBuilder.AppendLine("using Microsoft.Extensions.Configuration;");
            stringBuilder.AppendLine("using System.Threading.Tasks;");
            foreach (ReferenceType reference in endpoint.References)
            {
                stringBuilder.AppendLine($"using {reference.Namespace};");
            }
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"namespace Logic.Schema.Types.Clients");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine($"    public abstract class {fileName}ClientBase");
            stringBuilder.AppendLine("    {");
            stringBuilder.AppendLine($"        private const string HubUrl = \"{endpoint.Url}\";");
            stringBuilder.AppendLine("        private readonly IConfiguration _config;");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("        private HubConnection? _connection;");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("        public bool IsConnected => _connection is HubConnection;");
            stringBuilder.AppendLine("        public event Action? OnChange;");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"        protected {fileName}ClientBase(IConfiguration config)");
            stringBuilder.AppendLine("        {");
            stringBuilder.AppendLine("            _config = config;");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("        protected async Task SetupConnection()");
            stringBuilder.AppendLine("        {");
            stringBuilder.AppendLine("            string baseUrl = _config.GetValue<string>(\"Urls\");");
            stringBuilder.AppendLine("            _connection = new HubConnectionBuilder().WithUrl($\"{baseUrl}{HubUrl}\").Build();");
            stringBuilder.AppendLine();
            foreach (MethodType method in endpoint.Client.Methods)
            {
                stringBuilder.AppendLine($"            _connection.On<{method.Parameters.CompileAsTypeList()}>(\"{method.Name}\", {method.Name});");
            }
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("            await _connection.StartAsync();");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("        protected async Task CleanupConnection()");
            stringBuilder.AppendLine("        {");
            stringBuilder.AppendLine("            if (_connection is not HubConnection)");
            stringBuilder.AppendLine("            {");
            stringBuilder.AppendLine("                return;");
            stringBuilder.AppendLine("            }");
            stringBuilder.AppendLine("            await _connection.StopAsync();");
            stringBuilder.AppendLine("            await _connection.DisposeAsync();");
            stringBuilder.AppendLine("            _connection = null;");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine();
            foreach (MethodType method in endpoint.Client.Methods)
            {
                stringBuilder.AppendLine($"        protected abstract void {method.CompileSignature()};");
            }
            stringBuilder.AppendLine();
            foreach (MethodType method in endpoint.Server.Methods)
            {
                stringBuilder.AppendLine($"        public async Task {method.CompileSignature()}");
                stringBuilder.AppendLine("        {");
                stringBuilder.AppendLine($"            await _connection.SendAsync({method.Parameters.CompileAsArgumentList($"\"{method.Name}\"")});");
                stringBuilder.AppendLine("        }");
            }
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("        protected void NotifyStateChanged()");
            stringBuilder.AppendLine("        {");
            stringBuilder.AppendLine("            OnChange?.Invoke();");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine("    }");
            stringBuilder.AppendLine("}");
            context.AddSource($"{fileName}ClientBase.cs", SourceText.From(stringBuilder.ToString(), Encoding.UTF8));
        }
    }
}
