using Logic.Generators.Generated;
using System.Collections.Generic;
using System.Linq;

namespace Logic.Generators.Extensions
{
    public static class HubEndpointExtensions
    {
        public static string CompileSignature(this MethodType method) =>
            $"{method.Name}({method.Parameters.CompileAsParameterList()})";

        public static string CompileAsArgumentList(this IEnumerable<ParameterType> parameters) =>
            string.Join(", ", parameters.Select(parameter => parameter.Name));

        public static string CompileAsArgumentList(this IEnumerable<ParameterType> parameters, string prefixValue) =>
            string.Join(", ", Enumerable.Repeat(prefixValue, 1).Concat(parameters.Select(parameter => parameter.Name)));

        public static string CompileAsParameterList(this IEnumerable<ParameterType> parameters) =>
            string.Join(", ", parameters.Select(parameter => $"{parameter.Type} {parameter.Name}"));

        public static string CompileAsTypeList(this IEnumerable<ParameterType> parameters) =>
            string.Join(", ", parameters.Select(parameter => parameter.Type));
    }
}
