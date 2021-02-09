using System;
using System.Xml.Serialization;

namespace eyazisma.online.api.Api.V1X
{
    [Serializable()]
    [XmlType(Namespace = "urn:dpt:eyazisma:schema:xsd:PaketOzeti-1")]
    [XmlRoot("Reference", Namespace = "urn:dpt:eyazisma:schema:xsd:PaketOzeti-1", IsNullable = false)]
    public sealed class CT_Reference
    {
        [XmlElement(Order = 0)]
        public CT_DigestMethod DigestMethod { get; set; }

        [XmlElement(DataType = "base64Binary", Order = 1)]
        public byte[] DigestValue { get; set; }

        [XmlAttribute(DataType = "ID")]
        public string Id { get; set; }

        [XmlAttribute(DataType = "anyURI")]
        public string URI { get; set; }

        [XmlAttribute(DataType = "anyURI")]
        public string Type { get; set; }
    }
}
