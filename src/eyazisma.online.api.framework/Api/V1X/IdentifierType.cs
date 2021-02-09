using System;
using System.Xml.Serialization;

namespace eyazisma.online.api.framework.Api.V1X
{
    [Serializable()]
    [XmlType(Namespace = "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2")]
    [XmlRoot("OzId", Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-1", IsNullable = false)]
    public sealed class IdentifierType
    {
        [XmlAttribute(DataType = "normalizedString")]
        public string schemeID { get; set; }

        [XmlText(DataType = "normalizedString")]
        public string Value { get; set; }
    }
}
