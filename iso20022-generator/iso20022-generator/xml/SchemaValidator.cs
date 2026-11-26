using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace iso20022_generator.xml
{
    /// <summary>Validates documents against the embedded SIX schema pain.001.001.09.ch.03.xsd.</summary>
    public static class SchemaValidator
    {
        public const string Namespace = "urn:iso:std:iso:20022:tech:xsd:pain.001.001.09";
        private const string ResourceName = "iso20022_generator.schema.pain.001.001.09.ch.03.xsd";

        private static readonly Lazy<XmlSchemaSet> Schemas = new Lazy<XmlSchemaSet>(LoadSchemas);

        /// <summary>Returns all schema errors of the document (empty if valid).</summary>
        public static IReadOnlyList<string> Validate(XDocument document)
        {
            var errors = new List<string>();
            document.Validate(Schemas.Value, (sender, e) =>
            {
                var element = (sender as XElement)?.AncestorsAndSelf().Reverse().Select(a => a.Name.LocalName);
                var path = element == null ? "" : string.Join("/", element) + ": ";
                errors.Add($"{e.Severity}: {path}{e.Message}");
            });
            return errors;
        }

        private static XmlSchemaSet LoadSchemas()
        {
            var assembly = typeof(SchemaValidator).Assembly;
            using (var stream = assembly.GetManifestResourceStream(ResourceName))
            {
                if (stream == null)
                    throw new InvalidOperationException($"Embedded schema '{ResourceName}' not found");

                var set = new XmlSchemaSet();
                using (var reader = XmlReader.Create(stream))
                    set.Add(Namespace, reader);
                set.Compile();
                return set;
            }
        }
    }

    /// <summary>Thrown if a generated document does not conform to the SIX XML schema.</summary>
    public class Pain001ValidationException : Exception
    {
        public Pain001ValidationException(IReadOnlyList<string> errors)
            : base("The generated pain.001 document is not valid according to pain.001.001.09.ch.03.xsd:" +
                   Environment.NewLine + string.Join(Environment.NewLine, errors))
        {
            Errors = errors;
        }

        public IReadOnlyList<string> Errors { get; }
    }
}
