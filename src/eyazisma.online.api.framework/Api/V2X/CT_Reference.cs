using System;
using System.Xml.Serialization;

namespace eyazisma.online.api.framework.Api.V2X
{
    [Serializable()]
    [XmlType(Namespace = "urn:dpt:eyazisma:schema:xsd:PaketOzeti-2")]
    [XmlRoot("Reference", Namespace = "urn:dpt:eyazisma:schema:xsd:PaketOzeti-2", IsNullable = false)]
    public sealed class CT_Reference
    {
        [XmlElement(Order = 0)]
        public CT_DigestItem DigestItem { get; set; }

        [XmlElement("DigestItem", Order = 1)]
        public CT_DigestItem DigestItem1 { get; set; }

        [XmlAttribute(DataType = "ID")]
        public string Id { get; set; }

        [XmlAttribute(DataType = "anyURI")]
        public string URI { get; set; }

        [XmlAttribute(DataType = "anyURI")]
        public string Type { get; set; }
    }
}
