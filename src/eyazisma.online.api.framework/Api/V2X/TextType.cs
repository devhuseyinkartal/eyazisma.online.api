using System;
using System.Xml.Serialization;

namespace eyazisma.online.api.framework.Api.V2X
{
    [Serializable()]
    [XmlType(Namespace = "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2")]
    [XmlRoot("Konu", Namespace = "urn:dpt:eyazisma:schema:xsd:Tipler-2", IsNullable = false)]
    public sealed class TextType
    {
        [XmlAttribute(DataType = "language")]
        public string languageID { get; set; }

        [XmlText()]
        public string Value { get; set; }
    }
}
