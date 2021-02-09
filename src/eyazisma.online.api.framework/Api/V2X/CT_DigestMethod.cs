using System;
using System.Xml.Serialization;

namespace eyazisma.online.api.Api.V2X
{
    [Serializable()]
    [XmlType(Namespace = "urn:dpt:eyazisma:schema:xsd:PaketOzeti-2")]
    [XmlRoot("DigestMethod", Namespace = "urn:dpt:eyazisma:schema:xsd:PaketOzeti-2", IsNullable = false)]
    public sealed class CT_DigestMethod
    {
        [XmlText()]
        [XmlAnyElement(Order = 0)]
        public System.Xml.XmlNode[] Any { get; set; }

        [XmlAttribute(DataType = "anyURI")]
        public string Algorithm { get; set; }
    }
}
