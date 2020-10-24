using Logic.Generators.Files;
using Logic.Generators.Generated;
using Microsoft.CodeAnalysis;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Logic.Generators
{
    [Generator]
    public class HubEndpointSourceGenerator : ISourceGenerator
    {
        private readonly ClientInterfaceFile _clientInterfaceFile;
        private readonly ServerInterfaceFile _serverInterfaceFile;
        private readonly ClientImplementationFile _clientImplementationFile;

        public HubEndpointSourceGenerator()
        {
            _clientInterfaceFile = new();
            _serverInterfaceFile = new();
            _clientImplementationFile = new();
        }

        public void Execute(GeneratorExecutionContext context)
        {
            foreach (AdditionalText file in context.AdditionalFiles.Where(file => Path.GetFileNameWithoutExtension(Path.GetDirectoryName(file.Path)) == "HubEndpoints"))
            {
                using FileStream stream = File.OpenRead(file.Path);
                string fileName = Path.GetFileNameWithoutExtension(file.Path);
                string dir = Path.GetFileNameWithoutExtension(Path.GetDirectoryName(file.Path));

                XmlSerializer serializer = new XmlSerializer(typeof(HubEndpoint));
                HubEndpoint? maybeEndpoint = (HubEndpoint?) serializer.Deserialize(stream);

                if (maybeEndpoint is HubEndpoint endpoint)
                {
                    _clientInterfaceFile.Generate(context, endpoint, fileName);
                    _serverInterfaceFile.Generate(context, endpoint, fileName);
                    _clientImplementationFile.Generate(context, endpoint, fileName);
                }
                else
                {
                    throw new InvalidOperationException($"Failed to parse the definition of the hub \"{fileName}\"");
                }
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
        }
    }
}
